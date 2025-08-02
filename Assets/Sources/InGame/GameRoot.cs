using UnityEngine;

public class GameRoot : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);		
    }
}
