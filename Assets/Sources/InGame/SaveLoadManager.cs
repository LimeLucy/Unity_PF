using System.IO;
using UnityEngine;

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
			saveData.switchStates = MainManager.instance.gameSwitch.GetArrSwitch();
			Transform trfPlayer = GameEngine.instance.GetPlayer().gameObject.transform;
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
			MainManager.instance.gameSwitch.SetArrSwitch(data.switchStates);
			GameEngine.instance.GetPlayer().gameObject.transform.position = data.vecPlayerPos;
			GameEngine.instance.GetPlayer().gameObject.transform.rotation = data.quatPlayerRot;
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
