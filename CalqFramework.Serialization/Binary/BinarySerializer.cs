//using MemoryPack;

//namespace CalqFramework.Serialization.Binary {
//    public class BinarySerializer : ICalqSerializer {

//        public ReadOnlySpan<byte> Serialize<T>(T obj) {
//            return MemoryPackSerializer.Serialize(obj);
//        }

//        public void Populate<T>(ReadOnlySpan<byte> data, T? obj) {
//            MemoryPackSerializer.Deserialize(data.ToArray(), ref obj);
//        }
//    }
//}
