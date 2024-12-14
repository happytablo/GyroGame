using System;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class DeviceSpriteMap
	{
		[field: SerializeField] public DeviceType DeviceType { get; private set; }
		[field: SerializeField] public Sprite Icon { get; private set; }
	}
}