﻿using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.Interfaces;
using System.Collections.Generic;

namespace Archivist.BaseClasses
{
    /// <summary>
    /// Base class for file types in the Archivist library.
    /// </summary>
    /// <typeparam name="TFileType">The specific file type derived from this base class.</typeparam>
    /// <remarks>Initializes a new instance of the FileBaseClass class.</remarks>
    /// <param name="converter">The type converter.</param>
    public abstract class FileBaseClass<TFileType>(Convertinator? converter) : IGenericFile
        where TFileType : FileBaseClass<TFileType>
    {
        /// <summary>
        /// Gets or sets the metadata associated with the file.
        /// </summary>
        public Dictionary<string, string> Metadata { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets or sets the title of the file.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets the type converter.
        /// </summary>
        protected Convertinator? Converter { get; } = converter ?? Convertinator.Instance;

        /// <summary>
        /// Determines if the two file objects are not equal.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>True if the file objects are not equal, false otherwise.</returns>
        public static bool operator !=(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Determines if the first file object is less than the second file object.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>
        /// True if the first file object is less than the second file object, false otherwise.
        /// </returns>
        public static bool operator <(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            if (value1 is null)
                return value2 is not null;
            return value1.CompareTo(value2) < 0;
        }

        /// <summary>
        /// Determines if the first file object is less than or equal to the second file object.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>
        /// True if the first file object is less than or equal to the second file object, false otherwise.
        /// </returns>
        public static bool operator <=(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            return (value1?.CompareTo(value2) ?? 0) <= 0;
        }

        /// <summary>
        /// Determines if the two file objects are equal.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>True if the file objects are equal, false otherwise.</returns>
        public static bool operator ==(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            return value1?.Equals(value2) ?? value2 is null;
        }

        /// <summary>
        /// Determines if the first file object is greater than the second file object.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>
        /// True if the first file object is greater than the second file object, false otherwise.
        /// </returns>
        public static bool operator >(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            return value1 is not null
                && (value2 is null
                    || value1.CompareTo(value2) > 0);
        }

        /// <summary>
        /// Determines if the first file object is greater than or equal to the second file object.
        /// </summary>
        /// <param name="value1">The first file object.</param>
        /// <param name="value2">The second file object.</param>
        /// <returns>
        /// True if the first file object is greater than or equal to the second file object, false otherwise.
        /// </returns>
        public static bool operator >=(FileBaseClass<TFileType>? value1, FileBaseClass<TFileType>? value2)
        {
            return (value1 is null) ? value2 is null : value1.CompareTo(value2) >= 0;
        }

        /// <summary>
        /// Compares the file object to another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>0 if the objects are equal, -1 if this is smaller, 1 if it is larger.</returns>
        public int CompareTo(object? obj) => obj is TFileType FormatFileType ? CompareTo(FormatFileType) : -1;

        /// <summary>
        /// Compares the file object to another file object.
        /// </summary>
        /// <param name="other">The file object to compare to.</param>
        /// <returns>0 if the file objects are equal, -1 if this is smaller, 1 if it is larger.</returns>
        public abstract int CompareTo(TFileType? other);

        /// <summary>
        /// Determines if the file object is equal to another file object.
        /// </summary>
        /// <param name="other">The file object to compare to.</param>
        /// <returns>True if the file objects are equal, false otherwise.</returns>
        public abstract bool Equals(TFileType? other);

        /// <summary>
        /// Determines if the file object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the file objects are equal, false otherwise.</returns>
        public override bool Equals(object? obj) => (obj is TFileType TempItem) && Equals(TempItem);

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <returns>The content of the file.</returns>
        public abstract string? GetContent();

        /// <summary>
        /// Gets the hash code for the file object.
        /// </summary>
        /// <returns>The hash code for the file object.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode() ?? 0;

        /// <summary>
        /// Converts the file object to a specific file type.
        /// </summary>
        /// <typeparam name="TFile">The file type to convert to.</typeparam>
        /// <returns>The converted file object.</returns>
        public virtual TFile? ToFileType<TFile>()
            where TFile : IGenericFile
        {
            System.Type FileType = typeof(TFile);
            if (GetType() == FileType)
                return (TFile)(IGenericFile)this;
            if (FileType == typeof(Text))
                return (TFile)(IGenericFile)new Text(GetContent(), Title);
            return (TFile?)Converter?.Convert(this, typeof(TFile));
        }

        /// <summary>
        /// Converts the file object to a string.
        /// </summary>
        /// <returns>The content of the file object.</returns>
        public override string ToString() => GetContent() ?? "";
    }
}