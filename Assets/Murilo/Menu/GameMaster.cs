using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (gameMaster == null)
        {
            gameMaster = this;
        }
        else if (gameMaster != this)
        {
            Destroy(gameObject);
        }
       
    }

}
