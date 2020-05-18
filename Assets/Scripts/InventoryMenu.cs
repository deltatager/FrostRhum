using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    private List<GameObject> _inventory;
    private List<GameObject> _instanciatedInventory;
    
    [SerializeField]
    private List<GameObject> slots;

    private void Start()
    {
        _inventory = new List<GameObject>();
        _instanciatedInventory = new List<GameObject>();
        
    }

    private void OnEnable()
    {
        for (var i = 0; i < _inventory.Count; i++)
        {
            var slot = slots[i];
            _instanciatedInventory.Add(Instantiate(_inventory[i], slot.transform.position, Quaternion.identity));
        }
    }

    private void OnDisable()
    {
        foreach (var instance in _instanciatedInventory)
        {
            Destroy(instance);
        }
        _instanciatedInventory.Clear();
    }
    
    void Update()
    {
        
    }

    public bool UseObject(GameObject expected)
    {
        return _inventory.Remove(expected);
    }

    public void AddObject(GameObject obj)
    {
        _inventory.Add(obj);
    }



    
}
