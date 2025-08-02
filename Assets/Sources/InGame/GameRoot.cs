using UnityEngine;

public interface IGameRoot
{
	void Dispose();
}

public class GameRoot : MonoBehaviour, IGameRoot
{
    void Awake()
    {
        DontDestroyOnLoad(this);		
    }

	public void Dispose()
	{
		Destroy(gameObject);
	}
}
