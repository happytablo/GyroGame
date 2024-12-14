using System;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DeviceType = Configs.DeviceType;

namespace UI
{
	public class ChargeableDeviceView : MonoBehaviour
	{
		[SerializeField] private SpritesStorage _spritesStorage;
		[SerializeField] private DeviceType _deviceType;
		[SerializeField] private Image _deviceImage;
		[SerializeField] private TMP_Text _amountText;
		
		public DeviceType DeviceType => _deviceType;

		public void UpdateView(int amount)
		{
			var sprite = _spritesStorage.GetSpriteByType(_deviceType);
			_deviceImage.sprite = sprite;
			_amountText.text = $"{amount} {GetName()}";
		}

		private string GetName()
		{
			switch (_deviceType)
			{
				case DeviceType.Microwave:
					return "mikrovlnky";
				case DeviceType.Lamp:
					return "světel";
				case DeviceType.ElectroCar:
					return "electro aut";
				case DeviceType.Devices:
					return "zařízení";
				case DeviceType.WashMachine:
					return "praček";
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}