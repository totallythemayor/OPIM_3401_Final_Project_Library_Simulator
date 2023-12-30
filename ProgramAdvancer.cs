using System;

namespace LibrarySimulator
{
    // ProgramAdvancer is a static class, meaning that you can't instantiate it.
    static class ProgramAdvancer
    {
        // Lines 10 – 38 showcase the static attributes of the ProgramAdvancer class, a majority of these being lists of strings,
        // representing the options presented to the user, while Exit is a boolean that continues the flow of the program
        // (it is currently set to false, as the program continues while it is false).
        public static bool Exit = false;
        public static List<string> Decisions = new List<string>()
        {
            "1. Borrow an item.",
            "2. Return an item.",
            "3. View the items you've borrowed.",
            "4. View events.",
            "5. Exit library."
        };
        public static List<string> BorrowDecisions = new List<string>
        {
            "1. Borrow a book.",
            "2. Borrow a movie.",
            "3. Browse books and movies.",
            "4. Get a recommendation"
        };

        public static List<string> EventDecisions = new List<string>()
        {
            "1. Attend a book reading.",
            "2. Attend a movie screening."
        };

        public static List<string> RecommendDecisions = new List<string>()
        {
            "1. Get a recommendation for a book.",
            "2. Get a recommendation for a movie.",
            "3. Get a recommendation for either a movie or a book"
        };

        // ProgramAdvancer.Execute() is a static void method that take user user (LibraryVisitor) as an argument.
        public static void Execute(LibraryVisitor user)
        {
            // In lines 46 – 47, Library.libraryBooks and Library.libraryMovies are being populated using FileHelper.GetLibraryItems.
            // What's important to note here is that FileHelper.GetLibraryItems returns a list of lists, the first of those being the books
            // list and the second one being for movies, which is why Library.libraryBooks is being set to the first list
            // (FileHelper.GetLibraryItems("Books.csv")[0]), while Library.libraryMovies is being set to the second
            // (FileHelper.GetLibraryItems("Books.csv")[1]).
            Library.libraryBooks = FileHelper.GetLibraryItems("Books.csv")[0];
            Library.libraryMovies = FileHelper.GetLibraryItems("Books.csv")[1];
            // Library.PopulateLibrary() is called to populate Library.libraryItems, which contains both books and movies.
            Library.PopulateLibrary();

            // The user's name is set using ProgramAdvancer.GetName().
            user.Name = GetName();

            // The user's library card status is checked using ProgramAdvancer.CheckLibraryCard(), with user as an argument.
            CheckLibraryCard(user);

            // This while loop persists, which is essentially the entire program, while ProgramAdvancer.Exit is false (!Exit).
            while (!Exit)
            {
                // While ProgramAdvancer.Exit is false, ProgramAdvancer.GeneralOptions() is called, with user as an argument.
                GeneralOptions(user);
            }
        }

        // ProgramAdvancer.GetName() is a static method that returns a string.
        public static string GetName()
        {
            // This Console.WriteLine() statement prompts the user to enter their name.
            Console.WriteLine("Hello, there! What is your name?");
            // The user is allowed to submit an answer through name, which uses Console.ReadLine().
            string name = Console.ReadLine();

            // name, from above, is returned.
            return name;
        }

        // ProgramAdvancer.CheckLibraryCard() is a static void method that takes user (LibraryVisitor) as an argument.
        public static void CheckLibraryCard(LibraryVisitor user)
        {
            // By deafult, the user will not have a library card, but since this can be changed in Program.Main(), this if statement checks
            // if the user already has a library card (user.LibraryCard).
            if (user.LibraryCard)
            {
                Console.WriteLine("\nThank you for carrying your library card! We hope you find something you like!");
            }
            // If the user does not have a library card, the else block is triggered.
            else
            {
                // This Console.WriteLine() statement asks the user if they want to apply for a library card or not.
                Console.WriteLine("\nIt looks like you don't have a library card. Would you like to apply for a library card?\nYes\nNo");
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
                        // The if statement checks if input, when converted to lowercase(input.ToLower()) is equal to "yes."
                        if (input.ToLower() == "yes")
                        {
                            // If input is equal to "yes," the application is "processed" (nothing of substance is actually done),
                            // in lines 111 – 113, with Thread.Sleep() giving a short delay of 0.5 seconds to make it seem more realistic.
                            Console.WriteLine("\nPlease wait one moment while we process your application.");
                            Thread.Sleep(500);
                            Console.WriteLine("Application processed. Here's your library card!");
                            // Since the user now has a library card, user.LibraryCard is now set to true.
                            user.LibraryCard = true;
                            // proceed is set to true, breaking the while loop so that the program can continue.
                            proceed = true;
                        }
                        // The else if statement checks if input, when converted to lowercase (input.ToLower()) is equal to "no."
                        else if (input.ToLower() == "no")
                        {
                            // Since if a user does not have a library card, they can't do anything at a library, this Console.WriteLine()
                            // sees them off, and tells them to vist next time.
                            Console.WriteLine("\nOkay, come back next time!");
                            // proceed is set to true, breaking the while loop so that the program can continue.
                            proceed = true;
                            // ProgramAdvancer.Exit is set to true, breaking the loop in ProgramAdvancer.Execute(), stopping the program
                            // altogether.
                            Exit = true;
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
                        // This Console.WriteLine() statement informs the user that they did not give a valid answer, and asks them again
                        // if they'd like to apply for a library card.
                        Console.WriteLine("I'm sorry, I don't understand. Would you like to apply for a library card");
                        // Again, the user responds to the question through input, which uses Console.ReadLine().
                        input = Console.ReadLine();
                    }
                }
            }
        }

