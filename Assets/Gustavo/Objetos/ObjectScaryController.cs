using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaryController : ObjectBaseController
{
    private bool wasUsed = false;
    private QuartoBehavior quarto;
    private void Awake() {
        var coliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.1f, 0.1f),0);
        foreach (var colider in coliders) {
            if (colider.tag == "Quarto") {
                quarto = colider.GetComponent<QuartoBehavior>();
                break;
            }
        }
    }
    public override bool Interagir() {
        if (wasUsed) return false;
        return true;
    }

    public override void Usar() {
        wasUsed = true;
        quarto.Assustar();
    }
}
