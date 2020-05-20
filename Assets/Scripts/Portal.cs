using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private String sceneToLoadName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sceneToLoadName == "End")
            {
                Application.Quit();
            }
            else
            {
                GameManager.Instance.LoadLevel(sceneToLoadName);
            }
            
        }
    }
}
