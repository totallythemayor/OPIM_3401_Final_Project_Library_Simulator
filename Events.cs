using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // Events is a static class, meaning that you can't instantiate it.
    static class Events
    {
        // Events.AttendEvent() is a void method, and takes an argument mediaType (int).
        public static void AttendEvent(int mediaType)
        {
            // availableLibraryItems is currently an empty list of the LibraryItem type but will soon be populated based on
            // what mediaType is.
            List<LibraryItem> availableLibraryItems = new List<LibraryItem>();
            // The string array viewingOptions contains the viewing options ("reading" or "screening") for either case of mediaType.
            string[] viewingOptions = { "reading", "screening" };
            // viewing is currently an empty string but will take on either value of viewingOptions depending on what mediaType is.
            string viewing = "";

            // The if statement checks if mediaType is 1, which means that the user wants to go to a book event.
            if (mediaType == 1)
            {
                // I wanted to get a subset of the list of books, and just lists in general, so I researched it online and learned about
                // LINQs (https://stackoverflow.com/questions/1712975/how-to-get-a-sublist-in-c-sharp), which can be used to query a list.
                // In this case, I took the Library.libraryBooks list and queried items in the list that have a QuantityAvailable attribute
                // of at least 1. After getting that I applied .ToList() to make it a list and set it equal to availableLibraryItems, the
                // empty list from earlier.
                availableLibraryItems = Library.libraryBooks.Where(x => x.QuantityAvailable > 0).ToList();
                // viewing is set to the first item in viewingOptions (viewingOptions[0]) because a book is more likely to have a "reading"
                // than a "screening."
                viewing = viewingOptions[0];
            }
            // The else if statement checks if mediaType is 2, which means that the user wants to go to a movie event.
            else if (mediaType == 2)
            {
                // In this case, availableLibraryItems is created by taking Library.libraryMovies and querying the items that have a
                // QuantityAvailable attribute of at least 1. Again, .ToList() is used to make this query a list.
                availableLibraryItems = Library.libraryMovies.Where(x => x.QuantityAvailable > 0).ToList();
                // viewing is set to the second item in viewingOptions (viewingOptions[1]) because a movie is more likely to have a
                // "screening" than a "reading."
                viewing = viewingOptions[1];
            }

            // random is a new object of the Random class.
            Random random = new Random();
            // randomNumber is set to a random number between 0 and the length of availableLibraryItems (availableLibraryItems.Count). The
            // length of availableLibraryItems is used instead of availableLibraryItems.Count - 1 because the Random.Next()'s upper bound
            // is exclusive, preventing a potential index out of bounds error.
            int randomNumber = random.Next(0, availableLibraryItems.Count);
            // selectedItem is a LibraryItem object set to an element of availableLibraryItems indexed by randomNumber.
            LibraryItem selectedItem = availableLibraryItems[randomNumber];

            // This Console.WriteLine() statement uses string interpolation to use the correct form of viewing and give a short description
            // of the selected item using LibraryItem.GetInfo() (selectedItem.GetInfo()).
            Console.WriteLine($"\nPlease enjoy a special {viewing} of {selectedItem.GetInfo()}");

            // Thread.Sleep() is used to give the program a delay for 1 second (Thread.Sleep() operates in milliseconds), to make it seem
            // more natural.
            Thread.Sleep(1000);
            // selectedItem.Access() redirects the user to the Link attribute associated with selectedItem.
            selectedItem.Access();
        }
    }
}

