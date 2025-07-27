using System.Collections;

namespace Casual
{
	public interface IGameStateManager
	{
		void ChangeState(IGameState newState);
	}
}