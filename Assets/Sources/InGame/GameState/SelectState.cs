using System.Collections;

namespace Casual
{
	/// <summary>
	/// ������ ����
	/// </summary>
	public class SelectState : IGameState
	{
		public Selects m_select;

		public SelectState(Selects select)
		{
			m_select = select;
		}

		/// <summary>
		/// ������ UI ����
		/// </summary>
		public IEnumerator Enter()
		{
			GameEngine.instance.GetUISelect().SetSelectText(this);
			yield return null;
		}

		/// <summary>
		/// ������ UI ���� �� ������ ���¿� ���� ����� ���� ������Ʈ�� ����
		/// </summary>
		public IEnumerator Exit()
		{
			GameEngine.instance.SetObjects();
			GameEngine.instance.GetUISelect().HideSelectUI();
			yield return null;
		}
	}
}
