using UI;

namespace Structure.GameStates
{
	public class FinishState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly ScreenManager _screenManager;

		public FinishState(IStateSwitcher stateSwitcher, ScreenManager screenManager)
		{
			_stateSwitcher = stateSwitcher;
			_screenManager = screenManager;
		}

		public void Enter()
		{
			_screenManager.ShowFinishView(OnFinishViewEnded);
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