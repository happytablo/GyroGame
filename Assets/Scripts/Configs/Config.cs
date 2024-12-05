using Gameplay;
using UnityEngine;

namespace Configs
{
	[CreateAssetMenu(menuName = nameof(Config), fileName = nameof(Config))]
	public class Config : ScriptableObject
	{
		[SerializeField] private CloudMover _cloudPrefab;
		[SerializeField] private Sunbeam _sunbeamPrefab;
		[Space]
		[SerializeField] private Vector2Int _movementBordersAxisX = new Vector2Int(-110, 110);
		[Header("Sunbeams")]
		[SerializeField] private float _sunbeamSpawnPosY = 8;
		[SerializeField] private float _chargeableSunbeamSpawnPosZ;
		[SerializeField] private Vector3 _sunbeamRotation = new Vector3(0, 0, -15);
		[SerializeField] private float[] _notChargeableSunbeamSpawnPosZ;
		[Header("Clouds")]
		[SerializeField] private Vector3 _cloudSpawnPos = new Vector3(0, 9, 10);
		[SerializeField] private float _cloudOffsetX = 5f;

		public CloudMover CloudPrefab => _cloudPrefab;
		public Sunbeam SunbeamPrefab => _sunbeamPrefab;
		public Vector2Int MovementBordersAxisX => _movementBordersAxisX;
		public float SunbeamSpawnPosY => _sunbeamSpawnPosY;
		public float ChargeableSunbeamSpawnPosZ => _chargeableSunbeamSpawnPosZ;
		public Vector3 SunbeamRotation => _sunbeamRotation;
		public float[] NotChargeableSunbeamSpawnPosZ => _notChargeableSunbeamSpawnPosZ;
		public Vector3 CloudSpawnPos => _cloudSpawnPos;
		public float CloudOffsetX => _cloudOffsetX;
	}
}