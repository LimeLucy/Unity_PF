using UnityEngine;

[CreateAssetMenu(fileName = "Selects", menuName = "Scriptable Objects/Select")]
public class Selects : ScriptableObject
{
	public string m_strQuest = "";	// ������ ����
	public string[] m_strAns = new string[2];	// ���
	public int[] m_iAnsSwitchOnIdx = new int[2];	// ��信 ���� ų ����ġ ��ȣ
	public int[] m_iAnsSwitchOffIdx = new int[2];	// ��信 ���� �� ����ġ ��ȣ
}
