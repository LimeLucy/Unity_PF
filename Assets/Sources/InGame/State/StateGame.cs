using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	/// <summary>
	/// 메인 상태 중 게임 상태
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
			// Root 로드
			if(!IsMainSceneLoaded())
			{
				var async = SceneManager.LoadSceneAsync(m_RootSceneName);
				while (!async.isDone)
				{
					yield return null;
				}
			}
			

			// 게임 화면 돌입 시 셋팅 상태가 보일 수 있으므로 잠시 숨김
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			var gameSwitch = container.Resolve<GameSwitch>();
			var saveLoadManager = container.Resolve<ISaveLoadManager>();

			var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
			var screenMask = gameContainer.Resolve<IScreenMaskController>();			
			var gameStateManager = gameContainer.Resolve<IGameStateManager>();

			string strCurSceneName = saveLoadManager.GetCurrentSceneName();
			if (strCurSceneName != null)
			{
				SceneManager.UnloadSceneAsync(strCurSceneName);
				gameSwitch.SetArrCheckOnOff(null);
			}

			screenMask.ShowMask();
			var gameAsync = SceneManager.LoadSceneAsync(m_strSceneName, LoadSceneMode.Additive);
			while (!gameAsync.isDone)
			{
				yield return null;
			}

			if (m_isNewSet)
			{
				gameSwitch.ResetAllSwitches();  // 새로하기시 스위치 리셋
				saveLoadManager.ApplySpawnPosition(true, m_strSceneName);
			}
			else if(m_isLoadSaved)
			{
				saveLoadManager.Load();    // 이어하기시 스위치 로드
				saveLoadManager.ApplySpawnPosition(false, m_strSceneName);
			}
			else
			{
				saveLoadManager.ApplySpawnPosition(true, m_strSceneName);
			}

			saveLoadManager.SetCurrentSceneName(m_strSceneName);
			yield return new WaitForSeconds(0.5f);

			gameSwitch.CheckOnOffs();
			screenMask.HideMask();

			gameStateManager.ChangeState(new DefaultState());
		}

		bool IsMainSceneLoaded()
		{
			for(int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene loadedScene = SceneManager.GetSceneAt(i);
				if(loadedScene.name == m_RootSceneName)
					return true;
			}
			return false;
		}

		public IEnumerator Destroy()
		{			
			yield return null;
		}
	}
}
