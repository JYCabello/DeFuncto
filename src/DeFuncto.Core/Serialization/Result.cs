using System;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeFuncto.Serialization;

internal class ResultNewtonsoftConverter : JsonConverter
{
  public override bool CanConvert(Type objectType) =>
    objectType.IsGenericType &&
    objectType.GetGenericTypeDefinition() == typeof(Result<,>);

  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    if (value == null) throw new SerializationException("Results cannot be null");

    var resultType = value.GetType();
    var isOkProperty = resultType.GetField("IsOk")!;
    var isOk = (bool)isOkProperty.GetValue(value);

    writer.WriteStartObject();
    writer.WritePropertyName("State");
    writer.WriteValue(isOk ? "Ok" : "Error");

    var valueField = resultType.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance)!;
    var duValue = valueField.GetValue(value)!;

    writer.WritePropertyName("Value");
    if (isOk)
    {
      var t1ValueField = duValue.GetType().GetField("t1", BindingFlags.NonPublic | BindingFlags.Instance)!;
      serializer.Serialize(writer, t1ValueField.GetValue(duValue));
    }
    else
    {
      var t2ValueField = duValue.GetType().GetField("t2", BindingFlags.NonPublic | BindingFlags.Instance)!;
      serializer.Serialize(writer, t2ValueField.GetValue(duValue));
    }

    writer.WriteEndObject();
  }

  public override object ReadJson(
    JsonReader reader,
    Type objectType,
    object? existingValue,
    JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.Null)
      throw new SerializationException("Results cannot be null");

    var jsonObject = JObject.Load(reader);
    var state = jsonObject["State"]?.Value<string>();
    var valueToken = jsonObject["Value"];
    var valueType = objectType.GetGenericArguments()[0];
    var errorType = objectType.GetGenericArguments()[1];
    return state switch
    {
      "Ok" => GetOk(valueToken, valueType, errorType, serializer),
      "Error" => GetError(valueToken, valueType, errorType, serializer),
      _ => throw new SerializationException("Invalid state for Result")
    };
  }

  private static object GetOk(JToken? valueToken, Type valueType, Type errorType, JsonSerializer serializer)
  {
    if (valueToken == null)
      throw new SerializationException("Ok Result must have a value");

    var value = valueToken.ToObject(valueType, serializer);
    if (value == null)
      throw new SerializationException("Ok Result value cannot be null");

    var okMethod = typeof(Result<,>)
      .MakeGenericType(valueType, errorType)
      .GetMethod("Ok", BindingFlags.Public | BindingFlags.Static)!;

    return okMethod.Invoke(null, new[] { value });
  }

  private static object GetError(JToken? valueToken, Type valueType, Type errorType, JsonSerializer serializer)
  {
    if (valueToken == null)
      throw new SerializationException("Error Result must have a value");

    var value = valueToken.ToObject(errorType, serializer);
    if (value == null)
      throw new SerializationException("Error Result value cannot be null");

    var errorMethod = typeof(Result<,>)
      .MakeGenericType(valueType, errorType)
      .GetMethod("Error", BindingFlags.Public | BindingFlags.Static)!;

    return errorMethod.Invoke(null, new[] { value });
  }
}
