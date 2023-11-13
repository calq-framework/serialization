using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalqFramework.Serialization {
    public interface ICalqSerializer {
        public ReadOnlySpan<byte> Serialize<T>(T obj);

        public void Populate<T>(ReadOnlySpan<byte> data, T obj);
    }
}
