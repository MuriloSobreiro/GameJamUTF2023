using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAtencaoController : ObjectBaseController
{
    [SerializeField]
    private float delayUse = 5f;
    private bool canInteract = true;
    private QuartoBehavior quarto;
    private Transform attentionPoint;

    private void Awake() {
        attentionPoint = this.gameObject.transform.GetChild(0);
        var coliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.1f, 0.1f),0);
        foreach (var colider in coliders) {
            if (colider.tag == "Quarto") {
                quarto = colider.GetComponent<QuartoBehavior>();
                break;
            }
        }
    }
    public override bool Interagir() {
        if (!canInteract) return false;
        canInteract = false;
        print("Interagiu");
        quarto.Atrair(attentionPoint.position);
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
