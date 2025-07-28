using System.Collections;
using VContainer;
using VContainer.Unity;

namespace Casual 
{
	// 캐릭터 이동 상태
	public class DefaultState : IGameState
	{
		/// <summary>
		/// 캐릭터 이동 가능 상태로 변경
		/// </summary>
		public IEnumerator Enter()
		{
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.IsMovable = true;
			yield return null;
		}

		/// <summary>
		/// 이벤트 상태 돌입이므로 캐릭터 이동 불가 상태로 변경
		/// </summary>
		public IEnumerator Exit()
		{
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.IsMovable = false;
			yield return null;
		}
	}
}