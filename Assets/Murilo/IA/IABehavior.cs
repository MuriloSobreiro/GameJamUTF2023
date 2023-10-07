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
    public bool escada = false, assustado = false, atencao = false, animando = false, parado = false;
    private int i = 0;
    public float coolDownRonda = 5f, coolDownAtencao = 5f, tempoAtencao = 2f, velocidade;
    public float timer = 0;
    public Animator animator, animAtencao;
    public SpriteRenderer spriteRenderer;
    public AudioSource morrendoSound;
    public AudioSource subindoEscadaSound;
    public AudioSource descendoEscadaSound;
    public AudioSource exclamacao;

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
        if(parado)
            timer += Time.deltaTime;
        if (!assustado)
        {
            if (timer > coolDownRonda && !atencao)
            {
                timer = 0;
                DestRonda();
            }
            if (timer > coolDownAtencao && atencao)
            {
                atencao = false;
                timer = 0;
            }
        }
        else {
            animator.SetTrigger("Morrer");
            Invoke("Explodir", 1.2f);
        }

        Vector2 destino = DecidePonto();
        if (destino.x - transform.position.x > 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        if (Mathf.Abs(transform.position.x - destino.x) < 0.1)
        {
            parado = true;

        }
        animator.SetBool("Andando", !parado);
        rb.MovePosition(Vector2.MoveTowards(transform.position,new Vector2(destino.x,transform.position.y), 0.01f * velocidade));
        if (Mathf.Abs(transform.position.x - destino.x) < 0.1f && escada && !animando)
        {
            StartCoroutine("EsperaAnim");
        }
    }
    void DestRonda()
    {
        i++;
        if (i >= pontosRonda.Count)
            i = 0;
        MudarDestino(pontosRonda[i]);
    }
    void DestAtencao(Vector2 ponto)
    {
        StartCoroutine(esperaAtencao(ponto));
    }
    public void Assustar()
    {
        assustado = true;
    }
    private void Explodir()
    {
        Destroy(this.gameObject);
        morrendoSound.Play();
    }
    public void Dancar()
    {
        animator.SetTrigger("GameOver");
        this.enabled = false;
    }
    IEnumerator esperaAtencao(Vector2 ponto)
    {
        animAtencao.SetTrigger("Atencao");
        exclamacao.Play();
        yield return new WaitForSeconds(tempoAtencao);
        atencao = true;
        destinoDesejado = ponto;
    }
    IEnumerator EsperaAnim()
    {
        animando = true;
        timer = 0;
        Vector2 destino = DecidePonto();
        var a = quarto.GetEscada(destino.y);
        if (destinoDesejado.y > transform.position.y)
        {
            animator.SetTrigger("Subir");
            subindoEscadaSound.Play();
            yield return new WaitForSeconds(1.6f);
            transform.position = a.escadaDesce.transform.position;
        }
        else
        {
            transform.position = a.escadaSobe.transform.position;
            animator.SetTrigger("Descer");          
            descendoEscadaSound.Play();
            yield return new WaitForSeconds(1.6f);
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        escada = false;
        animando = false;
        timer = 0;
    }
    public void MudarDestino(Vector2 ponto, bool att = false)
    {
        parado = false;
        if (att)
        {
            DestAtencao(ponto);
            return;
        }
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
