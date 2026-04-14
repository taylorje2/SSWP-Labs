using Lab5.Models;
using Lab5.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Reflection;

namespace Lab6Tests
{
    [DoNotParallelize]
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
            // clear the books list ahead of time
            LibraryServices.books.Clear();

            // instance of LibraryServices
            LibraryServices testBook = new LibraryServices();

            // Act
            // run method
            testBook.ReadBooks();

            // Assert
            // Assert that books list contains data
            Assert.IsTrue(LibraryServices.books.Count > 0);
        }

        /// <summary>
        /// Test method for ReadUsers
        /// </summary>
        [TestMethod]
        public void TestReadUsers()
        {
            // Arrange
            // clear the users list
            LibraryServices.users.Clear();

            // instance of LibraryServices
            LibraryServices testUser = new LibraryServices();

            // Act
            // run method
            testUser.ReadUsers();

            // Assert
            // Assert that users list contains data
            Assert.IsTrue(LibraryServices.users.Count > 0);
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
            // check if the list contains the books
            Assert.IsTrue(LibraryServices.books.Contains(newBook));
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
            // check if users were added to list
            Assert.IsTrue(LibraryServices.users.Contains(newUser));


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

            // updated book instance
            var updatedBook = new Book { Title = newTitle, Author = newAuthor, ISBN = newISBN };

            // Act
            // update the existing book
            testBook.EditBook(existingBook.Id, updatedBook);

            // Assert
            // confirm if ISBN has changed or not
            if (!string.IsNullOrEmpty(newISBN))
            {
                Assert.IsTrue(existingBook.ISBN == newISBN);
            }
            else
            {
                Assert.IsFalse(existingBook.ISBN == newISBN);
            }

            // confirm if Title has changed or not
            if (!string.IsNullOrEmpty(newTitle))
            {
                Assert.IsTrue(existingBook.Title == newTitle);
            }
            else
            {
                Assert.IsFalse(existingBook.Title == newTitle);
            }

            // confirm if Author has changed or not
            if (!string.IsNullOrEmpty(newAuthor))
            {
                Assert.IsTrue(existingBook.Author == newAuthor);
            }
            else
            {
                Assert.IsFalse(existingBook.Author == newAuthor);
            }
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

            // variable to delete book with associated ID
            int bookId = existingBook.Id;

            // Act
            // delete book where ID = existing book's ID
            testBook.DeleteBook(bookId);

            // Assert
            // assert that the books list does not contain the previously existing book
            Assert.IsFalse(LibraryServices.books.Contains(existingBook));
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

            // variable for existing book's ID
            int userId = existingUser.Id;

            // Act
            // delete the user where ID = existing book's ID
            testUser.DeleteUser(userId);

            // Assert
            // Assert that the users list is empty
            Assert.IsFalse(LibraryServices.users.Contains(existingUser));
        }
    }
}
