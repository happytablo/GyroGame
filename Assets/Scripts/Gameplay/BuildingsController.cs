using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
	public class BuildingsController : MonoBehaviour
	{
		private List<MeshRenderer> _buildingMeshes;

		private void Awake()
		{
			_buildingMeshes = GetComponentsInChildren<MeshRenderer>().ToList();
		}

		public void UpdateBuildingsColor(Color color)
		{
			foreach (MeshRenderer buildingMesh in _buildingMeshes)
			{
				buildingMesh.material.color = color;
			}
		}
	}
}