using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public bool isOpened = false;
    
    [Range(0f, 4f)] 
    [Tooltip("Speed for door opening, degrees per sec")]
    [SerializeField] private float openSpeed = 3f;

    private Rigidbody _rbDoor;
    private HingeJoint _hinge;
    private JointLimits _hingeLim;
    private float _currentLim;

    private void Start()
    {
        _rbDoor = GetComponent<Rigidbody>();
        _hinge = GetComponent<HingeJoint>();
        MouseManager.Instance.onClickEnvironment.AddListener(HandleClickOnDoor);
    }

    private void FixedUpdate()
    {
        if (isOpened)
        {
            _currentLim = 85f;
            _rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
        }
        else
        {
            if (_currentLim > 1f)
                _currentLim -= .5f * openSpeed;
        }
        
        _hingeLim.max = _currentLim;
        _hingeLim.min = -_currentLim;
        _hinge.limits = _hingeLim;
    }

    private void HandleClickOnDoor(Vector3 clickTarget)
    {
        if (clickTarget.Equals(transform.position))
        {
            isOpened = !isOpened;
        }
    }
}