using UnityEngine;

namespace Casual
{
	public class MenuScene : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goExistSaveFile;
		[SerializeField]
		GameObject m_goNotExistSaveFile;

#region unity �Լ�
		private void Awake()
		{
			bool isExistSaveFile = MainManager.instance.saveLoadManager.IsExistSaveFile();
			m_goExistSaveFile.SetActive(isExistSaveFile);
			m_goNotExistSaveFile.SetActive(!isExistSaveFile);
		}
#endregion

		/// <summary>
		/// �����ϱ� ��ư�� ������ ��
		/// </summary>
		public void ClickMenuNewButton()
		{			
			MainManager.instance.ChangeState(new StateGame(true));
		}

		/// <summary>
		/// �̾��ϱ� ��ư�� ������ ��
		/// </summary>
		public void ClickMenuContinueButton()
		{			
			MainManager.instance.ChangeState(new StateGame(false));
		}
	}
}
