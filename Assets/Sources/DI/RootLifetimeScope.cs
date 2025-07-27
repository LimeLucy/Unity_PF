using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
	public class RootLifetimeScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponentInHierarchy<IMainManager>().As<IMainManager>();
			builder.RegisterComponentInHierarchy<ISaveLoadManager>().As<ISaveLoadManager>();
			builder.RegisterEntryPoint<GameSwitch>(Lifetime.Singleton).AsSelf();
		}
	}
}