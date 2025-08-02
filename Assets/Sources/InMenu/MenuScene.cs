using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{

	public class MenuScene : MonoBehaviour
	{
		private MenuSceneLogic m_logic = null;

		[SerializeField]
		GameObject m_goExistSaveFile;
		[SerializeField]
		GameObject m_goNotExistSaveFile;

		private void Awake()
		{
			if(m_logic == null)
			{
				var container = LifetimeScope.Find<RootLifetimeScope>().Container;
				var mainManager = container.Resolve<IMainStateManager>();
				var saveManager = container.Resolve<ISaveLoadManager>();
				m_logic = new MenuSceneLogic(mainManager, saveManager);
			}

			bool isExistSaveFile = m_logic.IsExistSaveFile();
			m_goExistSaveFile.SetActive(isExistSaveFile);
			m_goNotExistSaveFile.SetActive(!isExistSaveFile);
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		/// <summary>
		/// 새로하기 버튼을 눌렀을 때 // call inspector 함수
		/// </summary>
		public void ClickMenuNewButton()
		{
			m_logic.NewGame();
		}

		/// <summary>
		/// 이어하기 버튼을 눌렀을 때 // call inspector 함수
		/// </summary>
		public void ClickMenuContinueButton()
		{
			m_logic.ContinueGame();
		}
	}

	public class MenuSceneLogic
	{
		readonly IMainStateManager m_mainManager;
		readonly ISaveLoadManager m_saveLoadManager;
		public MenuSceneLogic(IMainStateManager mainManager, ISaveLoadManager saveManager)
		{
			m_mainManager = mainManager;
			m_saveLoadManager = saveManager;
		}

		public bool IsExistSaveFile()
		{
			return m_saveLoadManager.IsExistSaveFile();
		}

		public void NewGame()
		{
			string strNewScene = "3_GameScene";
			m_saveLoadManager.SetCurrentSceneName(null);
			m_mainManager.ChangeState(new StateGame(true, false, strNewScene));
		}

		public void ContinueGame()
		{
			string strSceneName = m_saveLoadManager.GetSavedSceneName();
			m_saveLoadManager.SetCurrentSceneName(null);
			if (strSceneName == null)
				NewGame();
			else
				m_mainManager.ChangeState(new StateGame(false, true, strSceneName));
		}
	}
}
