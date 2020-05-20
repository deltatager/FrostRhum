using System;
using UnityEngine;
using UnityEngine.Events;

public class GameButton : MonoBehaviour
{
    public ButtonClickedEvent buttonClickedEvent;
    private Animation _anim;
    private AudioSource _audioSource;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        _audioSource = GetComponent<AudioSource>();
        MouseManager.Instance.onClickEnvironment.AddListener(OnClickedEvent);
    }

    private void OnClickedEvent(Vector3 point)
    {
        
        if (!point.Equals(transform.position)) return;
        if (_anim != null)
            _anim.Play();
        _audioSource.Play();
        buttonClickedEvent.Invoke();
    }
}

[Serializable]
public class ButtonClickedEvent : UnityEvent
{
}