using System.Collections;

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
			GameEngine.instance.GetPlayer().IsMovable = true;
			yield return null;
		}

		/// <summary>
		/// 이벤트 상태 돌입이므로 캐릭터 이동 불가 상태로 변경
		/// </summary>
		public IEnumerator Exit()
		{
			GameEngine.instance.GetPlayer().IsMovable = false;
			yield return null;
		}
	}
}