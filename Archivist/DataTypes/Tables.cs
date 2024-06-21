using Archivist.BaseClasses;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a collection of tables.
    /// </summary>
    /// <seealso cref="FileBaseClass{TFileType}"/>
    public class Tables : FileBaseClass<Tables>, IComparable<Tables>, IEquatable<Tables>, IListConvertable, IList<Table>
    {
        /// <summary>
        /// The number of tables in the file.
        /// </summary>
        public int Count => TableEntries.Count;

        /// <summary>
        /// Is this file read only?
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// The table entries.
        /// </summary>
        private List<Table> TableEntries { get; } = new List<Table>();

        /// <summary>
        /// Gets or sets the table at the specified index.
        /// </summary>
        /// <param name="index">The index of the table to get or set.</param>
        /// <returns>The table at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is out of range.</exception>
        public Table this[int index]
        {
            get
            {
                if (index < 0 || index >= TableEntries.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return TableEntries[index];
            }
            set
            {
                if (index < 0 || index >= TableEntries.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                TableEntries[index] = value ?? new Table();
            }
        }

        /// <summary>
        /// Converts the Tables to a Card.
        /// </summary>
        /// <param name="file">The Tables to convert.</param>
        /// <returns>The Card converted from the Tables.</returns>
        public static implicit operator Card?(Tables? file)
        {
            if (file is null)
                return null;
            var ReturnValue = (Card?)file.TableEntries.FirstOrDefault();
            if (ReturnValue is null)
                return null;
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Converts the Tables to a Table.
        /// </summary>
        /// <param name="file">The Tables to convert.</param>
        /// <returns>The Table converted from the Tables.</returns>
        public static implicit operator Table?(Tables? file)
        {
            if (file is null)
                return null;
            Table? ReturnValue = file.TableEntries.FirstOrDefault();
            if (ReturnValue is null)
                return null;
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            ReturnValue.Title = file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Converts the Tables to a Text.
        /// </summary>
        /// <param name="file">The Tables to convert.</param>
        /// <returns>The Text converted from the Tables.</returns>
        public static implicit operator Text?(Tables? file)
        {
            if (file is null)
                return null;
            var ReturnValue = new Text(file.GetContent(), file.Title);
            foreach (KeyValuePair<string, string> Metadata in file.Metadata)
            {
                ReturnValue.Metadata.Add(Metadata.Key, Metadata.Value);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Determines whether two Tables objects are not equal.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns><c>true</c> if the two Tables objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Tables? left, Tables? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first Tables object is less than the second Tables object.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns>
        /// <c>true</c> if the first Tables object is less than the second Tables object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Tables? left, Tables? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first Tables object is less than or equal to the second Tables object.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns>
        /// <c>true</c> if the first Tables object is less than or equal to the second Tables
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Tables? left, Tables? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two Tables objects are equal.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns><c>true</c> if the two Tables objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Tables? left, Tables? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first Tables object is greater than the second Tables object.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns>
        /// <c>true</c> if the first Tables object is greater than the second Tables object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Tables? left, Tables? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first Tables object is greater than or equal to the second Tables object.
        /// </summary>
        /// <param name="left">The first Tables object.</param>
        /// <param name="right">The second Tables object.</param>
        /// <returns>
        /// <c>true</c> if the first Tables object is greater than or equal to the second Tables
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(Tables? left, Tables? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds a new table to the Table collection.
        /// </summary>
        /// <param name="item">The table to add to the collection.</param>
        public void Add(Table? item) => TableEntries.Add(item ?? new Table());

        /// <summary>
        /// Adds a new row to the Tables.
        /// </summary>
        /// <returns>The new row.</returns>
        public Table AddTable()
        {
            var NewTable = new Table();
            TableEntries.Add(NewTable);
            return NewTable;
        }

        /// <summary>
        /// Clears the tables.
        /// </summary>
        public void Clear() => TableEntries.Clear();

        /// <summary>
        /// Compares the current Tables object with another Tables object.
        /// </summary>
        /// <param name="other">The Tables object to compare with this Tables object.</param>
        /// <returns>A value that indicates the relative order of the Tables objects being compared.</returns>
        public override int CompareTo(Tables? other)
        {
            if (other is null)
                return 1;
            return other.GetContent()?.CompareTo(GetContent()) ?? 1;
        }

        /// <summary>
        /// Determines whether the Tables contains a specific table.
        /// </summary>
        /// <param name="item">The table to locate in the Tables.</param>
        /// <returns><c>true</c> if the table is found in the Tables; otherwise, <c>false</c>.</returns>
        public bool Contains(Table? item) => item is not null && TableEntries.Contains(item);

        /// <summary>
        /// Copies the list object to the Tables.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The list object to copy to the Tables.</param>
        public void ConvertFrom<TObject>(List<TObject?>? obj)
        {
            obj ??= new List<TObject?>();
            if (obj.Count == 0)
                return;
            TableEntries.Clear();
            Table NewTable = AddTable();
            NewTable.ConvertFrom(obj);
        }

        /// <summary>
        /// Converts this instance into the object array of the type specified.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting array.</returns>
        public List<TObject?> ConvertTo<TObject>() => TableEntries.SelectMany(x => x.ConvertTo<TObject?>()).ToList();

        /// <summary>
        /// Copies the elements of the Tables to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the Tables.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        /// <exception cref="ArgumentNullException">Thrown when the array is null.</exception>
        public void CopyTo(Table[]? array, int arrayIndex)
        {
            if (array is null || array.Length == 0)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            TableEntries.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines whether the current Tables object is equal to another Tables object.
        /// </summary>
        /// <param name="obj">The Tables object to compare with this Tables object.</param>
        /// <returns>
        /// <c>true</c> if the current Tables object is equal to the other Tables object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => obj is Tables Tables && Equals(Tables);

        /// <summary>
        /// Determines whether the current Tables object is equal to another Tables object.
        /// </summary>
        /// <param name="other">The Tables object to compare with this Tables object.</param>
        /// <returns>
        /// <c>true</c> if the current Tables object is equal to the other Tables object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Tables? other) => ReferenceEquals(this, other) || (other is not null && GetContent() == other.GetContent());

        /// <summary>
        /// Gets the content of the Tables.
        /// </summary>
        /// <returns>The content of the Tables.</returns>
        public override string? GetContent()
        {
            return new StringBuilder()
                    .AppendJoin('\n', TableEntries)
                    .ToString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Tables.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the Tables.</returns>
        public IEnumerator<Table> GetEnumerator() => TableEntries.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the Tables.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the Tables.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the hash code for the current Tables object.
        /// </summary>
        /// <returns>A hash code for the current Tables object.</returns>
        public override int GetHashCode()
        {
            var HashCode = new HashCode();
            foreach (Table Table in TableEntries)
            {
                HashCode.Add(Table);
            }
            return HashCode.ToHashCode();
        }

        /// <summary>
        /// Gets the index of the specified table.
        /// </summary>
        /// <param name="item">The table to locate in the Tables.</param>
        /// <returns>The index of the table if found; otherwise, -1.</returns>
        public int IndexOf(Table? item) => TableEntries.IndexOf(item!);

        /// <summary>
        /// Inserts a new table at the specified index.
        /// </summary>
        /// <param name="index">The index to insert the table at.</param>
        /// <param name="item">The table to insert.</param>
        public void Insert(int index, Table? item)
        {
            if (index < 0)
                index = 0;
            if (index > Count)
                index = Count;
            item ??= new Table();
            TableEntries.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific table from the Tables.
        /// </summary>
        /// <param name="item">The table to remove from the Tables.</param>
        /// <returns><c>true</c> if the table was successfully removed; otherwise, <c>false</c>.</returns>
        public bool Remove(Table? item) => item is not null && TableEntries.Remove(item);

        /// <summary>
        /// Removes the table at the specified index.
        /// </summary>
        /// <param name="index">The index of the table to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;
            TableEntries.RemoveAt(index);
        }

        /// <summary>
        /// Converts the Tables to the specified file type.
        /// </summary>
        /// <typeparam name="TFile">The type of the file.</typeparam>
        /// <returns>The file of the specified type.</returns>
        public override TFile? ToFileType<TFile>() where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue = null;
            if (FileType == typeof(Card))
                ReturnValue = (Card?)this;
            else if (FileType == typeof(Tables))
                ReturnValue = this;
            else if (FileType == typeof(Table))
                ReturnValue = (Table?)this;
            else if (FileType == typeof(Text))
                ReturnValue = (Text?)this;

            return (TFile?)ReturnValue;
        }
    }
}