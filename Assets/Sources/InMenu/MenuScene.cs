using UnityEngine;

namespace Casual
{
	public class MenuScene : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goExistSaveFile;
		[SerializeField]
		GameObject m_goNotExistSaveFile;

#region unity 함수
		private void Awake()
		{
			bool isExistSaveFile = MainManager.instance.saveLoadManager.IsExistSaveFile();
			m_goExistSaveFile.SetActive(isExistSaveFile);
			m_goNotExistSaveFile.SetActive(!isExistSaveFile);
		}
#endregion

		/// <summary>
		/// 새로하기 버튼을 눌렀을 때
		/// </summary>
		public void ClickMenuNewButton()
		{			
			MainManager.instance.ChangeState(new StateGame(true));
		}

		/// <summary>
		/// 이어하기 버튼을 눌렀을 때
		/// </summary>
		public void ClickMenuContinueButton()
		{			
			MainManager.instance.ChangeState(new StateGame(false));
		}
	}
}
