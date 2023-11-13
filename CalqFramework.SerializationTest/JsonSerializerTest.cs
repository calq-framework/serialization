using CalqFramework.Serialization.Json;
using System.Text;

namespace CalqFramework.SerializationTest;

public class JsonSerializerTest
{
    private ReadOnlySpan<byte> GetJsonBytes(string jsonText) {
        return Encoding.UTF8.GetBytes(jsonText);
    }

    [Fact]
    public void Populate_ShouldPopulateObjectProperties() {
        var serializer = new JsonSerializer();
        var json = "{\"Name\":\"John\",\"Age\":25}";
        var targetObject = new SomeData();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetObject);

        Assert.Equal("John", targetObject.Name);
        Assert.Equal(25, targetObject.Age);
    }

    [Fact]
    public void Populate_ShouldHandleEmptyJson() {
        var serializer = new JsonSerializer();
        var json = "{}";
        var targetObject = new SomeData { Name = "Alice", Age = 30 };

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetObject);

        Assert.Equal("Alice", targetObject.Name);
        Assert.Equal(30, targetObject.Age);
    }

    [Fact]
    public void Populate_ShouldHandleInvalidJson() {
        var serializer = new JsonSerializer();
        var json = "InvalidJson";
        var targetObject = new SomeData();

        Assert.ThrowsAny<Exception>(() => serializer.Populate(GetJsonBytes(json), targetObject));
    }

    //[Fact]
    //public void Populate_ShouldPopulateArray() {
    //    var serializer = new JsonSerializer();
    //    var json = "[1, 2, 3]";
    //    var targetArray = new int[3];

    //    var jsonBytes = GetJsonBytes(json);
    //    serializer.Populate(jsonBytes, targetArray);

    //    Assert.Equal(1, targetArray[0]);
    //    Assert.Equal(2, targetArray[1]);
    //    Assert.Equal(3, targetArray[2]);
    //}

    [Fact]
    public void Populate_ShouldPopulateObjectWithArray() {
        var serializer = new JsonSerializer();
        var json = "{\"Name\":\"John\",\"Age\":25,\"Skills\":[\"Programming\",\"Problem Solving\"]}";
        var targetObject = new SomeData();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetObject);

        Assert.Equal("John", targetObject.Name);
        Assert.Equal(25, targetObject.Age);
        Assert.NotNull(targetObject.Skills);
        Assert.Equal(2, targetObject.Skills.Length);
        Assert.Equal("Programming", targetObject.Skills[0]);
        Assert.Equal("Problem Solving", targetObject.Skills[1]);
    }

    [Fact]
    public void Populate_ShouldPopulateList() {
        var serializer = new JsonSerializer();
        var json = "[{\"Name\":\"John\",\"Age\":25},{\"Name\":\"Alice\",\"Age\":30}]";
        var targetList = new List<SomeData>();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetList);

        Assert.Equal(2, targetList.Count);
        Assert.Equal("John", targetList[0].Name);
        Assert.Equal(25, targetList[0].Age);
        Assert.Equal("Alice", targetList[1].Name);
        Assert.Equal(30, targetList[1].Age);
    }

    [Fact]
    public void Populate_ShouldPopulateObjectWithList() {
        var serializer = new JsonSerializer();
        var json = "{\"Name\":\"John\",\"Age\":25,\"Hobbies\":[\"Reading\",\"Gaming\"]}";
        var targetObject = new SomeData();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetObject);

        Assert.Equal("John", targetObject.Name);
        Assert.Equal(25, targetObject.Age);
        Assert.NotNull(targetObject.Hobbies);
        Assert.Equal(2, targetObject.Hobbies.Count);
        Assert.Equal("Reading", targetObject.Hobbies[0]);
        Assert.Equal("Gaming", targetObject.Hobbies[1]);
    }

    [Fact]
    public void Populate_ShouldPopulateDictionary() {
        var serializer = new JsonSerializer();
        var json = "{\"Key1\":{\"Name\":\"John\",\"Age\":25},\"Key2\":{\"Name\":\"Alice\",\"Age\":30}}";
        var targetDictionary = new Dictionary<string, SomeData>();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetDictionary);

        Assert.Equal(2, targetDictionary.Count);
        Assert.True(targetDictionary.ContainsKey("Key1"));
        Assert.True(targetDictionary.ContainsKey("Key2"));
        Assert.Equal("John", targetDictionary["Key1"].Name);
        Assert.Equal(25, targetDictionary["Key1"].Age);
        Assert.Equal("Alice", targetDictionary["Key2"].Name);
        Assert.Equal(30, targetDictionary["Key2"].Age);
    }

    [Fact]
    public void Populate_ShouldPopulateObjectWithDictionary() {
        var serializer = new JsonSerializer();
        var json = "{\"Name\":\"John\",\"Age\":25,\"Attributes\":{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}}";
        var targetObject = new SomeData();

        var jsonBytes = GetJsonBytes(json);
        serializer.Populate(jsonBytes, targetObject);

        Assert.Equal("John", targetObject.Name);
        Assert.Equal(25, targetObject.Age);
        Assert.NotNull(targetObject.Attributes);
        Assert.Equal(2, targetObject.Attributes.Count);
        Assert.Equal("Value1", targetObject.Attributes["Key1"]);
        Assert.Equal("Value2", targetObject.Attributes["Key2"]);
    }
}