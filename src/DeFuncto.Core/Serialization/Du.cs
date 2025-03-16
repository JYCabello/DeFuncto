using System;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeFuncto.Serialization;

internal class DuNewtonsoftConverter : JsonConverter
{
  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    if (value == null)
      throw new SerializationException("Du cannot be null");

    writer.WriteStartObject();
    var duType = value.GetType();
    var wasValueFound = false;
    var discriminator = (int)duType
      .GetField("Discriminator", BindingFlags.Public | BindingFlags.Instance)!
      .GetValue(value);
    for (var i = 1; i <= 7; i++)
    {
      if (discriminator != i - 1)
        continue;
      var fieldName = $"t{i}";
      var field = duType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
      if (field == null)
        continue;
      var fieldValue = field.GetValue(value);
      writer.WritePropertyName("State");
      writer.WriteValue(fieldName);
      writer.WritePropertyName("Value");
      serializer.Serialize(writer, fieldValue);
      wasValueFound = true;
    }

    if (!wasValueFound)
      throw new SerializationException("Du has no value");
  }

  public override object? ReadJson(
    JsonReader reader,
    Type objectType,
    object? existingValue,
    JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.Null)
      throw new SerializationException("Du cannot be null");

    var jsonObject = JObject.Load(reader);

    var state = jsonObject["State"]?.Value<string>();
    var genericTypes = objectType.GetGenericArguments();
    var duType = genericTypes.Length switch
    {
      2 => typeof(Du<,>).MakeGenericType(genericTypes),
      3 => typeof(Du3<,,>).MakeGenericType(genericTypes),
      4 => typeof(Du4<,,,>).MakeGenericType(genericTypes),
      5 => typeof(Du5<,,,,>).MakeGenericType(genericTypes),
      6 => typeof(Du6<,,,,,>).MakeGenericType(genericTypes),
      7 => typeof(Du7<,,,,,,>).MakeGenericType(genericTypes),
      _ => throw new ArgumentOutOfRangeException()
    };

    for (var i = 1; i <= 7; i++)
    {
      var fieldName = $"t{i}";
      if (state != fieldName)
        continue;
      var valueToken = jsonObject["Value"];
      var value = valueToken?.ToObject(genericTypes[i - 1], serializer);
      if (value == null)
        throw new SerializationException("Du value cannot be null");
      var factoryMethod = GetFactoryMethod(duType, i);
      return factoryMethod.Invoke(null, new[] { value });
    }

    throw new SerializationException("Invalid state for Du");
  }

  private static MethodInfo GetFactoryMethod(Type duType, int index) =>
    index switch
    {
      1 => duType.GetMethod("First", BindingFlags.Public | BindingFlags.Static)!,
      2 => duType.GetMethod("Second", BindingFlags.Public | BindingFlags.Static)!,
      3 => duType.GetMethod("Third", BindingFlags.Public | BindingFlags.Static)!,
      4 => duType.GetMethod("Fourth", BindingFlags.Public | BindingFlags.Static)!,
      5 => duType.GetMethod("Fifth", BindingFlags.Public | BindingFlags.Static)!,
      6 => duType.GetMethod("Sixth", BindingFlags.Public | BindingFlags.Static)!,
      7 => duType.GetMethod("Seventh", BindingFlags.Public | BindingFlags.Static)!,
      _ => throw new ArgumentOutOfRangeException()
    };

  public override bool CanConvert(Type objectType) =>
    objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Du<,>);
}
