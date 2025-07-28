using Casual;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
		builder.RegisterComponentInHierarchy<IGameStateManager>().As<IGameStateManager>();

		builder.RegisterComponentInHierarchy<IScreenMaskController>().As<IScreenMaskController>();
		builder.RegisterComponentInHierarchy<IChoiceObjectController>().As<IChoiceObjectController>();
		builder.RegisterComponentInHierarchy<IPlayerProvider>().As<IPlayerProvider>();

		builder.RegisterComponentInHierarchy<UIDialogue>().AsSelf();
		builder.RegisterComponentInHierarchy<UISelect>().AsSelf();
		builder.RegisterComponentInHierarchy<UIInGameMenu>().AsSelf();

		builder.Register<DialogueUIController>(Lifetime.Singleton);
		builder.Register<ChoiceUIController>(Lifetime.Singleton);
		builder.Register<GameMenuUIController>(Lifetime.Singleton);
		builder.Register<UIMediator>(Lifetime.Singleton);
	}
}
