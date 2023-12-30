using System;

namespace LibrarySimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // In lines 11 – 12, a LibraryVisitor instance, called user, is created, which is passed as an argument to the Execute() method
            // of the ProgramAdvancer static class, executing the program.
            LibraryVisitor user = new LibraryVisitor();
            ProgramAdvancer.Execute(user);
        }
    }
}