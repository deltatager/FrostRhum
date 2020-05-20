using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryMenu : Singleton<InventoryMenu>
{
    [SerializeField]
    private List<GameObject> _inventory;
    [SerializeField]
    private List<GameObject> _instanciatedInventory;
    
    [SerializeField]
    private List<GameObject> slots;
    

    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.onGameStateChange.AddListener(HandleGameStateChange);
        _inventory = new List<GameObject>();
        _instanciatedInventory = new List<GameObject>();
        
    }
    
    private void HandleGameStateChange(GameManager.GameState current, GameManager.GameState previous)
    {
        if (previous == GameManager.GameState.Running && current == GameManager.GameState.Inventory)
        {
            OpenInventory();
        }
        if (previous == GameManager.GameState.Inventory && current == GameManager.GameState.Running)
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        Debug.Log("Opening inv");
        for (var i = 0; i < _inventory.Count; i++)
        {
            var slot = slots[i];
            var instantiate = Instantiate(_inventory[i], slot.transform.position, Quaternion.identity, slot.transform);
            _instanciatedInventory.Add(instantiate);
            _instanciatedInventory[i].transform.localScale *= 100;
            
        }
        
    }

    private void CloseInventory()
    {
        Debug.Log("Inventory closed");
        foreach (var instance in _instanciatedInventory)
        {
            Destroy(instance);
        }
        _instanciatedInventory.Clear();
    }

    public bool UseObject(GameObject expected)
    {
        return _inventory.Remove(expected);
    }

    public bool ContainsObject(GameObject expected)
    {
        return _inventory.Contains(expected);
    }

    public void AddObject(GameObject obj)
    {
        _inventory.Add(obj);
        //Out of sight out of mind
        obj.transform.Translate(Vector3.one * -1000);
    }
}
