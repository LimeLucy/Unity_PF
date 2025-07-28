using System.IO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Casual
{
/// <summary>
/// ����� savedata ����
/// </summary>
	public class SaveData
	{
		public bool[] switchStates = new bool[GameSwitch.CNT_SWITCH];
		public Vector3 vecPlayerPos = Vector3.zero;
		public Quaternion quatPlayerRot;
	}

	public class SaveLoadManager : MonoBehaviour, ISaveLoadManager
	{
		private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

		/// <summary>
		/// ����
		/// </summary>
		public void Save()
		{
			// savedata ����
			SaveData saveData = new SaveData();
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			saveData.switchStates = gameSwitch.GetArrSwitch();
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			Transform trfPlayer = player.gameObject.transform;
			saveData.vecPlayerPos = trfPlayer.position;
			saveData.quatPlayerRot = trfPlayer.rotation;

			// file�� ����
			string json = JsonUtility.ToJson(saveData, true);
			File.WriteAllText(SavePath, json);
			Debug.Log($"Saved to {SavePath}");
		}

		/// <summary>
		/// �ε�
		/// </summary>
		public void Load()
		{
			if (!IsExistSaveFile())
			{
				Debug.LogWarning("No save file found.");
				return;
			}

			string json = File.ReadAllText(SavePath);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			// �о� �� ������ ����
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			gameSwitch.SetArrSwitch(data.switchStates);
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.gameObject.transform.position = data.vecPlayerPos;
			player.gameObject.transform.rotation = data.quatPlayerRot;
			Debug.Log("Load completed");
		}

		/// <summary>
		/// ���̺����� ���� ����
		/// </summary>
		/// <returns>true : ����, false : ����</returns>
		public bool IsExistSaveFile()
		{
			return File.Exists(SavePath);
		}
	}
}
