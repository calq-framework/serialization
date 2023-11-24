namespace CalqFramework.Serialization {
    public interface ISerializer {
        public ReadOnlySpan<byte> Serialize<T>(T obj);

        public void Populate<T>(ReadOnlySpan<byte> data, T obj);
    }
}
