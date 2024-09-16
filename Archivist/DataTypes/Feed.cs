using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.DataTypes.Feeds;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a Feed object.
    /// </summary>
    /// <seealso cref="FileBaseClass{Feed}"/>
    /// <seealso cref="IComparable{Feed}"/>
    /// <seealso cref="IEquatable{Feed}"/>
    /// <seealso cref="IList{Feed}"/>
    public class Feed : FileBaseClass<Feed>, IComparable<Feed>, IEquatable<Feed>, IList<Channel>
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

        /// <inheritdoc/>
        public bool IsReadOnly => ((ICollection<Channel>)Channels).IsReadOnly;

        /// <inheritdoc/>
        public Channel this[int index]
        {
            get
            {
                if (index < 0 || index >= Channels.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                return Channels[index];
            }
            set
            {
                if (index < 0 || index >= Channels.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                Channels[index] = value;
            }
        }

        /// <summary>
        /// Converts the Feed to a Calendar.
        /// </summary>
        /// <param name="file">The Feed to convert.</param>
        public static implicit operator Calendar?(Feed? file)
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
        /// Determines whether two Feed objects are not equal.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>True if the Feed objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Feed? left, Feed? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether one Feed object is less than another Feed object.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>True if the first Feed is less than the second Feed; otherwise, false.</returns>
        public static bool operator <(Feed? left, Feed? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether one Feed object is less than or equal to another Feed object.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>
        /// True if the first Feed is less than or equal to the second Feed; otherwise, false.
        /// </returns>
        public static bool operator <=(Feed? left, Feed? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two Feed objects are equal.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>True if the Feed objects are equal; otherwise, false.</returns>
        public static bool operator ==(Feed? left, Feed? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether one Feed object is greater than another Feed object.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>True if the first Feed is greater than the second Feed; otherwise, false.</returns>
        public static bool operator >(Feed? left, Feed? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether one Feed object is greater than or equal to another Feed object.
        /// </summary>
        /// <param name="left">The first Feed to compare.</param>
        /// <param name="right">The second Feed to compare.</param>
        /// <returns>
        /// True if the first Feed is greater than or equal to the second Feed; otherwise, false.
        /// </returns>
        public static bool operator >=(Feed? left, Feed? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <inheritdoc/>
        public void Add(Channel? item)
        {
            if (item is null)
                return;
            Channels.Add(item);
        }

        /// <inheritdoc/>
        public void Clear() => Channels.Clear();

        /// <summary>
        /// Compares the Feed to another Feed based on their content.
        /// </summary>
        /// <param name="other">The other Feed to compare.</param>
        /// <returns>An integer that indicates the relative order of the Feeds.</returns>
        public override int CompareTo(Feed? other) => string.Compare(other?.GetContent(), GetContent(), StringComparison.OrdinalIgnoreCase);

        /// <inheritdoc/>
        public bool Contains(Channel? item)
        {
            if (item is null)
                return false;
            return Channels.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Channel[]? array, int arrayIndex)
        {
            if (array == null)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                return;
            Channels.CopyTo(array, arrayIndex);
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
        public override string? GetContent() => string.Join("\r\n", Channels);

        /// <inheritdoc/>
        public IEnumerator<Channel> GetEnumerator() => Channels.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => Channels.GetEnumerator();

        /// <summary>
        /// Gets the hash code of the Feed based on its content.
        /// </summary>
        /// <returns>The hash code of the Feed.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

        /// <inheritdoc/>
        public int IndexOf(Channel? item)
        {
            if (item is null)
                return -1;
            return Channels.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, Channel? item)
        {
            if (item is null || index < 0 || index > Channels.Count)
                return;
            Channels.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(Channel? item)
        {
            if (item is null)
                return false;
            return Channels.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Channels.Count)
                return;
            Channels.RemoveAt(index);
        }

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