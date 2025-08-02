using System;
using System.Collections.Generic;

namespace Casual
{
	public class GameSwitch
	{
		// switch
		public const int CNT_SWITCH = 10; // ����ġ ����
		bool[] m_isArrSwitch = new bool[CNT_SWITCH]; // ����ġ

		EventCheckOnOff[] m_eventCheckOnOffs = null;
		public void SetArrCheckOnOff(EventCheckOnOff[] eventCheckOnOffs)
		{
			m_eventCheckOnOffs = eventCheckOnOffs;
		}

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

		public void ResetAllSwitches()
		{
			Array.Fill(m_isArrSwitch, false);
		}

	#region Save Module�� ����ġ get,set
		public bool[] GetArrSwitch()
		{
			return m_isArrSwitch;
		}

		public void SetArrSwitch(bool[] isArrSwitch)
		{
			m_isArrSwitch = isArrSwitch;			 
		}
	#endregion
	}
}
