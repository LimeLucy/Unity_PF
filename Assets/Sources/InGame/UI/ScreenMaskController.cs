using UnityEngine;

namespace Casual
{
	public interface IScreenMaskController
	{
		/// <summary>
		/// Hide Mask�� ŵ�ϴ�.
		/// </summary>
		void ShowMask();

		/// <summary>
		/// /// Hide Mask�� ���ϴ�.
		/// </summary>
		void HideMask();
	}

	public class ScreenMaskController : MonoBehaviour, IScreenMaskController
	{
		[SerializeField]
		GameObject m_maskObject = null;

		public void ShowMask() => m_maskObject?.SetActive(true);
		public void HideMask() => m_maskObject?.SetActive(false);
	}
}