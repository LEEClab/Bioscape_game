using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private void Awake()
    {
        if(( instance != null) && (instance != this) )
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        Debug.Log("SaveGame called");
    }

    public void LoadGame()
    {
        Debug.Log("LoadGame called");
    }
}
