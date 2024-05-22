using Archivist.Enums;
using Archivist.Tests.BaseClasses;
using System;
using Xunit;

namespace Archivist.Tests.Enums
{
    public class CommonCardFieldsTests : TestBaseClass
    {
        protected override Type? ObjectType { get; } = typeof(CommonCardFields);

        [Fact]
        public static void CanGetAddress() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Address);

        [Fact]
        public static void CanGetAnniversary() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Anniversary);

        [Fact]
        public static void CanGetBirthday() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Birthday);

        [Fact]
        public static void CanGetBirthplace() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Birthplace);

        [Fact]
        public static void CanGetDeathdate() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Deathdate);

        [Fact]
        public static void CanGetDeathplace() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Deathplace);

        [Fact]
        public static void CanGetEmail() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Email);

        [Fact]
        public static void CanGetExpertise() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Expertise);

        [Fact]
        public static void CanGetFullName() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.FullName);

        [Fact]
        public static void CanGetGender() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Gender);

        [Fact]
        public static void CanGetGeo() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Geo);

        [Fact]
        public static void CanGetHobby() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Hobby);

        [Fact]
        public static void CanGetIMPP() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.IMPP);

        [Fact]
        public static void CanGetInterest() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Interest);

        [Fact]
        public static void CanGetKey() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Key);

        [Fact]
        public static void CanGetLabel() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Label);

        [Fact]
        public static void CanGetLanguage() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Language);

        [Fact]
        public static void CanGetLogo() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Logo);

        [Fact]
        public static void CanGetName() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Name);

        [Fact]
        public static void CanGetNickname() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Nickname);

        [Fact]
        public static void CanGetNote() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Note);

        [Fact]
        public static void CanGetOrganization() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Organization);

        [Fact]
        public static void CanGetPhone() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Phone);

        [Fact]
        public static void CanGetPhoto() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Photo);

        [Fact]
        public static void CanGetRole() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Role);

        [Fact]
        public static void CanGetSound() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Sound);

        [Fact]
        public static void CanGetTimeZone() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.TimeZone);

        [Fact]
        public static void CanGetTitle() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.Title);

        [Fact]
        public static void CanGetURL() =>
            // Assert
            Assert.IsType<string>(CommonCardFields.URL);
    }
}