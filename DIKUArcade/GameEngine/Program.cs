using Gtk;

namespace GameEngine
{
    class MainClass
    {
        public static void Main()
        {
            Gdk.Threads.Init();
            Application.Init();
            var window = new GameWindow();
            window.ShowAll();
            new GameEngine(window);
            Application.Run();
        }
    }
}