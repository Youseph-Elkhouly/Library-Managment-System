using System;
using System.Collections.Generic;
using System.IO;

class Book
{
    // Properties for Book
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    // Constructor
    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    // Method to display book details
    public void Display()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, Year: {Year}");
    }

    // Method to save book to a file
    public void SaveToFile(StreamWriter file)
    {
        file.WriteLine($"{Title},{Author},{Year}");
    }

    // Static method to load book from a file
    public static Book LoadFromFile(string data)
    {
        var details = data.Split(',');
        return new Book(details[0], details[1], int.Parse(details[2]));
    }
}

class Program
{
    static void Main()
    {
        List<Book> library = new List<Book>();
        int choice;

        do
        {
            // Display Menu
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Display Books");
            Console.WriteLine("3. Save Books to File");
            Console.WriteLine("4. Load Books from File");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Add a new book
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter year of publication: ");
                    int year = int.Parse(Console.ReadLine());

                    // Create a new book object and add it to the library list
                    library.Add(new Book(title, author, year));
                    Console.WriteLine("Book added successfully!");
                    break;

                case 2:
                    // Display all books in the library
                    Console.WriteLine("\nBooks in Library:");
                    foreach (Book book in library)
                    {
                        book.Display();
                    }
                    break;

                case 3:
                    // Save books to a file
                    using (StreamWriter file = new StreamWriter("library.txt"))
                    {
                        foreach (Book book in library)
                        {
                            book.SaveToFile(file);
                        }
                    }
                    Console.WriteLine("Books saved to file successfully!");
                    break;

                case 4:
                    // Load books from a file
                    library.Clear();
                    if (File.Exists("library.txt"))
                    {
                        using (StreamReader file = new StreamReader("library.txt"))
                        {
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                library.Add(Book.LoadFromFile(line));
                            }
                        }
                        Console.WriteLine("Books loaded from file successfully!");
                    }
                    else
                    {
                        Console.WriteLine("File not found!");
                    }
                    break;

                case 5:
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        } while (choice != 5);
    }
}
