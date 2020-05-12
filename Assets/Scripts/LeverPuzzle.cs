using System;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private List<Lever> levers;
    [SerializeField] private GameButton button;
    [SerializeField] private Door door;
    [SerializeField] private Portal goodPortal;
    [SerializeField] private Portal badPortal;

    private void Awake()
    {
        button.buttonClickedEvent.AddListener(OnButtonClickedHandler);
    }

    private void OnButtonClickedHandler()
    {
        door.isOpened = true;
        goodPortal.gameObject.SetActive(levers.TrueForAll(lever => lever.State));
        badPortal.gameObject.SetActive(!levers.TrueForAll(lever => lever.State));

    }
}