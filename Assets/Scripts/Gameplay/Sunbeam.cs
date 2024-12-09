using UnityEngine;

namespace Gameplay
{
	public class Sunbeam : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private LayerMask _cloudLayer;
		
		private readonly Collider[] _colliders = new Collider[1];

		public bool IsCovered { get; private set; }

		private void Update()
		{
			IsCovered = IsCoveredByClouds();
			_spriteRenderer.enabled = !IsCovered;
		}

		private bool IsCoveredByClouds()
		{
			int count = Physics.OverlapBoxNonAlloc(transform.position, transform.localScale, _colliders, transform.rotation, _cloudLayer);

			return count > 0;
		}
	}
}