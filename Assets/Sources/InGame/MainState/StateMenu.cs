using System.Collections;
using UnityEngine.SceneManagement;

namespace Casual
{
	/// <summary>
	/// 메인 상태 중 메뉴 상태
	/// </summary>
	public class StateMenu : IState
	{
		private string m_sceneName = "1_MenuScene";

		public IEnumerator Load()
		{
			var async = SceneManager.LoadSceneAsync(m_sceneName);
			while(!async.isDone)
			{
				yield return null;
			}
		}

		public IEnumerator Destroy()
		{
			yield return null;
		}
	}
}
