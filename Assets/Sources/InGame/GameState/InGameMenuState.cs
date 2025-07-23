using System.Collections;

namespace Casual
{
	public class InGameMenuState : IGameState
	{
		public IEnumerator Enter()
		{
			GameEngine.instance.GetUIInGameMenu().ShowMenu();
			yield return null;
		}

		public IEnumerator Exit()
		{
			GameEngine.instance.GetUIInGameMenu().HideMenu();
			yield return null;
		}
	}
}