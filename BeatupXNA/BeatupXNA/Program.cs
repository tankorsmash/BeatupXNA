namespace BeatupXNA
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Beatup game = new Beatup())
            {
                game.Run();
            }
        }
    }
#endif
}

