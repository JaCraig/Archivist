using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a Feed object.
    /// </summary>
    public class Feed : FileBaseClass<Feed>, IComparable<Feed>, IEquatable<Feed>, IObjectConvertable, IEnumerable<Channel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// </summary>
        public Feed()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class with the specified converter.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        public Feed(Convertinator? converter)
            : base(converter)
        {
        }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <value>The channels.</value>
        public List<Channel> Channels { get; } = new List<Channel>();

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        public int Count => Channels.Count;

        /// <summary>
        /// Converts the Feed to a Calendar.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        public static implicit operator Calendar(Feed? file)
        {
            return FeedToCalendarConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Feed to a Card.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        /// <returns>The Card representation of the Feed.</returns>
        public static implicit operator Card?(Feed? file)
        {
            return FeedToCardConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Feed to a structured object.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        /// <returns>The structured object representation of the Feed.</returns>
        public static implicit operator StructuredObject?(Feed? file)
        {
            return FeedToStructuredObjectConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Feed to a table.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        /// <returns>The table representation of the Feed.</returns>
        public static implicit operator Table?(Feed? file)
        {
            return FeedToTableConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Feed to a Tables file.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        /// <returns>The Tables representation of the Feed.</returns>
        public static implicit operator Tables?(Feed? file)
        {
            return FeedToTablesConverter.Convert(file);
        }

        /// <summary>
        /// Converts the Feed to text.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        /// <returns>The text representation of the Feed.</returns>
        public static implicit operator Text?(Feed? file)
        {
            if (file is null)
                return null;
            Text? ReturnValue = AnythingToTextConverter.Convert(file);
            if (ReturnValue is not null)
                ReturnValue.Title ??= file.Channels?.FirstOrDefault()?.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Compares the Feed to another Feed based on their content.
        /// </summary>
        /// <param name="other">The other Feed to compare.</param>
        /// <returns>An integer that indicates the relative order of the Feeds.</returns>
        public override int CompareTo(Feed? other) => string.Compare(other?.GetContent(), GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Converts the object to the Feed.
        /// </summary>
        /// <typeparam name="TObject">The type of object to convert.</typeparam>
        /// <param name="obj">The object to convert.</param>
        public void ConvertFrom<TObject>(TObject obj)
        {
            if (obj is null)
                return;
            if (Channels.Count == 0)
                _ = AddEvent("", "", "", DateTime.Now, DateTime.Now);
            foreach (System.Reflection.PropertyInfo Property in typeof(TObject).GetProperties())
            {
                var Found = false;
                foreach (FeedComponent Component in Components)
                {
                    KeyValueField? Field = Component.Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                    if (Field is not null)
                    {
                        Field.Value = Property.GetValue(obj)?.ToString() ?? "";
                        Found = true;
                    }
                }
                if (!Found)
                    Events[0].Fields.Add(new KeyValueField(Property.Name, Array.Empty<KeyValueParameter>(), Property.GetValue(obj)?.ToString() ?? ""));
            }
        }

        /// <summary>
        /// Converts the Feed to the specified object type.
        /// </summary>
        /// <typeparam name="TObject">The type to convert the Feed to.</typeparam>
        /// <returns>The converted Feed.</returns>
        public TObject? ConvertTo<TObject>()
        {
            System.Reflection.PropertyInfo[] Properties = typeof(TObject).GetProperties();
            TObject? Result = Activator.CreateInstance<TObject>();
            foreach (System.Reflection.PropertyInfo Property in Properties)
            {
                foreach (FeedComponent Component in Components)
                {
                    KeyValueField? Field = Component.Fields.Find(field => string.Equals(field?.Property, Property.Name, StringComparison.OrdinalIgnoreCase));
                    if (Field is null)
                        continue;
                    Property.SetValue(Result, Field.Value);
                }
            }
            return Result;
        }

        /// <summary>
        /// Determines whether the Feed is equal to another Feed based on their content.
        /// </summary>
        /// <param name="other">The other Feed to compare.</param>
        /// <returns>True if the Feeds are equal; otherwise, false.</returns>
        public override bool Equals(Feed? other) => string.Equals(GetContent(), other?.GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the Feed is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the Feed is equal to the object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Feed FeedObject && Equals(FeedObject));

        /// <summary>
        /// Gets the content of the Feed.
        /// </summary>
        /// <returns>The content of the Feed.</returns>
        public override string? GetContent() => (Channels?.Count ?? 0) == 0 ? "" : string.Join('\n', Channels.Select(channel => channel.GetContent()));

        /// <inheritdoc/>
        public IEnumerator<Channel> GetEnumerator() => Channels.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => Channels.GetEnumerator();

        /// <summary>
        /// Gets the hash code of the Feed based on its content.
        /// </summary>
        /// <returns>The hash code of the Feed.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

        /// <summary>
        /// Converts the Feed to the specified object type.
        /// </summary>
        /// <typeparam name="TFile">The type to convert the Feed to.</typeparam>
        /// <returns>The converted Feed.</returns>
        public override TFile? ToFileType<TFile>()
            where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue;
            if (FileType == typeof(Feed))
                ReturnValue = (IGenericFile?)this;
            else if (FileType == typeof(Calendar))
                ReturnValue = (Calendar?)this;
            else if (FileType == typeof(Card))
                ReturnValue = (Card?)this;
            else if (FileType == typeof(Table))
                ReturnValue = (Table?)this;
            else if (FileType == typeof(Tables))
                ReturnValue = (Tables?)this;
            else if (FileType == typeof(Text))
                ReturnValue = (Text?)this;
            else if (FileType == typeof(StructuredObject))
                ReturnValue = (StructuredObject?)this;
            else
                ReturnValue = (IGenericFile?)Converter?.Convert(this, typeof(TFile));

            return (TFile?)ReturnValue;
        }
    }
}