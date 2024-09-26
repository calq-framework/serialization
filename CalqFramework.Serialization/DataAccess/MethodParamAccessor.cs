using CalqFramework.Serialization.DataAccess;
using System.Collections;
using System.Reflection;

namespace CalqFramework.Cli.DataAccess {
    internal class MethodParamAccessor : IDataAccessor<string, object?>
    {
        public ParameterInfo[] Parameters { get; }
        private object?[] ParamValues { get; }
        public HashSet<ParameterInfo> AssignedParameters { get; }
        public MethodInfo Method { get; }

        public object? this[string key] {
            get {
                if (TryGetParamIndex(key, out var index)) {
                    return ParamValues[index];
                }
                throw new MissingMemberException();
            }
            set {
                if (TryGetParamIndex(key, out var index)) {
                    ParamValues[index] = value;
                    AssignedParameters.Add(Parameters[index]);
                    return;
                }
                throw new MissingMemberException();
            }
        }

        public MethodParamAccessor(MethodInfo method)
        {
            Method = method;
            Parameters = Method.GetParameters();
            ParamValues = new object?[Parameters.Length];
            AssignedParameters = new HashSet<ParameterInfo>();
        }

        public string ParameterToString(ParameterInfo parameter, BindingFlags bindingAttr) {
            return parameter.Name;
        }

        public object? Invoke(object? obj)
        {
            var assignedParamNames = AssignedParameters.Select(x => x.Name).ToHashSet();
            for (var j = 0; j < Parameters.Length; ++j)
            {
                var param = Parameters[j];
                if (!assignedParamNames.Contains(param.Name))
                {
                    if (!param.IsOptional)
                    {
                        throw new Exception($"unassigned parameter {param.Name}");
                    }
                    SetValue(j, param.DefaultValue!);
                }
            }
            return Method.Invoke(obj, ParamValues);
        }

        private bool TryGetParamIndex(string key, out int result)
        {
            result = default;
            var success = false;

            if (key.Length == 1)
            {
                for (var i = 0; i < Parameters.Length; i++)
                {
                    var param = Parameters[i];
                    if (param.Name[0] == key[0])
                    {
                        result = i;
                        success = true;
                    }
                }
            }
            else
            {
                for (var i = 0; i < Parameters.Length; i++)
                {
                    var param = Parameters[i];
                    if (param.Name == key)
                    {
                        result = i;
                        success = true;
                    }
                }
            }

            return success;
        }

        public string GetKey(int index)
        {
            if (index >= Parameters.Length)
            {
                throw new Exception("passed too many args");
            }
            return Parameters[index].Name!;
        }

        public Type GetType(int index)
        {
            if (index >= Parameters.Length)
            {
                throw new Exception("passed too many args");
            }
            return Parameters[index].ParameterType;
        }

        public void SetValue(int index, object? value)
        {
            if (index >= Parameters.Length)
            {
                throw new Exception("passed too many args");
            }
            ParamValues[index] = value;
            AssignedParameters.Add(Parameters[index]);
        }

        public  object GetValueOrInitialize(string key)
        {
            if (TryGetParamIndex(key, out var index))
            {
                var value = ParamValues[index] ??
                   Activator.CreateInstance(Parameters[index].ParameterType) ??
                   Activator.CreateInstance(Nullable.GetUnderlyingType(Parameters[index].ParameterType)!)!;
                ParamValues[index] = value;
                return value;
            }
            throw new MissingMemberException();
        }

        public  Type GetDataType(string key)
        {
            if (TryGetParamIndex(key, out var index))
            {
                return Parameters[index].ParameterType;
            }
            throw new MissingMemberException();
        }

        public  bool ContainsKey(string key)
        {
            return TryGetParamIndex(key, out var _);
        }

        public  bool SetOrAddValue(string key, object? value)
        {
            var type = GetDataType(key);
            var isCollection = type.GetInterface(nameof(ICollection)) != null;
            if (isCollection == false)
            {
                this[key] = value;
                return false;
            }
            else
            {
                var collectionObj = (GetValueOrInitialize(key) as ICollection)!;
                //AddValue(collectionObj, value);
                TryGetParamIndex(key, out var index);
                AssignedParameters.Add(Parameters[index]);
                return true;
            }
        }
    }
}