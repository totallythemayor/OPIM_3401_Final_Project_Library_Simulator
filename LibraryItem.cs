using System;
using System.Diagnostics;
using LibrarySimulator;

namespace LibrarySimulator
{
    // An abstract class allows for a class to act as a framework for other classes, without being able to instantiate it.
    abstract class LibraryItem
    {
        // Along with the distinct characteristics of each type of library item (books and movies), all them have the attributes shown in
        // lines 12 – 16, which they inherit from the LibraryItem abstract class.
        public string Title; // The title of the LibraryItem object.
        public string Year; // The year the LibraryItem was released.
        public int QuantityAvailable; // The amount of the LibraryItem the library has.
        public string Genre; // The genre of the LibraryItem.
        public string Link; // The link where the LibraryItem can be accessed, for the purposes of this project,
                            // movies will be just trailers.

        // LibraryItem.GetInfo() returns a string and basically provides basic details of the library item it is applied to. The "virtual"
        // allows for child classes (Book and Movie) to override it, through polymorphism.
        public virtual string GetInfo()
        {
            return ($"{Title} ({Year})");
        }

        // LibraryItem.CheckOut() is a void method that merely decreases the QuantityAvailable attribute of any library item by 1, mimicking
        // the item getting checked out. This method is inherited by the child claseses — Book and Movie.
        public void CheckOut()
        {
            // The -- operator decrements the QuantityAvailable attribute by 1.
            QuantityAvailable--;
        }

        // LibraryItem.DoReturn() is a void method that merely increases the QuantityAvailable attribute of any library item by 1, mimicking
        // the item getting returned. This method is inherited by the child claseses — Book and Movie.
        public void DoReturn()
        {
            // The ++ operator increments the QuantityAvailable attribute by 1.
            QuantityAvailable++;
        }

        // LibraryItem.Access() is a void method that basically accesses the library item that it is applied to, using the Link attribute.
        // This method is inherited by the child claseses — Book and Movie.
        public void Access()
        {
            // A try-catch is implemented here just in case the link doesn't work, so that the program will continue, without crashing.
            // The method first tries to access the link in the try block.
            try
            {
                // In order to see what was the safest way open a link in C#, cross-OS, I conferred with ChatGPT and it suggested
                // doing this, and essentially, what's happening is that a new ProcessStartInfo object is being created with the FileName
                // attribute being set to the Link attribute of the LibraryItem object, and UseShellExecute is true so that the program
                // opens the link in the device's default browser.
                Process.Start(new ProcessStartInfo
                {
                    FileName = Link,
                    UseShellExecute = true
                });
            }
            // If the link is not able to be accessed, the catch block is triggered.
            catch (Exception e)
            {
                // The following 2 lines tells the user that the link cannot be accessed at the moment, with the error message being printed
                // on the first of the 2 lines. Failure to open the link does not crash the program, instead, ProgramAdvancer.Execute() just
                // continues.
                Console.WriteLine($"Error opening URL: {e.Message}");
                Console.WriteLine("Please try again soon");
            }
        }
    }
}

