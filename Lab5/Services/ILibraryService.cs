using Lab5.Components.Pages;
using Lab5.Models;

namespace Lab5.Services
{
    public interface ILibraryService
    {
        // Books list and users list
        public static List<Book> books { get; set; } 
        public static List<User> users { get; set; } 

        // Reads data from Books.csv
        void ReadBooks();

        // Reads data from Users.csv
        void ReadUsers();

        // Create : adds new book to the list
        void AddBook(Book newBook);

        // Update : edits information about a book
        void EditBook(int bookId, Book updatedBook);

        // Delete : removes a book from the list
        void DeleteBook(int bookId);

        // Create : adds a new user to the list
        void AddUser(User newUser);

        // Update : edits information about a user
        void EditUser(int userId, User updatedUser);

        // Delete: Removes a user from the list
        void DeleteUser(int userId);

        void BorrowBook(int bookId, int userId);

        void ReturnBook(User user, Book book);

    }
}
