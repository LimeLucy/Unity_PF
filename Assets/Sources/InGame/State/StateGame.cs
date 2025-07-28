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

			// 게임 화면 돌입 시 셋팅 상태가 보일 수 있으므로 잠시 숨김
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			var gameSwitch = container.Resolve<GameSwitch>();
			var saveLoadManager = container.Resolve<ISaveLoadManager>();

			var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
			var screenMask = gameContainer.Resolve<IScreenMaskController>();
			var choiceController = gameContainer.Resolve<IChoiceObjectController>();
			var gameStateManager = gameContainer.Resolve<IGameStateManager>();
			screenMask.ShowMask();

			// 초기 셋팅
			if (m_isNewSet)
				gameSwitch.ResetAllSwitches();	// 새로하기시 스위치 리셋
			else
				saveLoadManager.Load();    // 이어하기시 스위치 로드
			choiceController.UpdateObjects();   // 스위치에 따른 오브젝트 셋팅
			yield return new WaitForSeconds(0.5f);

			screenMask.HideMask();

			gameStateManager.ChangeState(new DefaultState());
		}

		public IEnumerator Destroy()
		{
			yield return null;
		}
	}
}
