using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void CarregarCena(string nomeCena)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nomeCena);
    }

    public void Sair()
    {
        Application.Quit();
        print("Saiu do jogo!");
    }
}
