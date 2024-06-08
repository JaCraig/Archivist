using Archivist.BaseClasses;
using ObjectCartographer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Represents a table in the Archivist system.
    /// </summary>
    /// <seealso cref="FileBaseClass{Table}"/>
    public class Table : FileBaseClass<Table>, IComparable<Table>, IEquatable<Table>, IEnumerable<TableRow?>, IEnumerable
    {
        /// <summary>
        /// Gets the headers of the table.
        /// </summary>
        /// <value>The headers.</value>
        public List<string> Columns { get; } = new List<string>();

        /// <summary>
        /// Gets the rows of the table.
        /// </summary>
        public List<TableRow> Rows { get; } = new List<TableRow>();

        /// <summary>
        /// Determines whether two table objects are not equal.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns><c>true</c> if the two table objects are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Table left, Table right)
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
        public static bool operator <(Table left, Table right)
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
        public static bool operator <=(Table left, Table right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two table objects are equal.
        /// </summary>
        /// <param name="left">The first table object.</param>
        /// <param name="right">The second table object.</param>
        /// <returns><c>true</c> if the two table objects are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Table left, Table right)
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
        public static bool operator >(Table left, Table right)
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
        public static bool operator >=(Table left, Table right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

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
        /// Converts this instance into the object array of the type specified.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <returns>The resulting array.</returns>
        public List<TObject> Convert<TObject>()
        {
            var ReturnValues = new List<TObject>();
            for (var X = 0; X < Rows.Count; ++X)
            {
                IDictionary<string, object?> TempValue = new ExpandoObject();
                for (var Y = 0; Y < Columns.Count; ++Y)
                {
                    TempValue[Columns[Y]] = Rows[X].Cells[Y].Content;
                }
                ReturnValues.Add(TempValue.To<TObject>());
            }
            return ReturnValues;
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
        public IEnumerator<TableRow?> GetEnumerator()
        {
            foreach (TableRow Row in Rows)
            {
                yield return Row;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the rows of the table.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the rows of the table.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Returns the hash code for the current table object.
        /// </summary>
        /// <returns>A hash code for the current table object.</returns>
        public override int GetHashCode() => HashCode.Combine(Columns, Rows);
    }
}