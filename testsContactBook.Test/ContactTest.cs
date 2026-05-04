using Xunit;
using ContactBook;

namespace ContactBook.Tests
{
    public class ContactTests
    {
        // ---------- Constructor Tests ----------

        [Fact]
        public void Constructor_DefaultValues_ShouldInitializeEmptyStrings()
        {
            var contact = new Contact();

            Assert.Equal("", contact.GetFname());
            Assert.Equal("", contact.GetLname());
            Assert.Equal("", contact.GetPhone());
            Assert.Equal("", contact.GetEmail());
        }

        [Fact]
        public void Constructor_WithValues_ShouldSetPropertiesCorrectly()
        {
            var contact = new Contact("John", "Doe", "123456", "john@test.com");

            Assert.Equal("John", contact.GetFname());
            Assert.Equal("Doe", contact.GetLname());
            Assert.Equal("123456", contact.GetPhone());
            Assert.Equal("john@test.com", contact.GetEmail());
        }

        // ---------- Getter/Setter Tests ----------

        [Fact]
        public void SetFname_ShouldUpdateValue()
        {
            var contact = new Contact();
            contact.SetFname("Alice");

            Assert.Equal("Alice", contact.GetFname());
        }

        [Fact]
        public void SetLname_ShouldUpdateValue()
        {
            var contact = new Contact();
            contact.SetLname("Smith");

            Assert.Equal("Smith", contact.GetLname());
        }

        [Fact]
        public void SetPhone_ShouldUpdateValue()
        {
            var contact = new Contact();
            contact.SetPhone("999");

            Assert.Equal("999", contact.GetPhone());
        }

        [Fact]
        public void SetEmail_ShouldUpdateValue()
        {
            var contact = new Contact();
            contact.SetEmail("a@test.com");

            Assert.Equal("a@test.com", contact.GetEmail());
        }

        // ---------- ToString Tests ----------

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            var contact = new Contact("John", "Doe", "123", "john@test.com");

            var result = contact.ToString();

            Assert.Equal("Contact[fname=John, lname=Doe, phone=123, email=john@test.com]", result);
        }

        // ---------- Equality Tests ----------

        [Fact]
        public void Equals_SameValues_ShouldReturnTrue()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.True(c1.Equals(c2));
        }

        [Fact]
        public void Equals_DifferentValues_ShouldReturnFalse()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("Jane", "Doe", "123", "a@test.com");

            Assert.False(c1.Equals(c2));
        }

        [Fact]
        public void Equals_Null_ShouldReturnFalse()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.False(c1.Equals(null));
        }

        [Fact]
        public void Equals_SameReference_ShouldReturnTrue()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.True(c1.Equals(c1));
        }

        // ---------- Object Equals ----------

        [Fact]
        public void Equals_ObjectVersion_ShouldWorkCorrectly()
        {
            object c1 = new Contact("John", "Doe", "123", "a@test.com");
            object c2 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.True(c1.Equals(c2));
        }

        // ---------- Operator Tests ----------

        [Fact]
        public void OperatorEqual_SameValues_ShouldReturnTrue()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.True(c1 == c2);
        }

        [Fact]
        public void OperatorNotEqual_DifferentValues_ShouldReturnTrue()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("Jane", "Doe", "123", "a@test.com");

            Assert.True(c1 != c2);
        }

        [Fact]
        public void OperatorEqual_BothNull_ShouldReturnTrue()
        {
            Contact? c1 = null;
            Contact? c2 = null;

            Assert.True(c1 == c2);
        }

        [Fact]
        public void OperatorEqual_OneNull_ShouldReturnFalse()
        {
            Contact? c1 = new Contact();
            Contact? c2 = null;

            Assert.False(c1 == c2);
        }

        // ---------- HashCode Tests ----------

        [Fact]
        public void GetHashCode_SameValues_ShouldBeEqual()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("John", "Doe", "123", "a@test.com");

            Assert.Equal(c1.GetHashCode(), c2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_DifferentValues_ShouldNotBeEqual()
        {
            var c1 = new Contact("John", "Doe", "123", "a@test.com");
            var c2 = new Contact("Jane", "Doe", "123", "a@test.com");

            Assert.NotEqual(c1.GetHashCode(), c2.GetHashCode());
        }

        // ---------- Edge Cases ----------

        [Fact]
        public void Allows_EmptyStrings()
        {
            var contact = new Contact("", "", "", "");

            Assert.Equal("", contact.GetFname());
            Assert.Equal("", contact.GetLname());
            Assert.Equal("", contact.GetPhone());
            Assert.Equal("", contact.GetEmail());
        }

        [Fact]
        public void Setters_ShouldAcceptEmptyStrings()
        {
            var contact = new Contact("A", "B", "C", "D");

            contact.SetFname("");
            contact.SetLname("");
            contact.SetPhone("");
            contact.SetEmail("");

            Assert.Equal("", contact.GetFname());
            Assert.Equal("", contact.GetLname());
            Assert.Equal("", contact.GetPhone());
            Assert.Equal("", contact.GetEmail());
        }
    }
}