using System.Collections.Generic;
using Structure;
using UnityEngine;

namespace Gameplay
{
	public class BuildingsController : MonoBehaviour
	{
		[SerializeField] private List<MeshRenderer> _buildingMeshes;

		[SerializeField] private Color _grayColor;
		[SerializeField] private Color _greenColor;

		private IGameLoop _gameLoop;

		public void Init(IGameLoop gameLoop)
		{
			_gameLoop = gameLoop;

			_gameLoop.Started += SetupGrayColor;
			_gameLoop.Finished += SetupGreenColor;
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