using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    internal class DualDataMemberAccessor : IDataMemberAccessor {
        public IDataMemberAccessor PrimaryAccessor { get; }
        public IDataMemberAccessor SecondaryAccessor { get; }

        public DualDataMemberAccessor(IDataMemberAccessor primaryAccessor, IDataMemberAccessor secondaryAccessor) {
            PrimaryAccessor = primaryAccessor;
            SecondaryAccessor = secondaryAccessor;
        }

        public MemberInfo? GetDataMember(Type type, string dataMemberKey) {
            return PrimaryAccessor.GetDataMember(type, dataMemberKey) ?? SecondaryAccessor.GetDataMember(type, dataMemberKey);
        }

        public Type GetDataMemberType(Type type, string dataMemberKey) {
            if (PrimaryAccessor.GetDataMember(type, dataMemberKey) != null) {
                return PrimaryAccessor.GetDataMemberType(type, dataMemberKey);
            } else {
                return SecondaryAccessor.GetDataMemberType(type, dataMemberKey);
            }
        }

        public object? GetDataMemberValue(object obj, string dataMemberKey) {
            if (PrimaryAccessor.GetDataMember(obj.GetType(), dataMemberKey) != null) {
                return PrimaryAccessor.GetDataMemberValue(obj, dataMemberKey);
            } else {
                return SecondaryAccessor.GetDataMemberValue(obj, dataMemberKey);
            }
        }

        public object GetOrInitializeDataMemberValue(object obj, string dataMemberKey) {
            if (PrimaryAccessor.GetDataMember(obj.GetType(), dataMemberKey) != null) {
                return PrimaryAccessor.GetOrInitializeDataMemberValue(obj, dataMemberKey);
            } else {
                return SecondaryAccessor.GetOrInitializeDataMemberValue(obj, dataMemberKey);
            }
        }

        public void SetDataMemberValue(object obj, string dataMemberKey, object? value) {
            if (PrimaryAccessor.GetDataMember(obj.GetType(), dataMemberKey) != null) {
                PrimaryAccessor.SetDataMemberValue(obj, dataMemberKey, value);
            } else {
                SecondaryAccessor.SetDataMemberValue(obj, dataMemberKey, value);
            }
        }
    }
}