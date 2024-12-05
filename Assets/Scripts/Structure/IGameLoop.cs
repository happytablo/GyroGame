using System;

namespace Structure
{
	public interface IGameLoop
	{
		event Action Finished;
		event Action Started;
	}
}