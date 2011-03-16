using System;

namespace wing_ding_pong
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (wingdingpong game = new wingdingpong())
            {
                game.Run();
            }
        }
    }
#endif
}

