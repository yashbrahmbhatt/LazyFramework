using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace LazyFramework.DX.Shared.Models
{
    public abstract class DictionaryObject : DynamicObject
    {
        public Dictionary<string, object> Data = new();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string propertyName = binder.Name;

            var property = GetType().GetProperty(propertyName);
            if (property == null)
                throw new InvalidOperationException($"Property '{propertyName}' does not exist on type '{GetType().Name}'.");

            if (Data.TryGetValue(propertyName, out var value))
            {
                result = value;
                return true;
            }

            throw new KeyNotFoundException($"Key '{propertyName}' not found in the dictionary.");
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            string propertyName = binder.Name;

            var property = GetType().GetProperty(propertyName);
            if (property == null)
                throw new InvalidOperationException($"Property '{propertyName}' does not exist on type '{GetType().Name}'.");

            Data[propertyName] = value;
            return true;
        }

        public object this[string key]
        {
            get => Data.TryGetValue(key, out var value) ? value : throw new KeyNotFoundException($"Key '{key}' not found.");
            set => Data[key] = value;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data, Formatting.Indented);
        }



    }
;

    public static class TypeParsers
    {
        public static readonly Dictionary<Type, Func<string, object>> Parsers = new()
        {
            { typeof(string), value => value },
            { typeof(int), value => ParsePrimitive<int>(value) },
            { typeof(double), value => ParsePrimitive<double>(value) },
            { typeof(bool), value => bool.Parse(value) },
            { typeof(DateTime), value => DateTime.Parse(value) },
            { typeof(TimeSpan), value => TimeSpan.Parse(value) },
            { typeof(List<string>), value => ParseList<string>(value) },
            { typeof(List<int>), value => ParseList<int>(value) },
            { typeof(List<double>), value => ParseList<double>(value) },
            { typeof(List<bool>), value => ParseList<bool>(value) },
            { typeof(List<DateTime>), value => ParseList<DateTime>(value) },
            { typeof(List<TimeSpan>), value => ParseList<TimeSpan>(value) },
            { typeof(string[]), value => ParseArray<string>(value) },
            { typeof(int[]), value => ParseArray<int>(value) },
            { typeof(double[]), value => ParseArray<double>(value) },
            { typeof(bool[]), value => ParseArray<bool>(value) },
            { typeof(DateTime[]), value => ParseArray<DateTime>(value) },
            { typeof(TimeSpan[]), value => ParseArray<TimeSpan>(value) },
        };

        // Generic helper method to parse primitive types
        private static T ParsePrimitive<T>(string value)
        {
            if (typeof(T) == typeof(DateTime))
            {
                return (T)(object)DateTime.Parse(value);  // Specific handling for DateTime
            }
            if (typeof(T) == typeof(TimeSpan))
            {
                return (T)(object)TimeSpan.Parse(value);  // Specific handling for TimeSpan
            }

            return (T)Convert.ChangeType(value, typeof(T)); // Fallback to Convert.ChangeType for other types
        }

        // Helper method to parse lists of primitive types
        private static List<T> ParseList<T>(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? new List<T>()
                : value.Split(',').Select(v => ParsePrimitive<T>(v)).ToList();
        }

        // Helper method to parse arrays of primitive types
        private static T[] ParseArray<T>(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? new T[0]
                : value.Split(',').Select(v => ParsePrimitive<T>(v)).ToArray();
        }
    }


    public static class DictionaryObjectFactory
    {
        public static TDerived FromDictionary<TDerived>(Dictionary<string, object> dictionary)
            where TDerived : DictionaryObject, new()
        {
            TDerived instance = new();

            // Retrieve all members (properties and fields)
            var members = typeof(TDerived).GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.MemberType == MemberTypes.Property || m.MemberType == MemberTypes.Field);

            foreach (var member in members)
            {
                object parsedValue;

                // Check if the dictionary contains a matching key
                if (!dictionary.TryGetValue(member.Name, out var rawValue))
                    continue;

                if (rawValue is string stringValue)
                {
                    // Get the type of the member
                    var memberType = member is PropertyInfo prop ? prop.PropertyType : ((FieldInfo)member).FieldType;

                    // Parse the value
                    if (TypeParsers.Parsers.TryGetValue(memberType, out var parser))
                    {
                        parsedValue = parser(stringValue);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            $"Type '{memberType.Name}' is not supported for '{member.Name}' in '{typeof(TDerived).Name}'.");
                    }
                }
                else
                {
                    // Directly map non-string values
                    parsedValue = rawValue;
                }

                // Set the value
                if (member is PropertyInfo property)
                {
                    property.SetValue(instance, parsedValue);
                }
                else if (member is FieldInfo field)
                {
                    field.SetValue(instance, parsedValue);
                }

                // Add to the instance dictionary
                instance[member.Name] = parsedValue;
            }

            return instance;
        }




    }
}