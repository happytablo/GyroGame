using System;
using Configs;

namespace Structure
{
	public interface IGameLoop
	{
		event Action Finished;
		event Action Started;
		event Action<bool> LevelFinished;
		int CurrentLevelIndex { get; }
		LevelConfig CurrentLevelConfig { get; }
		bool IsLastLevel { get; }
	}
}