namespace Casual
{
	public interface IMainManager
	{
		GameSwitch gameSwitch { get; }
		ISaveLoadManager saveLoadManager { get; }
		void ChangeState(IState newState);
	}
}