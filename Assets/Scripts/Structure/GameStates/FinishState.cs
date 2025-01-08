using UI;

namespace Structure.GameStates
{
	public class FinishState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly Screen _screen;

		public FinishState(IStateSwitcher stateSwitcher, Screen screen)
		{
			_stateSwitcher = stateSwitcher;
			_screen = screen;
		}

		public void Enter()
		{
			_screen.ShowFinishView(OnFinishViewEnded);
		}

		public void Exit()
		{
		}

		private void OnFinishViewEnded()
		{
			_stateSwitcher.ChangeState<MenuState>();
		}
	}
}