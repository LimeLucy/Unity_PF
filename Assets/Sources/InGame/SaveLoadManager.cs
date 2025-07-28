using System.IO;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			saveData.switchStates = gameSwitch.GetArrSwitch();
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			Transform trfPlayer = player.gameObject.transform;
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
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			gameSwitch.SetArrSwitch(data.switchStates);
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			player.gameObject.transform.position = data.vecPlayerPos;
			player.gameObject.transform.rotation = data.quatPlayerRot;
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
