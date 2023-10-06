using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerController playerController;
    private ObjectBaseController attentionObjectBaseController;
    private ObjectBaseController scaryObjectBaseController;
    private bool canInteractAttention = false;
    private bool canInteractScary = false;

     void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Attention")
        {
            canInteractAttention = true;
            attentionObjectBaseController = trigger.gameObject.GetComponent<ObjectBaseController>();
        }
        
        if (trigger.gameObject.tag == "Scary")
        {
            canInteractScary = true;
            scaryObjectBaseController = trigger.gameObject.GetComponent<ObjectBaseController>();
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Attention")
        {
            canInteractAttention = false;
            attentionObjectBaseController = null;
        }
        if (trigger.gameObject.tag == "Scary")
        {
            canInteractScary = false;
            scaryObjectBaseController = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canInteractAttention)
                playerController.CallAttention(attentionObjectBaseController);
            if (canInteractScary)
                playerController.InteractScary(scaryObjectBaseController);
        }
    }
}
