using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archivist.DataTypes
{
    /// <summary>
    /// Image data type.
    /// </summary>
    /// <seealso cref="FileBaseClass{Image}"/>
    /// <seealso cref="IComparable{Image}"/>
    /// <seealso cref="IEquatable{Image}"/>
    public class Image : FileBaseClass<Image>, IComparable<Image>, IEquatable<Image>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class with the specified converter.
        /// </summary>
        /// <param name="converter">The converter to use.</param>
        public Image(Convertinator? converter)
            : base(converter)
        {
        }

        /// <summary>
        /// Gets or sets the data of the image.
        /// </summary>
        public byte[] Data { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// The description of the image (if using OCR/LLM processor, that information will be
        /// stored here).
        /// </summary>
        public string Description
        {
            get => Metadata?.GetValueOrDefault("Description") ?? "";
            set
            {
                if (Metadata is null)
                    return;
                Metadata["Description"] = value ?? "";
            }
        }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the type of the image.
        /// </summary>
        public string ImageType
        {
            get => Metadata?.GetValueOrDefault("ImageType") ?? "";
            set
            {
                if (Metadata is null)
                    return;
                Metadata["ImageType"] = value ?? "";
            }
        }

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Converts the Image to a Calendar.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        public static implicit operator Calendar?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(Calendar)) as Calendar;
        }

        /// <summary>
        /// Converts the Image to a Card.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The Card representation of the Image.</returns>
        public static implicit operator Card?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(Card)) as Card;
        }

        /// <summary>
        /// Converts the Image to a Feed.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The Feed representation of the Image.</returns>
        public static implicit operator Feed?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(Feed)) as Feed;
        }

        /// <summary>
        /// Converts the Image to a structured object.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The structured object representation of the Image.</returns>
        public static implicit operator StructuredObject?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(StructuredObject)) as StructuredObject;
        }

        /// <summary>
        /// Converts the Image to a table.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The table representation of the Image.</returns>
        public static implicit operator Table?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(Table)) as Table;
        }

        /// <summary>
        /// Converts the Image to a Tables file.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The Tables representation of the Image.</returns>
        public static implicit operator Tables?(Image? file)
        {
            return ImageToAnythingConverter.Convert(file, typeof(Tables)) as Tables;
        }

        /// <summary>
        /// Converts the Image to text.
        /// </summary>
        /// <param name="file">The Image to convert.</param>
        /// <returns>The text representation of the Image.</returns>
        public static implicit operator Text?(Image? file)
        {
            if (file is null)
                return null;
            Text? ReturnValue = AnythingToTextConverter.Convert(file);
            if (ReturnValue is not null)
                ReturnValue.Title ??= file.Title;
            return ReturnValue;
        }

        /// <summary>
        /// Determines whether two Image objects are not equal.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>True if the Image objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Image? left, Image? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether one Image object is less than another Image object.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>True if the first Image is less than the second Image; otherwise, false.</returns>
        public static bool operator <(Image? left, Image? right)
        {
            return left is null ? right is not null : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Determines whether one Image object is less than or equal to another Image object.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>
        /// True if the first Image is less than or equal to the second Image; otherwise, false.
        /// </returns>
        public static bool operator <=(Image? left, Image? right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Determines whether two Image objects are equal.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>True if the Image objects are equal; otherwise, false.</returns>
        public static bool operator ==(Image? left, Image? right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether one Image object is greater than another Image object.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>True if the first Image is greater than the second Image; otherwise, false.</returns>
        public static bool operator >(Image? left, Image? right)
        {
            return left?.CompareTo(right) > 0;
        }

        /// <summary>
        /// Determines whether one Image object is greater than or equal to another Image object.
        /// </summary>
        /// <param name="left">The first Image to compare.</param>
        /// <param name="right">The second Image to compare.</param>
        /// <returns>
        /// True if the first Image is greater than or equal to the second Image; otherwise, false.
        /// </returns>
        public static bool operator >=(Image? left, Image? right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the Image to another Image based on their content.
        /// </summary>
        /// <param name="other">The other Image to compare.</param>
        /// <returns>An integer that indicates the relative order of the Images.</returns>
        public override int CompareTo(Image? other)
        {
            if (other is null || other.Data is null)
                return 1;
            if (Data is null)
                return -1;
            if (Width != other.Width)
                return Width.CompareTo(other.Width);
            if (Height != other.Height)
                return Height.CompareTo(other.Height);
            if (Data.Length != other.Data.Length)
                return Data.Length.CompareTo(other.Data.Length);
            for (var I = 0; I < Data.Length; I++)
            {
                if (Data[I] != other.Data[I])
                    return Data[I].CompareTo(other.Data[I]);
            }
            return 0;
        }

        /// <summary>
        /// Determines whether the Image is equal to another Image based on their content.
        /// </summary>
        /// <param name="other">The other Image to compare.</param>
        /// <returns>True if the Images are equal; otherwise, false.</returns>
        public override bool Equals(Image? other)
        {
            if (other is null)
                return false;
            if (Width != other.Width)
                return false;
            if (Height != other.Height)
                return false;
            if (Data.Length != other.Data.Length)
                return false;
            return Data.SequenceEqual(other.Data);
        }

        /// <summary>
        /// Determines whether the Image is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the Image is equal to the object; otherwise, false.</returns>
        public override bool Equals(object? obj) => ReferenceEquals(this, obj) || (obj is Image ImageObject && Equals(ImageObject));

        /// <summary>
        /// Gets the content of the Image.
        /// </summary>
        /// <returns>The content of the Image.</returns>
        public override string? GetContent()
        {
            if (!string.IsNullOrEmpty(Description))
                return Description;
            return $"{Title ?? "Image"}: {ImageType ?? "Unknown Type"}({Width}x{Height})";
        }

        /// <summary>
        /// Gets the hash code of the Image based on its content.
        /// </summary>
        /// <returns>The hash code of the Image.</returns>
        public override int GetHashCode() => GetContent()?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

        /// <summary>
        /// Converts the Image to the specified object type.
        /// </summary>
        /// <typeparam name="TFile">The type to convert the Image to.</typeparam>
        /// <returns>The converted Image.</returns>
        public override TFile? ToFileType<TFile>()
            where TFile : default
        {
            Type FileType = typeof(TFile);
            IGenericFile? ReturnValue;
            if (FileType == typeof(Image))
                ReturnValue = (IGenericFile?)this;
            else if (FileType == typeof(Feed))
                ReturnValue = (Feed?)this;
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