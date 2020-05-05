using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{

    private Animator _anim;
    public NavMeshAgent navMeshAgent;
    private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    
    void Start()
    {    
        MouseManager.Instance.onClickEnvironment.AddListener(HandleMouseClickEvent);
        _anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        _anim.SetFloat(PlayerSpeed, navMeshAgent.velocity.magnitude);
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }
    }
    
    private void HandleMouseClickEvent(Vector3 arg0)
    {
        
    }
}