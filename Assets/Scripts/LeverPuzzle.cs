using System;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private List<Lever> levers;
    [SerializeField] private Door door;

    private void LateUpdate()
    {
        if (levers.TrueForAll(lever => lever.State))
        {
            door.isOpened = true;
        }
    }
}