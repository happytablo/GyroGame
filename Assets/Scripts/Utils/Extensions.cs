using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
	public static class Extensions
	{
		public static T GetRandomElement<T>(this IEnumerable<T> array)
		{
			var enumerable = array as T[] ?? array.ToArray();
			if (!enumerable.Any())
			{
				return default;
			}

			var index = Random.Range(0, enumerable.Count());
			return enumerable.ToArray()[index];
		}
	}
}