using System.Collections;

namespace Casual
{
	public class InGameMenuState : IGameState
	{
		readonly UIMediator m_ui;
		public InGameMenuState(UIMediator ui)
		{
			m_ui = ui;
		}

		public IEnumerator Enter()
		{
			m_ui.Menu.Show();
			yield return null;
		}

		public IEnumerator Exit()
		{
			m_ui.Menu.Hide();
			yield return null;
		}
	}
}