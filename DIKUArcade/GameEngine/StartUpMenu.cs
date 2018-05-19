using System;
using GameConcepts.Geometry;
using GameConcepts;
using Gtk;

namespace GameEngine
{
    /// <summary>
    /// StartUpMenu is the main class that controls a menu, which allows the 
    /// user to pick the game that is to be initiated.
    /// Once a game is picked the event will be rerouted to the game.
    /// </summary>
    public class StartUpMenu : Game
    {

        private GameWindow _win;
        private Drawing.Label _pickGameLabel;
        private Drawing.Image _SpaceBackground;

        /// <summary>
        /// Main Constructor for the StartUpMenu.
        /// </summary>
        /// <param name="window">Window that displays the graphics</param>
        public StartUpMenu(GameWindow window)
            : base(new Arena(new Position(1000, 1000), 1000, 1000))
        {
            _win = window;

            const string SpacetaxiLine = " Press 't' for SpaceTaxi\n";
            const string GalagaLine = " Press 'g' for Galaga\n";
            const string quitLine = " Press 'q' to quit";

            _pickGameLabel = new Drawing.Label(
                _win.Area,
                12,
                SpacetaxiLine + GalagaLine + quitLine);

            int maxX = Drawing.Scaler.MaxX;
            int maxY = Drawing.Scaler.MaxY;
            var scnPic = ImageStream(@"GameEngine.Resources.Images.SpaceBackground.png");
            _SpaceBackground = new Drawing.Image(scnPic, maxX, maxY);

        }

        /// <summary>
        /// Sets the game to played and redirects events from this class to the game
        /// </summary>
        /// <param name="pressedKey">The pressed key which represents the desired game</param>
        /// \pre preesedKey must be an option in the method OnkeyPress switch statement
        private void _SetCorrectGame(Gdk.Key pressedKey)
        {
            Game selectedGame;
            if(pressedKey == Gdk.Key.g)
            {
                selectedGame = new GalagaGameFactory(_win.Area).Game();
            }
            else if(pressedKey == Gdk.Key.t) // for spacetaxi and incorrect input. yes i know its poor design.
            {
                selectedGame = new SpaceTaxiFactory(_win.Area).Game();
            }
            else
            {
                throw new ArgumentException("pressedkey does not have a corresponding key");
            }

            // remove this objects eventhandlers
            _win.KeyPressEvent -= OnKeyPress;
            _win.KeyReleaseEvent -= OnKeyRelease;
            _win.DrawScene -= OnDrawScene;

            // add the games eventhandlers
            _win.DrawScene += new DrawSceneHandler(selectedGame.OnDrawScene);
            _win.KeyPressEvent += new Gtk.KeyPressEventHandler(selectedGame.OnKeyPress);
            _win.KeyReleaseEvent += new Gtk.KeyReleaseEventHandler(selectedGame.OnKeyRelease);
            _win.DrawScene += new DrawSceneHandler(selectedGame.OnDrawScene);
            // it does not work if DrawScene isn't added twice but why does it need to be added twice?
        }

        public override void OnDrawScene(Drawing.GameArea area)
        {
            if(_SpaceBackground != null)
            {
                _win.Area.Pixmap.DrawImage(_SpaceBackground);
            }
            this.DrawLabelCentered(_pickGameLabel, 50);
        }

        public override void OnKeyPress(object o, Gtk.KeyPressEventArgs args)
        {
            switch(args.Event.Key)
            {
                case Gdk.Key.g:
                    _SetCorrectGame(Gdk.Key.g);
                    break;
                case Gdk.Key.t:
                    _SetCorrectGame(Gdk.Key.t);
                    break;
                case Gdk.Key.q:
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }

        public override void OnKeyRelease(object o, Gtk.KeyReleaseEventArgs args)
        {
            // do nothing
        }

        public override System.IO.Stream ImageStream(string resourceName)
        {
            var thisExe = System.Reflection.Assembly.GetExecutingAssembly();

            var retval = thisExe.GetManifestResourceStream(resourceName);
            return retval;
        }

        private void DrawLabelCentered(Drawing.Label label, int y)
        {
            label.DrawAligned(_win.Area.Pixmap, Drawing.TextAlignment.Center, y);
        }

        public override void CheckGameState()
        {
            // nothing needs to be implemented
        }

        public override void EndGame()
        {
            // nothing needs to be implemented
        }

        protected override void StartNewGame()
        {
            base.StartNewGame();
        }

    }
}

