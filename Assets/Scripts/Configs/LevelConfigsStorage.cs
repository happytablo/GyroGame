using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = nameof(LevelConfigsStorage), fileName = nameof(LevelConfigsStorage))]
	public class LevelConfigsStorage : ScriptableObject
	{
		[SerializeField] private List<LevelConfig> _levelConfigs;

		public LevelConfig GetLevelByIndex(int index)
		{
			if (_levelConfigs.Count > index)
			{
				return _levelConfigs[index];
			}

			return null;
		}
	}
}