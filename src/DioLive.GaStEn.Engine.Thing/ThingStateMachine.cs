namespace DioLive.GaStEn.Engine.Thing
{
    public class ThingStateMachine : StateMachine
    {
        public ThingStateMachine(int players)
            : base(new PlayState(players))
        {
        }
    }
}