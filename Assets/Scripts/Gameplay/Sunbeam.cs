using System.Collections;
using UnityEngine;

namespace Gameplay
{
	public class Sunbeam : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private LayerMask _cloudLayer;

		private readonly Collider[] _colliders = new Collider[1];

		public bool IsCovered { get; private set; }
		private Coroutine _fadeCoroutine;

		private void Update()
		{
			bool newIsCovered = IsCoveredByClouds();

			if (newIsCovered != IsCovered)
			{
				IsCovered = newIsCovered;

				if (_fadeCoroutine != null)
					StopCoroutine(_fadeCoroutine);

				_fadeCoroutine = StartCoroutine(FadeSprite(!IsCovered, 0.2f));
			}
		}

		private bool IsCoveredByClouds()
		{
			int count = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale, _colliders, transform.rotation, _cloudLayer);
			return count > 0;
		}

		private IEnumerator FadeSprite(bool show, float duration)
		{
			float startAlpha = _spriteRenderer.color.a;
			float endAlpha = show ? 1f : 0f;
			float elapsedTime = 0f;

			Color color = _spriteRenderer.color;

			while (elapsedTime < duration)
			{
				elapsedTime += Time.deltaTime;
				color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
				_spriteRenderer.color = color;
				yield return null;
			}

			color.a = endAlpha;
			_spriteRenderer.color = color;
			_spriteRenderer.enabled = show;
		}
	}
}