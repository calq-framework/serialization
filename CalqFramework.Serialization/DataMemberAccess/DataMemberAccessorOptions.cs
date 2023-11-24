using System.Reflection;

namespace CalqFramework.Serialization.DataMemberAccess {
    public class DataMemberAccessorOptions {

        public const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

        public bool AccessFields { get; } = false;
        public bool AccessProperties { get; } = true;

        public BindingFlags BindingAttr { get; } = DefaultLookup;

        public DataMemberAccessorOptions() {
        }
    }
}