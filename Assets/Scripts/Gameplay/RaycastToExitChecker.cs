using System;
using UnityEngine;

namespace Gameplay
{
	public class RaycastToExitChecker : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private LayerMask _targetLayer;

		public event Action TriggerToMenu;

		private ExitToMenuTrigger _exitToMenuTrigger;
		private bool _isRaycasting;

		private void Awake()
		{
			_isRaycasting = true;
		}

		private void Update()
		{
			if (!_isRaycasting)
				return;

			Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _targetLayer))
			{
				_exitToMenuTrigger = hit.collider.GetComponent<ExitToMenuTrigger>();

				_exitToMenuTrigger.Increase();
				if (_exitToMenuTrigger.Activated)
				{
					_isRaycasting = false;
					TriggerToMenu?.Invoke();
				}
			}
			else
			{
				if (_exitToMenuTrigger != null)
				{
					_exitToMenuTrigger.Reset();
					_exitToMenuTrigger = null;
				}
			}
		}
	}
}