using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = nameof(LevelConfigsStorage), fileName = nameof(LevelConfigsStorage))]
	public class LevelConfigsStorage : ScriptableObject
	{
		[SerializeField] private List<LevelConfig> _levelConfigs;

		public IReadOnlyList<LevelConfig> LevelConfigs => _levelConfigs;

		public LevelConfig GetLevelByIndex(int index)
		{
			if (LevelConfigs.Count > index)
			{
				return LevelConfigs[index];
			}

			return null;
		}

		public bool HasNextLevel(int currentLevelIndex)
		{
			return LevelConfigs.Count - 1 > currentLevelIndex;
		}
	}
}