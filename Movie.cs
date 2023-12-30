using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    // The ":" followed by "LibraryItem" indicates that the Movie class inherits from the LibraryItem abstract class, making the Movie class
    // a child class of the LibraryItem parent class.
    class Movie : LibraryItem
    {
        // A Movie object has the attributes of Director, Producer, IMDbID, and the attributes that it inherits from the LibraryItem class.
        public string Director; // The director of the Movie.
        public string Producer; // The producer of the Movie.
        public string IMDbID; // The IMDbID of the Movie.

        // Movie.GetInfo() returns a string and basically provides basic details of the movie it is applied to. The "override"
        // allows for it to override LibraryItem.GetInfo(), through polymorphism.
        public override string GetInfo()
        {
            string message = $"{Title} ({Year}) directed by {Director} and produced by {Producer}";
            return message;
        }
    }
}