        // ProgramAdvancer.GeneralOptions() is a static void method that takes user (LibraryVisitor) as an argument.
        public static void GeneralOptions(LibraryVisitor user)
        {
            Console.WriteLine();
            // This foreach loop iterates through every option in ProgramAdvancer.Decisions.
            foreach (string option in Decisions)
            {
                // This Console.WriteLine() statement prints the current option.
                Console.WriteLine(option);
            }

            // selectionNum, which will represent the user's selection, is set equal to ProgramAdvancer.AcceptInput(), with the length of
            // ProgramAdvancer.Decisions as an argument (Decisions.Count).
            int selectionNum = AcceptInput(Decisions.Count);

            // This switch statement uses selectionNum for its cases.
            switch (selectionNum)
            {
                // case 1 is for when selectionNum is 1.
                case 1:
                    // If selectionNum is 1, ProgramAdvancer.BorrowOptions() is called, with user as an argument, which means that the user
                    // wants to borrow something.
                    BorrowOptions(user);
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
                // case 2 is for when selectionNum is 2.
                case 2:
                    // If selectionNum is 2, Library.Return() is called, with user as an argument, which means that the user wants to return
                    // an item.
                    Library.Return(user);
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
                // case 3 is for when selectionNum is 3.
                case 3:
                    // If selectionNum is 3, ProgramAdvancer.ListBorrowedMedia() is called, with user as an argument, which means that the
                    // user wants to view what they've already borrowed.
                    ListBorrowedMedia(user);
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
                // case 4 is for when selectionNum is 4.
                case 4:
                    // If selectionNum is 4, ProgramAdvancer.EventOptions() is called, which means that the user wants to attend an event.
                    EventOptions();
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
                // case 5 is for when selectionNum is 5.
                case 5:
                    // If selectionNum is 5, that means that the user wants to exit the library, so this Console.WriteLine() statement
                    // sees them off.
                    Console.WriteLine("\nThank you for visiting the library! Come back again soon!");
                    // ProgramAdvancer.Exit is set to false, terminating the program.
                    Exit = true;
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
            }
        }

        // ProgramAdvancer.BorrowOptions() is a static void method that takes user (LibraryVisitor) as an argument.
        public static void BorrowOptions(LibraryVisitor user)
        {
            Console.WriteLine();
            // This foreach loop iterates through every option in ProgramAdvancer.BorrowDecisions.
            foreach (string option in BorrowDecisions)
            {
                // This Console.WriteLine() statement prints the current option.
                Console.WriteLine(option);
            }

            // selectionNum, which will represent the user's selection, is set equal to ProgramAdvancer.AcceptInput(), with the length of
            // ProgramAdvancer.BorrowDecisions as an argument (BorrowDecisions.Count).
            int selectionNum = AcceptInput(BorrowDecisions.Count);

            // This switch statement uses selectionNum for its cases.
            switch (selectionNum)
            {
                // case 1 or 2 or 3 is for when selectionNum is 1, 2, or 3.
                // NOTE: Typically, for OR, double pipes (||) are used in C#, however, as I learned, through sheer luck,
                // for switch statements, "or" is used.
                case 1 or 2 or 3:
                    // If selectionNum is 1, 2, or 3, Library.BorrowItem() is called, with user and selectionNum as arguments.
                    Library.BorrowItem(user, selectionNum);
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
                // case 4 is for when selectionNum is 4.
                case 4:
                    // If selectionNum is 4, ProgramAdvancer.RecommendationOptions() is called, with user as an argument.
                    RecommendationOptions(user);
                    // The break statement is there so that the program can "break" out of the switch statement.
                    break;
            }
        }

