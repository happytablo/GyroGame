using Gameplay;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = nameof(Config), fileName = nameof(Config))]
	public class Config : ScriptableObject
	{
		[SerializeField] private CloudMover[] _cloudPrefabs;
		[SerializeField] private Sunbeam _sunbeamPrefab;
		[Header("Levels")]
		[SerializeField] private LevelConfigsStorage _levelConfigsStorage;
		[SerializeField] private TimingConfig _timingConfig;
		[Space]
		[SerializeField] private Vector2Int _movementBordersAxisX = new Vector2Int(-110, 110);
		[Header("Sunbeams")]
		[SerializeField] private Vector3 _sunbeamSpawnPos = new Vector3(0, 8, 11);
		[SerializeField] private Vector3 _sunbeamRotation = new Vector3(0, 0, -15);
		[Header("Clouds")]
		[SerializeField] private float _cloudOffsetX = 5f;

		public Sunbeam SunbeamPrefab => _sunbeamPrefab;
		public Vector2Int MovementBordersAxisX => _movementBordersAxisX;
		public Vector3 SunbeamSpawnPos => _sunbeamSpawnPos;
		public Vector3 SunbeamRotation => _sunbeamRotation;
		public float CloudOffsetX => _cloudOffsetX;
		public CloudMover[] CloudPrefabs => _cloudPrefabs;
		public LevelConfigsStorage LevelsConfigsStorage => _levelConfigsStorage;
		public TimingConfig TimingConfig => _timingConfig;
	}
}