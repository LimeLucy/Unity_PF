using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class ZombieController : MonoBehaviour
	{
		ZombieControllerLogic m_logic = null;

		public enum eEvtState {
			None,
			Dialogue,
			Select,
		}
		[SerializeField]
		Scripts m_script = null;

		private void Awake()
		{
			if (m_logic == null)
			{
				var gameStateManager = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IGameStateManager>();
				var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
				var uiMediator = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<UIMediator>();
				var gameEngine = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IGameEngine>();
				m_logic = new ZombieControllerLogic(gameStateManager, gameSwitch, uiMediator, gameEngine);
			}
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		/// <summary>
		/// 스크립트 run
		/// </summary>
		public void RunEvent()
		{
			m_logic.RunEvent(m_script);
		}
	}

	public class ZombieControllerLogic
	{
		IGameStateManager m_gameStateManager;
		GameSwitch m_gameSwitch;
		UIMediator m_ui;
		IGameEngine m_gameEngine;

		public ZombieControllerLogic(IGameStateManager gameStateManager, GameSwitch gameSwitch, UIMediator ui, IGameEngine gameEngine)
		{
			m_gameStateManager = gameStateManager;
			m_gameSwitch = gameSwitch;
			m_ui = ui;
			m_gameEngine = gameEngine;
		}

		public void RunEvent(Scripts script)
		{
			bool isCheckEvt = m_gameSwitch.GetSwitch(script.m_iCheckEventIdx);
			if ((isCheckEvt && !string.IsNullOrEmpty(script.m_strTrueText)) || (!isCheckEvt))
				m_gameStateManager.ChangeState(new DialogueState(script, m_ui));
			else
				m_gameStateManager.ChangeState(new SelectState(script.m_trueSelect, m_ui, m_gameEngine));
		}
	}
}
