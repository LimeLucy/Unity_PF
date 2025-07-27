using System.Collections;

namespace Casual
{
	/// <summary>
	/// ��� ����
	/// </summary>
	public class DialogueState : IGameState
	{
		Scripts m_script = null;

		public DialogueState(Scripts script)
		{		
			m_script = script;		
		}

		/// <summary>
		/// ��� UI ����
		/// </summary>
		/// <returns></returns>
		public IEnumerator Enter()
		{
			yield return GameEngine.instance.GetUIDialogue().SetDialogueText(m_script);
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
