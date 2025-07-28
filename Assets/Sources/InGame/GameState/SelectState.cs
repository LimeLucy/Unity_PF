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
		readonly IChoiceObjectController m_choiceController;

		public SelectState(Selects select, UIMediator ui, IChoiceObjectController choiceController)
		{
			m_select = select;
			m_ui = ui;
			m_choiceController = choiceController;
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
			m_choiceController.UpdateObjects();
			m_ui.Choice.Hide();
			yield return null;
		}
	}
}
