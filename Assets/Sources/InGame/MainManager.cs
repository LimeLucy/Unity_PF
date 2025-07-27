using System.Collections;
using UnityEngine;

namespace Casual
{
	// 전체 State interface // ex) game, menu
	public interface IState
	{
		/// <summary>
		/// 스테이트 변경시 로드 상태에서 필요한 것들 구현 합니다.
		/// </summary>
		public IEnumerator Load() { yield return null; }

		/// <summary>
		/// 스테이트 변경시 해제 상태에서 필요한 것들 구현 합니다.
		/// </summary>
		public IEnumerator Destroy() { yield return null; }

	}

	/// <summary>
	/// 메인 상태 관리자
	/// </summary>
	public class MainManager : MonoBehaviour, IMainManager
	{
		static MainManager s_Instance = null;
		public static MainManager instance { get { return s_Instance; } }

		private IState m_currentState = null;	// 현재 state 상태
		bool m_isLoading = false;

		GameSwitch m_gameSwitch = null;
		public GameSwitch gameSwitch { get { return m_gameSwitch; } }
		ISaveLoadManager m_saveLoadManager = null;
		public ISaveLoadManager saveLoadManager { get { return m_saveLoadManager; } }



		void Awake()
		{
			s_Instance = this;
			DontDestroyOnLoad(this);
			m_gameSwitch = new GameSwitch();
		}

		private void OnDestroy()
		{
			m_gameSwitch = null;
			s_Instance = null;
		}

		private void Start()
		{		
			ChangeState(new StateMenu());
		}

		[VContainer.Inject]
		public void Construct(ISaveLoadManager saveLoad)
		{
			m_saveLoadManager = saveLoad;
		}


		/// <summary>
		/// 상태를 변경합니다. 
		/// </summary>
		/// <param name="newState"> 변경할 상태 </param>
		IEnumerator _ChangeState(IState newState)
		{
			if (m_isLoading) yield break;

			if (m_currentState != null)
				yield return StartCoroutine(m_currentState.Destroy());
			yield return StartCoroutine(newState.Load());
			m_currentState = newState;
		}


		public void ChangeState(IState newState)
		{
			StartCoroutine(_ChangeState(newState));
		}	
	}
}
