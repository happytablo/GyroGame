using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class LevelFinishView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _text;

		private Coroutine _coroutine;

		private void Awake()
		{
			_text.gameObject.SetActive(false);
		}

		public void Show(int levelNumber, float pauseBetweenLevelsDuration, bool isWon)
		{
			_text.text = isWon ? $"Congrats! Level {levelNumber} completed!" : "Time's up! Try again!";
			StartCoroutine(FadeCoroutine(pauseBetweenLevelsDuration));
		}

		private IEnumerator FadeCoroutine(float pauseBetweenLevelsDuration)
		{
			_text.gameObject.SetActive(true);

			float elapsedTime = 0;
			while (elapsedTime < pauseBetweenLevelsDuration)
			{
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			_text.gameObject.SetActive(false);
		}
	}
}