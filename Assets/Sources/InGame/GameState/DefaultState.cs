using System.Collections;
using VContainer;
using VContainer.Unity;

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
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.IsMovable = true;
			yield return null;
		}

		/// <summary>
		/// �̺�Ʈ ���� �����̹Ƿ� ĳ���� �̵� �Ұ� ���·� ����
		/// </summary>
		public IEnumerator Exit()
		{
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.IsMovable = false;
			yield return null;
		}
	}
}