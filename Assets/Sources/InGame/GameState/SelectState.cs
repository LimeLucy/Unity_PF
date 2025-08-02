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
		GameSwitch m_gameSwitch;

		public SelectState(Selects select, UIMediator ui, GameSwitch gameSwitch)
		{
			m_select = select;
			m_ui = ui;
			m_gameSwitch = gameSwitch;
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
			m_gameSwitch.CheckOnOffs();
			m_ui.Choice.Hide();
			yield return null;
		}
	}
}
