using Casual;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
		builder.RegisterComponentInHierarchy<IGameEngine>().As<IGameEngine>();
		builder.RegisterComponentInHierarchy<IGameStateManager>().As<IGameStateManager>();
	}
}
