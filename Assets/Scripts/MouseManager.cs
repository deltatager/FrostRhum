using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : Singleton<MouseManager>
{
    //Know what objects are clickable
    public LayerMask clickableLayer;

    //Swap Cursors per object
    public Texture2D pointer; //Normal Pointer
    public Texture2D target; //Cursor for clickable objects like the world
    public Texture2D doorway; //Cursor for doorways
    public Texture2D item; //Cursor combat actions

    public ClickEvent onClickEnvironment;

	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool itm = false;

             if(hit.collider.gameObject.CompareTag("Doorway"))
             {
                 Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                 door = true;
             }
            
             else if(hit.collider.gameObject.CompareTag("Item"))
             {
                 Cursor.SetCursor(item, new Vector2(16, 16), CursorMode.Auto);
                 itm = true;
             }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (door)
                {
                    Transform doorPos = hit.collider.gameObject.transform;

                    onClickEnvironment.Invoke(doorPos.position);
                }

                else if(itm)
                {
                    Transform itemPos = hit.collider.gameObject.transform;
                
                    onClickEnvironment.Invoke(itemPos.position);
                }

                else
                {
                    onClickEnvironment.Invoke(hit.point);
                }
            }
        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
	}
}

[System.Serializable]
public class ClickEvent : UnityEvent<Vector3> { }
