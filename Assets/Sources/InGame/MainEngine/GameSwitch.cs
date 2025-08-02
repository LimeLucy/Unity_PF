using System;

namespace Casual
{
	public class GameSwitch
	{
		// switch
		public const int CNT_SWITCH = 10; // 스위치 갯수
		bool[] m_isArrSwitch = new bool[CNT_SWITCH]; // 스위치

		EventCheckOnOff[] m_eventCheckOnOffs = null;

		/// <summary>
		/// 이벤트 상태 체크해야 하는 오브젝트들 셋팅
		/// </summary>
		/// <param name="eventCheckOnOffs"></param>
		public void SetArrCheckOnOff(EventCheckOnOff[] eventCheckOnOffs)
		{
			m_eventCheckOnOffs = eventCheckOnOffs;
		}

		/// <summary>
		/// 이벤트 오브젝트들 이벤트 체크
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

		/// <summary>
		/// 모든 스위치 리셋
		/// </summary>
		public void ResetAllSwitches()
		{
			Array.Fill(m_isArrSwitch, false);
		}

		/// <summary>
		/// 저장을 위해 스위치를 리턴
		/// </summary>
		/// <returns></returns>
		public bool[] GetArrSwitch()
		{
			return m_isArrSwitch;
		}

		/// <summary>
		/// 저장시 저장된 값으로 스위치 셋팅
		/// </summary>
		/// <param name="isArrSwitch"></param>
		public void SetArrSwitch(bool[] isArrSwitch)
		{
			m_isArrSwitch = isArrSwitch;			 
		}
	}
}
