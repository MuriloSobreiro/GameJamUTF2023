using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    public Canvas opcoes;
    public AudioMaster audioMaster;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            opcoes.enabled = !opcoes.enabled;
        }
    }
    public void HideOpcoes()
    {
        opcoes.enabled = false;
    }
    public void GameOver()
    {
        audioMaster.lugar = "GameOver";
    }

}
