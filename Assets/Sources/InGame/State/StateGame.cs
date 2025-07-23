using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Casual
{
	/// <summary>
	/// ���� ���� �� ���� ����
	/// </summary>
	public class StateGame : IState
	{
		private string m_sceneName = "2_GameScene";

		bool m_isNewSet = true;

		public StateGame(bool isNew)
		{
			m_isNewSet = isNew;
		}

		public IEnumerator Load()
		{
			var async = SceneManager.LoadSceneAsync(m_sceneName);
			while (!async.isDone)
			{
				yield return null;
			}		

			// ���� ȭ�� ���� �� ���� ���°� ���� �� �����Ƿ� ��� ����
			GameEngine.instance.m_goHideScreen?.SetActive(true);

			// �ʱ� ����
			if(m_isNewSet)
				MainManager.instance.gameSwitch.ResetAllSwitches();	// �����ϱ�� ����ġ ����
			else
				MainManager.instance.saveLoadManager.Load();	// �̾��ϱ�� ����ġ �ε�
			GameEngine.instance.SetObjects();	// ����ġ�� ���� ������Ʈ ����
			yield return new WaitForSeconds(0.5f);			

			GameEngine.instance.m_goHideScreen?.SetActive(false);

			GameStateManager.instance.ChangeState(new DefaultState());
		}

		public IEnumerator Destroy()
		{
			yield return null;
		}
	}
}
