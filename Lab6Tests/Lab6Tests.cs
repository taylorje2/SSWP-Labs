using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab5.Services;
using Lab5.Models;
using System.Reflection;

namespace Lab6Tests
{
    [TestClass]
    public sealed class Lab6Tests
    {
        //////////////////////////////////////////////
        // Test methods for reading books and users
        /////////////////////////////////////////////
        /// <summary>
        /// Test method for ReadBooks
        /// </summary>
        [TestMethod]
        public void TestReadBooks()
        {
            // Arrange

            // Act

            // Assert
        }

        /// <summary>
        /// Test method for ReadUsers
        /// </summary>
        [TestMethod]
        public void TestReadUsers()
        {
            // Arrange

            // Act

            // Assert
        }

        ////////////////////////////////////////////////////
        // Test methods for adding books and users
        ///////////////////////////////////////////////////
        /// <summary>
        /// Test method for AddBook
        /// </summary>
        [DataTestMethod]
        [DataRow("To Kill a Mockingbird", "Harper Lee", "123456-A")] // happy
        [DataRow("", "Harper Lee", "123456-A")]
        [DataRow("To Kill a Mockingbird", "", "123456-A")]
        [DataRow(null, null, "123456-A")]
        [DataRow(null, "Harper Lee", null)]
        public void TestAddBook(string title, string author, string isbn)
        {
            // Arrange
            // clear the book list for the test method
            LibraryServices.books.Clear();

            // new instance of LibraryServices
            LibraryServices testBook = new LibraryServices();

            // new book instance
            var newBook = new Book { Title = title, Author = author, ISBN = isbn };

            // Act
            // add the new book using AddBook method
            testBook.AddBook(newBook);

            // Assert
            // check if the list contains a book
            Assert.AreEqual(1, LibraryServices.books.Count);

            // check if ID was generated
            Assert.AreEqual(1, LibraryServices.books[0].Id);
        }

        /// <summary>
        /// Test method for AddUser
        /// </summary>
        [DataTestMethod]
        [DataRow("John Smith", "example@email.com")] // happy
        [DataRow("John Smith", null)]
        [DataRow(null, "example@email.com")]
        [DataRow(null, null)]
        public void TestAddUser(string name, string email)
        {
            // Arrange
            // clear the user list for the test method
            LibraryServices.users.Clear();

            // new instance of LibraryServices
            LibraryServices testUser = new LibraryServices();

            // new user instance
            var newUser = new User { Name = name, Email = email };

            // Act
            // add the new user using AddUser method
            testUser.AddUser(newUser);

            // Assert
            // check if list contains a user
            Assert.AreEqual(1, LibraryServices.users.Count);

            // check if ID was generated for new user
            Assert.AreEqual(1, LibraryServices.users[0].Id);
        }

        ////////////////////////////////////////////////
        // Test methods for editing books and users
        ///////////////////////////////////////////////
        /// <summary>
        /// Test method for EditBook
        /// </summary>
        [DataTestMethod]
        [DataRow("To Kill a Mockingbird", "Harper Lee", "123456-A")]
        [DataRow("", "Harper Lee", "123456-A")]
        [DataRow("To Kill a Mockingbird", "", "123456-A")]
        [DataRow(null, null, "123456-A")]
        [DataRow(null, null, null)]
        public void TestEditBook(int bookId, string newTitle, string newAuthor, string newISBN)
        {
            // Arrange
            // clear the book list for the test method
            LibraryServices.books.Clear();

            // new instance of LibraryServices
            LibraryServices testBook = new LibraryServices();

            // fake book to update
            var existingBook = new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "123456-A" };

            // add the fake book to the list
            testBook.AddBook(existingBook);

            // update book set up
            var updatedBook = new Book { Id = bookId, Title = newTitle, Author = newAuthor, ISBN = newISBN };

            // Act
            // update the book that where ID = 1
            testBook.EditBook(1, updatedBook);

            // Assert
            // Assert that the new title was set
            Assert.AreEqual(newTitle, LibraryServices.books[0].Title);

            // Assert that the new author was set
            Assert.AreEqual(newAuthor, LibraryServices.books[0].Author);

            // Assert that the new ISBN was set
            Assert.AreEqual(newISBN, LibraryServices.books[0].ISBN);
        }

        /// <summary>
        /// Test method for EditUser
        /// </summary>
        [TestMethod]
        public void TestEditUser()
        {
            // Arrange

            // Act

            // Assert
        }

        /////////////////////////////////////////////////////////////
        // Test methods for deleting books and users
        ////////////////////////////////////////////////////////////
        /// <summary>
        /// Test method for DeleteBook
        /// </summary>
        [TestMethod]
        public void TestDeleteBook()
        {
            // Arrange

            // Act

            // Assert
        }

        /// <summary>
        /// Test method for DeleteUser
        /// </summary>
        [TestMethod]
        public void TestDeleteUser()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
