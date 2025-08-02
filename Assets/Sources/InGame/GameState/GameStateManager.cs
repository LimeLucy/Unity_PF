using System.Collections;
using UnityEngine;

namespace Casual
{
	/// <summary>
	/// ���� ���� ���� interface ex)�̵�����, ������, ����������
	/// </summary>
	public interface IGameState
	{
		IEnumerator Enter();
		IEnumerator Exit();
	}

	public interface IGameStateManager
	{
		void ChangeState(IGameState newState);
	}

	/// <summary>
	/// ���� ���� ������
	/// </summary>
	public class GameStateManager : MonoBehaviour, IGameStateManager
	{
		private IGameState m_curGState;
	
		private IEnumerator _ChangeStateRoutine(IGameState newState)
			{
				if (m_curGState == newState) yield break;

				if (m_curGState != null)
					yield return StartCoroutine(m_curGState.Exit());

				m_curGState = newState;

				if (m_curGState != null)
					yield return StartCoroutine(m_curGState.Enter());
			}

		public void ChangeState(IGameState newState)
		{
			StartCoroutine(_ChangeStateRoutine(newState));
		}
	}
}
