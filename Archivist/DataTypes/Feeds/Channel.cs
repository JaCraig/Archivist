using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Archivist.DataTypes.Feeds
{
    /// <summary>
    /// Channel
    /// </summary>
    public class Channel : IList<FeedItem>, IComparable<Channel>, IEquatable<Channel>
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public List<string> Categories { get; } = new List<string>();

        /// <summary>
        /// Gets the cloud.
        /// </summary>
        /// <value>The cloud.</value>
        public string? Cloud { get; set; } = "";

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        /// <value>The copyright.</value>
        public string Copyright { get; set; } = "Copyright " + DateTime.Now.ToString("yyyy", CultureInfo.InvariantCulture) + ". All rights reserved.";

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public string? Description { get; set; } = "";

        /// <summary>
        /// Gets the docs.
        /// </summary>
        /// <value>The docs.</value>
        public string Docs { get; set; } = "http://blogs.law.harvard.edu/tech/rss";

        /// <summary>
        /// Gets or sets a value indicating whether this <see
        /// cref="T:FileCurator.Formats.Data.Interfaces.IChannel"/> is explicit.
        /// </summary>
        /// <value><c>true</c> if explicit; otherwise, <c>false</c>.</value>
        public bool Explicit { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string? ImageUrl { get; set; } = "";

        /// <summary>
        /// Gets a value indicating whether the <see
        /// cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public List<FeedItem> Items { get; } = new List<FeedItem>();

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; } = "en-us";

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        /// <value>The link.</value>
        public string? Link { get; set; } = "";

        /// <summary>
        /// Gets or sets the pub date.
        /// </summary>
        /// <value>The pub date.</value>
        public DateTime PubDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string? Title { get; set; } = "";

        /// <summary>
        /// Gets or sets the TTL.
        /// </summary>
        /// <value>The TTL.</value>
        public int TTL { get; set; } = 5;

        /// <summary>
        /// Gets or sets the web master.
        /// </summary>
        /// <value>The web master.</value>
        public string? WebMaster { get; set; } = "";

        /// <summary>
        /// Gets or sets the <see cref="FeedItem"/> at the specified index.
        /// </summary>
        /// <value>The <see cref="FeedItem"/>.</value>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="FeedItem"/>.</returns>
        public FeedItem this[int index]
        {
            get
            {
                if (index < 0 || index >= Items.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return Items[index];
            }
            set
            {
                if (index < 0 || index >= Items.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Items[index] = value;
            }
        }

        /// <summary>
        /// Are the two channels not equal each other.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>True if the two channels are not equal; otherwise, false.</returns>
        public static bool operator !=(Channel? left, Channel? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Is the first channel less than the second channel.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>True if the first channel is less than the second channel; otherwise, false.</returns>
        public static bool operator <(Channel? left, Channel? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Is the first channel less than or equal to the second channel.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>
        /// True if the first channel is less than or equal to the second channel; otherwise, false.
        /// </returns>
        public static bool operator <=(Channel? left, Channel? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Are the two channels equal each other.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>True if the two channels are equal; otherwise, false.</returns>
        public static bool operator ==(Channel? left, Channel? right)
        {
            return (left is null) ? right is null : left.Equals(right);
        }

        /// <summary>
        /// Is the first channel greater than the second channel.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>
        /// True if the first channel is greater than the second channel; otherwise, false.
        /// </returns>
        public static bool operator >(Channel? left, Channel? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Is the first channel greater than or equal to the second channel.
        /// </summary>
        /// <param name="left">The first channel to compare.</param>
        /// <param name="right">The second channel to compare.</param>
        /// <returns>
        /// True if the first channel is greater than or equal to the second channel; otherwise, false.
        /// </returns>
        public static bool operator >=(Channel? left, Channel? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        public void Add(FeedItem? item)
        {
            if (item is null)
                return;
            Items.Add(item);
        }

        /// <summary>
        /// Adds the list of items to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="items">The items to add.</param>
        public void AddRange(IEnumerable<FeedItem>? items)
        {
            if (items is null)
                return;
            Items.AddRange(items);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        public void Clear() => Items.Clear();

        /// <summary>
        /// Compares the current <see cref="Channel"/> object with another <see cref="Channel"/> object.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Channel"/> object to compare with the current <see cref="Channel"/> object.
        /// </param>
        /// <returns>
        /// A value indicating the relative order of the objects being compared. The return value
        /// has the following meanings:
        /// - Less than zero: The current object is less than the <paramref name="other"/> object.
        /// - Zero: The current object is equal to the <paramref name="other"/> object.
        /// - Greater than zero: The current object is greater than the <paramref name="other"/> object.
        /// </returns>
        public int CompareTo(Channel? other)
        {
            if (other is null)
                return 1;
            if (ReferenceEquals(this, other))
                return 0;
            if (Equals(other))
                return 0;
            if (Items.Count != other.Items.Count)
                return Items.Count.CompareTo(other.Items.Count);
            for (var X = 0; X < Items.Count; ++X)
            {
                var Result = Items[X].CompareTo(other.Items[X]);
                if (Result != 0)
                    return Result;
            }
            if (Cloud != other.Cloud)
                return Cloud?.CompareTo(other.Cloud) ?? -1;
            if (Copyright != other.Copyright)
                return Copyright?.CompareTo(other.Copyright) ?? -1;
            if (Description != other.Description)
                return Description?.CompareTo(other.Description) ?? -1;
            if (Docs != other.Docs)
                return Docs?.CompareTo(other.Docs) ?? -1;
            if (Explicit != other.Explicit)
                return Explicit.CompareTo(other.Explicit);
            if (ImageUrl != other.ImageUrl)
                return ImageUrl?.CompareTo(other.ImageUrl) ?? -1;
            if (Language != other.Language)
                return Language?.CompareTo(other.Language) ?? -1;
            if (Link != other.Link)
                return Link?.CompareTo(other.Link) ?? -1;
            if (PubDate != other.PubDate)
                return PubDate.CompareTo(other.PubDate);
            if (Title != other.Title)
                return Title?.CompareTo(other.Title) ?? -1;
            if (TTL != other.TTL)
                return TTL.CompareTo(other.TTL);
            if (WebMaster != other.WebMaster)
                return WebMaster?.CompareTo(other.WebMaster) ?? -1;
            return 0;
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains
        /// a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see
        /// cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(FeedItem? item)
        {
            if (item is null)
                return false;
            return Items.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to
        /// an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements
        /// copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see
        /// cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(FeedItem[] array, int arrayIndex)
        {
            if (array is null)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                return;
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object is equal to the current object; otherwise, false.
        /// </returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is not null && Equals(obj as Channel));

        /// <summary>
        /// Determines whether the specified <see cref="Channel"/> object is equal to the current
        /// <see cref="Channel"/> object.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Channel"/> object to compare with the current <see cref="Channel"/> object.
        /// </param>
        /// <returns>
        /// true if the specified <see cref="Channel"/> object is equal to the current <see
        /// cref="Channel"/> object; otherwise, false.
        /// </returns>
        public bool Equals(Channel? other)
        {
            if (other is null)
                return false;
            if (Cloud != other.Cloud || Copyright != other.Copyright ||
                Description != other.Description || Docs != other.Docs ||
                Explicit != other.Explicit || ImageUrl != other.ImageUrl ||
                Language != other.Language || Link != other.Link || PubDate != other.PubDate ||
                Title != other.Title || TTL != other.TTL || WebMaster != other.WebMaster)
            {
                return false;
            }

            foreach (var Category in Categories)
            {
                if (!other.Categories.Contains(Category))
                    return false;
            }
            foreach (var Category in other.Categories)
            {
                if (!Categories.Contains(Category))
                    return false;
            }

            foreach (FeedItem Item in Items)
            {
                if (!other.Items.Contains(Item))
                    return false;
            }
            foreach (FeedItem Item in other.Items)
            {
                if (!Items.Contains(Item))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<FeedItem> GetEnumerator() => Items.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate
        /// through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Computes the hash code for the current <see cref="Channel"/> object.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override int GetHashCode()
        {
            var Hash1 = HashCode.Combine(Cloud, Copyright, Description, Docs, Explicit, ImageUrl);
            var Hash2 = HashCode.Combine(Language, Link, PubDate, Title, TTL, WebMaster);
            var ReturnValue = HashCode.Combine(Hash1, Hash2);
            foreach (var Category in Categories)
            {
                ReturnValue = HashCode.Combine(ReturnValue, Category);
            }
            foreach (FeedItem Item in Items)
            {
                ReturnValue = HashCode.Combine(ReturnValue, Item);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(FeedItem? item)
        {
            if (item is null)
                return -1;
            return Items.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the
        /// specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <paramref name="item"/> should be inserted.
        /// </param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        public void Insert(int index, FeedItem? item)
        {
            if (item is null)
                return;
            if (index < 0 || index > Items.Count)
                return;
            Items.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see
        /// cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also
        /// returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public bool Remove(FeedItem? item)
        {
            if (item is null)
                return false;
            return Items.Remove(item);
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Items.Count)
                return;
            Items.RemoveAt(index);
        }

        /// <summary>
        /// Converts the <see cref="Channel"/> object to its string representation.
        /// </summary>
        /// <returns>A string that represents the current <see cref="Channel"/> object.</returns>
        public override string ToString()
        {
            return new StringBuilder()
                .Append("Channel:Begin\r\n")
                .Append("Title: " + (Title ?? "No Title") + "\r\n")
                .Append("Description: " + (Description ?? "No Description") + "\r\n")
                .Append("Link: " + (Link ?? "No Link") + "\r\n")
                .Append("Language: " + Language + "\r\n")
                .Append("PubDate: " + PubDate.ToString("R") + "\r\n")
                .Append("WebMaster: " + (WebMaster ?? "No WebMaster") + "\r\n")
                .Append("Cloud: " + (Cloud ?? "No Cloud") + "\r\n")
                .Append("Docs: " + Docs + "\r\n")
                .Append("Explicit: " + Explicit + "\r\n")
                .Append("ImageUrl: " + (ImageUrl ?? "No ImageUrl") + "\r\n")
                .Append("TTL: " + TTL + "\r\n")
                .Append("Categories: " + string.Join(", ", Categories) + "\r\n")
                .Append("FeedItems:Begin\r\n")
                .Append(string.Join("\r\n", Items) + "\r\n")
                .Append("FeedItems:End\r\n")
                .Append("Channel:End")
                .ToString();
        }
    }
}