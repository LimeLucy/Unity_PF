using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSpawnPos", menuName = "Scriptable Objects/PlayerSpawnPos")]
public class PlayerSpawnPos : ScriptableObject
{
	[System.Serializable]
	public class SpawnPos
	{
		public string m_strSceneName;
		public Vector3 m_vecPos;
		public Vector3 m_vecRot;
	}
	public SpawnPos[] m_spawnPos;
}
