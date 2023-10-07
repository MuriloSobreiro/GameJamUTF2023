using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteBehavior : MonoBehaviour
{
    public IABehavior npc;
    public GameObject destino;
    public bool ativar;

    private void Update()
    {
        if (ativar)
        {
            ativar = false;
            npc.MudarDestino(destino.transform.position);
        }
    }
}
