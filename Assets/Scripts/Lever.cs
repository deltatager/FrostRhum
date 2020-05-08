using UnityEngine;

public class Lever : MonoBehaviour
{
    private static readonly int LeverUp = Animator.StringToHash("LeverUp");
    
    private Animator _anim;
    private bool _state;
    
    public bool State => _state;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        MouseManager.Instance.onClickEnvironment.AddListener(OnClickedEvent);
    }

    private void OnClickedEvent(Vector3 point)
    {
        if (!point.Equals(transform.position)) return;

        _anim.SetBool(LeverUp, _state = !_state);
    }
}