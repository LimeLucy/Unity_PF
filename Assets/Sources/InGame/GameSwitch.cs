using System;
using System.Collections.Generic;

namespace Casual
{
	public class GameSwitch
	{
		// switch
		public const int CNT_SWITCH = 10; // 스위치 갯수
		bool[] m_isArrSwitch = new bool[CNT_SWITCH]; // 스위치

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
		/// 스위치 상태를 셋팅합니다.
		/// </summary>
		/// <param name="iSwitchIdx"> 스위치 인덱스 </param>
		/// <param name="isOn"> 스위치 상태 </param>
		public void SetSwitch(int iSwitchIdx, bool isOn)
		{
			if (iSwitchIdx < 0 || iSwitchIdx >= CNT_SWITCH) return;
			m_isArrSwitch[iSwitchIdx] = isOn;
		}
		/// <summary>
		/// 셋팅된 스위치 상태를 가져옵니다.
		/// </summary>
		/// <param name="iSwitchIdx"> 스위치 인덱스 </param>
		/// <returns> 스위치 상태 </returns>
		public bool GetSwitch(int iSwitchIdx)
		{
			return m_isArrSwitch[iSwitchIdx];
		}

		public void ResetAllSwitches()
		{
			Array.Fill(m_isArrSwitch, false);
		}

	#region Save Module용 스위치 get,set
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
