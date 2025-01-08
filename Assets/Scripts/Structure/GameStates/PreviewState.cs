using Gameplay;
using UI;

namespace Structure.GameStates
{
	public class PreviewState : IGameState
	{
		private readonly IStateSwitcher _stateSwitcher;
		private readonly Screen _screen;

		public PreviewState(IStateSwitcher stateSwitcher, Screen screen)
		{
			_stateSwitcher = stateSwitcher;
			_screen = screen;
		}

		public void Enter()
		{
			DeviceGyro.DisableGyro();

			_screen.ShowPreviewScreen(OnPreviewEnded);
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