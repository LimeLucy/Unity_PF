using System.IO;
using UnityEngine;

namespace Casual
{
/// <summary>
/// 저장될 savedata 구조
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
		/// 저장
		/// </summary>
		public void Save()
		{
			// savedata 구성
			SaveData saveData = new SaveData();
			saveData.switchStates = MainManager.instance.gameSwitch.GetArrSwitch();
			Transform trfPlayer = GameEngine.instance.GetPlayer().gameObject.transform;
			saveData.vecPlayerPos = trfPlayer.position;
			saveData.quatPlayerRot = trfPlayer.rotation;

			// file로 저장
			string json = JsonUtility.ToJson(saveData, true);
			File.WriteAllText(SavePath, json);
			Debug.Log($"Saved to {SavePath}");
		}

		/// <summary>
		/// 로드
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

			// 읽어 온 데이터 셋팅
			MainManager.instance.gameSwitch.SetArrSwitch(data.switchStates);
			GameEngine.instance.GetPlayer().gameObject.transform.position = data.vecPlayerPos;
			GameEngine.instance.GetPlayer().gameObject.transform.rotation = data.quatPlayerRot;
			Debug.Log("Load completed");
		}

		/// <summary>
		/// 세이브파일 존재 여부
		/// </summary>
		/// <returns>true : 있음, false : 없음</returns>
		public bool IsExistSaveFile()
		{
			return File.Exists(SavePath);
		}
	}
}
