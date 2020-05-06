using UnityEngine;
using UnityEngine.AI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private bool useDifferentCam;
    [SerializeField] private Camera newCam;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponent<NavMeshAgent>().Warp(destination.position);
        if (!useDifferentCam) return;
        
        Camera.main.gameObject.SetActive(false);
        newCam.gameObject.SetActive(true);
    }
}
