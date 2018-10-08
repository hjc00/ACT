using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }


    public T LoadResources<T>(string path) where T:Object
    {
        Object go = Resources.Load(path);
        if (go == null)
            return null;
        return (T)go;
    }
}
