using System;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Asteroids game = new Asteroids())
            {
                try
                {
                    game.Run();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    System.Console.WriteLine(e.Source);
                    System.Console.WriteLine(e.TargetSite);
                    System.Console.WriteLine(e.StackTrace);
                }
                
            }
        }
    }
}

