using System;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    [SerializeField] private GameObject scrollUi;
    
    void Start()
    {
        MouseManager.Instance.onClickEnvironment.AddListener(HandleClickEvent);
    }

    private void Update()
    {
        if (scrollUi.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            scrollUi.SetActive(false);
            Cursor.visible = true;
        }
    }

    void HandleClickEvent(Vector3 clickPos)
    {
        if (clickPos.Equals(transform.position))
        {
            scrollUi.SetActive(true);
            Cursor.visible = false;
        }
    }
}
