using Casual;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
		builder.RegisterComponentInHierarchy<IGameStateManager>().As<IGameStateManager>();

		builder.RegisterComponentInHierarchy<IScreenMaskController>().As<IScreenMaskController>();
		builder.RegisterComponentInHierarchy<IPlayerProvider>().As<IPlayerProvider>();

		// UI
		builder.RegisterComponentInHierarchy<UIDialogue>().AsSelf();
		builder.RegisterComponentInHierarchy<UISelect>().AsSelf();
		builder.RegisterComponentInHierarchy<UIInGameMenu>().AsSelf();
		builder.RegisterComponentInHierarchy<UINameMarker>().AsSelf();
		
		builder.Register<DialogueUIController>(Lifetime.Singleton);
		builder.Register<SelectUIController>(Lifetime.Singleton);
		builder.Register<GameMenuUIController>(Lifetime.Singleton);
		builder.Register<NameMarkerUIController>(Lifetime.Singleton);
		builder.Register<UIMediator>(Lifetime.Singleton);
	}
}