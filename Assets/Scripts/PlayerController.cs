using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{

    private Animator _anim;
    private NavMeshAgent _navMeshAgent;
    private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    
    void Start()
    {
        _anim = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        _anim.SetFloat(PlayerSpeed, _navMeshAgent.velocity.magnitude);
        if (_navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
        }
    }
}