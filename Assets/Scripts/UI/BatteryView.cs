using Gameplay;
using UnityEngine;
using Utils;

namespace UI
{
	public class BatteryView : MonoBehaviour
	{
		[SerializeField] private SlicedFilledImage _filledImage;

		private SolarBattery _solarBattery;
		private bool _isSubscribed;

		public void Init(SolarBattery solarBattery)
		{
			_solarBattery = solarBattery;
			
			UpdateView();
			Subscribe();
		}

		private void OnEnable()
		{
			if (_solarBattery != null)
			{
				UpdateView();
				Subscribe();
			}
		}

		private void Subscribe()
		{
			if (!_isSubscribed)
			{
				_solarBattery.ValueChanged += UpdateView;
				_isSubscribed = true;
			}
		}

		private void OnDisable()
		{
			if (_isSubscribed)
			{
				_solarBattery.ValueChanged -= UpdateView;
				_isSubscribed = false;
			}
		}

		private void UpdateView()
		{
			if (_filledImage != null)
			{
				_filledImage.fillAmount = _solarBattery.ChargeValue / 1;
			}
		}
	}
}