using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementManager : MonoBehaviour
{
    public float health = 500;
    private static BasementManager instance;
    public static BasementManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
                return null;
        }
    }


    void Start()
    {
        instance = this;
    }

    void Update()
    {

    }
}
