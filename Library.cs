using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // Library is a static class, meaning that you can't instantiate it. There is only 1 library.
    static class Library
    {
        // In lines 10 – 12, lists of the LibraryItem type are created, which will eventually hold of the library's media.
        public static List<LibraryItem> libraryItems = new List<LibraryItem>();
        public static List<LibraryItem> libraryBooks = new List<LibraryItem>();
        public static List<LibraryItem> libraryMovies = new List<LibraryItem>();

        // Library.PopulateLibrary() is a static void method.
        public static void PopulateLibrary()
        {
            // The first foreach loop takes every item in libraryBooks and appends it to libraryItems, which is supposed to have both books
            // and movies.
            foreach (LibraryItem item in libraryBooks)
            {
                libraryItems.Add(item);
            }
            // The second foreach loop takes every item in libraryMovies and appends it to libraryItems, which is supposed to have both
            // books and movies.
            foreach (LibraryItem item in libraryMovies)
            {
                libraryItems.Add(item);
            }
        }

        // Library.BorrowItem() is a void method, and takes 2 arguments — user (LibraryVisitor) and mediaType (int).
        public static void BorrowItem(LibraryVisitor user, int mediaType)
        {
            // availableLibraryItems is currently an empty list of the LibraryItem type.
            List<LibraryItem> availableLibraryItems = new List<LibraryItem>();

            // The if statement checks if mediaType is 1, which means that the user wishes to borrow a book.
            if (mediaType == 1)
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryBooks where the attribute of QuantityAvailable is
                // at least 1.
                // For more information on LINQs, please see Events.cs.
                availableLibraryItems = libraryBooks.Where(x => x.QuantityAvailable > 0).ToList();
            }
            // The else if statement checks if mediaType is 2, which means that the user wishes to borrow a movie.
            else if (mediaType == 2)
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryMovies where the attribute of QuantityAvailable is
                // at least 1.
                // For more information on LINQs, please see Events.cs.
                availableLibraryItems = libraryMovies.Where(x => x.QuantityAvailable > 0).ToList();
            }
            // If mediaType is neither 1 nor 2, it is 3, which is caught in the else, which means that the user wants to browse both
            // books and movies.
            else
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryItems where the attribute of QuantityAvailable is
                // at least 1.
                // For more information on LINQs, please see Events.cs.
                availableLibraryItems = libraryItems.Where(x => x.QuantityAvailable > 0).ToList();
            }

            // titles is an empty list of strings that is yet to be populated.
            List<string> titles = new List<string>();

            // This foreach loop iterates through availableLibraryItems.
            foreach (LibraryItem lib in availableLibraryItems)
            {
                // For each item in availableLibraryItems, the title of the library item is appended to titles.
                titles.Add(lib.Title);
            }

            // counter is initially 0, but is meant to help enumerate library items.
            int counter = 0;

            Console.WriteLine();

            // This foreach loop iterates through titles.
            foreach (string title in titles)
            {
                // counter is incremented by 1 (counter++) since it was 0 at first, and since unlike computers, humans don't start at 0,
                // incrementing by 1 here allows counter to start the list at 1.
                counter++;
                // This Console.WriteLine() statement uses string interpolation to print the title and the number associated with it
                // (counter).
                Console.WriteLine($"{counter}. {title}");
            }

            // selectionNum is set equal to ProgramAdvancer.AcceptInput(), where the length of availableLibraryItems
            // (availableLibraryItems.Count) is fed in as a parameter.
            int selectionNum = ProgramAdvancer.AcceptInput(availableLibraryItems.Count);

            // Since lists start at 0 and humans start numbers at 1, index, which is the index that will be used to index the item, is
            // set to selectionNum - 1.
            int index = selectionNum - 1;

            // selectedItem is set to availableLibraryItems at the position of index (availableLibraryItems[index]).
            LibraryItem selectedItem = availableLibraryItems[index];

            // LibraryVisitor.UserCheckout(), with selectedItem (LibraryItem) as an argument is used to add selectedItem to the user's
            // borrowedMedia dictionary.
            user.UserCheckout(selectedItem);
            // LibraryItem.CheckOut() is used to change the QuantityAvailable attribute of selectedItem to reflect that it has been
            // borrowed.
            selectedItem.CheckOut();

            // The Console.WriteLine() statement uses string interpolation to inform the user about selectedItem (selectedItem.GetInfo())
            // and when to return it.
            Console.WriteLine($"\nExcellent choice! Please return {selectedItem.GetInfo()} in 2 weeks.");
        }

        // Library.Return() is a void method, and takes user (LibraryVisitor) as an argument.
        public static void Return(LibraryVisitor user)
        {
            // The if statement checks if number of values within the user's borrowedMedia is greater than 0, meaning there are things that
            // they have to return.
            if (user.borrowedMedia.Count > 0)
            {
                // titles is an empty list of strings that is yet to be populated.
                List<string> titles = new List<string>();

                // This foreach loop iterates through user.borrowedMedia.Keys (.Keys is used to access only the keys of a dictionary). 
                foreach (LibraryItem lib in user.borrowedMedia.Keys)
                {
                    // For each item in user.borrowedMedia.Keys, the title of the LibraryItem (lib.Title) is appended to titles.
                    titles.Add(lib.Title);
                }

                // counter is initially 0 but is meant to help enumerate library items.
                int counter = 0;

                Console.WriteLine();

                // This foreach loop iterates through titles.
                foreach (string title in titles)
                {
                    // counter is incremented by 1 (counter++) since it was 0 at first, and since unlike computers, humans don't start at 0,
                    // so incrementing by 1 here allows counter to start the list at 1.
                    counter++;
                    // This Console.WriteLine() statement uses string interpolation to print the title and the number associated with it
                    // (counter).
                    Console.WriteLine($"{counter}. {title}");
                }

                // selectionNum is set equal to ProgramAdvancer.AcceptInput(), where the length of user.borrowedMedia.Keys
                // (user.borrowedMedia.Keys.Count) is fed in as a parameter. .ToList() is necessary because ProgramAdvancer.InRange()
                // only accepts lists, and keys from a dictionary are not the same thing.
                int selectionNum = ProgramAdvancer.AcceptInput(user.borrowedMedia.Keys.ToList().Count);

                // Since lists start at 0 and humans start numbers at 1, index, which is the index that will be used to index the item, is
                // set to selectionNum - 1. 
                int index = selectionNum - 1;

                // selectedItem is set to user.borrowedMedia.Keys at the position of index (availableLibraryItems[index]). Again, .ToList()
                // is required because you can index a list but you cannot index keys from a dictionary.
                LibraryItem selectedItem = user.borrowedMedia.Keys.ToList()[index];

                // LibraryItem.DoReturn() is used to change the QuantityAvailable attribute of selectedItem to reflect that it has been
                // return. 
                selectedItem.DoReturn();
                // LibraryVisitor.UserCheckout(), with selectedItem (LibraryItem) as an argument is used to alter the user's borrowedMedia
                // dictionary.
                user.UserReturn(selectedItem);

                // The Console.WriteLine() statement uses string interpolation to inform the user that they successfully returned
                // selectedItem.
                Console.WriteLine($"\nThank you for returning {selectedItem.Title}.");
            }
            // The else block is triggered if the number of values within the user's borrowedMedia is less than 1, meaning the user didn't
            // borrow anything, meaning they shouldn't be able to return anything.
            else
            {
                // The Console.WriteLine() statement informs the user that they don't have anything to return.
                Console.WriteLine("\nI'm sorry it looks like you don't have anything to return.");
            }
        }
    }
}

