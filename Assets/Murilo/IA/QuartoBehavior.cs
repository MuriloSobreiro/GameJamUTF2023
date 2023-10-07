using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuartoBehavior : MonoBehaviour
{
    public List<IABehavior> NPCs = new List<IABehavior>();
    public List<QuartoBehavior> Quartos = new List<QuartoBehavior>();
    public GameObject escadaSobe, escadaDesce;
    public SpriteRenderer fundo;
    public bool paredeEsquerda, paredeDireita;
    public void AddNPC(IABehavior npc)
    {
        if(!NPCs.Contains(npc))
            NPCs.Add(npc);
    }
    public void RemoveNPC(IABehavior npc)
    {
        NPCs.Remove(npc);
    }
    public void Assustar()
    {
        foreach (var npc in NPCs)
        {
            npc.Assustar();
        }
    }

    public void Atrair(Vector2 ponto)
    {
        
        foreach (var q in Quartos)
        {
            q.AtrairNpcs(ponto);
        }
    }
    public void AtrairNpcs(Vector2 ponto)
    {

        foreach (var npc in NPCs)
        {
            npc.MudarDestino(ponto, true);
        }
    }
    public bool GameOver()
    {
        if (NPCs.Count > 0)
        {
            foreach (var npc in NPCs)
            {
                npc.Dancar();
            }
            return true;
        }
        
        return false;
    }
    public QuartoBehavior GetEscada(float y)
    {
        QuartoBehavior q = this;
        foreach(var quarto in Quartos)
        {
            if(Mathf.Abs(quarto.transform.position.y - y) > Mathf.Abs(q.transform.position.y - y))
            {
                q = quarto;
            }
        }
        return q;
    }
    private void Start()
    {
        var quartos  = Physics2D.OverlapBoxAll(transform.position, new Vector2(2.1f, 2.1f),0);
        foreach(var q in quartos)
        {
            if (q.tag == "Quarto")
            {
                if (q.transform.position.x == transform.position.x || q.transform.position.y == transform.position.y)
                {
                    if (q.transform.position.y < transform.position.y && escadaDesce)
                        Quartos.Add(q.GetComponent<QuartoBehavior>());
                    else if (q.transform.position.y > transform.position.y && escadaSobe)
                        Quartos.Add(q.GetComponent<QuartoBehavior>());
                    else if (q.transform.position.x < transform.position.x && !paredeEsquerda)
                        Quartos.Add(q.GetComponent<QuartoBehavior>());
                    else if (q.transform.position.x > transform.position.x && !paredeDireita)
                        Quartos.Add(q.GetComponent<QuartoBehavior>());
                }
            }
        }
        Quartos.Remove(this);
    }
    private void Update()
    {
        if(NPCs.Count>0)
            fundo.color = new Color(0.5f, 0.7f, 0.2f);
        else
            fundo.color = new Color(0.5f, 0.5f, 0.5f);
    }
}
