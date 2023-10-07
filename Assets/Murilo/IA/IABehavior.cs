using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehavior : MonoBehaviour
{
    private List<Vector2> pontosRonda = new List<Vector2>();
    private QuartoBehavior quarto;
    public GameObject rondas;
    private Rigidbody2D rb;
    public Vector2 velocidade = Vector2.right, destino = Vector2.zero, destinoTemp;
    private bool escada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in rondas.transform)
        {
            pontosRonda.Add(child.position);
        }
        MudarRonda();
    }

    void MudarRonda()
    {
        var a = pontosRonda[Random.Range(0, pontosRonda.Count)];
        MudarDestino(a);
        Invoke("MudarRonda", 5f);
    }
    public void MudarDestino(Vector2 d)
    {
        CancelInvoke();
        if (Mathf.Abs(d.y - transform.position.y) > 1)
        {
            destinoTemp = destino;
            print("MudarAndar");
            MudarAndar(d);
        }
        else
        {
            destino = d;
            Invoke("MudarRonda", 5f);
        }
    }
    public void Assustar()
    {

    }
    private void MudarAndar(Vector2 d)
    {
        destino = (d.y - transform.position.y) > 1 ? quarto.escadaSobe.transform.position : quarto.escadaDesce.transform.position;
        print("MudarAndar2");
        if (Mathf.Abs(transform.position.x - destino.x) < 0.5f)
        {
            escada = true;
            var a = quarto.GetEscada(destino.y);
            if (destino.y > transform.position.y)
                transform.position = a.escadaSobe.transform.position;
            else
                transform.position = a.escadaDesce.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    private void Update()
    {
        if (!escada)
            rb.MovePosition(Vector2.MoveTowards(transform.position, destino, 0.1f));
        else
            escada = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Quarto")
        {
            quarto = collision.gameObject.GetComponent<QuartoBehavior>();
            quarto.AddNPC(this);
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
