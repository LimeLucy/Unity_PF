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
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			var gameSwitch = container.Resolve<GameSwitch>();
			var saveLoadManager = container.Resolve<ISaveLoadManager>();

			var gameContainer = LifetimeScope.Find<GameLifetimeScope>().Container;
			var screenMask = gameContainer.Resolve<IScreenMaskController>();
			var choiceController = gameContainer.Resolve<IChoiceObjectController>();
			var gameStateManager = gameContainer.Resolve<IGameStateManager>();
			screenMask.ShowMask();

			// �ʱ� ����
			if (m_isNewSet)
				gameSwitch.ResetAllSwitches();	// �����ϱ�� ����ġ ����
			else
				saveLoadManager.Load();    // �̾��ϱ�� ����ġ �ε�
			choiceController.UpdateObjects();   // ����ġ�� ���� ������Ʈ ����
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
