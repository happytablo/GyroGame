using Configs;

namespace Structure
{
	public interface ILevelInfo
	{
		int CurrentLevelIndex { get; }
		LevelConfig CurrentLevelConfig { get; }
		bool IsLastLevel { get; }
	}
}