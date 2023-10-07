using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaryController : ObjectBaseController
{
    public bool wasUsed = false;
    private Animator animator;
    private QuartoBehavior quarto;
    public AudioSource entrando;
    public AudioSource assustando;
    private void Awake() {
        var coliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.01f, 0.01f),0);
        animator = GetComponent<Animator>();
        foreach (var colider in coliders) {
            if (colider.tag == "Quarto") {
                quarto = colider.GetComponent<QuartoBehavior>();
                break;
            }
        }
    }
    public override bool Interagir() {
        if (wasUsed) return false;
        entrando.Play();
        return true;
    }

    public override void Usar() {
        wasUsed = true;
        assustando.Play();
        quarto.Assustar();
        animator.SetBool("isScarying", true);
    }
}
