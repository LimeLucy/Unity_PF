using TMPro;
using UnityEngine;

namespace Casual
{
	public class UINameMarker : MonoBehaviour
	{
		[SerializeField]
		GameObject m_goRoot = null;
		TextMeshProUGUI m_tmpName = null;

		public void Show(ZombieController zombie)
		{		
			if(m_tmpName == null)
				m_tmpName = m_goRoot.GetComponentInChildren<TextMeshProUGUI>(true);
			m_tmpName.text = zombie.GetName();

			Vector3 screenPos = Camera.main.WorldToScreenPoint(zombie.GetMarkerPos());

			Canvas canvas = m_goRoot.GetComponentInParent<Canvas>();
			RectTransform canvasRect = canvas.GetComponent<RectTransform>();

			// 스크린 좌표 -> UI 로컬 좌표로 변환
			Vector2 localPos;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
					canvasRect, screenPos, canvas.worldCamera, out localPos))
			{
				m_goRoot.GetComponent<RectTransform>().anchoredPosition = localPos;
			}
			m_goRoot.SetActive(true);
		}

		public void Hide()
		{
			m_goRoot.SetActive(false);
		}
	}
}
