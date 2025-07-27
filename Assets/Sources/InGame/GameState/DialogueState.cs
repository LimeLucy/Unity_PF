using System.Collections;

namespace Casual
{
	/// <summary>
	/// ��� ����
	/// </summary>
	public class DialogueState : IGameState
	{
		Scripts m_script = null;
		UIMediator m_ui = null;

		public DialogueState(Scripts script, UIMediator ui)
		{
			m_script = script;
			m_ui = ui;
		}

		/// <summary>
		/// ��� UI ����
		/// </summary>
		/// <returns></returns>
		public IEnumerator Enter()
		{
			yield return m_ui.Dialogue.Show(m_script);
		}

		/// <summary>
		/// ��� UI ����, ��� ��� �̺�Ʈ ����ġ ON
		/// </summary>
		/// <returns></returns>

		public IEnumerator Exit()
		{
			yield return null;
		}
	}
}
