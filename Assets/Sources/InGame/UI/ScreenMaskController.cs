using UnityEngine;

namespace Casual
{
	public interface IScreenMaskController
	{
		/// <summary>
		/// Hide Mask¸¦ Åµ´Ï´Ù.
		/// </summary>
		void ShowMask();

		/// <summary>
		/// /// Hide Mask¸¦ ²ü´Ï´Ù.
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