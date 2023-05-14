using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Players_From_Lobby : MonoBehaviour
{
    private Move_Players_From_Lobby _instance;


    private void Start()
    {        
        _instance = this;

        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}


