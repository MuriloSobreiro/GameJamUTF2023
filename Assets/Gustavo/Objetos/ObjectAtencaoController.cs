using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAtencaoController : ObjectBaseController
{
    [SerializeField]
    private float delayUse = 5f;
    private bool canInteract = true;
    public override bool Interagir() {
        if (!canInteract) return false;
        canInteract = false;
        print("Interagiu");
        Invoke("ResetUse", delayUse);

        return true;
    }

    private void ResetUse() {
        canInteract = true;
    }

    public override void Usar() {
        print("Usou");
    }
}
