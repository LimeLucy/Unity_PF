using UnityEngine;

[CreateAssetMenu(fileName = "Selects", menuName = "Scriptable Objects/Select")]
public class Selects : ScriptableObject
{
	public string m_strQuest = "";	// 선택지 질문
	public string[] m_strAns = new string[2];	// 대답
	public int[] m_iAnsSwitchOnIdx = new int[2];	// 대답에 따라 킬 스위치 번호
	public int[] m_iAnsSwitchOffIdx = new int[2];	// 대답에 따라 끌 스위치 번호
}
