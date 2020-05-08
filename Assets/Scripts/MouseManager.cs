using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private LayerMask clickableLayer;
    [SerializeField] private Texture2D pointer; //Normal Pointer
    [SerializeField] private Texture2D target; //Cursor for clickable objects like the world
    [SerializeField] private Texture2D doorway; //Cursor for doorways
    [SerializeField] private Texture2D item; //Cursor combat actions
    [SerializeField] private float interactionDistance;
    
    private GameObject _player;

    public ClickEvent onClickEnvironment;
    

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += (arg0, mode) =>
        {
            Debug.Log("New scene loaded, re-finding player...");
            _player = GameObject.FindGameObjectWithTag("Player");
        };
    }
    

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 50, clickableLayer.value))
        {
            if (hit.collider.gameObject.CompareTag("Doorway"))
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
            }
            else if (hit.collider.gameObject.CompareTag("Item"))
            {
                Cursor.SetCursor(item, new Vector2(16, 16), CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                interactionDistance = 1;
                if (Vector3.Distance(hit.point, _player.transform.position) < interactionDistance)
                {
                    onClickEnvironment.Invoke(hit.collider.gameObject.transform.position);
                }
                else
                {
                    _player.GetComponent<NavMeshAgent>().destination = hit.point;
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[Serializable]
public class ClickEvent : UnityEvent<Vector3>
{
}