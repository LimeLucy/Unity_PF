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
			if(m_logic == null)
			{
				var gameStateManager = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IGameStateManager>();
				var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
				m_logic = new ZombieControllerLogic(gameStateManager, gameSwitch);
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

		public ZombieControllerLogic(IGameStateManager gameStateManager, GameSwitch gameSwitch)
		{
			m_gameStateManager = gameStateManager;
			m_gameSwitch = gameSwitch;
		}

		public void RunEvent(Scripts script)
		{
			bool isCheckEvt = m_gameSwitch.GetSwitch(script.m_iCheckEventIdx);
			if ((isCheckEvt && !string.IsNullOrEmpty(script.m_strTrueText)) || (!isCheckEvt))
				m_gameStateManager.ChangeState(new DialogueState(script));
			else
				m_gameStateManager.ChangeState(new SelectState(script.m_trueSelect));
		}
	}
}
