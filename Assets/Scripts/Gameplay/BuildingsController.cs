using System.Collections.Generic;
using System.Linq;
using Structure;
using UnityEngine;

namespace Gameplay
{
	public class BuildingsController : MonoBehaviour
	{
		[SerializeField] private Color _grayColor;
		[SerializeField] private Color _greenColor;

		private List<MeshRenderer> _buildingMeshes;
		private IGameLoop _gameLoop;
		private ISolarBattery _solarBattery;

		public void Init(IGameLoop gameLoop, ISolarBattery solarBattery)
		{
			_gameLoop = gameLoop;
			_solarBattery = solarBattery;

			_gameLoop.Started += SetupGrayColor;
			_gameLoop.Finished += SetupGreenColor;
		}

		private void Awake()
		{
			_buildingMeshes = GetComponentsInChildren<MeshRenderer>().ToList();
		}

		private void OnDestroy()
		{
			if (_gameLoop != null)
			{
				_gameLoop.Started -= SetupGrayColor;
				_gameLoop.Finished -= SetupGreenColor;
			}
		}

		private void SetupGreenColor()
		{
			if (!_solarBattery.IsCharged)
				return;

			foreach (MeshRenderer buildingMesh in _buildingMeshes)
			{
				buildingMesh.material.color = _greenColor;
			}
		}

		private void SetupGrayColor()
		{
			foreach (MeshRenderer buildingMesh in _buildingMeshes)
			{
				buildingMesh.material.color = _grayColor;
			}
		}
	}
}