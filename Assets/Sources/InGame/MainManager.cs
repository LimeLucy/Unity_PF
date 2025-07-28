using System.Collections;
using UnityEngine;

namespace Casual
{
	// ��ü State interface // ex) game, menu
	public interface IState
	{
		/// <summary>
		/// ������Ʈ ����� �ε� ���¿��� �ʿ��� �͵� ���� �մϴ�.
		/// </summary>
		public IEnumerator Load() { yield return null; }

		/// <summary>
		/// ������Ʈ ����� ���� ���¿��� �ʿ��� �͵� ���� �մϴ�.
		/// </summary>
		public IEnumerator Destroy() { yield return null; }

	}

	/// <summary>
	/// ���� ���� ������
	/// </summary>
	public class MainStateManager : MonoBehaviour, IMainStateManager
	{
		private IState m_currentState = null;	// ���� state ����
		bool m_isLoading = false;


		void Awake()
		{
			DontDestroyOnLoad(this);
		}

		private void Start()
		{		
			ChangeState(new StateMenu());
		}


		/// <summary>
		/// ���¸� �����մϴ�. 
		/// </summary>
		/// <param name="newState"> ������ ���� </param>
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
