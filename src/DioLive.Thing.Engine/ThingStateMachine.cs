using DioLive.GaStEn.Engine;

namespace DioLive.Thing.Engine
{
    public class ThingStateMachine : StateMachine
    {
        public ThingStateMachine(int players)
            : base(new PlayState(players))
        {
        }
    }
}