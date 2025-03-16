using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeFuncto.Tests.Core.Serialization.Newtonsoft;

public class SerializationTests
{
  [Theory(DisplayName = "serializes and deserializes as expected")]
  [MemberData(nameof(Data))]
  public void Test(object instance)
  {
    var type = instance.GetType();
    var serialized = JsonConvert.SerializeObject(instance);
    var deserialized = JsonConvert.DeserializeObject(serialized, type);
    Assert.Equal(instance, deserialized);
  }

  public static IEnumerable<object[]> Data => Samples.Data;
}
