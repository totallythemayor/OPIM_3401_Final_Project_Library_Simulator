using System;
using LibrarySimulator;

namespace LibrarySimulator
{
    class LibraryVisitor
    {
        // A LibraryVisitor object has the attributes of Name, LibraryCard, and borrowedMedia.
        public string Name; // The name of the LibraryVisitor.
        public bool LibraryCard; // Whether or not the LibraryVisitor has a library card. This can be changed in Main() or by running
                                 // ProgramAdvancer.Execute().
        public Dictionary<LibraryItem, int> borrowedMedia = new Dictionary<LibraryItem, int>(); // A dictionary containing a LibraryItem as
                                                                                                // a key and an int, conveying the number of
                                                                                                // it they have borrowed as the value.

        // LibraryVisitor.UserCheckout() is a void method, and takes an argument of media (LibraryItem).
        public void UserCheckout(LibraryItem media)
        {
            // The if statement checks if media is already within borrowedMedia, by seeing if media is already in the keys of borrowedMedia
            // (borrowedMedia.ContainsKey(media)).
            if (borrowedMedia.ContainsKey(media))
            {
                // If media is already in borrowedMedia, the value associated with the key media in borrowedMedia is incremented by 1.
                borrowedMedia[media] += 1;
            }
            // If the media is not already in borrowedMedia, the else is triggered.
            else
            {
                // If media does not already exist in borrowedMedia, a key for media is created and the value is set to 1
                // (borrowedMedia[media] = 1).
                borrowedMedia[media] = 1;
            }
        }

        // LibraryVisitor.UserReturn() is a void method and takes an argument of media (LibraryItem).
        public void UserReturn(LibraryItem media)
        {
            // The if statement checks if the value associated with the key media in borrowedMedia is more than 1 (i.e., the LibraryVisitor
            // borrowed more than 1 copy of media).
            if (borrowedMedia[media] > 1)
            {
                // If LibraryVisitor has more than 1 copy of media in borrowedMedia, the value associated with the key media in
                // borrowedMedia is decremented by 1.
                borrowedMedia[media] -= 1;
            }
            // If the value associated with the key media in borrowedMedia is just 1, the else is triggered.
            else
            {
                // If there is only one copy of media in borrowedMedia, the key media is removed from borrowedMedia
                // (borrowedMedia.Remove(media)).
                borrowedMedia.Remove(media);
            }

        }
    }
}

