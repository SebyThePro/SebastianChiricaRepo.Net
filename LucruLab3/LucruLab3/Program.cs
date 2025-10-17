//Trick question
//Aplicarea .ToList() inainte de paginare (adică înainte de Skip().
//Take()) aduce toate datele in memoria aplicatiei.
//Este foarte ineficient si incetineste aplicatia,
//deoarece se transfera si se procesează milioane de inregistrari,
//desi este nevoie doar de o pagina mica de date.

using System;
using System.Collections.Generic;
using System.Linq;

Console.WriteLine("--- Library Book and Borrower Tracker ---");
Console.WriteLine("----------------------------------------------------\n");

// Requirement 1: Modeling with record and with
Console.WriteLine("REQUIREMENT 1: Record & 'with' Cloning");
ModelingRecordsAndWith();

// Requirement 2: Use of init-only properties
Console.WriteLine("\nREQUIREMENT 2: Init-only Properties");
UseInitOnlyProperties();

// Requirement 3: Top-level statements, User interaction
Console.WriteLine("\nREQUIREMENT 3: User Interaction");
var userBooks = GetUserBooks(); // Local function call to handle input
DisplayBooks(userBooks);         // Local function call to display results

// Requirement 4: Pattern Matching Advanced
Console.WriteLine("\nREQUIREMENT 4: Pattern Matching");
// Corrected Book initialization: using the new (Title, Author, Year) constructor overload
var sampleBook = new Book("Dune", "Frank Herbert", 1965); 
var sampleBorrower = new Borrower(101, "Ana Popescu", new List<Book> { sampleBook });
CheckObjectType(sampleBook);
CheckObjectType(sampleBorrower);
CheckObjectType(DateTime.Now); // Test case for 'Unknown type'

Console.WriteLine("\nREQUIREMENT 5 & 6: Update and Pagination (Advanced Library Operations)");
// Note: The call to StaticLambdaFiltering has been removed as the logic is now in AdvancedLibraryOperations
AdvancedLibraryOperations();

// Auxiliary Methods Implementation (Local Functions)

//Implements record and 'with' cloning
void ModelingRecordsAndWith() 
{
    // Corrected Book initialization: using the new (Title, Author, Year) constructor overload
    var book1 = new Book("1984", "George Orwell", 1949);
    var book2 = new Book("Brave New World", "Aldous Huxley", 1932);

    // Initial Borrower instance. Records provide concise constructor syntax
    var borrower1 = new Borrower(
        Id: 1,
        Name: "Ion Vasile",
        BorrowedBooks: new List<Book> { book1 }
    );

    Console.WriteLine($"  - Initial Borrower: {borrower1.Name}, Books: {borrower1.BorrowedBooks.Count}");

    // 'with' expression: creates a new Borrower instance (borrower2) 
    // by copying borrower1 and modifying only the specified property (BorrowedBooks)
    var borrower2 = borrower1 with
    {
        // We use LINQ's Append() and ToList() to ensure a completely new list is created
        BorrowedBooks = borrower1.BorrowedBooks.Append(book2).ToList()
    };

    Console.WriteLine($"  - Cloned Borrower (using 'with'): {borrower2.Name}, Books: {borrower2.BorrowedBooks.Count}");
}

// Step 2: Implements init-only properties
void UseInitOnlyProperties()
{
    // Properties are set only during object initialization using 'init'
    // The C# 9.0 'required' keyword would prevent the CS8618 warnings, but since we 
    // are explicitly setting them here, we will suppress the warning temporarily or ignore it 
    // for this older C# version context, though modern practice uses 'required'.
    var librarian = new Librarian
    {
        Name = "Chirica Sebastian",
        Email = "sebychirica100@gmail.com",
        LibrarySection = "Fiction"
    };
    
    // Attempting to change librarian.Name here (e.g., librarian.Name = "New Name";) 
    // would result in a compile-time error
    
    Console.WriteLine($"  - Librarian Info: {librarian}");
}

// Step 3: Logic for user interaction (Get input)
List<Book> GetUserBooks()
{
    var books = new List<Book>();
    Console.WriteLine("  - Enter book titles. Type 'stop' to finish.");
    
    while (true)
    {
        Console.Write("  - Book Title: ");
        var title = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(title) || title.ToLower() == "stop")
            break;

        // Corrected Book initialization: using the new (Title, Author, Year) constructor overload
        books.Add(new Book(title, "Anonymous", DateTime.Now.Year));
    }
    return books;
}

