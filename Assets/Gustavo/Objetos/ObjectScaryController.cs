using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaryController : ObjectBaseController
{
    private bool wasUsed = false;
    public override bool Interagir() {
        if (wasUsed) return false;
        print("Interagiu susto");
        return true;
    }

    public override void Usar() {
        wasUsed = true;
        print("Usou susto");
    }
}
