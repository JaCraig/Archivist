using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using Newtonsoft.Json;
using ObjectCartographer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a structured object.
    /// </summary>
    public class StructuredObject : FileBaseClass<StructuredObject>, IComparable<StructuredObject>, IEquatable<StructuredObject>, IObjectConvertable, IDictionary<string, object?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredObject"/> class.
        /// </summary>
        public StructuredObject()
            : this(new ExpandoObject())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredObject"/> class with the
        /// specified content.
        /// </summary>
        /// <param name="value">The content of the structured object.</param>
        public StructuredObject(IDictionary<string, object?>? value)
        {
            Content = value ?? new ExpandoObject();
        }

        /// <summary>
        /// Gets the number of items in the structured object.
        /// </summary>
        public int Count => Content.Count;

        /// <summary>
        /// Determines whether the structured object is read-only.
        /// </summary>
        public bool IsReadOnly => Content.IsReadOnly;

        /// <summary>
        /// Keys in the structured object.
        /// </summary>
        public ICollection<string> Keys => Content.Keys;

        /// <summary>
        /// Values in the structured object.
        /// </summary>
        public ICollection<object?> Values => Content.Values;

        /// <summary>
        /// Gets or sets the content of the structured object.
        /// </summary>
        private IDictionary<string, object?> Content { get; set; }

        /// <summary>
        /// Gets the value of the structured object with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value of the structured object with the specified key.</returns>
        public object? this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    return null;
                _ = Content.TryGetValue(key, out var Value);
                return Value;
            }
            set
            {
                if (string.IsNullOrEmpty(key))
                    return;
                Content[key] = value;
            }
        }

        /// <summary>
        /// Converts the structured object to a card.
        /// </summary>
        /// <param name="structuredObject">The structured object to convert.</param>
        public static implicit operator Card?(StructuredObject? structuredObject)
        {
            return StructuredObjectToCardConverter.Convert(structuredObject);
        }

        /// <summary>
        /// Converts the structured object to a table.
        /// </summary>
        /// <param name="structuredObject">The structured object to convert.</param>
        public static implicit operator Table?(StructuredObject? structuredObject)
        {
            return StructuredObjectToTableConverter.Convert(structuredObject);
        }

        /// <summary>
        /// Converts the structured object to a tables object.
        /// </summary>
        /// <param name="structuredObject">The structured object to convert.</param>
        public static implicit operator Tables?(StructuredObject? structuredObject)
        {
            return StructuredObjectToTablesConverter.Convert(structuredObject);
        }

        /// <summary>
        /// Converts the structured object to a text object.
        /// </summary>
        /// <param name="structuredObject">The structured object to convert.</param>
        public static implicit operator Text?(StructuredObject? structuredObject)
        {
            return AnythingToTextConverter.Convert(structuredObject);
        }

        /// <summary>
        /// Determines whether two structured objects are not equal.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns><c>true</c> if the structured objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(StructuredObject? left, StructuredObject? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the left structured object is less than the right structured object.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns>
        /// <c>true</c> if the left structured object is less than the right structured object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(StructuredObject? left, StructuredObject? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the left structured object is less than or equal to the right
        /// structured object.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns>
        /// <c>true</c> if the left structured object is less than or equal to the right structured
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(StructuredObject? left, StructuredObject? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two structured objects are equal.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns><c>true</c> if the structured objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(StructuredObject? left, StructuredObject? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the left structured object is greater than the right structured object.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns>
        /// <c>true</c> if the left structured object is greater than the right structured object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(StructuredObject? left, StructuredObject? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the left structured object is greater than or equal to the right
        /// structured object.
        /// </summary>
        /// <param name="left">The left structured object.</param>
        /// <param name="right">The right structured object.</param>
        /// <returns>
        /// <c>true</c> if the left structured object is greater than or equal to the right
        /// structured object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(StructuredObject? left, StructuredObject? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds a key-value pair to the structured object.
        /// </summary>
        /// <param name="key">The key of the value to add.</param>
        /// <param name="value">The value to add.</param>
        public void Add(string key, object? value)
        {
            if (string.IsNullOrEmpty(key))
                return;
            Content.Add(key, value);
        }

        /// <summary>
        /// Adds a key-value pair to the structured object.
        /// </summary>
        /// <param name="item">The key-value pair to add.</param>
        public void Add(KeyValuePair<string, object?> item)
        {
            if (string.IsNullOrEmpty(item.Key))
                return;
            Content.Add(item);
        }

        /// <summary>
        /// Clears the structured object.
        /// </summary>
        public void Clear() => Content.Clear();

        /// <summary>
        /// Compares the current structured object with another structured object.
        /// </summary>
        /// <param name="other">The structured object to compare with this structured object.</param>
        /// <returns>A value indicating the relative order of the objects being compared.</returns>
        public override int CompareTo(StructuredObject? other)
        {
            if (other?.Content is null && Content is null)
                return 0;
            if (other?.Content is null)
                return 1;
            if (Content is null)
                return -1;
            return other.GetContent()?.CompareTo(GetContent()) ?? 0;
        }

        /// <summary>
        /// Determines whether the current structured object contains the specified key-value pair.
        /// </summary>
        /// <param name="item">The key-value pair to locate in the structured object.</param>
        /// <returns>True if the structured object contains the key-value pair; otherwise, false.</returns>
        public bool Contains(KeyValuePair<string, object?> item)
        {
            if (string.IsNullOrEmpty(item.Key))
                return false;
            return Content.Contains(item);
        }

        /// <summary>
        /// Determines whether the current structured object contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the structured object.</param>
        /// <returns>True if the structured object contains the key; otherwise, false.</returns>
        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            return Content.ContainsKey(key);
        }

        /// <summary>
        /// Converts the structured object from the specified object.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to convert from.</typeparam>
        /// <param name="obj">The object to convert from.</param>
        public void ConvertFrom<TObject>(TObject obj)
        {
            if (obj is null)
                return;
            if (obj is string StringObject)
            {
                Content = JsonConvert.DeserializeObject<ExpandoObject>(StringObject) ?? new ExpandoObject();
                return;
            }
            Content = obj.To<ExpandoObject>();
        }

        /// <summary>
        /// Converts the structured object to the specified object type.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to convert to.</typeparam>
        /// <returns>The converted object.</returns>
        public TObject? ConvertTo<TObject>()
        {
            if (Content is null)
                return default;
            return Content.To<TObject>();
        }

        /// <summary>
        /// Copies the key-value pairs of the structured object to an array.
        /// </summary>
        /// <param name="array">The array to copy the key-value pairs to.</param>
        /// <param name="arrayIndex">
        /// The index in the array to start copying the key-value pairs to.
        /// </param>
        public void CopyTo(KeyValuePair<string, object?>[] array, int arrayIndex)
        {
            if (array is null)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                return;
            Content.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines whether the current structured object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current structured object.</param>
        /// <returns>
        /// <c>true</c> if the current structured object is equal to the other object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is not StructuredObject StructuredObject)
                return false;
            return Equals(StructuredObject);
        }

        /// <summary>
        /// Determines whether the current structured object is equal to another structured object.
        /// </summary>
        /// <param name="other">The structured object to compare with the current structured object.</param>
        /// <returns>
        /// <c>true</c> if the current structured object is equal to the other structured object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(StructuredObject? other) => other is not null && other.GetContent() == GetContent();

        /// <summary>
        /// Gets the content of the structured object as a string.
        /// </summary>
        /// <returns>The content of the structured object as a string.</returns>
        public override string? GetContent()
        {
            if (Content is null)
                return "";
            return JsonConvert.SerializeObject(Content);
        }

        /// <summary>
        /// Gets the enumerator for the structured object.
        /// </summary>
        /// <returns>The enumerator for the structured object.</returns>
        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator() => Content.GetEnumerator();

        /// <summary>
        /// Gets the enumerator for the structured object.
        /// </summary>
        /// <returns>The enumerator for the structured object.</returns>
        IEnumerator IEnumerable.GetEnumerator() => Content.GetEnumerator();

        /// <summary>
        /// Gets the hash code of the structured object.
        /// </summary>
        /// <returns>The hash code of the structured object.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode() ?? 0;

        /// <summary>
        /// Gets the value of the structured object with the specified key.
        /// </summary>
        /// <typeparam name="TObject">The type of the value to get.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value of the structured object with the specified key.</returns>
        public TObject? GetValue<TObject>(string key)
        {
            _ = TryGetValue(key, out TObject? Value);
            return Value;
        }

        /// <summary>
        /// Removes the key-value pair with the specified key from the structured object.
        /// </summary>
        /// <param name="key">The key of the key-value pair to remove.</param>
        /// <returns><c>true</c> if the key-value pair was removed; otherwise, <c>false</c>.</returns>
        public bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;
            return Content.Remove(key);
        }

        /// <summary>
        /// Removes the key-value pair with the specified key from the structured object.
        /// </summary>
        /// <param name="item">The key-value pair to remove.</param>
        /// <returns><c>true</c> if the key-value pair was removed; otherwise, <c>false</c>.</returns>
        public bool Remove(KeyValuePair<string, object?> item)
        {
            if (string.IsNullOrEmpty(item.Key))
                return false;
            return Content.Remove(item);
        }

        /// <summary>
        /// Sets the value of the structured object with the specified key.
        /// </summary>
        /// <typeparam name="TObject">The type of the value to set.</typeparam>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">Value to set</param>
        /// <returns>The structured object with the value set.</returns>
        public StructuredObject SetValue<TObject>(string key, TObject value)
        {
            if (string.IsNullOrEmpty(key))
                return this;

            Content[key] = value;

            return this;
        }

        /// <summary>
        /// Converts the structured object to the specified file type.
        /// </summary>
        /// <typeparam name="TFile">The type of the file.</typeparam>
        /// <returns>The file of the specified type.</returns>
        public override TFile? ToFileType<TFile>() where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue = null;
            if (FileType == typeof(Card))
                ReturnValue = (Card?)this;
            else if (FileType == typeof(Table))
                ReturnValue = (Table?)this;
            else if (FileType == typeof(Tables))
                ReturnValue = (Tables?)this;
            else if (FileType == typeof(Text))
                ReturnValue = (Text?)this;
            else if (FileType == typeof(StructuredObject))
                ReturnValue = this;

            return (TFile?)ReturnValue;
        }

        /// <summary>
        /// Tries to get the value of the structured object with the specified key.
        /// </summary>
        /// <typeparam name="TObject">The type of the value to get.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">The value of the structured object with the specified key.</param>
        /// <returns><c>true</c> if the value was retrieved; otherwise, <c>false</c>.</returns>
        public bool TryGetValue<TObject>(string key, out TObject? value)
        {
            value = default;
            if (string.IsNullOrEmpty(key))
                return false;
            if (!Content.TryGetValue(key, out var Value))
                return false;
            value = Value.To<TObject>();
            return true;
        }

        /// <summary>
        /// Tries to get the value of the structured object with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">The value of the structured object with the specified key.</param>
        /// <returns><c>true</c> if the value was retrieved; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out object? value)
        {
            value = default;
            if (string.IsNullOrEmpty(key))
                return false;
            return Content.TryGetValue(key, out _);
        }
    }
}