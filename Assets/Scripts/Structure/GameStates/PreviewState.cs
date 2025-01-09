using Gameplay;
using UI;

namespace Structure.GameStates
{
	public class PreviewState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly ScreenManager _screenManager;

		public PreviewState(IStateSwitcher stateSwitcher, ScreenManager screenManager)
		{
			_stateSwitcher = stateSwitcher;
			_screenManager = screenManager;
		}

		public void Enter()
		{
			_screenManager.ShowPreviewScreen(OnPreviewEnded);
		}

		public void Exit()
		{
		}

		private void OnPreviewEnded()
		{
			_stateSwitcher.ChangeState<GameLoopState>();
		}
	}
}