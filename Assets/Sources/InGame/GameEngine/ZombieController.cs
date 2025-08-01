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
		[SerializeField]
		Transform m_marker = null;
		[SerializeField]
		string m_strName = null;

		private void Awake()
		{
			if (m_logic == null)
			{
				var gameStateManager = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IGameStateManager>();
				var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
				var uiMediator = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<UIMediator>();
				m_logic = new ZombieControllerLogic(gameStateManager, gameSwitch, uiMediator);
			}
		}

		private void OnDestroy()
		{
			m_logic = null;
		}

		/// <summary>
		/// ��ũ��Ʈ run
		/// </summary>
		public void RunEvent()
		{
			m_logic.RunEvent(m_script);
		}

		public Vector3 GetMarkerPos()
		{
			return m_marker.position;
		}

		public string GetName()
		{
			return m_strName;
		}
	}

	public class ZombieControllerLogic
	{
		IGameStateManager m_gameStateManager;
		GameSwitch m_gameSwitch;
		UIMediator m_ui;

		public ZombieControllerLogic(IGameStateManager gameStateManager, GameSwitch gameSwitch, UIMediator ui)
		{
			m_gameStateManager = gameStateManager;
			m_gameSwitch = gameSwitch;
			m_ui = ui;
		}

		public void RunEvent(Scripts script)
		{
			bool isCheckEvt = m_gameSwitch.GetSwitch(script.m_iCheckEventIdx);
			if ((isCheckEvt && !string.IsNullOrEmpty(script.m_strTrueText)) || (!isCheckEvt))
				m_gameStateManager.ChangeState(new DialogueState(script, m_ui));
			else
				m_gameStateManager.ChangeState(new SelectState(script.m_trueSelect, m_ui, m_gameSwitch));
		}
	}
}
