using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	/// <summary>
	/// ���� ���� �� ���� ����
	/// </summary>
	public class StateGame : IState
	{
		string m_RootSceneName = "2_GameRootScene";		
		
		bool m_isLoadSaved = false;
		bool m_isNewSet = true;
		string m_strSceneName;

		public StateGame(bool isNew, bool isLoadSaved = false, string strSceneName = "3_GameScene")
		{
			m_isNewSet = isNew;
			m_isLoadSaved = isLoadSaved;
			m_strSceneName = strSceneName;
		}

		public IEnumerator Load()
		{			
			// Root �ε�
			if(!_IsMainSceneLoaded())
			{
				var async = SceneManager.LoadSceneAsync(m_RootSceneName);
				while (!async.isDone)
				{
					yield return null;
				}
			}			

			// ���� ȭ�� ���� �� ���� ���°� ���� �� �����Ƿ� ��� ����
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			var gameSwitch = container.Resolve<GameSwitch>();
			var saveLoadManager = container.Resolve<ISaveLoadManager>();

			var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
			var screenMask = gameContainer.Resolve<IScreenMaskController>();			
			var gameStateManager = gameContainer.Resolve<IGameStateManager>();

			// ���� �� ����
			string strCurSceneName = saveLoadManager.GetCurrentSceneName();
			if (strCurSceneName != null)
			{
				SceneManager.UnloadSceneAsync(strCurSceneName);
				gameSwitch.SetArrCheckOnOff(null);
			}

			screenMask.ShowMask();
			// ���ο� �� �ε�
			var gameAsync = SceneManager.LoadSceneAsync(m_strSceneName, LoadSceneMode.Additive);
			while (!gameAsync.isDone)
			{
				yield return null;
			}

			Scene loadedScene = SceneManager.GetSceneByName(m_strSceneName);
			if (loadedScene.IsValid() && loadedScene.isLoaded)
			{
				SceneManager.SetActiveScene(loadedScene);
			}

			if (m_isNewSet)
			{
				gameSwitch.ResetAllSwitches();  // �����ϱ�� ����ġ ����
				saveLoadManager.ApplySpawnPosition(true, m_strSceneName);
			}
			else if(m_isLoadSaved)
			{
				saveLoadManager.Load();    // �̾��ϱ�� ����ġ �ε�
				saveLoadManager.ApplySpawnPosition(false, m_strSceneName);
			}
			else  // �� �� �ܼ� �� �̵��� ĳ���� ��ġ ����
			{
				saveLoadManager.ApplySpawnPosition(true, m_strSceneName);
			}

			saveLoadManager.SetCurrentSceneName(m_strSceneName);
			yield return new WaitForSeconds(0.5f);

			gameSwitch.CheckOnOffs();
			screenMask.HideMask();

			gameStateManager.ChangeState(new DefaultState());
		}		

		public IEnumerator Destroy()
		{			
			yield return null;
		}

		/// <summary>
		/// GameRoot ���� �ε�Ǿ� �ִ��� üũ 
		/// </summary>
		/// <returns></returns>
		bool _IsMainSceneLoaded()
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene loadedScene = SceneManager.GetSceneAt(i);
				if (loadedScene.name == m_RootSceneName)
					return true;
			}
			return false;
		}
	}
}
