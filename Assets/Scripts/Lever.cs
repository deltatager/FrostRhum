using UnityEngine;

public class Lever : MonoBehaviour
{
    private static readonly int LeverUp = Animator.StringToHash("LeverUp");

    [SerializeField] private bool inverted;
    
    private Animator _anim;
    private bool _state;
    
    public bool State => _state ^ inverted;

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