        // ProgramAdvancer.EventOptions() is a static void method.
        public static void EventOptions()
        {
            Console.WriteLine();
            // This foreach loop iterates through every option in ProgramAdvancer.EventDecisions.
            foreach (string option in EventDecisions)
            {
                // This Console.WriteLine() statement prints the current option.
                Console.WriteLine(option);
            }

            // This Console.WriteLine() statement prompts the user for a response in terms of a what kind of event they would like to
            // attend.
            Console.WriteLine("\nWhat would you like to do?");

            // selectionNum, which will represent the user's selection, is set equal to ProgramAdvancer.AcceptInput(), with the length of
            // ProgramAdvancer.EventDecisions as an argument (EventDecisions.Count).
            int selectionNum = AcceptInput(EventDecisions.Count);

            // Events.AttendEvent() is being called, with selectionNum as an argument.
            Events.AttendEvent(selectionNum);
        }

        // ProgramAdvancer.RecommendationOptions() is a static void method that takes user (LibraryVisitor) as an argument.
        public static void RecommendationOptions(LibraryVisitor user)
        {
            Console.WriteLine();
            // This foreach loop iterates through every option in ProgramAdvancer.RecommendationDecisions.
            foreach (string option in RecommendDecisions)
            {
                // This Console.WriteLine() statement prints the current option.
                Console.WriteLine(option);
            }

            // This Console.WriteLine() statement prompts the user for a response in terms of a what kind of recommendation
            // they would like.
            Console.WriteLine("\nWhat kind of recommendation would you like?");

            // selectionNum, which will represent the user's selection, is set equal to ProgramAdvancer.AcceptInput(), with the length of
            // ProgramAdvancer.RecommendationDecisions as an argument (RecommendationDecisions.Count).
            int selectionNum = AcceptInput(RecommendDecisions.Count);

            // Librarian.RecommendItem() is being called, with user and selectionNum as arguments.
            Librarian.RecommendItem(user, selectionNum);
        }

