using System;
using UnityEngine;

namespace Configs
{
	[Serializable]
	public class TimingConfig
	{
		[field: SerializeField] public float PauseBetweenLevels { get; private set; }
		[field: SerializeField] public float PreviewDelay { get; private set; }
		[field: SerializeField] public float FinishViewDelay { get; private set; }
		[field: SerializeField] public float ExitToMenuDelay { get; private set; }
	}
}