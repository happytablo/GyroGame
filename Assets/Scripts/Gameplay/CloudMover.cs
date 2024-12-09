using UnityEngine;

namespace Gameplay
{
	public class CloudMover : MonoBehaviour
	{
		[SerializeField] private float _speed;
		
		private float _rightPosX;
		private bool _inited;

		public void Init(float rightPosX)
		{
			_rightPosX = rightPosX;

			_inited = true;
		}

		void Update()
		{
			if (!_inited)
				return;

			transform.Translate(Vector3.left * _speed * Time.deltaTime);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out LeftBoundTrigger trigger))
			{
				transform.position = new Vector3(_rightPosX, transform.position.y, transform.position.z);
			}
		}
	}
}