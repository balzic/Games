using GameConcepts;

namespace GameEngine
{
    public class GameEngine
    {
        private GameWindow _window;
        private Game _selectedGame;

        public GameEngine(GameWindow window)
        {
            _window = window;
            _selectedGame = new StartUpMenu(_window);

            _window.KeyPressEvent += new Gtk.KeyPressEventHandler(_selectedGame.OnKeyPress);
            _window.KeyReleaseEvent += new Gtk.KeyReleaseEventHandler(_selectedGame.OnKeyRelease);
            _window.DrawScene += new DrawSceneHandler(_selectedGame.OnDrawScene);
        }
    }
}