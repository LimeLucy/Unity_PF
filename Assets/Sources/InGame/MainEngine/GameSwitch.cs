using System;

namespace Casual
{
	public class GameSwitch
	{
		// switch
		public const int CNT_SWITCH = 10; // ����ġ ����
		bool[] m_isArrSwitch = new bool[CNT_SWITCH]; // ����ġ

		EventCheckOnOff[] m_eventCheckOnOffs = null;

		/// <summary>
		/// �̺�Ʈ ���� üũ�ؾ� �ϴ� ������Ʈ�� ����
		/// </summary>
		/// <param name="eventCheckOnOffs"></param>
		public void SetArrCheckOnOff(EventCheckOnOff[] eventCheckOnOffs)
		{
			m_eventCheckOnOffs = eventCheckOnOffs;
		}

		/// <summary>
		/// �̺�Ʈ ������Ʈ�� �̺�Ʈ üũ
		/// </summary>
		public void CheckOnOffs()
		{
			if(m_eventCheckOnOffs != null)
			{
				foreach (EventCheckOnOff evtCheck in m_eventCheckOnOffs)
				{
					evtCheck?.SetOnOff();
				}
			}
		}

		/// <summary>
		/// ����ġ ���¸� �����մϴ�.
		/// </summary>
		/// <param name="iSwitchIdx"> ����ġ �ε��� </param>
		/// <param name="isOn"> ����ġ ���� </param>
		public void SetSwitch(int iSwitchIdx, bool isOn)
		{
			if (iSwitchIdx < 0 || iSwitchIdx >= CNT_SWITCH) return;
			m_isArrSwitch[iSwitchIdx] = isOn;
		}

		/// <summary>
		/// ���õ� ����ġ ���¸� �����ɴϴ�.
		/// </summary>
		/// <param name="iSwitchIdx"> ����ġ �ε��� </param>
		/// <returns> ����ġ ���� </returns>
		public bool GetSwitch(int iSwitchIdx)
		{
			return m_isArrSwitch[iSwitchIdx];
		}

		/// <summary>
		/// ��� ����ġ ����
		/// </summary>
		public void ResetAllSwitches()
		{
			Array.Fill(m_isArrSwitch, false);
		}

		/// <summary>
		/// ������ ���� ����ġ�� ����
		/// </summary>
		/// <returns></returns>
		public bool[] GetArrSwitch()
		{
			return m_isArrSwitch;
		}

		/// <summary>
		/// ����� ����� ������ ����ġ ����
		/// </summary>
		/// <param name="isArrSwitch"></param>
		public void SetArrSwitch(bool[] isArrSwitch)
		{
			m_isArrSwitch = isArrSwitch;			 
		}
	}
}
