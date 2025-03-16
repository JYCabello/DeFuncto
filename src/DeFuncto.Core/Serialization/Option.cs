using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeFuncto.Serialization;

internal class OptionNewtonsoftConverter : JsonConverter
{
  public override bool CanConvert(Type objectType) =>
    objectType.IsGenericType &&
    objectType.GetGenericTypeDefinition() == typeof(Option<>);

  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    if (value == null)
    {
      writer.WriteNull();
      return;
    }

    var optionType = value.GetType();
    var isSomeProperty = optionType.GetField("IsSome")!;
    var isSome = (bool)isSomeProperty.GetValue(value);

    writer.WriteStartObject();
    writer.WritePropertyName("State");
    writer.WriteValue(isSome ? "Some" : "None");

    if (!isSome)
    {
      writer.WriteEndObject();
      return;
    }

    var valueField = optionType.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance)!;
    var duValue = valueField.GetValue(value)!;
    var t2ValueField = duValue.GetType().GetField("t2", BindingFlags.NonPublic | BindingFlags.Instance)!;

    writer.WritePropertyName("Value");
    serializer.Serialize(writer, t2ValueField.GetValue(duValue));
    writer.WriteEndObject();
  }

  public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.Null)
      return GetNoneOption(objectType);

    var jsonObject = JObject.Load(reader);
    var state = jsonObject["State"]?.Value<string>();

    if (state != "Some")
      return GetNoneOption(objectType);

    var valueToken = jsonObject["Value"];

    if (valueToken == null)
      return GetNoneOption(objectType);

    var valueType = objectType.GetGenericArguments()[0];
    var value = valueToken.ToObject(valueType, serializer);

    if (value == null)
      return GetNoneOption(objectType);

    return CreateSomeOption(valueType, value);
  }

  private static object GetNoneOption(Type optionType) =>
    typeof(Option<>)
      .MakeGenericType(optionType.GetGenericArguments()[0])
      .GetProperty("None", BindingFlags.Public | BindingFlags.Static)!
      .GetValue(null);

  private static object CreateSomeOption(Type valueType, object value) =>
    typeof(Option<>)
      .MakeGenericType(valueType)
      .GetMethod("Some", BindingFlags.Public | BindingFlags.Static)!
      .Invoke(null, new[] { value });
}
