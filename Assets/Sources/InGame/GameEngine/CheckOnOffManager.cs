using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class CheckOnOffManager: MonoBehaviour
	{
		private void Start()
		{
			EventCheckOnOff[] eventCheckOnOffs = gameObject.GetComponentsInChildren<EventCheckOnOff>(true);
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			gameSwitch.SetArrCheckOnOff(eventCheckOnOffs);
		}
	}
}
