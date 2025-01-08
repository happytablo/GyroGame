using Structure.GameStates;

namespace Structure
{
	public interface IStateSwitcher
	{
		public void ChangeState<TState>() where TState : class, IGameState;
	}
}