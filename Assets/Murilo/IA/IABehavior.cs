using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehavior : MonoBehaviour
{
    public List<Vector2> pontosRonda = new List<Vector2>();
    public GameObject rondas;
    private Rigidbody2D rb;
    public Vector2 velocidade = Vector2.right, destino = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in rondas.transform)
        {
            pontosRonda.Add(child.position);
        }
        MudarDestino(Vector2.zero);
    }

    void MudarRonda()
    {
        MudarDestino(Vector2.zero);
    }
    public void MudarDestino(Vector2 d)
    {
        if (d == Vector2.zero)
        {
            Invoke("MudarRonda", 5f);
            destino = pontosRonda[Random.Range(0, pontosRonda.Count)];
        }
        else
        {
            CancelInvoke();
            destino = d;
        }
    }
    public void Assustar()
    {

    }
    private void Update()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, destino, 0.1f));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Quarto")
        {
            var a = collision.gameObject.GetComponent<QuartoBehavior>();
            a.AddNPC(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Quarto")
        {
            var a = collision.gameObject.GetComponent<QuartoBehavior>();
            a.RemoveNPC(this);
        }
    }
}
