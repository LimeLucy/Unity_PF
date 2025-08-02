namespace Casual
{
	public interface ISaveLoadManager
	{
		void Save();
		void Load();
		bool IsExistSaveFile();
		string GetSavedSceneName();
		void SetCurrentSceneName(string sceneName);
		string GetCurrentSceneName();
		void ApplySpawnPosition(bool isNew, string sceneName);
	}
}