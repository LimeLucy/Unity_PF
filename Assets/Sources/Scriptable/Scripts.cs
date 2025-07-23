using UnityEngine;

[CreateAssetMenu(fileName = "Scripts", menuName = "Scriptable Objects/Scripts")]
public class Scripts : ScriptableObject
{
    public int m_iCheckEventIdx = 0;	// �̺�Ʈ ��ȣ üũ�Ͽ� true, false text�߿� ����
	public int m_iOnEventIdx = 1;	// ��ũ��Ʈ ������ �Ѿ� �� �̺�Ʈ �ε���, -1�̸� �̺�Ʈ ����ġ ���� ����
	public string m_strFalseText = "false";
	public string m_strTrueText = "";	// true text�� �����Ǿ� ���� ������
	public Selects m_trueSelect;	// select�� ����
}
