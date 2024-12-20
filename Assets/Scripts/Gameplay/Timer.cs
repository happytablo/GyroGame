﻿using System;
using UnityEngine;

namespace Gameplay
{
	public class Timer : MonoBehaviour
	{
		public event Action Ticked;
		public event Action Finished;

		public float RemainingTime { get; private set; }
		private bool IsPaused { get; set; }

		public void Update()
		{
			if (IsPaused)
				return;

			RemainingTime -= Time.deltaTime;
			Ticked?.Invoke();

			if (RemainingTime <= 0)
			{
				Stop();
			}
		}

		public void StartTimer(float time)
		{
			RemainingTime = time;
			IsPaused = false;
		}

		public void Pause(bool isPaused)
		{
			IsPaused = isPaused;
		}

		public void Stop()
		{
			IsPaused = true;
			Finished?.Invoke();
		}
	}
}