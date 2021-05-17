using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHolder : MonoBehaviour
{
    public static DifficultyHolder instance;
    public float difficulty;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
