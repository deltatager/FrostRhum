using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    public bool isOpened = false;
    [Range(0f, 4f)]
    [Tooltip("Speed for door opening, degrees per sec")]
    public float openSpeed = 3f;
    
    // Hinge
    private Rigidbody _rbDoor;
    private HingeJoint _hinge;
    private JointLimits _hingeLim;
    private float _currentLim;

    void Start()
    {
        _rbDoor = GetComponent<Rigidbody>();
        _hinge = GetComponent<HingeJoint>();
        MouseManager.Instance.onClickEnvironment.AddListener(HandleClickOnDoor);
    }
    private void FixedUpdate() // door is physical object
    {
        if (isOpened)
        {
            _currentLim = 85f;
            _rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
        }
        else
        {
            // currentLim = hinge.angle; // door will closed from current opened angle
            if (_currentLim > 1f)
                _currentLim -= .5f * openSpeed;
        }

        // using values to door object
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
