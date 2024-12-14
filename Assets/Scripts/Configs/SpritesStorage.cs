using System.Linq;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = nameof(SpritesStorage), fileName = nameof(SpritesStorage))]
	public class SpritesStorage : ScriptableObject
	{
		[SerializeField] private DeviceSpriteMap[] _deviceSpriteMaps;

		public Sprite GetSpriteByType(DeviceType type)
		{
			return _deviceSpriteMaps.FirstOrDefault(map => map.DeviceType == type)?.Icon;
		}
	}
}