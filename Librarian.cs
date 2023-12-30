using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // Librarian is a static class, meaning that you can't instantiate it.
    static class Librarian
    {
        // The Librarian class has only one static attribute — Name — which is a string.
        public static string Name = "Paige Reed";

        // Librarian.RecommendItem() is a void method, and takes 2 arguments — user (LibraryVisitor) and mediaType (int).
        public static void RecommendItem(LibraryVisitor user, int mediaType)
        {
            // availableLibraryItems is currently an empty list of the LibraryItem type but will soon be populated based on
            // what mediaType is.
            List<LibraryItem> availableLibraryItems = new List<LibraryItem>();

            // The string array recommendationTypes contains the recommendation types that can be offered ("book," "movie," or "general")
            // for any case of mediaType.
            string[] recommendationTypes = { "book", "movie", "general" };
            // recommendation is currently an empty string but will take on any value of recommendationTypes depending on what mediaType is.
            string recommendation = "";

            // The if statement checks if mediaType is 1, which means that the user wants a book recommendation.
            if (mediaType == 1)
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryBooks where the attribute of QuantityAvailable is
                // at least 1.
                // For more information on LINQs, please see Events.cs.
                availableLibraryItems = Library.libraryBooks.Where(x => x.QuantityAvailable > 0).ToList();
                // recommendation is set to the the first value in recommendationTypes (recommendationTypes[0]) because when mediaType is 1,
                // the user wants a book recommendation.
                recommendation = recommendationTypes[0];
            }
            // The else if statement checks if mediaType is 2, which means that the user wants a movie recommendation.
            else if (mediaType == 2)
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryMovies where the attribute of QuantityAvailable is
                // at least 1.
                availableLibraryItems = Library.libraryMovies.Where(x => x.QuantityAvailable > 0).ToList();
                // recommendation is set to the the second value in recommendationTypes (recommendationTypes[1]) because when mediaType is 2,
                // the user wants a movie recommendation.
                recommendation = recommendationTypes[1];
            }
            // The else block is triggered if mediaType is not 1 or 2, which means it has to be 3, which means that the user wants a general
            // recommendation.
            else
            {
                // This LINQ sets availableLibraryItems to the subset of Library.libraryItems where the attribute of QuantityAvailable is
                // at least 1.
                availableLibraryItems = Library.libraryItems.Where(x => x.QuantityAvailable > 0).ToList();
                // recommendation is set to the the third value in recommendationTypes (recommendationTypes[2]) because when mediaType is 3,
                // the user wants a general recommendation.
                recommendation = recommendationTypes[2];
            }

            // genreItems is a dictionary containing string keys and a list of strings as values. The keys will eventually be the distinct
            // genres available in availableLibraryItems, while the values will be the library items that fit each resepective genre.
            Dictionary<string, List<string>> genreItems = new Dictionary<string, List<string>>();

            // This Console.WriteLine() introduces the user to our lovely librarian, Paige Reed. It uses string interpolation for both the
            // the user's and Paige's name.
            Console.WriteLine($"\nHello, {user.Name}! My name is {Name}, and I am the librarian.");
            // This Console.WriteLine() statement lets the user know that Paige knows what recommendation type the user would like to hear
            // about, using string interpolation.
            Console.WriteLine($"\nWell, it looks like you want a {recommendation} recommendation. Let me see what I can do.\n");

            // This foreach loop iterates through every item of availableLibraryItems with the ultimate goal of locating the unique genres
            // and adding the titles associated with each genere.
            foreach (LibraryItem lib in availableLibraryItems)
            {
                // The if statement checks if the genre (lib.Genre) is already within genreItems, by seeing if lib.Genre is already in the
                // keys of genreItems (genreItems.ContainsKey(lib.Genre)).
                if (genreItems.ContainsKey(lib.Genre))
                {
                    // If the current genre (lib.Genre) already exists in the dictionary, the current title is appended to the values list
                    // associated with that genre key.
                    genreItems[lib.Genre].Add(lib.Title);
                }
                // If the genre (lib.Genre) is not already in genreItems, the else is triggered.
                else
                {
                    // If the current genre (lib.Genre) does not already exist in genreItems, a key for lib.Genre is created with the value
                    // being a new list of strings, with the current title (lib.Title) appended to the list.
                    genreItems[lib.Genre] = new List<string>() { lib.Title };
                }
            }

            // Thread.Sleep() is used here to add a delay for 2 seconds (Thread.Sleep() operates in milliseconds), to make it seem more
            // "realistic," like Paige is actually scanning through the library to help the user.
            Thread.Sleep(2000);

            // counter is initially 0, but is meant to help enumerate library items.
            int counter = 0;

            // This foreach loop iterates through the genres in genreItems by accessing its keys (genreItems.Keys).
            foreach (string genre in genreItems.Keys)
            {
                // counter is incremented by 1 (counter++) since it was 0 at first, and since unlike computers, humans don't start at 0,
                // incrementing by 1 here allows counter to start the list at 1.
                counter++;
                // This Console.WriteLine() statement uses string interpolation to print the genre and the number associated with it
                // (counter).
                Console.WriteLine($"{counter}. {genre}");
            }

            // This Console.WriteLine() statement prompts the user to tell Paige which genre they are interested in, by number.
            Console.WriteLine("\nWhich genre catches your fancy?");
            // selectionNum is set equal to ProgramAdvancer.AcceptInput(), where the length of genreItems.Keys
            // (genreItems.Keys.ToList().Count) is fed in as a parameter. .ToList() is required because .Count can only be applied to lists
            // and the keys of a dictionary are not equivalent to a list.
            int selectionNum = ProgramAdvancer.AcceptInput(genreItems.Keys.ToList().Count);

            // Since lists start at 0 and humans start numbers at 1, index, which is the index that will be used to index the item, is
            // set to selectionNum - 1.
            int index = selectionNum - 1;

            // selectedItem is set to genreItems.Keys at the position of index (genreItems.Keys.ToList()[index]). .ToList()
            // is required because indexing can only be applied to lists and the keys of a dictionary are not equivalent to a list.
            string selectedGenre = genreItems.Keys.ToList()[index];

            // random is a new object of the Random class.
            Random random = new Random();
            // randomNumber is set to a random number between 0 and the length of genreItems at the selected key or genre
            // (genreItems[selectedGenre].Count). The length of genreItems[selectedGenre] is used instead of
            // genreItems[selectedGenre].Count - 1 because the Random.Next()'s upper bound is exclusive,
            // preventing a potential index out of bounds error.
            int randomNumber = random.Next(0, genreItems[selectedGenre].Count);
            // selectedTitle is random title from the selected genre, indexed by randomNumber (genreItems[selectedGenre][randomNumber]).
            string selectedTitle = genreItems[selectedGenre][randomNumber];

            // selectedItem, which is a an object of the LibraryItem class, is currently null, but will eventually be set to the LibraryItem
            // object that has the title of the selectedTitle.
            LibraryItem selectedItem = null;

            // This foreach loop iterates through every item in availableLibraryItems.
            foreach (LibraryItem lib in availableLibraryItems)
            {
                // The if statement checks if the title of the current LibraryItem object (lib.Title) is equal to selectedTitle.
                if (lib.Title == selectedTitle)
                {
                    // If lib.Title and selectedTitle are the same, selectedItem is set to the current LibraryItem object.
                    selectedItem = lib;
                }
            }

            // This Console.WriteLine() statement uses string interpolation to offer the suggestion to the user by using the
            // LibraryItem.GetInfo() method on selectedItem (selectedItem.GetInfo()).
            Console.WriteLine($"\nI highly recommend {selectedItem.GetInfo()}");

            // This Console.WriteLine() statement basically just asks if the user wants to borrow the item.
            Console.WriteLine("\nWould you like to borrow it?\nYes\nNo");
            // The user responds to the above prompt through input, which uses Console.ReadLine().
            string input = Console.ReadLine();

            // proceed is a boolean that will remain false until an input receives an acceptable value — "yes" or "no."
            bool proceed = false;

            // This while loop persists while proceed is false (!proceed).
            while (!proceed)
            {
                // The try block checks the value of input to determine what to do next.
                try
                {
                    // The if statement checks if input, when converted to lowercase (input.ToLower()) is equal to "yes."
                    if (input.ToLower() == "yes")
                    {
                        // If input is equal to "yes", the check out is done so that the selectedItem is added to user.borrowedMedia
                        // (user.UserCheckout(selectedItem)), and the changes are reflected in the library (selectedItem.CheckOut()).
                        // For more information, please see Library.CheckOut() in Library.cs.
                        user.UserCheckout(selectedItem);
                        selectedItem.CheckOut();
                        // The Console.WriteLine() statement uses string interpolation to inform the user about selectedItem
                        // (selectedItem.GetInfo()) and when to return it.
                        Console.WriteLine($"\nExcellent choice! Please return {selectedItem.GetInfo()} in 2 weeks.");

                        // proceed is set to true, breaking the while loop so that the program can continue.
                        proceed = true;
                    }
                    // The else if statement checks if input, when converted to lowercase (input.ToLower()) is equal to "no."
                    else if (input.ToLower() == "no")
                    {
                        // If input is equal to "no", nothing really happens, but this Console.WriteLine() statement wishes the user well
                        // on their journey through the library.
                        Console.WriteLine("\nOkay, have fun browsing the rest of the library!");
                        // proceed is set to true, breaking the while loop so that the program can continue.
                        proceed = true;
                    }
                    // If input is not equal to "yes" or "no," the else block is triggered.
                    else
                    {
                        // If input is not equal to "yes" or "no," something is wrong so an exception is thrown, forcing the program into
                        // the catch block.
                        throw new Exception();
                    }
                }
                // The catch block is triggered if input is not "yes" or "no."
                catch
                {
                    // This Console.WriteLine() statement informs the user that they did not give a valid answer.
                    Console.WriteLine("I'm sorry, I don't understand.");
                    // Lines 204 – 205 reiterate the process in lines 153 – 155. 
                    Console.WriteLine($"Would you like to borrow {selectedItem.GetInfo()}?\nYes\nNo");
                    input = Console.ReadLine();
                }
            }
        }
    }
}

