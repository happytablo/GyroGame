using UnityEngine;

namespace Structure.GameStates
{
	public class MenuState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;

		public MenuState(IStateSwitcher stateSwitcher)
		{
			_stateSwitcher = stateSwitcher;
		}

		public void Enter()
		{
			Debug.Log($"Exit to MainMenu.");
		}

		public void Exit()
		{
		}
	}
}