using Archivist.DataTypes;
using Archivist.Tests.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Archivist.Tests.DataTypes
{
    public class CardTests : TestBaseClass<Card>
    {
        public CardTests()
        {
            TestObject = new Card();
        }

        private static readonly Card _TestClass = new();

        [Fact]
        public void CanCallCompareTo()
        {
            // Arrange
            var Card = new Card();
            var Other = new Card();

            // Act
            var Result = Card.CompareTo(Other);

            // Assert
            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanCallConvertFrom()
        {
            // Arrange
            var Card = new Card();
            var TestValue = new TestClass { FN = "John Doe" };

            // Act
            Card.ConvertFrom(TestValue);

            // Assert
            Assert.Equal("John Doe", Card.FullName?.Value);
        }

        [Fact]
        public void CanCallConvertTo()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            // Act
            TestClass? Result = Card.ConvertTo<TestClass>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("John Doe", Result.FN);
        }

        [Fact]
        public void CanCallEqualityOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left == Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullLeft()
        {
            // Act
            var Result = default == new Card();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualityOperatorWithNullRight()
        {
            // Act
            var Result = new Card() == default;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallEqualsWithCard()
        {
            // Arrange
            var Card = new Card();
            var Other = new Card();

            // Act
            var Result = Card.Equals(Other);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallEqualsWithObj()
        {
            // Arrange
            _ = new Card();
            var Obj = new object();

            // Act
            var Result = _TestClass.Equals(Obj);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorForIEnumerableWithNoParameters()
        {
            // Act
            IEnumerator Result = ((IEnumerable)_TestClass).GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetEnumeratorWithNoParameters()
        {
            // Act
            IEnumerator<KeyValueField?> Result = _TestClass.GetEnumerator();

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanCallGetHashCode()
        {
            // Act
            var Result = _TestClass.GetHashCode();

            // Assert
            _ = Assert.IsType<int>(Result);
            Assert.NotEqual(0, Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left >= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Card?) >= new Card();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Card() >= default(Card?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left > Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Card?) > new Card();

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallGreaterThanOperatorWithNullRight()
        {
            // Act
            var Result = new Card() > default(Card?);

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left != Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullLeft()
        {
            // Act
            var Result = default != new Card();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallInequalityOperatorWithNullRight()
        {
            // Act
            var Result = new Card() != default;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left <= Right;

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullLeft()
        {
            // Act
            var Result = default(Card?) <= new Card();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanEqualToOperatorWithNullRight()
        {
            // Act
            var Result = new Card() <= default(Card?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperator()
        {
            // Arrange
            var Left = new Card();
            var Right = new Card();

            // Act
            var Result = Left < Right;

            // Assert
            Assert.False(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullLeft()
        {
            // Act
            var Result = default(Card?) < new Card();

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallLessThanOperatorWithNullRight()
        {
            // Act
            var Result = new Card() < default(Card?);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallToFileType()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            //Act
            Table? Result = Card.ToFileType<Table>();

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("John Doe", Result.Title);
            _ = Assert.Single(Result.Columns);
            Assert.Equal("FN", Result.Columns[0]);
            _ = Assert.Single(Result);
            Assert.Equal("John Doe", Result[0][0].Content);
        }

        [Fact]
        public void CanConvertToTable()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            //Act
            var Result = (Table?)Card;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("John Doe", Result.Title);
            _ = Assert.Single(Result.Columns);
            Assert.Equal("FN", Result.Columns[0]);
            _ = Assert.Single(Result);
            Assert.Equal("John Doe", Result[0][0].Content);
        }

        [Fact]
        public void CanConvertToText()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            //Act
            var Result = (Text?)Card;

            // Assert
            Assert.NotNull(Result);
            Assert.Equal("John Doe", Result.Title);
            Assert.Equal("FN (): John Doe", Result.Content);
        }

        [Fact]
        public void CanGetAddresses()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Addresses;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetAnniversaries()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Anniversaries;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetBirthdays()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Birthdays;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetCount()
        {
            // Arrange
            var Card = new Card();
            // Assert
            var Result = Assert.IsType<int>(Card.Count);

            Assert.Equal(0, Result);
        }

        [Fact]
        public void CanGetEmails()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Emails;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetFields()
        {
            // Assert
            List<KeyValueField> Result = Assert.IsType<List<KeyValueField>>(_TestClass.Fields);

            Assert.NotNull(Result);
            Assert.Empty(Result);
        }

        [Fact]
        public void CanGetInstantMessengers()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.InstantMessengers;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetLanguages()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Languages;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetLogos()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Logos;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetNicknames()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Nicknames;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetNotes()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Notes;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetOrganizations()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Organizations;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetPhoneNumbers()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.PhoneNumbers;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetPhotos()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Photos;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetRoles()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Roles;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetSounds()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Sounds;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetTimeZones()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.TimeZones;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetTitles()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Titles;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanGetWebsites()
        {
            // Assert
            IEnumerable<KeyValueField?> Results = _TestClass.Websites;

            // Assert
            Assert.NotNull(Results);
        }

        [Fact]
        public void CanSetAndGetFirstName()
        {
            // Arrange
            const string TestValue = "TestValue708085278";

            // Act
            _TestClass.FirstName = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.FirstName);
        }

        [Fact]
        public void CanSetAndGetIndexerForInt()
        {
            var TestValue = new KeyValueField("TestValue747048514", new[] { new KeyValueParameter("TestValue1416617271", "TestValue1522366486"), new KeyValueParameter("TestValue19427011", "TestValue1087865086"), new KeyValueParameter("TestValue396608921", "TestValue558580127") }, "TestValue277639823");
            _ = Assert.IsType<KeyValueField>(_TestClass[0]);
            _TestClass[0] = TestValue;
            Assert.Same(TestValue, _TestClass[0]);
        }

        [Fact]
        public void CanSetAndGetLastName()
        {
            // Arrange
            const string TestValue = "TestValue492377529";

            // Act
            _TestClass.LastName = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.LastName);
        }

        [Fact]
        public void CanSetAndGetMiddleName()
        {
            // Arrange
            const string TestValue = "TestValue1355456103";

            // Act
            _TestClass.MiddleName = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.MiddleName);
        }

        [Fact]
        public void CanSetAndGetPrefix()
        {
            // Arrange
            const string TestValue = "TestValue955570229";

            // Act
            _TestClass.Prefix = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Prefix);
        }

        [Fact]
        public void CanSetAndGetSuffix()
        {
            // Arrange
            const string TestValue = "TestValue1086535377";

            // Act
            _TestClass.Suffix = TestValue;

            // Assert
            Assert.Equal(TestValue, _TestClass.Suffix);
        }

        [Fact]
        public void ImplementsIComparable_Card()
        {
            // Arrange
            var BaseValue = new Card();
            var EqualToBaseValue = new Card();
            var GreaterThanBaseValue = new Card();
            GreaterThanBaseValue.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            // Assert
            Assert.Equal(0, BaseValue.CompareTo(EqualToBaseValue));
            Assert.True(BaseValue.CompareTo(GreaterThanBaseValue) > 0);
            Assert.True(GreaterThanBaseValue.CompareTo(BaseValue) < 0);
        }

        [Fact]
        public void ImplementsIEnumerable_CardField()
        {
            // Arrange
            var Enumerable = new Card();
            Enumerable.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Enumerable.Fields.Add(new KeyValueField("EMAIL", null, "test@test.com"));
            Enumerable.Fields.Add(new KeyValueField("TEL", null, "1234567890"));
            Enumerable.Fields.Add(new KeyValueField("ADR", null, "123 Main St."));
            const int ExpectedCount = 4;
            var ActualCount = 0;

            // Act
            using (IEnumerator<KeyValueField?> Enumerator = Enumerable.GetEnumerator())
            {
                Assert.NotNull(Enumerator);
                while (Enumerator.MoveNext())
                {
                    ActualCount++;
                    _ = Assert.IsType<KeyValueField>(Enumerator.Current);
                }
            }

            // Assert
            Assert.Equal(ExpectedCount, ActualCount);
        }

        [Fact]
        public void ImplementsIEquatable_Card()
        {
            // Arrange
            var Card = new Card();
            var Same = new Card();
            var Different = new Card();
            Different.Fields.Add(new KeyValueField("FN", null, "John Doe"));

            // Assert
            Assert.False(Card.Equals(default(object)));
            Assert.False(Card.Equals(new object()));
            Assert.True(Card.Equals((object)Same));
            Assert.False(Card.Equals((object)Different));
            Assert.True(Card.Equals(Same));
            Assert.False(Card.Equals(Different));
            Assert.Equal(Same.GetHashCode(), Card.GetHashCode());
            Assert.NotEqual(Different.GetHashCode(), Card.GetHashCode());
            Assert.True(Card == Same);
            Assert.False(Card == Different);
            Assert.False(Card != Same);
            Assert.True(Card != Different);
        }

        [Fact]
        public void TestComparison()
        {
            // Arrange
            var Card1 = new Card();
            Card1.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card1.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            var Card2 = new Card();
            Card2.Fields.Add(new KeyValueField("FN", null, "Jane Doe"));
            Card2.Fields.Add(new KeyValueField("EMAIL", null, "janedoe@example.com"));

            // Act
            var IsLessThan = Card1 < Card2;
            var IsLessThanOrEqual = Card1 <= Card2;
            var IsGreaterThan = Card1 > Card2;
            var IsGreaterThanOrEqual = Card1 >= Card2;

            // Assert
            Assert.True(IsLessThan);
            Assert.True(IsLessThanOrEqual);
            Assert.False(IsGreaterThan);
            Assert.False(IsGreaterThanOrEqual);
        }

        [Fact]
        public void TestContent()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            // Act
            var Content = Card.GetContent();

            // Assert
            Assert.Equal("FN (): John Doe" + Environment.NewLine + "EMAIL (): johndoe@example.com", Content);
        }

        [Fact]
        public void TestCount()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            // Act
            var Count = Card.Count;

            // Assert
            Assert.Equal(2, Count);
        }

        [Fact]
        public void TestEquality()
        {
            // Arrange
            var Card1 = new Card();
            Card1.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card1.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            var Card2 = new Card();
            Card2.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card2.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            var Card3 = new Card();
            Card3.Fields.Add(new KeyValueField("FN", null, "Jane Doe"));
            Card3.Fields.Add(new KeyValueField("EMAIL", null, "janedoe@example.com"));

            // Act
            var AreEqual1 = Card1.Equals(Card2);
            var AreEqual2 = Card1 == Card2;
            var AreEqual3 = Card1 != Card3;

            // Assert
            Assert.True(AreEqual1);
            Assert.True(AreEqual2);
            Assert.True(AreEqual3);
        }

        [Fact]
        public void TestFirstName()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("N", null, "Doe;John"));

            // Act
            var FirstName = Card.FirstName;

            // Assert
            Assert.Equal("John", FirstName);
        }

        [Fact]
        public void TestIndexer()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));

            // Act
            KeyValueField? Field = Card[1];

            // Assert
            Assert.Equal("EMAIL", Field?.Property);
            Assert.Equal("johndoe@example.com", Field?.Value);
        }

        [Fact]
        public void TestIndexerWithPropertyName()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));
            Card.Fields.Add(new KeyValueField("TEL", null, "1234567890"));

            // Act
            IEnumerable<KeyValueField?> Fields = Card["EMAIL"];

            // Assert
            KeyValueField? Field = Assert.Single(Fields);
            Assert.Equal("EMAIL", Field?.Property);
        }

        [Fact]
        public void TestIndexerWithPropertyNameAndParameter()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("FN", null, "John Doe"));
            Card.Fields.Add(new KeyValueField("EMAIL", null, "johndoe@example.com"));
            Card.Fields.Add(new KeyValueField("TEL", new List<KeyValueParameter> { new("TYPE", "HOME") }, "1234567890"));

            // Act
            IEnumerable<KeyValueField?> Fields = Card["TEL", "TYPE=HOME"];

            // Assert
            KeyValueField? Field = Assert.Single(Fields);
            Assert.Equal("TEL", Field?.Property);
        }

        [Fact]
        public void TestLastName()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("N", null, "Doe;John"));

            // Act
            var LastName = Card.LastName;

            // Assert
            Assert.Equal("Doe", LastName);
        }

        [Fact]
        public void TestMiddleName()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("N", null, "Doe;John;Smith"));

            // Act
            var MiddleName = Card.MiddleName;

            // Assert
            Assert.Equal("Smith", MiddleName);
        }

        [Fact]
        public void TestPrefix()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("N", null, "Doe;John;Smith;Mr."));

            // Act
            var Prefix = Card.Prefix;

            // Assert
            Assert.Equal("Mr.", Prefix);
        }

        [Fact]
        public void TestSuffix()
        {
            // Arrange
            var Card = new Card();
            Card.Fields.Add(new KeyValueField("N", null, "Doe;John;Smith;Mr.;Jr."));

            // Act
            var Suffix = Card.Suffix;

            // Assert
            Assert.Equal("Jr.", Suffix);
        }

        public class TestClass
        {
            public string? FN { get; set; }
        }
    }
}