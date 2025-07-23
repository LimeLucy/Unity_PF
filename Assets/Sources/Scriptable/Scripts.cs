using UnityEngine;

[CreateAssetMenu(fileName = "Scripts", menuName = "Scriptable Objects/Scripts")]
public class Scripts : ScriptableObject
{
    public int m_iCheckEventIdx = 0;	// 이벤트 번호 체크하여 true, false text중에 실행
	public int m_iOnEventIdx = 1;	// 스크립트 종료후 켜야 할 이벤트 인덱스, -1이면 이벤트 스위치 조작 없음
	public string m_strFalseText = "false";
	public string m_strTrueText = "";	// true text가 설정되어 있지 않으면
	public Selects m_trueSelect;	// select로 실행
}
