using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalqFramework.SerializationTest {
    public class SomeData {
        public string Name { get; set; }
        public int Age { get; set; }
        public string[] Skills { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<string> Hobbies { get; set; }
    }
}
