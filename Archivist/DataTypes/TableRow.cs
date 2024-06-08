using System;
using System.Collections;
using System.Collections.Generic;

namespace Archivist.DataTypes
{
    /// <summary>
    /// </summary>
    public class TableRow : IEnumerable<TableCell>, IEnumerable, IComparable<TableRow>, IEquatable<TableRow>
    {
        /// <summary>
        /// </summary>
        public List<TableCell> Cells { get; } = new List<TableCell>();

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TableRow left, TableRow right)
        {
            return !(left == right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(TableRow left, TableRow right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(TableRow left, TableRow right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TableRow left, TableRow right)
        {
            if (left is null)
                return right is null;
            return left.Equals(right);
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(TableRow left, TableRow right)
        {
            return left is not null && left.CompareTo(right) > 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(TableRow left, TableRow right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(TableRow? other) => throw new NotImplementedException();

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TableRow? other) => throw new System.NotImplementedException();

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator() => throw new NotImplementedException();

        IEnumerator<TableCell> IEnumerable<TableCell>.GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => throw new NotImplementedException();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Join(' ', Cells);
    }
}