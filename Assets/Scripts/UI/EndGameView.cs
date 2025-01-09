using Structure;
using UnityEngine;

namespace UI
{
	public class EndGameView : MonoBehaviour
	{
		[SerializeField] private ChargeableDeviceView[] _chargeableDeviceViews;
		[SerializeField] private LevelProgressView _levelProgressView;

		private ILevelInfo _levelInfo;
		private bool _isSubscribed;

		public void Init(ILevelInfo levelInfo)
		{
			_levelInfo = levelInfo;
			_levelProgressView.Init(levelInfo);
		}

		public void UpdateView()
		{
			_levelProgressView.UpdateView();

			foreach (var chargeableDeviceView in _chargeableDeviceViews)
			{
				var deviceInfo = _levelInfo.CurrentLevelConfig.Devices.GetDeviceInfo(chargeableDeviceView.DeviceType);
				chargeableDeviceView.UpdateView(deviceInfo.Amount);
			}
		}
	}
}