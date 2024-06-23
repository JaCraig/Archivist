using Archivist.Interfaces;
using ObjectCartographer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a table row.
    /// </summary>
    public class TableRow : IComparable<TableRow>, IEquatable<TableRow>, IObjectConvertable, IList<TableCell>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableRow"/> class with the specified columns.
        /// </summary>
        /// <param name="columns">The list of column names.</param>
        public TableRow(List<string>? columns)
        {
            Columns = columns ?? new List<string>();
        }

        /// <summary>
        /// Gets the number of cells in the row.
        /// </summary>
        public int Count => Cells.Count;

        /// <summary>
        /// Gets a value indicating whether the row is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the list of cells in the row.
        /// </summary>
        private List<TableCell> Cells { get; } = new List<TableCell>();

        /// <summary>
        /// Gets the list of column names.
        /// </summary>
        private List<string> Columns { get; }

        /// <summary>
        /// Gets or sets the cell at the specified index.
        /// </summary>
        /// <param name="index">The index of the cell.</param>
        /// <returns>The cell at the specified index.</returns>
        public TableCell this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return Cells[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Cells[index] = value;
            }
        }

        /// <summary>
        /// Gets or sets the cell with the specified column name.
        /// </summary>
        /// <param name="column">The name of the column.</param>
        /// <returns>The cell with the specified column name.</returns>
        public TableCell this[string column]
        {
            get
            {
                var ColumnIndex = Columns.IndexOf(column);
                if (ColumnIndex == -1)
                    throw new ArgumentOutOfRangeException(nameof(column));
                return Cells[ColumnIndex];
            }
            set
            {
                var ColumnIndex = Columns.IndexOf(column);
                if (ColumnIndex == -1)
                    throw new ArgumentOutOfRangeException(nameof(column));
                Cells[ColumnIndex] = value;
            }
        }

        /// <summary>
        /// Determines whether two <see cref="TableRow"/> objects are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="TableRow"/> objects are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(TableRow? left, TableRow? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the first <see cref="TableRow"/> is less than the second <see cref="TableRow"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableRow"/> is less than the second <see
        /// cref="TableRow"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(TableRow? left, TableRow? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="TableRow"/> is less than or equal to the second
        /// <see cref="TableRow"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableRow"/> is less than or equal to the second <see
        /// cref="TableRow"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(TableRow? left, TableRow? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two <see cref="TableRow"/> objects are equal.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="TableRow"/> objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(TableRow? left, TableRow? right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether the first <see cref="TableRow"/> is greater than the second <see cref="TableRow"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableRow"/> is greater than the second <see
        /// cref="TableRow"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(TableRow? left, TableRow? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether the first <see cref="TableRow"/> is greater than or equal to the
        /// second <see cref="TableRow"/>.
        /// </summary>
        /// <param name="left">The first <see cref="TableRow"/> to compare.</param>
        /// <param name="right">The second <see cref="TableRow"/> to compare.</param>
        /// <returns>
        /// <c>true</c> if the first <see cref="TableRow"/> is greater than or equal to the second
        /// <see cref="TableRow"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >=(TableRow? left, TableRow? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Adds a cell to the row.
        /// </summary>
        /// <param name="item">The cell to add.</param>
        public void Add(TableCell? item) => Cells.Add(item ?? new TableCell(""));

        /// <summary>
        /// Adds a cell to the row with the specified content.
        /// </summary>
        /// <param name="item">The content of the cell to add.</param>
        public void Add(string? item) => Add(new TableCell(item));

        /// <summary>
        /// Adds a list of cells to the row.
        /// </summary>
        /// <param name="collection">The list of cells to add.</param>
        public void AddRange(IEnumerable<TableCell> collection) => Cells.AddRange(collection ?? Array.Empty<TableCell>());

        /// <summary>
        /// Adds a list of cells to the row with the specified content.
        /// </summary>
        /// <param name="collection">The list of content to add.</param>
        public void AddRange(IEnumerable<string> collection)
        {
            if (collection is null)
                return;
            foreach (var Item in collection)
                Add(Item);
        }

        /// <summary>
        /// Removes all cells from the row.
        /// </summary>
        public void Clear() => Cells.Clear();

        /// <summary>
        /// Compares the current <see cref="TableRow"/> with another <see cref="TableRow"/> and
        /// returns an integer that indicates whether the current <see cref="TableRow"/> precedes,
        /// follows, or occurs in the same position in the sort order as the other <see cref="TableRow"/>.
        /// </summary>
        /// <param name="other">The <see cref="TableRow"/> to compare with the current <see cref="TableRow"/>.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(TableRow? other)
        {
            if (other is null)
                return 1;
            if (other.Count != Count)
                return Count.CompareTo(other.Count);
            for (var X = 0; X < Count; ++X)
            {
                var TempValue = Cells[X].CompareTo(other.Cells[X]);
                if (TempValue != 0)
                    return TempValue;
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the row contains a specific cell.
        /// </summary>
        /// <param name="item">The cell to locate in the row.</param>
        /// <returns><c>true</c> if the cell is found in the row; otherwise, <c>false</c>.</returns>
        public bool Contains(TableCell? item) => item is not null && Cells.Contains(item);

        /// <summary>
        /// Copies the object to the row.
        /// </summary>
        /// <typeparam name="TObject">The object type</typeparam>
        /// <param name="obj">The object to copy.</param>
        public void ConvertFrom<TObject>(TObject obj)
        {
            Cells.Clear();
            if (obj is null)
                return;
            foreach (System.Reflection.PropertyInfo Property in obj.GetType().GetProperties())
            {
                var ColumnIndex = Columns.IndexOf(Property.Name);
                if (ColumnIndex == -1)
                {
                    Columns.Add(Property.Name);
                    _ = Columns.Count - 1;
                }
                Cells.Add(new TableCell(Property.GetValue(obj)?.ToString() ?? ""));
            }
        }

        /// <summary>
        /// Converts the current <see cref="TableRow"/> to an object array of the specified type.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting array.</returns>
        public TObject ConvertTo<TObject>()
        {
            IDictionary<string, object?> TempValue = new ExpandoObject();
            for (var Y = 0; Y < Cells.Count; ++Y)
            {
                var ColumnName = Columns.Count > Y ? Columns[Y] : Y.ToString();
                TempValue[ColumnName] = Cells[Y].Content;
            }
            return TempValue.To<TObject>();
        }

        /// <summary>
        /// Copies the elements of the row to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the row.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying begins.
        /// </param>
        public void CopyTo(TableCell[]? array, int arrayIndex)
        {
            if (array is null || array.Length == 0)
                return;
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            Cells.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Determines whether the current <see cref="TableRow"/> is equal to another <see cref="TableRow"/>.
        /// </summary>
        /// <param name="other">The <see cref="TableRow"/> to compare with the current <see cref="TableRow"/>.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="TableRow"/> objects are equal; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(TableRow? other) => other is not null && CompareTo(other) == 0;

        /// <summary>
        /// Determines whether the current <see cref="TableRow"/> is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current <see cref="TableRow"/>.</param>
        /// <returns>
        /// <c>true</c> if the object is a <see cref="TableRow"/> and is equal to the current <see
        /// cref="TableRow"/>; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            return obj is TableRow TableRowObject && Equals(TableRowObject);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the cells in the row.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the cells in the row.</returns>
        public IEnumerator GetEnumerator() => Cells.GetEnumerator();

        /// <summary>
        /// Returns the enumerator that iterates through the cells in the row.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the cells in the row.</returns>
        IEnumerator<TableCell> IEnumerable<TableCell>.GetEnumerator() => Cells.GetEnumerator();

        /// <summary>
        /// Returns the hash code for the row.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            var HashCode = new HashCode();
            foreach (TableCell Cell in Cells)
                HashCode.Add(Cell);
            return HashCode.ToHashCode();
        }

        /// <summary>
        /// Searches for the specified cell and returns the zero-based index of the first occurrence
        /// within the entire row.
        /// </summary>
        /// <param name="item">The cell to locate in the row.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of the cell within the entire row, if
        /// found; otherwise, -1.
        /// </returns>
        public int IndexOf(TableCell? item) => Cells.IndexOf(item ?? TableCell.Empty);

        /// <summary>
        /// Inserts a cell into the row at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the cell should be inserted.</param>
        /// <param name="item">The cell to insert into the row.</param>
        public void Insert(int index, TableCell? item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            Cells.Insert(index, item ?? new TableCell(""));
        }

        /// <summary>
        /// Removes the first occurrence of a specific cell from the row.
        /// </summary>
        /// <param name="item">The cell to remove from the row.</param>
        /// <returns><c>true</c> if the cell is successfully removed; otherwise, <c>false</c>.</returns>
        public bool Remove(TableCell? item) => item is not null && Cells.Remove(item);

        /// <summary>
        /// Removes the cell at the specified index from the row.
        /// </summary>
        /// <param name="index">The zero-based index of the cell to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                return;
            Cells.RemoveAt(index);
        }

        /// <summary>
        /// Returns a string that represents the row.
        /// </summary>
        /// <returns>A string that represents the row.</returns>
        public override string ToString() => string.Join(' ', Cells);
    }
}