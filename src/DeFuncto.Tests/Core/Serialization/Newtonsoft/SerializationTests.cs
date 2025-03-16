using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeFuncto.Tests.Core.Serialization.Newtonsoft;

public class SerializationTests
{
  [Theory(DisplayName = "serializes and deserializes as ")]
  [MemberData(nameof(Data))]
  public void Test(object[] instance)
  {
    var serialized = JsonConvert.SerializeObject(instance[0]);
    var deserialized = JsonConvert.DeserializeObject(serialized, instance[0].GetType());
    Assert.Equal(instance[0], deserialized);
  }

  public static TheoryData<object[]> Data => Samples.Data;
}
