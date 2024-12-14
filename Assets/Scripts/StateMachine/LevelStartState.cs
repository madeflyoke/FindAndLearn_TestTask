using StateMachine.Interfaces;
using VContainer;

namespace StateMachine
{
    public class LevelStartState : InitialLevelStartState //not so elegant, but for now conditions its ok
    {
        public LevelStartState(IStateMachine stateMachine, IObjectResolver resolver) : base(stateMachine, resolver)
        {
        }
    }
}
