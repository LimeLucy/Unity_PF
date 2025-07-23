using System.Collections;

namespace Casual
{
	/// <summary>
	/// 선택지 상태
	/// </summary>
	public class SelectState : IGameState
	{
		public Selects m_select;

		public SelectState(Selects select)
		{
			m_select = select;
		}

		/// <summary>
		/// 선택지 UI 셋팅
		/// </summary>
		public IEnumerator Enter()
		{
			GameEngine.instance.GetUISelect().SetSelectText(this);
			yield return null;
		}

		/// <summary>
		/// 선택지 UI 숨김 및 선택지 상태에 따라 변경될 게임 오브젝트들 변경
		/// </summary>
		public IEnumerator Exit()
		{
			GameEngine.instance.SetObjects();
			GameEngine.instance.GetUISelect().HideSelectUI();
			yield return null;
		}
	}
}
