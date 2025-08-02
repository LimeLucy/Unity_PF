using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class SceneChangeTrigger : MonoBehaviour
	{
		[SerializeField]
		string m_strTargetScene = "4_GameScene";

		IMainStateManager m_mainStateManager;
		ISaveLoadManager m_saveLoadManager;

		private void Awake()
		{
			var container = LifetimeScope.Find<RootLifetimeScope>().Container;
			m_mainStateManager = container.Resolve<IMainStateManager>();
			m_saveLoadManager = container.Resolve<ISaveLoadManager>();
		}

		private void OnTriggerEnter(Collider other)
		{
			if(!other.CompareTag("Player")) return;
			m_mainStateManager.ChangeState(new StateGame(false, false, m_strTargetScene));
		}
	}
}
