using System;
using UnityEngine;

namespace Gameplay
{
	public class Sunbeam : MonoBehaviour
	{
		private const string BatteryTag = "Battery";

		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private LayerMask _cloudLayer;
		[SerializeField] private LayerMask _panelLayer;
		[SerializeField] private float _raycastLength = 20f;

		public event Action HitBatteryPanel;

		private readonly Collider[] _colliders = new Collider[1];

		private Vector3 _sunDirection;

		private void Start()
		{
			Bounds bounds = _spriteRenderer.bounds;
			Vector3 center = bounds.center;
			Vector3 bottomCenter = new Vector3(0, bounds.min.y, 0);
			var worldPoint = transform.TransformPoint(bottomCenter);
			_sunDirection = (center - worldPoint).normalized;
		}

		private void Update()
		{
			bool isCovered = IsCoveredByClouds();
			_spriteRenderer.enabled = !isCovered;

			if (!isCovered)
			{
				CheckHitPanel();
			}
		}

		private bool IsCoveredByClouds()
		{
			int count = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale, _colliders, transform.rotation, _cloudLayer);

			return count > 0;
		}

		private void CheckHitPanel()
		{
			//Debug.DrawRay(transform.position, _sunDirection * _raycastLength);

			if (Physics.Raycast(transform.position, _sunDirection, out RaycastHit hit, _raycastLength, _panelLayer))
			{
				if (hit.collider.CompareTag(BatteryTag))
				{
					HitBatteryPanel?.Invoke();
				}
			}
		}
	}
}