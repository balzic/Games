using GameConcepts;

namespace GameEngine
{
    public abstract class GameFactory
    {
        protected Drawing.GameArea _area;

        public GameFactory(Drawing.GameArea area)
        {
            _area = area;
        }

        public abstract Game Game();
    }

    public class GalagaGameFactory : GameFactory
    {
        public GalagaGameFactory(Drawing.GameArea area)
            : base(area)
        {
            // All the work is done in the base-class constructor.
        }

        public override Game Game()
        {
            return GalagaGame.GalagaGame.GetGame(_area);
        }
    }

    public class SpaceTaxiFactory : GameFactory
    {
        public SpaceTaxiFactory(Drawing.GameArea area) : base(area)
        {

        }

        public override Game Game()
        {
            return SpaceTaxi.SpaceTaxiGame.GetGame(_area);
        }
    }
}