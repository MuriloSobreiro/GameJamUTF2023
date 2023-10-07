using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool facingRight = true;
    private Vector2 velocity = Vector2.zero;
    private ObjectBaseController scaryObject;
    public bool isHide = false;
    public bool canMove = true;
    public QuartoBehavior quarto;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void CallAttention(ObjectBaseController objectController) {
        if(quarto.GameOver()) {
            GameOver();
            return;
        }
        if (!objectController.Interagir()) return;
        
        sprite.color = new Color(1f, 1f, 1f, 1f);
        animator.SetBool("isUsingObject", true);
        Invoke("MakeInvisible", 0.5f);
    }

    public void InteractScary(ObjectBaseController objectController) {
        if (isHide) return;
        if(quarto.GameOver()) {
            GameOver();
            return;
        }
        if (!objectController.Interagir()) return;
        animator.SetBool("isInteracting", true);


        scaryObject = objectController;
        sprite.color = new Color(1f, 1f, 1f, 1f);

        canMove = false;
        Invoke("HidePlayer", 0.75f);
    }

    public void Move(float moveX, float moveY){
        if(isHide || !canMove) return;

        Vector2 targetVelocity = new Vector2(moveX, moveY);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (moveX > 0 && !facingRight){
            Flip();
        } else if (moveX < 0 && facingRight){
            Flip();
        }
    }

    private void Flip(){
        sprite.flipX = facingRight;
        facingRight = !facingRight;
    }

    private void MakeInvisible(){
        animator.SetBool("isUsingObject", false);
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void HidePlayer(){
        isHide = true;
        rb.velocity = Vector2.zero;
        sprite.color = new Color(1f, 1f, 1f, 0f);
    }

    private void UnhidePlayer(){
        isHide = false;
        canMove = true;
        animator.SetBool("isInteracting", false);
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void GameOver(){
        print("Game Over");
        animator.SetBool("isGameOver", true);
    }

    void Update()
    {
        if (isHide && Input.GetKeyDown(KeyCode.E))
        {
            scaryObject.Usar();
            Invoke("UnhidePlayer", 0.5f);
        }

        if (isHide && Input.GetKeyDown(KeyCode.Q))
        {
            isHide = false;
            Invoke("UnhidePlayer", 0.5f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Quarto")
        {
            quarto = other.GetComponent<QuartoBehavior>();
        }
    }

}
