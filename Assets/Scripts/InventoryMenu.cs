using System;
using System.Collections;
using System.Collections.Generic;
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
        if (previous == GameManager.GameState.Running && current == GameManager.GameState.Inventory)
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        Debug.Log("Opening inv");
        for (var i = 0; i < _inventory.Count; i++)
        {
            Debug.Log(i);
            var slot = slots[i];
            _instanciatedInventory.Add(Instantiate(_inventory[i], slot.transform.position, Quaternion.identity));
        }
    }

    private void CloseInventory()
    {
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

    public void AddObject(GameObject obj)
    {
        _inventory.Add(obj);
    }
}
