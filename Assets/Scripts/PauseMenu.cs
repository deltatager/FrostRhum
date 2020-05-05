﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
        
        
        
    }

    private void HandleQuitClicked()
    {
        GameManager.Instance.Quit();
    }

    private void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    private void HandleResumeClicked()
    {
        GameManager.Instance.TogglePause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
