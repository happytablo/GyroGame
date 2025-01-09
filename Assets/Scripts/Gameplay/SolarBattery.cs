using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
	public class SolarBattery : MonoBehaviour
	{
		public event Action ValueChanged;
		public event Action Fulled;

		private float _stepPerFrame;
		private bool _isChargeable;

		private readonly List<Sunbeam> _collidedSunbeams = new List<Sunbeam>();

		public float ChargeValue { get; private set; }

		private void Update()
		{
			if (!_isChargeable)
				return;

			if (_collidedSunbeams.Any(sunbeam => !sunbeam.IsCovered))
			{
				Charge();
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!_isChargeable)
				return;

			if (other.TryGetComponent(out Sunbeam sunbeam))
			{
				if (_collidedSunbeams.Contains(sunbeam))
				{
					return;
				}

				_collidedSunbeams.Add(sunbeam);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (!_isChargeable)
				return;

			if (other.TryGetComponent(out Sunbeam sunbeam))
			{
				if (_collidedSunbeams.Contains(sunbeam))
				{
					_collidedSunbeams.Remove(sunbeam);
				}
			}
		}

		public void BeginCharging(float stepPerFrame)
		{
			_stepPerFrame = stepPerFrame;
			Reset();
			_isChargeable = true;
		}

		public void StopCharging()
		{
			if (_isChargeable)
				_isChargeable = false;
		}

		public void Cleanup()
		{
			_collidedSunbeams.Clear();
		}

		private void Charge()
		{
			if (!_isChargeable)
				return;

			ChargeValue += _stepPerFrame * Time.deltaTime;
			ValueChanged?.Invoke();

			if (ChargeValue >= 1)
			{
				StopCharging();
				Fulled?.Invoke();
			}
		}

		private void Reset()
		{
			ChargeValue = 0;
			ValueChanged?.Invoke();
		}
	}
}