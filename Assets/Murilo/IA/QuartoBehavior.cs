using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuartoBehavior : MonoBehaviour
{
    private List<IABehavior> NPCs = new List<IABehavior>();
    public List<QuartoBehavior> Quartos = new List<QuartoBehavior>();
    public void AddNPC(IABehavior npc)
    {
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
        foreach (var npc in NPCs)
        {
            npc.MudarDestino(ponto);
        }
    }
    private void Start()
    {
        var quartos  = Physics2D.OverlapBoxAll(transform.position, new Vector2(2.1f, 2.1f),0);
        foreach(var q in quartos)
        {
            if (q.tag == "Quarto")
            {
                if(q.transform.position.x == transform.position.x || q.transform.position.y == transform.position.y)
                    Quartos.Add(q.GetComponent<QuartoBehavior>());
            }
        }
        Quartos.Remove(this);
    }
}