        // ProgramAdavancer.ListBorrowedMedia is a static void method that takes user (LibraryVisitor) as an argument.
        public static void ListBorrowedMedia(LibraryVisitor user)
        {
            // The if statement checks if the user.borrowedMedia has at least 1 item (user.borrowedMedia.Count > 0) because a user should
            // only be able to be prompted to return items if they have borrowed at least 1 item.
            if (user.borrowedMedia.Count > 0)
            {
                Console.WriteLine();

                // counter is initially 0, but is meant to help enumerate library items.
                int counter = 0;

                // This foreach loop iterates through every LibraryItem in user.borrowedMedia.Keys (every item the user has borrowed).
                foreach (LibraryItem lib in user.borrowedMedia.Keys)
                {
                    // counter is incremented by 1 (counter++) since it was 0 at first, and since unlike computers, humans don't start at 0,
                    // incrementing by 1 here allows counter to start the list at 1.
                    counter++;
                    // This Console.WriteLine() statement uses string interpolation to print the title of the current LibraryItem
                    // (lib.Title) and the number associated with it (counter).
                    Console.WriteLine($"{counter}. {lib.Title}");
                }

                // This Console.WriteLine() statement asks the user if they want to view any of the items that they have borrowed.
                Console.WriteLine("\nWould you like to look at an item?\nYes\nNo");
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
                        // The if statement checks if input, when converted to lowercase(input.ToLower()) is equal to "yes."
                        if (input.ToLower() == "yes")
                        {
                            // selectionNum, which will represent the user's selection, is set equal to ProgramAdvancer.AcceptInput(),
                            // with the length of user.borrowedMedia.Keys as an argument (user.borrowedMedia.Keys.ToList().Count). .ToList()
                            // is necessary because .Count can only be applied to lists, and the keys of a dictionary are not equivalent
                            // to a list.
                            int selectionNum = AcceptInput(user.borrowedMedia.Keys.ToList().Count);

                            // Since lists start at 0 and humans start numbers at 1, index, which is the index that will be used to index
                            // the item, is set to selectionNum - 1.
                            int index = selectionNum - 1;

                            // selectedItem is set to user.borrowedMedia at the position of index (user.borrowedMedia.Keys.ToList()[index]).
                            // .ToList() is required because indexing can only be applied to lists and the keys of a dictionary are not
                            // equivalent to a list.
                            LibraryItem selectedItem = user.borrowedMedia.Keys.ToList()[index];

                            // LibraryItem.Access() is being called with selectedItem.
                            selectedItem.Access();

                            // proceed is set to true, breaking the while loop so that the program can continue.
                            proceed = true;
                        }
                        // The else if statement checks if input, when converted to lowercase (input.ToLower()) is equal to "no."
                        else if (input.ToLower() == "no")
                        {
                            // If input is equal to "no", nothing really happens, but this Console.WriteLine() statement wishes the user
                            // well on their journey through the library.
                            Console.WriteLine("\nOkay, have fun browsing the rest of the library!");
                            // proceed is set to true, breaking the while loop so that the program can continue.
                            proceed = true;
                        }
                        // If input is not equal to "yes" or "no," the else block is triggered. 
                        else
                        {
                            // If input is not equal to "yes" or "no," something is wrong so an exception is thrown, forcing the program
                            // into the catch block.
                            throw new Exception();
                        }
                    }
                    // The catch block is triggered if input is not "yes" or "no."
                    catch
                    {
                        // This Console.WriteLine() statement informs the user that they did not give a valid answer, and asks them again
                        // if they'd like to view an item.
                        Console.WriteLine("I'm sorry, I don't understand.Would you like to look at an item?\nYes\nNo?");
                        // Again, the user responds to the question through input, which uses Console.ReadLine().
                        input = Console.ReadLine();
                    }

                }
            }
            // If the user.borrowedMedia doesn't have at least 1 item, meaning they haven't borrowed anything. the else is trigerred.
            else
            {
                // This Console.WriteLine() informs the user that since they haven't borrowed anything, they have nothing to view.
                Console.WriteLine("\nI'm sorry it looks like you haven't borrowed anything yet.");
            }

        }

        // ProgramAdvancer.AcceptInput is a static method that returns an int, and takes length (int) as an argument.
        public static int AcceptInput(int length)
        {
            // This Console.WriteLine() statement prompts the user to give their selection number.
            // NOTE: This method is used throughout the program, which is why it's very general.
            Console.WriteLine("\nPlease indicate your selection via number");
            // The user responds to the above prompt through input, which uses Console.ReadLine().
            string input = Console.ReadLine();

            // convertedInput will eventually hold the value of input after it has been converted to an int.
            int convertedInput = 0;

            // This while loop persists while convertedInput is 0, and this works because none of the selection prompts has 0 as a valid
            // option.
            while (convertedInput == 0)
            {
                // This try statement attempts to convert input to an int (Convert.ToInt32(input)),
                // and sets it to convertedInput.
                try
                {
                    // convertedInput takes on the value of input if the conversion is successful. If it is successful,
                    // the program continues to the if block at the end of this while loop.
                    convertedInput = Convert.ToInt32(input);
                }
                // If the conversion is not possible, the catch block is triggered.
                catch (Exception e)
                {
                    // This Console.WriteLine() statement prints the Message attribute of the Exception object.
                    Console.WriteLine(e.Message);
                    // This Console.WriteLine() statement reprompts the user to enter their selection number.
                    Console.WriteLine("\nPlease indicate your selection via number");
                    // Again, the user responds to the question through input, which uses Console.ReadLine().
                    input = Console.ReadLine();
                    // continue is used to have to program not proceeed to the if statement and go back to the try block instead,
                    // and this will happen until the catch is not triggered (i.e., a valid input value is given).
                    continue;
                }
                // The if statement checks if the convertedInput is not greater than 0 but less than or equal to length, which if true,
                // means that convertedInput is out of bounds.
                if (!(convertedInput > 0 && convertedInput <= length))
                {
                    // Since the while loop terminates when convertedInput is not 0 and convertedInput should be non-zero, at this point,
                    // convertedInput is set to 0 to continue the input verification process.
                    convertedInput = 0;
                    // This Console.WriteLine() statement informs the user that their selection is not in range.
                    Console.WriteLine("\nThis selection is not in range");
                    // This Console.WriteLine() statement reprompts the user to enter their selection number.
                    Console.WriteLine("\nPlease indicate your selection via number");
                    // Again, the user responds to the question through input, which uses Console.ReadLine().
                    input = Console.ReadLine();
                }
            }

            // convertedInput is returned
            return convertedInput;
        }
    }
}