﻿namespace Structure
{
	public interface IGameplayManager : IGameLoop
	{
		void RestartGame();
		void LoadNextLevel();
		int CurrentLevelIndex { get; }
	}
}