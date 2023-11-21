//using CalqFramework.Serialization.Binary;
//using System.Text;

//namespace CalqFramework.SerializationTest;

//public class BinarySerializerTest {
//    private ReadOnlySpan<byte> GetBinaryBytes<T>(T obj) {
//        return new BinarySerializer().Serialize<T>(obj);
//    }

//    [Fact]
//    public void Populate_ShouldPopulateObjectProperties() {
//        var serializer = new BinarySerializer();
//        var targetObject = new SomeData { Name = "John", Age = 25 };

//        var binaryData = GetBinaryBytes(targetObject);
//        serializer.Populate(binaryData, targetObject);

//        Assert.Equal("John", targetObject.Name);
//        Assert.Equal(25, targetObject.Age);
//    }

//    //[Fact]
//    //public void Populate_ShouldHandleEmptyObject() {
//    //    var serializer = new BinarySerializer();
//    //    var targetObject = new SomeData { Name = "Alice", Age = 30 };

//    //    var obj = new { };
//    //    var binaryData = GetBinaryBytes(obj);

//    //    serializer.Populate(binaryData, targetObject);

//    //    Assert.Equal("Alice", targetObject.Name);
//    //    Assert.Equal(30, targetObject.Age);
//    //}

//    //[Fact]
//    //public void Populate_ShouldPopulateArray() {
//    //    var serializer = new BinarySerializer();
//    //    var targetArray = new int[3];

//    //    var binaryData = GetBinaryBytes(targetArray);
//    //    serializer.Populate(binaryData, targetArray);

//    //    Assert.Equal(1, targetArray[0]);
//    //    Assert.Equal(2, targetArray[1]);
//    //    Assert.Equal(3, targetArray[2]);
//    //}

//    [Fact]
//    public void Populate_ShouldPopulateObjectWithArray() {
//        var serializer = new BinarySerializer();
//        var targetObject = new SomeData();

//        var data = new SomeData { Name = "John", Age = 25, Skills = new[] { "Programming", "Problem Solving" } };
//        var binaryData = GetBinaryBytes(data);

//        serializer.Populate(binaryData, targetObject);

//        Assert.Equal("John", targetObject.Name);
//        Assert.Equal(25, targetObject.Age);
//        Assert.NotNull(targetObject.Skills);
//        Assert.Equal(2, targetObject.Skills.Length);
//        Assert.Equal("Programming", targetObject.Skills[0]);
//        Assert.Equal("Problem Solving", targetObject.Skills[1]);
//    }

//    [Fact]
//    public void Populate_ShouldPopulateList() {
//        var serializer = new BinarySerializer();
//        var targetList = new List<SomeData>();

//        var data = new List<SomeData>
//        {
//        new SomeData { Name = "John", Age = 25 },
//        new SomeData { Name = "Alice", Age = 30 }
//    };

//        var binaryData = GetBinaryBytes(data);
//        serializer.Populate(binaryData, targetList);

//        Assert.Equal(2, targetList.Count);
//        Assert.Equal("John", targetList[0].Name);
//        Assert.Equal(25, targetList[0].Age);
//        Assert.Equal("Alice", targetList[1].Name);
//        Assert.Equal(30, targetList[1].Age);
//    }

//    [Fact]
//    public void Populate_ShouldPopulateObjectWithList() {
//        var serializer = new BinarySerializer();
//        var targetObject = new SomeData();

//        var data = new SomeData { Name = "John", Age = 25, Hobbies = new List<string> { "Reading", "Gaming" } };
//        var binaryData = GetBinaryBytes(data);

//        serializer.Populate(binaryData, targetObject);

//        Assert.Equal("John", targetObject.Name);
//        Assert.Equal(25, targetObject.Age);
//        Assert.NotNull(targetObject.Hobbies);
//        Assert.Equal(2, targetObject.Hobbies.Count);
//        Assert.Equal("Reading", targetObject.Hobbies[0]);
//        Assert.Equal("Gaming", targetObject.Hobbies[1]);
//    }

//    [Fact]
//    public void Populate_ShouldPopulateDictionary() {
//        var serializer = new BinarySerializer();
//        var targetDictionary = new Dictionary<string, SomeData>();

//        var data = new Dictionary<string, SomeData>
//        {
//        {"Key1", new SomeData { Name = "John", Age = 25 }},
//        {"Key2", new SomeData { Name = "Alice", Age = 30 }}
//    };

//        var binaryData = GetBinaryBytes(data);
//        serializer.Populate(binaryData, targetDictionary);

//        Assert.Equal(2, targetDictionary.Count);
//        Assert.True(targetDictionary.ContainsKey("Key1"));
//        Assert.True(targetDictionary.ContainsKey("Key2"));
//        Assert.Equal("John", targetDictionary["Key1"].Name);
//        Assert.Equal(25, targetDictionary["Key1"].Age);
//        Assert.Equal("Alice", targetDictionary["Key2"].Name);
//        Assert.Equal(30, targetDictionary["Key2"].Age);
//    }

//    [Fact]
//    public void Populate_ShouldPopulateObjectWithDictionary() {
//        var serializer = new BinarySerializer();
//        var targetObject = new SomeData();

//        var data = new SomeData {
//            Name = "John",
//            Age = 25,
//            Attributes = new Dictionary<string, string> { { "Key1", "Value1" }, { "Key2", "Value2" } }
//        };

//        var binaryData = GetBinaryBytes(data);
//        serializer.Populate(binaryData, targetObject);

//        Assert.Equal("John", targetObject.Name);
//        Assert.Equal(25, targetObject.Age);
//        Assert.NotNull(targetObject.Attributes);
//        Assert.Equal(2, targetObject.Attributes.Count);
//        Assert.Equal("Value1", targetObject.Attributes["Key1"]);
//        Assert.Equal("Value2", targetObject.Attributes["Key2"]);
//    }
//}