// Step 3: Display the complete list of books
void DisplayBooks(List<Book> books)
{
    Console.WriteLine("\n  - List of books entered by user:");
    if (books.Any())
    {
        foreach (var book in books)
        {
            Console.WriteLine($"- {book.Title}");
        }
    }
    else
    {
        Console.WriteLine("  - No books were entered.");
    }
}

// Step 4: Implements Advanced Pattern Matching
// This function uses the 'switch expression' to safely check and extract data based on object type
void CheckObjectType(object obj)
{
    // The switch expression syntax is concise and modern.
    var result = obj switch
    {
        // Type Pattern: 'Book b' matches the type and assigns the object to a new local variable 'b'
        Book b => $"Book: Title: {b.Title}, Year: {b.YearPublished}",
        
        // Type Pattern: Matches Borrower and extracts its properties
        Borrower bo => $"Borrower: Name: {bo.Name}, Borrowed Books Count: {bo.BorrowedBooks.Count}",
        
        // Discard Pattern (_): Acts as the default/catch-all case for any other type
        _ => "Unknown type",
    };
    
    Console.WriteLine($"  - Object Type Check ({obj.GetType().Name}): {result}");
}

// Step 5 & 6: Implements Update and Pagination
void AdvancedLibraryOperations()
{
    // Initialize a list of books with IDs for testing
    var libraryBooks = new List<Book>
    {
        new Book(1, "The Great Gatsby", "F. Scott Fitzgerald", 1925),
        new Book(2, "The Alchemist", "Paulo Coelho", 1988),
        new Book(3, "The Name of the Wind", "Patrick Rothfuss", 2007),
        new Book(4, "Project Hail Mary", "Andy Weir", 2021),
        new Book(5, "Klara and the Sun", "Kazuo Ishiguro", 2021),
        new Book(6, "Foundation", "Isaac Asimov", 1951)
    };
    
    //Update Operation (PUT /books/{id})
    
    int bookIdToUpdate = 3;
    var existingBook = libraryBooks.FirstOrDefault(b => b.Id == bookIdToUpdate);

    if (existingBook != null)
    {
        // Use the 'with' expression for non-destructive update (immutability pattern)
        var updatedBook = existingBook with
        {
            Title = "The Name of the Wind (Updated Edition)",
            YearPublished = 2008
        };
        // In memory, we find the index and replace the old record with the new one.
        int index = libraryBooks.FindIndex(b => b.Id == bookIdToUpdate);
        libraryBooks[index] = updatedBook;

        Console.WriteLine($"  - UPDATE: Book ID {bookIdToUpdate} updated to '{updatedBook.Title}' ({updatedBook.YearPublished}).");
    }

    //Pagination Operation (GET /books?page=2&pageSize=3) 
    
    int pageNumber = 2;
    int pageSize = 3;
    
    //LINQ is used for sorting, skipping, and taking
    var pagedBooksQuery = libraryBooks
        .OrderBy(b => b.Id)     
        .Skip((pageNumber - 1) * pageSize) 
        .Take(pageSize) 
        .ToList(); 

    Console.WriteLine($"\n  - PAGINATION: Page {pageNumber} (Size {pageSize}) results:");
    if (pagedBooksQuery.Any())
    {
        foreach (var book in pagedBooksQuery)
        {
            Console.WriteLine($"- ID {book.Id}: {book.Title}");
        }
    }
    else
    {
        Console.WriteLine("  - No results for this page.");
    }
}

// Class and Record Definitions (Data Modeling)

// Requirement 1: Book record - Automatically provides value equality and ToString implementation
public record Book(int Id, string Title, string Author, int YearPublished)
{
    // Overloaded constructor for convenience when an ID is not available (like user input)
    public Book(string Title, string Author, int YearPublished) 
        : this(0, Title, Author, YearPublished) { } // Calls the primary constructor with Id = 0
}

// Requirement 1: Borrower record - Used to model the library user
public record Borrower(int Id, string Name, List<Book> BorrowedBooks);

// Requirement 2: Class with init-only properties
public class Librarian
{
    // 'init' accessor ensures the properties are immutable after the object is constructed
    // The use of 'required' (C# 11+) would eliminate the CS8618 warnings.
    public string Name { get; init; }
    public string Email { get; init; }
    public string LibrarySection { get; init; }
    
    // Custom ToString for cleaner console output
    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Section: {LibrarySection}";
    }
}