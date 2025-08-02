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
		public bool[] m_switchStates = new bool[GameSwitch.CNT_SWITCH];
		public Vector3 m_vecPlayerPos = Vector3.zero;
		public Quaternion m_quatPlayerRot;
		public string m_strSceneName;
	}

	public interface ISaveLoadManager
	{
		/// <summary>
		/// ����
		/// </summary>
		void Save();

		/// <summary>
		/// �ε�
		/// </summary>
		void Load();

		/// <summary>
		/// ���̺����� ���� ����
		/// </summary>
		/// <returns>true : ����, false : ����</returns>
		bool IsExistSaveFile();

		/// <summary>
		/// ����� �� �̸� ��������
		/// </summary>
		/// <returns></returns>
		string GetSavedSceneName();

		/// <summary>
		/// ���� �� �̸� ����
		/// </summary>
		/// <param name="strSceneName"></param>
		void SetCurrentSceneName(string sceneName);

		/// <summary>
		/// ���� �� �̸� ��������
		/// </summary>
		/// <returns></returns>
		string GetCurrentSceneName();

		/// <summary>
		/// �÷��̾� ���� ���� �������� ���̺����Ϸ� ����, �����ͷ� ���� �Ǵ��Ͽ� ����
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
			// savedata ����
			SaveData saveData = new SaveData();
			var gameSwitch = LifetimeScope.Find<RootLifetimeScope>().Container.Resolve<GameSwitch>();
			saveData.m_switchStates = gameSwitch.GetArrSwitch();
			var player = LifetimeScope.Find<GameLifetimeScope>().Container.Resolve<IPlayerProvider>().GetPlayer();
			Transform trfPlayer = player.gameObject.transform;
			saveData.m_vecPlayerPos = trfPlayer.position;
			saveData.m_quatPlayerRot = trfPlayer.rotation;
			saveData.m_strSceneName = m_strCurSceneName;

			// file�� ����
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

			// �о� �� ������ ����
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
