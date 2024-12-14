using System.Collections;
using UnityEngine;

namespace Structure
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator routine);
		void StopCoroutine(Coroutine routine);
	}
}