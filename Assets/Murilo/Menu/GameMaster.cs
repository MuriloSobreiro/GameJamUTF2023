using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMaster;
    public Canvas opcoes;
    public AudioMaster audioMaster;
    public List<IABehavior> Inimigos;
    public List<ObjectScaryController> Sustos;
    public int i = 0;
    public bool debug = false;
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
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu")
        {
            i = 0;
        }
        if (scene.name.StartsWith("Fase"))
        {
            foreach (var quarto in FindObjectsOfType<QuartoBehavior>())
            {
                quarto.fundo.gameObject.SetActive(false);
            }
            Sustos.AddRange(FindObjectsOfType<ObjectScaryController>());
            InvokeRepeating("CheckFinal", 1f,1f);
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
        print("GameOverMAster");
        audioMaster.lugar = "GameOver";
        Invoke("VoltaMenu", 5f);
    }

    private void VoltaMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    private void CheckFinal()
    {
        if (audioMaster.lugar == "Fase")
        {
            if (FindObjectsOfType<IABehavior>().Length == 0)
            {
                string fase = "Fase" + ++i;
                UnityEngine.SceneManagement.SceneManager.LoadScene(fase);
                print("Caralho");
                return;
            }

        }
    }
}
