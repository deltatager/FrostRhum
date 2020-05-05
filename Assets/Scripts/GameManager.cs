using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>{}


public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running,
        Pause
    }
    private GameState _currentGameState = GameState.Running;
    [FormerlySerializedAs("onChange")] public EventGameState onGameStateChange;
    
    private string _currentLevelName = "Room1";
    private List<AsyncOperation> _loadOperations;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
    }


    private void Update()
    {
        if (_currentGameState != GameState.Pregame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
        }
    }
    
    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
       if (ao==null)
       {
           Debug.Log("Unable to load level: "+levelName);
       }
       ao.completed += OnLoadOperationComplete;
       _loadOperations.Add(ao);
       _currentLevelName = levelName;
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
    
    
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao==null)
        {
            Debug.Log("Unable to unload level: "+levelName);
        }
        ao.completed += OnUnloadOperationComplete;
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload completed");
    }

    void UpdateGameState(GameState gameState)
     {
         GameState previous = _currentGameState;
         _currentGameState = gameState;
         switch(_currentGameState)
         {
             case GameState.Pregame:
                 Time.timeScale = 1.0f;
                 break;
             case GameState.Running:
                 Time.timeScale = 1.0f;
                 break;
             case GameState.Pause:
                 Time.timeScale = 0.0f;
                 break;
             default:
                 break;
             
         }
         
         onGameStateChange.Invoke(_currentGameState, previous);
     }
     public GameState CurrentGameState
     {
         get
         {
             return _currentGameState;
         }
         private set
         {
             _currentGameState = value;
         }
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
         if (_currentGameState == GameState.Running)
         {
             UpdateGameState(GameState.Pause);
         } else if (_currentGameState == GameState.Pause)
         {
             UpdateGameState(GameState.Running);
         }
         
         
     }
}
