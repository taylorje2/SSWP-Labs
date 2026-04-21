using Lab5.Services;
using Lab5.Models;
using System.Reflection;
using System.Xml.Linq;

namespace Lab5.Services
{
    public class LibraryServices : ILibraryService
    {
        /// <summary>
        /// books and users lists
        /// </summary>
        public static List<Book> books { get; set; } = new List<Book>();
        public static List<User> users { get; set; } = new List<User>();

        /// <summary>
        /// Dictionary for tracking borrowed books
        /// </summary>
        public static Dictionary<User, List<Book>> borrowedBooks = new Dictionary<User, List<Book>>();

        /// <summary>
        /// Constructor for getting data
        /// </summary>
        public LibraryServices()
        {
            ReadBooks();
            ReadUsers();
        }

        /// <summary>
        /// Method for saving changes to Books csv
        /// </summary>
        public void WriteBooksCSV()
        {
            var entries = books.Select(b => $"{b.Id},{b.Title},{b.Author},{b.ISBN}");
            File.WriteAllLines("Books.csv", entries);
        }

        /// <summary>
        /// Method for saving changes to Users csv
        /// </summary>
        public void WriteUsersCSV()
        {
            var entries = users.Select(u => $"{u.Id},{u.Name},{u.Email}");
            File.WriteAllLines("Users.csv", entries);
        }

        /// <summary>
        /// Method for reading the data in the book csv and adding it to the books list
        /// </summary>
        public void ReadBooks()
        {
            // refresh list
            books.Clear();

            // read each entry and populate books list
            foreach (var line in File.ReadLines("Books.csv"))
            {
                var fields = line.Split(',');

                if (fields.Length >= 4)
                {
                    var book = new Book
                    {
                        Id = int.Parse(fields[0].Trim()),
                        Title = fields[1].Trim(),
                        Author = fields[2].Trim(),
                        ISBN = fields[3].Trim()
                    };
                    // add book to list
                    books.Add(book);
                }
            }
        }

        /// <summary>
        /// Method for reading the data in the user csv and adding it to the users list
        /// </summary>
        public void ReadUsers()
        {
            // refresh list
            users.Clear();

            // read each entry and populate users list
            foreach (var line in File.ReadLines("Users.csv"))
            {
                var fields = line.Split(',');

                if (fields.Length >= 3) 
                {
                    var user = new User
                    {
                        Id = int.Parse(fields[0].Trim()),
                        Name = fields[1].Trim(),
                        Email = fields[2].Trim()
                    };

                    // add user to list
                    users.Add(user);
                }
            }
        }

        /// <summary>
        /// Method for adding a new book to the books list
        /// </summary>
        /// <param name="newBook">The new book being created/added</param>
        public void AddBook(Book newBook)
        {
            // create new id
            int id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
            newBook.Id = id;

            // save the entry to the book list 
            books.Add(newBook);

            // save changes to csv
            WriteBooksCSV();
        }

        /// <summary>
        /// Method for editing book information
        /// </summary>
        /// <param name="bookId">The id of the book being edited</param>
        /// <param name="updateBook">The new state/info of the book</param>
        public void EditBook(int bookId, Book updateBook)
        {
            // Look for entry that matches the specific ID
            Book book = books.FirstOrDefault(b => b.Id == bookId);

            // If the book exists, allow edits
            if (book != null)
            {
                // allows user to skip changing book title
                if (!string.IsNullOrEmpty(updateBook.Title))
                {
                    book.Title = updateBook.Title;
                }

                // allows user to skip changing book author
                if (!string.IsNullOrEmpty(updateBook.Author))
                {
                    book.Author = updateBook.Author;
                }

                // allows user to skip changing ISBN
                if (!string.IsNullOrEmpty(updateBook.ISBN))
                {
                    book.ISBN = updateBook.ISBN;
                }
            }
            // save changes to the csv
            WriteBooksCSV();
        }

        /// <summary>
        /// Method for deleting a book
        /// </summary>
        /// <param name="bookId">The ID of the book going to be deleted</param>
        public void DeleteBook(int bookId)
        {
            // Look for entry that matches the specific ID
            var book = books.FirstOrDefault(b => b.Id == bookId);

            // if book exists, remove it
            if (book != null)
            {
                // remove book from list
                books.Remove(book);

                // save changes to csv
                WriteBooksCSV();
            }
        }

        /// <summary>
        /// Method for adding a new user 
        /// </summary>
        /// <param name="newUser">The new user</param>
        public void AddUser(User newUser)
        {
            // create new id
            int id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            newUser.Id = id;

            // add new entry to users list
            users.Add(newUser);

            // save changes to csv
            WriteUsersCSV();
        }

        /// <summary>
        /// Method for editing a user's information
        /// </summary>
        /// <param name="userId">User id of user being edited</param>
        /// <param name="updateUser">new user state</param>
        public void EditUser(int userId, User updateUser)
        {
            // look for entry that matches the specific ID
            User user = users.FirstOrDefault(u => u.Id == userId);

            // if the entry exists, allow edits
            if (user != null)
            {
                // allows user to skip changing the name
                if (!string.IsNullOrEmpty(updateUser.Name))
                {
                    user.Name = updateUser.Name;
                }

                // allows the user to skip entering new email
                if(!string.IsNullOrEmpty(updateUser.Email))
                {
                    user.Email = updateUser.Email;
                }
            }
            // save changes to csv
            WriteUsersCSV();
        }

        /// <summary>
        /// Method for a user being deleted
        /// </summary>
        /// <param name="userId">user id of user</param>
        public void DeleteUser(int userId)
        {
            // look for entry that matches specific ID
            var user = users.FirstOrDefault(u => u.Id == userId);

            // if it exists, remove it
            if (user != null)
            {
                // remove entry from the list
                users.Remove(user);

                // save changes to csv
                WriteUsersCSV();
            }
        }

        /// <summary>
        /// Method for borrowing books
        /// </summary>
        public void BorrowBook(int bookId, int userId)
        {
            // search for book and user id's
            var book = books.FirstOrDefault(b => b.Id == bookId);
            var user = users.FirstOrDefault(u => u.Id == userId);

            // if book and user exist, add the book to the user's list and remove book from books 
            if (book != null &&  user != null)
            {
                if (!borrowedBooks.ContainsKey(user))
                {
                    borrowedBooks[user] = new List<Book>();
                }

                borrowedBooks[user].Add(book);
                books.Remove(book);
            }
        }

        /// <summary>
        /// Method for returning books
        /// </summary>
        public void ReturnBook(User user, Book book)
        {
            // if list contains user, remove book from user's list and add to books list
            if (borrowedBooks.ContainsKey(user))
            {
                borrowedBooks[user].Remove(book);
                books.Add(book);
            }
        }
    }
}
