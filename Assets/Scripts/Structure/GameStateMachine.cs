﻿using System;
using System.Collections.Generic;
using Configs;
using Structure.GameStates;
using UI;

namespace Structure
{
	public class GameStateMachine : IStateSwitcher
	{
		private readonly Dictionary<Type, IGameState> _states;
		private IGameState _currentState;

		public GameStateMachine(Config config, Screen screen, GameplayManager gameplayManager, ICoroutineRunner coroutineRunner)
		{
			_states = new Dictionary<Type, IGameState>
			{
				[typeof(PreviewState)] = new PreviewState(this, screen),
				[typeof(GameLoopState)] = new GameLoopState(this, screen, config, gameplayManager, coroutineRunner),
				[typeof(FinishState)] = new FinishState(this, screen),
				[typeof(MenuState)] = new MenuState(this)
			};
		}

		public void ChangeState<TState>() where TState : class, IGameState
		{
			_currentState?.Exit();
			var state = _states[typeof(TState)] as TState;
			_currentState = state;
			state?.Enter();
		}
	}
}