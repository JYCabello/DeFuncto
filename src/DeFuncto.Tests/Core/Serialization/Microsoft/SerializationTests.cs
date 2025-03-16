using System.Collections.Generic;
using System.Text.Json;

namespace DeFuncto.Tests.Core.Serialization.Microsoft;

public class SerializationTests
{
  public static IEnumerable<object[]> Data => Samples.Data;

  [Theory(DisplayName = "serializes and deserializes as expected")]
  [MemberData(nameof(Data))]
  public void Test(object instance)
  {
    var type = instance.GetType();
    var serialized = JsonSerializer.Serialize(instance);
    var deserialized = JsonSerializer.Deserialize(serialized, type);
    Assert.Equal(instance, deserialized);
  }
}
