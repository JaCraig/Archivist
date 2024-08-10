using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a table in the Archivist system.
    /// </summary>
    /// <seealso cref="FileBaseClass{Table}"/>
    public class Table : FileBaseClass<Table>, IComparable<Table>, IEquatable<Table>, IListConvertable, IList<TableRow>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
            : this(null, ",")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="delimiter">The delimiter used by the table.</param>
        public Table(string delimiter)
            : this(null, delimiter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        /// <param name="converter">The type converter.</param>
        /// <param name="delimiter">The delimiter used by the table.</param>
        public Table(Convertinator? converter, string delimiter = ",")
            : base(converter)
        {
            Delimiter = delimiter;
        }

        /// <summary>
        /// Gets the headers of the table.
        /// </summary>
        /// <value>The headers.</value>
        public List<string> Columns { get; } = new List<string>();

        /// <summary>
        /// Gets the number of rows in the table.
        /// </summary>
        public int Count => Rows.Count;

        /// <summary>
        /// Gets or sets the delimiter used by the table.
        /// </summary>
        public string Delimiter
        {
            get
            {
                _ = Metadata.TryGetValue("Delimiter", out var Delimiter);
                return Delimiter ?? ",";
            }
            set => Metadata["Delimiter"] = value;
        }

        /// <summary>
        /// Gets a value indicating whether the table is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the rows of the table.
        /// </summary>
        private List<TableRow> Rows { get; } = new List<TableRow>();

        /// <summary>
        /// Gets or sets the row at the specified index.
        /// </summary>
        /// <param name="index">The index of the table.</param>
        /// <returns>The row at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The index is out of range.</exception>
        public TableRow this[int index]
        {
            get
            {
                if (index < 0 || index >= Rows.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return Rows[index];
            }
            set
            {
                if (index < 0 || index >= Rows.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Rows[index] = value ?? new TableRow(Columns);
            }
        }

        /// <summary>
        /// Converts the table to a calendar.
        /// </summary>
        /// <param name="file">The table to convert.</param>
        public static implicit operator Calendar?(Table? file)
        {
            return TableToCalendarConverter.Convert(file);
        }

        /// <summary>
        /// Converts the table to a card.
        /// </summary>
        /// <param name="file">The table to convert.</param>
        /// <returns>The card representation of the table.</returns>
        public static implicit operator Card?(Table? file)
        {
            return TableToCardConverter.Convert(file);
        }

        /// <summary>
        /// Converts the table to a structured object.
        /// </summary>
        /// <param name="file">The table to convert.</param>
        public static implicit operator StructuredObject?(Table? file)
        {
            return TableToStructuredObjectConverter.Convert(file);
        }

        /// <summary>
        /// Converts the table to a tables object.
        /// </summary>
        /// <param name="file">The table to convert.</param>
        /// <returns>The tables representation of the table.</returns>
        public static implicit operator Tables?(Table? file)
        {
            return TableToTablesConverter.Convert(file);
        }

        /// <summary>
        /// Converts the table to a text object.
        /// </summary>
        /// <param name="file">The table to convert.</param>
        /// <returns>The text representation of the table.</returns>
        public static implicit operator Text?(Table? file)
        {
            return AnythingToTextConverter.Convert(file);
        }

        /// <summary>
        /// Determines whether two table objects are not equal.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns><c>true</c> if the two table objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Table? left, Table? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first table object is less than the second table object.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns>
        /// <c>true</c> if the first table object is less than the second table object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Table? left, Table? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first table object is less than or equal to the second table object.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns>
        /// <c>true</c> if the first table object is less than or equal to the second table object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Table? left, Table? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two table objects are equal.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns><c>true</c> if the two table objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Table? left, Table? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first table object is greater than the second table object.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns>
        /// <c>true</c> if the first table object is greater than the second table object;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Table? left, Table? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first table object is greater than or equal to the second table object.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns>
        /// <c>true</c> if the first table object is greater than or equal to the second table
        /// object; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(Table? left, Table? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds a row to the table.
        /// </summary>
        /// <param name="item">The row to add to the table.</param>
        public void Add(TableRow? item) => Rows.Add(item ?? new TableRow(Columns));

        /// <summary>
        /// Adds a new row to the table.
        /// </summary>
        /// <returns>The new row.</returns>
        public TableRow AddRow()
        {
            var Row = new TableRow(Columns);
            Rows.Add(Row);
            return Row;
        }

        /// <summary>
        /// Clears the table.
        /// </summary>
        public void Clear() => Rows.Clear();

        /// <summary>
        /// Compares the current table object with another table object.
        /// </summary>
        /// <param name="other">The table object to compare with this table object.</param>
        /// <returns>A value that indicates the relative order of the table objects being compared.</returns>
        public override int CompareTo(Table? other)
        {
            if (other is null)
                return 1;
            return other.GetContent()?.CompareTo(GetContent()) ?? 1;
        }

        /// <summary>
        /// Determines if the table contains the specified row.
        /// </summary>
        /// <param name="item">The row to find in the table.</param>
        /// <returns><c>true</c> if the table contains the row; otherwise, <c>false&gt;</c>.</returns>
        public bool Contains(TableRow? item) => item is not null && Rows.Contains(item);

        /// <summary>
        /// Copies the list object to the table.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The list object to copy to the table.</param>
        public void ConvertFrom<TObject>(List<TObject?> obj)
        {
            obj ??= new List<TObject?>();
            Columns.Clear();
            Rows.Clear();
            foreach (System.Reflection.PropertyInfo Property in typeof(TObject).GetProperties())
            {
                Columns.Add(Property.Name);
            }
            foreach (TObject? Item in obj)
            {
                var Row = new TableRow(Columns);
                Row.ConvertFrom(Item);
                Rows.Add(Row);
            }
        }

        /// <summary>
        /// Converts this instance into the object array of the type specified.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting array.</returns>
        public List<TObject?> ConvertTo<TObject>() => Rows.ConvertAll(x => x.ConvertTo<TObject?>());

        /// <summary>
        /// Copies the rows of the table to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The array to copy the rows to.</param>
        /// <param name="arrayIndex">The index in the array at which to start copying the rows.</param>
        public void CopyTo(TableRow[]? array, int arrayIndex)
        {
            if (array is null || array.Length == 0)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            var Count = Math.Min(array.Length - arrayIndex, Rows.Count);
            Rows.CopyTo(0, array, arrayIndex, Count);
        }

        /// <summary>
        /// Determines whether the current table object is equal to another table object.
        /// </summary>
        /// <param name="obj">The table object to compare with this table object.</param>
        /// <returns>
        /// <c>true</c> if the current table object is equal to the other table object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj) => obj is Table Table && Equals(Table);

        /// <summary>
        /// Determines whether the current table object is equal to another table object.
        /// </summary>
        /// <param name="other">The table object to compare with this table object.</param>
        /// <returns>
        /// <c>true</c> if the current table object is equal to the other table object; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Table? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other is null)
                return false;
            return GetContent() == other.GetContent();
        }

        /// <summary>
        /// Gets the content of the table.
        /// </summary>
        /// <returns>The content of the table.</returns>
        public override string? GetContent()
        {
            return new StringBuilder()
                    .AppendJoin(' ', Columns)
                    .Append('\n')
                    .AppendJoin('\n', Rows)
                    .ToString();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the rows of the table.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the rows of the table.</returns>
        public IEnumerator<TableRow> GetEnumerator() => Rows.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the rows of the table.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the rows of the table.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the hash code for the current table object.
        /// </summary>
        /// <returns>A hash code for the current table object.</returns>
        public override int GetHashCode()
        {
            var HashCode = new HashCode();
            foreach (var Column in Columns)
            {
                HashCode.Add(Column);
            }
            foreach (TableRow Row in Rows)
            {
                HashCode.Add(Row);
            }
            return HashCode.ToHashCode();
        }

        /// <summary>
        /// Returns the index of the specified row in the table.
        /// </summary>
        /// <param name="item">The row to find in the table.</param>
        /// <returns>The index of the row in the table.</returns>
        public int IndexOf(TableRow? item) => Rows.IndexOf(item!);

        /// <summary>
        /// Inserts a row into the table at the specified index.
        /// </summary>
        /// <param name="index">The index at which to insert the row.</param>
        /// <param name="item">The row to insert into the table.</param>
        public void Insert(int index, TableRow? item)
        {
            if (index < 0)
                index = 0;
            if (index > Count)
                index = Count;
            item ??= new TableRow(Columns);
            Rows.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific row from the table.
        /// </summary>
        /// <param name="item">The row to remove from the table.</param>
        /// <returns><c>true</c> if the row was successfully removed; otherwise, <c>false&gt;</c>.</returns>
        public bool Remove(TableRow? item) => item is not null && Rows.Remove(item);

        /// <summary>
        /// Removes the row at the specified index.
        /// </summary>
        /// <param name="index">The index of the row to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;
            Rows.RemoveAt(index);
        }

        /// <summary>
        /// Converts the table to the specified file type.
        /// </summary>
        /// <typeparam name="TFile">The type of the file.</typeparam>
        /// <returns>The file of the specified type.</returns>
        public override TFile? ToFileType<TFile>() where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue;
            if (FileType == typeof(Card))
                ReturnValue = (Card?)this;
            else if (FileType == typeof(Calendar))
                ReturnValue = (Calendar?)this;
            else if (FileType == typeof(Table))
                ReturnValue = this;
            else if (FileType == typeof(Tables))
                ReturnValue = (Tables?)this;
            else if (FileType == typeof(Text))
                ReturnValue = (Text?)this;
            else if (FileType == typeof(StructuredObject))
                ReturnValue = (StructuredObject?)this;
            else
                ReturnValue = (IGenericFile?)Converter?.Convert(this, FileType);

            return (TFile?)ReturnValue;
        }
    }
}