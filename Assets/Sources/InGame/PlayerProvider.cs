using UnityEngine;

namespace Casual
{
	public interface IPlayerProvider
	{
		PlayerController GetPlayer();
	}

	public class PlayerProvider : MonoBehaviour, IPlayerProvider
	{
		[SerializeField]
		PlayerController m_player = null;

		public PlayerController GetPlayer() => m_player;
	}
}