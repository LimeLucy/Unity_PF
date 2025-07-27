using System.Collections;

namespace Casual
{
	/// <summary>
	/// 대사 상태
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
		/// 대사 UI 셋팅
		/// </summary>
		/// <returns></returns>
		public IEnumerator Enter()
		{
			yield return m_ui.Dialogue.Show(m_script);
		}

		/// <summary>
		/// 대사 UI 숨김, 대사 사용 이벤트 스위치 ON
		/// </summary>
		/// <returns></returns>

		public IEnumerator Exit()
		{
			yield return null;
		}
	}
}
