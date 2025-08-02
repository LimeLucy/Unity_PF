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
		public bool[] m_switchStates = new bool[GameSwitch.CNT_SWITCH];
		public Vector3 m_vecPlayerPos = Vector3.zero;
		public Quaternion m_quatPlayerRot;
		public string m_strSceneName;
	}

	public interface ISaveLoadManager
	{
		/// <summary>
		/// 저장
		/// </summary>
		void Save();

		/// <summary>
		/// 로드
		/// </summary>
		void Load();

		/// <summary>
		/// 세이브파일 존재 여부
		/// </summary>
		/// <returns>true : 있음, false : 없음</returns>
		bool IsExistSaveFile();

		/// <summary>
		/// 저장된 씬 이름 가져오기
		/// </summary>
		/// <returns></returns>
		string GetSavedSceneName();

		/// <summary>
		/// 현재 씬 이름 셋팅
		/// </summary>
		/// <param name="strSceneName"></param>
		void SetCurrentSceneName(string sceneName);

		/// <summary>
		/// 현재 씬 이름 가져오기
		/// </summary>
		/// <returns></returns>
		string GetCurrentSceneName();

		/// <summary>
		/// 플레이어 셋팅 스폰 포지션을 세이브파일로 할지, 데이터로 할지 판단하여 셋팅
		/// </summary>
		/// <param name="isNew"></param>
		/// <param name="strSceneName"></param>
		void ApplySpawnPosition(bool isNew, string sceneName);
	}

	public class SaveLoadManager : MonoBehaviour, ISaveLoadManager
	{
		[SerializeField]
		PlayerSpawnPos m_spawnData = null;

		private string m_strSavePath => Path.Combine(Application.persistentDataPath, "save.json");
		string m_strCurSceneName;
		
		public void Save()
		{
			// savedata 구성
			SaveData saveData = new SaveData();
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			saveData.m_switchStates = gameSwitch.GetArrSwitch();
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			Transform trfPlayer = player.gameObject.transform;
			saveData.m_vecPlayerPos = trfPlayer.position;
			saveData.m_quatPlayerRot = trfPlayer.rotation;
			saveData.m_strSceneName = m_strCurSceneName;

			// file로 저장
			string json = JsonUtility.ToJson(saveData, true);
			File.WriteAllText(m_strSavePath, json);
			Debug.Log($"Saved to {m_strSavePath}");
		}
		
		public void Load()
		{
			if (!IsExistSaveFile())
			{
				Debug.LogWarning("No save file found.");
				return;
			}

			string json = File.ReadAllText(m_strSavePath);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			// 읽어 온 데이터 셋팅
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			gameSwitch.SetArrSwitch(data.m_switchStates);
			m_strCurSceneName = data.m_strSceneName;
			Debug.Log("Load completed");
		}
		
		public bool IsExistSaveFile()
		{
			return File.Exists(m_strSavePath);
		}
		
		public string GetSavedSceneName()
		{ 
			if(!IsExistSaveFile()) return null;
			string json = File.ReadAllText(m_strSavePath);
			SaveData saveData = JsonUtility.FromJson<SaveData>(json);
			return saveData.m_strSceneName;
		}
		
		public void SetCurrentSceneName(string strSceneName)
		{
			m_strCurSceneName = strSceneName;
		}
		
		public string GetCurrentSceneName()
		{
			return m_strCurSceneName;
		}
		
		public void ApplySpawnPosition(bool isNew, string strSceneName)
		{
			bool isSetNew = true;
			if(isNew && m_spawnData == null) return;

			isSetNew = isNew;

			string json;
			SaveData saveData;
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();

			if (!isNew)
			{
				json = File.ReadAllText(m_strSavePath);
				saveData = JsonUtility.FromJson<SaveData>(json);
				if(!IsExistSaveFile() && strSceneName != saveData.m_strSceneName)
					isSetNew = true;

				if(!isSetNew)
				{
					player.transform.position = saveData.m_vecPlayerPos;
					player.transform.rotation = saveData.m_quatPlayerRot;
				}
			}
			
			if (isSetNew)
			{
				if (m_spawnData == null) return;
				{					
					foreach(var sp in m_spawnData.m_spawnPos)
					{
						if(sp.m_strSceneName == strSceneName)
						{
							player.transform.position = sp.m_vecPos;
							player.transform.eulerAngles = sp.m_vecRot;
							break;
						}
					}
				}
			}
		}
	}
}
