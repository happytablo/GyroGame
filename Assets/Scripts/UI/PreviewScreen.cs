using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PreviewScreen : MonoBehaviour
	{
		[SerializeField] private TMP_Text _countdownText;
		[SerializeField] private GameObject _infoView;

		public void Enable()
		{
			gameObject.SetActive(true);
			_infoView.SetActive(true);
			_countdownText.gameObject.SetActive(false);
		}

		public void Disable()
		{
			gameObject.SetActive(false);
		}

		public IEnumerator StartCountdownCoroutine()
		{
			_infoView.SetActive(false);
			_countdownText.text = "3";
			var oneSec = new WaitForSeconds(1);

			_countdownText.gameObject.SetActive(true);
			yield return oneSec;

			_countdownText.text = "2";
			yield return oneSec;

			_countdownText.text = "1";
			yield return oneSec;

			_countdownText.text = "START";
			yield return oneSec;

			_countdownText.gameObject.SetActive(false);
			_infoView.SetActive(true);
		}
	}
}