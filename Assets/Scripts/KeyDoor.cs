using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : Door
{

    [SerializeField] private GameObject key;

    public override void ToggleDoor()
    {
        if (!InventoryMenu.Instance.ContainsObject(key)) return;
        InventoryMenu.Instance.UseObject(key);
        base.ToggleDoor();
    }
}
