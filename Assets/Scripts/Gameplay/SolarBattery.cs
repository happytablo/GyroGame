using System;
using UnityEngine;

namespace Gameplay
{
	public class SolarBattery : MonoBehaviour
	{
		public event Action ValueChanged;
		public event Action Fulled;
		
		private ISunbeamsProvider _sunbeamsProvider;
		private float _stepPerFrame;
		private bool _isChargeable;

		public float ChargeValue { get; private set; }

		public void Init(ISunbeamsProvider sunbeamsProvider)
		{
			_sunbeamsProvider = sunbeamsProvider;
		}

		public void Subscribe(float stepPerFrame)
		{
			_stepPerFrame = stepPerFrame;
			Reset();

			foreach (Sunbeam sunbeam in _sunbeamsProvider.Sunbeams)
			{
				sunbeam.HitBatteryPanel += OnBatteryHit;
			}

			_isChargeable = true;
		}

		private void OnBatteryHit()
		{
			if (!_isChargeable)
				return;

			ChargeValue += _stepPerFrame * Time.deltaTime;
			ValueChanged?.Invoke();

			if (ChargeValue >= 1)
			{
				Fulled?.Invoke();
				_isChargeable = false;
			}
		}

		public void Unsubscribe()
		{
			foreach (Sunbeam sunbeam in _sunbeamsProvider.Sunbeams)
			{
				sunbeam.HitBatteryPanel -= OnBatteryHit;
			}

			_isChargeable = false;
		}

		private void Reset()
		{
			ChargeValue = 0;
		}
	}
}