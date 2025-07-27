namespace Casual
{
	public interface ISaveLoadManager
	{
		void Save();
		void Load();
		bool IsExistSaveFile();
	}
}