using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // FileHelper is a static class, meaning that you can't instantiate it.
    static class FileHelper
    {
        // FileHelper.GetLibraryItems() is a method that returns a list of lists of the LibraryItem type, and takes an argument filepath
        // (string).
        public static List<List<LibraryItem>> GetLibraryItems(string filepath)
        {
            // allLibraryItems is a list of lists of the LibraryItem type which is what this method will eventually return.
            List<List<LibraryItem>> allLibraryItems = new List<List<LibraryItem>>();
            // Books and Movies are both lists of the LibraryItem type, which will be populated later on. LibraryItem is a suitable type
            // because, though Books will contain Book objects and Movies will contain Movie objects, a Book object and a Movie object are
            // both also LibraryItem objects, as a result of inheritance.
            List<LibraryItem> Books = new List<LibraryItem>();
            List<LibraryItem> Movies = new List<LibraryItem>();

            // The string array inputMedia is being populated by the contents of filepath using File.ReadAllLines().
            string[] inputMedia = File.ReadAllLines(filepath);

            // This foreach loop will iterate through every entry in inputMedia.
            foreach (string input in inputMedia)
            {
                // Lines 28 – 29 are creating new Book and Movie objects, book1 and movie1, respectively.
                Book book1 = new Book();
                Movie movie1 = new Movie();

                // For each iteration of inputMedia (input), the string array itemInfo is set to each individual part of input, split based
                // on commas (input.Split(",")).
                string[] itemInfo = input.Split(",");

                // The if statement checks if the first entry in itemInfo, when lowercased (itemInfo[0].ToLower()), is "book."
                if (itemInfo[0].ToLower() == "book")
                {
                    // In lines 39 – 45, the remainder of the attributes of book1 are populated from the subsequent entries in itemInfo.
                    book1.Title = itemInfo[1];
                    book1.Year = itemInfo[2];
                    book1.QuantityAvailable = Convert.ToInt32(itemInfo[3]); // Only QuantityAvailable has to be an int, which is why
                                                                            // Convert.ToInt32() is being applied; the rest are strings.
                    book1.Genre = itemInfo[4];
                    book1.Author = itemInfo[5];
                    book1.ISBN = itemInfo[6];
                    book1.Link = itemInfo[7];

                    // After all the attributes have been filled, book1 is added to the LibraryItem list, Books.
                    Books.Add(book1);
                }
                // The else if statement checks if the first entry in itemInfo, when lowercased (itemInfo[0].ToLower()), is "movie."
                else if (itemInfo[0].ToLower() == "movie")
                {
                    // In lines 55 – 62, the remainder of the attributes of movie1 are populated from the subsequent entries in itemInfo.
                    movie1.Title = itemInfo[1];
                    movie1.Year = itemInfo[2];
                    movie1.QuantityAvailable = Convert.ToInt32(itemInfo[3]); // Only QuantityAvailable has to be an int, which is why
                                                                             // Convert.ToInt32() is being applied; the rest are strings.
                    movie1.Genre = itemInfo[4];
                    movie1.Director = itemInfo[5];
                    movie1.Producer = itemInfo[6];
                    movie1.IMDbID = itemInfo[7];
                    movie1.Link = itemInfo[8];

                    // After all the attributes have been filled, movie1 is added to the LibraryItem list, Movies.
                    Movies.Add(movie1);
                }
            }

            // In lines 71 – 72, Books and Movies are added to allLibraryItems using .Add(). The order in which they are appended is
            // important for how Library.PopulateLibrary() functions.
            allLibraryItems.Add(Books);
            allLibraryItems.Add(Movies);

            // allLibraryItems, which now contains the list of books and the list of movies is returned.
            return allLibraryItems;
        }

    }
}

