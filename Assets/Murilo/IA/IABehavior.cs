using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehavior : MonoBehaviour
{
    public List<Vector2> pontosRonda = new List<Vector2>();
    private QuartoBehavior quarto;
    public GameObject rondas;
    private Rigidbody2D rb;
    public Vector2 destinoDesejado = Vector2.zero;
    public bool escada = false, assustado = false, atencao = false;
    private int i = 0;
    public float coolDownRonda = 5f, coolDownAtencao = 5f;
    private float timer = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in rondas.transform)
        {
            pontosRonda.Add(child.position);
        }
        MudarDestino(pontosRonda[0]);
    }

    private Vector2 DecidePonto()
    {
        Vector2 destino = Vector2.zero;
        if (Mathf.Abs(destinoDesejado.y - transform.position.y) > 0.5f)
        {
            escada = true;
            destino = (destinoDesejado.y - transform.position.y) > 1 ? quarto.escadaSobe.transform.position : quarto.escadaDesce.transform.position;
        }
        else
            destino = destinoDesejado;
        return destino;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (!assustado)
        {
            if (timer > coolDownRonda && !atencao)
                DestRonda();
            if (timer > coolDownAtencao && atencao)
                atencao = false;
        }
        else
            Explodir();

        Vector2 destino = DecidePonto();
        rb.MovePosition(Vector2.MoveTowards(transform.position,new Vector2(destino.x,transform.position.y), 0.1f));
        if (Mathf.Abs(transform.position.x - destino.x) < 0.6f && escada)
        {
            var a = quarto.GetEscada(destino.y);
            if (destino.y > transform.position.y)
                transform.position = a.escadaSobe.transform.position;
            else
                transform.position = a.escadaDesce.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            escada = false;
        }
    }
    void DestRonda()
    {
        timer = 0;
        i++;
        if (i >= pontosRonda.Count)
            i = 0;
        MudarDestino(pontosRonda[i]);
    }
    void DestAtencao()
    {
        timer = 0;
        atencao = true;
    }
    public void Assustar()
    {
        assustado = true;
    }
    private void Explodir()
    {

    }
    public void MudarDestino(Vector2 ponto, bool att = false)
    {
        if (att)
            DestAtencao();
        destinoDesejado = ponto;
    }

    private void OnTriggerStay2D(Collider2D collision)
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
