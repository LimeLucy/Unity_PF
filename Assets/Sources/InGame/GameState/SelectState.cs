using System.Collections;

namespace Casual
{
	/// <summary>
	/// ������ ����
	/// </summary>
	public class SelectState : IGameState
	{
		public Selects m_select;
		readonly UIMediator m_ui;
		readonly IGameEngine m_gameEngine;

		public SelectState(Selects select, UIMediator ui, IGameEngine gameEngine)
		{
			m_select = select;
			m_ui = ui;
			m_gameEngine = gameEngine;
		}

		/// <summary>
		/// ������ UI ����
		/// </summary>
		public IEnumerator Enter()
		{
			m_ui.Choice.Show(this);
			yield return null;
		}

		/// <summary>
		/// ������ UI ���� �� ������ ���¿� ���� ����� ���� ������Ʈ�� ����
		/// </summary>
		public IEnumerator Exit()
		{
			m_gameEngine.SetObjects();
			m_ui.Choice.Hide();
			yield return null;
		}
	}
}
