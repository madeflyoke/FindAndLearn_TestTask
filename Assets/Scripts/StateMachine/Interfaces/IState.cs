namespace StateMachine.Interfaces
{
    public interface IState
    {
        public void Enter();
        public void Exit();
    }
}