using Lab5.Models;
using Lab5.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
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
        [DataRow("New Book", "No Author", "ABC123")]
        [DataRow("", "No Author", "ABC123")]
        [DataRow("New Book", "", "ABC123")]
        [DataRow(null, null, "ABC123")]
        [DataRow(null, null, null)]
        public void TestEditBook(string newTitle, string newAuthor, string newISBN)
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
            var updatedBook = new Book { Title = newTitle, Author = newAuthor, ISBN = newISBN };

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
        [DataTestMethod]
        [DataRow("New User", "newEmail@email.com")] // happy
        [DataRow("New User", null)]
        [DataRow(null, "newEmail@email.com")]
        [DataRow(null, null)]
        public void TestEditUser(string newName, string newEmail)
        {
            // Arrange
            // clear the user list for the test method
            LibraryServices.users.Clear();

            // new instance of LibraryServices
            LibraryServices testUser = new LibraryServices();

            // fake user to update
            var existingUser = new User { Name = "John Smith", Email = "example@email.com" };

            // add the fake user to the list
            testUser.AddUser(existingUser);

            // update user set up
            var updatedUser= new User { Name = newName, Email = newEmail };

            // Act
            // update the user that where ID = 1
            testUser.EditUser(1, updatedUser);

            // Assert
            // Assert that the new name was set
            Assert.AreEqual(newName, LibraryServices.users[0].Name);

            // Assert that the new author was set
            Assert.AreEqual(newEmail, LibraryServices.users[0].Name);
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
            // clear the book list for the test method
            LibraryServices.books.Clear();

            // new instance of LibraryServices
            LibraryServices testBook = new LibraryServices();

            // fake book to delete
            var existingBook = new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "123456-A" };

            // add the fake book to the list
            testBook.AddBook(existingBook);

            // Act
            // delete book where ID = 1
            testBook.DeleteBook(1);

            // Assert
            // assert that the books list is empty
            Assert.AreEqual(0, LibraryServices.books.Count);
        }

        /// <summary>
        /// Test method for DeleteUser
        /// </summary>
        [TestMethod]
        public void TestDeleteUser()
        {
            // Arrange
            // clear the user list for the test method
            LibraryServices.users.Clear();

            // new instance of LibraryServices
            LibraryServices testUser = new LibraryServices();

            // fake user to delete
            var existingUser = new User { Name = "John Smith", Email = "example@email.com" };

            // add the fake user to the list
            testUser.AddUser(existingUser);

            // Act
            // delete the user where ID = 1
            testUser.DeleteUser(1);

            // Assert
            // Assert that the users list is empty
            Assert.AreEqual(0, LibraryServices.users.Count);
        }
    }
}
