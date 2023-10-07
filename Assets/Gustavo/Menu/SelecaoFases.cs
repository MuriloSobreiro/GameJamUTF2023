using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecaoFases : MonoBehaviour
{
    public GameObject buttonPrefab;
    int sceneCount = 0;

    void Awake()
    {
        sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
    }

    void Start()
    {
        print(sceneCount);
        for (int i = 0; i < sceneCount - 3; i++) {
            string fase = "Fase" + (i+1);
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(transform);
            button.GetComponent<Button>().onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(fase));
        }
    }
}
