using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CalqFramework.Serialization.DataAccess.DataMemberAccess {
    public abstract class ClassMemberResolverBase<TKey, TValue> : IDataMediatorResolver<TKey, MemberInfo> {
        protected object ParentObject { get; }
        protected Type ParentType { get; }
        protected BindingFlags BindingAttr { get; }

        public ClassMemberResolverBase(object obj, BindingFlags bindingAttr) {
            ParentObject = obj;
            BindingAttr = bindingAttr;
            ParentType = obj.GetType();
        }

        public bool ContainsKey(TKey key) {
            var result = GetClassMember(key);
            return result != null;
        }

        public bool TryGetDataMediator(TKey key, [MaybeNullWhen(false)] out MemberInfo result) {
            result = GetClassMember(key);
            return result != null;
        }

        public MemberInfo GetDataMediator(TKey key) {
            return GetClassMember(key) ?? throw new MissingMemberException($"Missing {key} in {ParentType}.");
        }

        protected abstract MemberInfo? GetClassMember(TKey key);
        public abstract bool ContainsDataMediator(MemberInfo key);
    }
}
