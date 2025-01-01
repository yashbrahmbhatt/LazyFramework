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
        
        public override string ToString() {
            return JsonConvert.SerializeObject(Data, Formatting.Indented);
        }



    }

    public static class TypeParsers
    {
        public static readonly Dictionary<Type, Func<string, object>> Parsers = new()
    {
        { typeof(string), value => value },
        { typeof(int), value => int.Parse(value) },
        { typeof(double), value => double.Parse(value) },
        { typeof(bool), value => bool.Parse(value) },
        { typeof(DateTime), value => DateTime.Parse(value) },
        { typeof(TimeSpan), value => TimeSpan.Parse(value) },
        { typeof(List<string>), value => value.Split(',').ToList() },
        { typeof(List<int>), value => value.Split(',').Select(int.Parse).ToList() },
        { typeof(List<double>), value => value.Split(',').Select(double.Parse).ToList() },
        { typeof(string[]), value => value.Split(',') },
        { typeof(int[]), value => value.Split(',').Select(int.Parse).ToArray() },
        { typeof(double[]), value => value.Split(',').Select(double.Parse).ToArray() },
    };
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