using Archivist.Converters;
using Archivist.DataTypes;
using Archivist.DataTypes.Feeds;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace Archivist.Tests.Converters
{
    public class FeedToStructuredObjectConverterTests : TestBaseClass<FeedToStructuredObjectConverter>
    {
        public FeedToStructuredObjectConverterTests()
        {
            _Converter = new FeedToStructuredObjectConverter();
            TestObject = new FeedToStructuredObjectConverter();
        }

        private readonly FeedToStructuredObjectConverter _Converter;

        [Fact]
        public void CanCallCanConvert()
        {
            // Arrange
            Type Source = typeof(Feed);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidDestination()
        {
            // Arrange
            Type Source = typeof(Feed);
            Type Destination = typeof(string);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallCanConvertWithInvalidSource()
        {
            // Arrange
            Type Source = typeof(string);
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.CanConvert(Source, Destination);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void Convert_ReturnsNotNull_WhenDestinationIsNull()
        {
            // Arrange
            var File = new Feed();

            // Act
            StructuredObject? Result = FeedToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenFileIsNull()
        {
            // Arrange
            Feed? File = null;

            // Act
            StructuredObject? Result = FeedToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsNull_WhenSourceIsNotFeed()
        {
            // Arrange
            var Source = new object();
            Type Destination = typeof(StructuredObject);

            // Act
            var Result = _Converter.Convert(Source, Destination);

            // Assert
            Assert.Null(Result);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectContent()
        {
            // Arrange
            var File = new Feed()
            {
                new Channel()
            };
            File.Title = "Test Title";
            File[0].Items.Add(new FeedItem() { Title = "Test" });
            File[0].Items.Add(new FeedItem() { Title = "Test2" });

            // Act
            StructuredObject? Result = FeedToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Title", Result.Title);
            _ = Assert.Single(Result);
            Assert.True(Result.ContainsKey("Channels"));
            var Channels = (List<ExpandoObject>?)Result["Channels"];
            Assert.NotNull(Channels);
            IDictionary<string, object?> TestChannel = Assert.Single(Channels);
            Assert.True(TestChannel.ContainsKey("Items"));
            var Items = (List<ExpandoObject>?)TestChannel["Items"];
            Assert.NotNull(Items);
            Assert.Equal(2, Items.Count);
            IDictionary<string, object?> TestItem = Items[0];
            Assert.True(TestItem.ContainsKey("Title"));
            Assert.Equal("Test", TestItem["Title"]?.ToString());
            TestItem = Items[1];
            Assert.True(TestItem.ContainsKey("Title"));
            Assert.Equal("Test2", TestItem["Title"]?.ToString());
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectMetadata()
        {
            // Arrange
            var File = new Feed();
            File.Metadata.Add("Key1", "Value1");
            File.Metadata.Add("Key2", "Value2");

            // Act
            StructuredObject? Result = FeedToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal(2, Result.Metadata.Count);
            Assert.True(Result.Metadata.ContainsKey("Key1"));
            Assert.True(Result.Metadata.ContainsKey("Key2"));
            Assert.Equal("Value1", Result.Metadata["Key1"]);
            Assert.Equal("Value2", Result.Metadata["Key2"]);
        }

        [Fact]
        public void Convert_ReturnsStructuredObject_WithCorrectTitle()
        {
            // Arrange
            var File = new Feed
            {
                Title = "Test Title"
            };

            // Act
            StructuredObject? Result = FeedToStructuredObjectConverter.Convert(File);

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("Test Title", Result.Title);
        }
    }
}