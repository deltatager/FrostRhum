﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[Serializable]
public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
{
}


public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Running,
        Pause,
        Inventory
    }

    private GameState _currentGameState = GameState.Running;
    [SerializeField] public EventGameState onGameStateChange;
    
    private List<AsyncOperation> _loadOperations;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
        if (Input.GetKeyDown(KeyCode.I))
            ToggleInventory();
        
        
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
       if (ao==null)
       {
           Debug.Log("Unable to load level: "+levelName);
           return;
       }
       ao.completed += OnLoadOperationComplete;
       _loadOperations.Add(ao);
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            if (_loadOperations.Count == 0)
            {
                UpdateGameState(GameState.Running);
            }
        }
            
        Debug.Log("Load completed");
    }

    private void UpdateGameState(GameState gameState)
     {
         var previous = _currentGameState;
         _currentGameState = gameState;
         switch(_currentGameState)
         {
             case GameState.Running:
                 Time.timeScale = 1.0f;
                 break;
             case GameState.Pause:
                 Time.timeScale = 0.0f;
                 break;
             case GameState.Inventory:
                 //Time.timeScale = 0.0f;
                 break;

             default:
                 throw new ArgumentOutOfRangeException(nameof(gameState));
         }
         
         onGameStateChange.Invoke(_currentGameState, previous);
     }

    public void RestartGame()
     {
         
         
     }
     
     public void Quit()
     {
         Application.Quit();
     }
     
     
     public void TogglePause()
     {
         switch (_currentGameState)
         {
             case GameState.Running:
                 UpdateGameState(GameState.Pause);
                 break;
             case GameState.Pause:
                 UpdateGameState(GameState.Running);
                 break;
         }
     }
     
     
     public void ToggleInventory()
     {
         switch (_currentGameState)
         {
             case GameState.Running:
                 UpdateGameState(GameState.Inventory);
                 break;
             case GameState.Inventory:
                 UpdateGameState(GameState.Running);
                 break;
         }
     }
}