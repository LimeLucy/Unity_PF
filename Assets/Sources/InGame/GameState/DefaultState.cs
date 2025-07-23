using System.Collections;

namespace Casual 
{
	// ĳ���� �̵� ����
	public class DefaultState : IGameState
	{
		/// <summary>
		/// ĳ���� �̵� ���� ���·� ����
		/// </summary>
		public IEnumerator Enter()
		{
			GameEngine.instance.GetPlayer().IsMovable = true;
			yield return null;
		}

		/// <summary>
		/// �̺�Ʈ ���� �����̹Ƿ� ĳ���� �̵� �Ұ� ���·� ����
		/// </summary>
		public IEnumerator Exit()
		{
			GameEngine.instance.GetPlayer().IsMovable = false;
			yield return null;
		}
	}
}