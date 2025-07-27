using System.Collections;

namespace Casual
{
	/// <summary>
	/// 선택지 상태
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
		/// 선택지 UI 셋팅
		/// </summary>
		public IEnumerator Enter()
		{
			m_ui.Choice.Show(this);
			yield return null;
		}

		/// <summary>
		/// 선택지 UI 숨김 및 선택지 상태에 따라 변경될 게임 오브젝트들 변경
		/// </summary>
		public IEnumerator Exit()
		{
			m_gameEngine.SetObjects();
			m_ui.Choice.Hide();
			yield return null;
		}
	}
}
