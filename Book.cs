using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // The ":" followed by "LibraryItem" indicates that the Book class inherits from the LibraryItem abstract class, making the Book class
    // a child class of the LibraryItem parent class.
    class Book : LibraryItem
    {
        // A Book object has the attributes of Author, ISBN, and the attributes that it inherits from the LibraryItem class.
        public string Author; // The author of the Book.
        public string ISBN; // The ISBN of the Book.

        // Book.GetInfo() returns a string and basically provides basic details of the book it is applied to. The "override"
        // allows for it to override LibraryItem.GetInfo(), through polymorphism.
        public override string GetInfo()
        {
            string message = $"{Title} ({Year}) written by {Author}";
            return message;
        }
    }
}

