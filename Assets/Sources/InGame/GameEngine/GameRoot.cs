using UnityEngine;

namespace Casual
{
	public interface IGameRoot
	{
		void Dispose();
	}

	public class GameRoot : MonoBehaviour, IGameRoot
	{
		private void Awake()
		{
			DontDestroyOnLoad(this);		
		}

		public void Dispose()
		{
			Destroy(gameObject);
		}
	}
}