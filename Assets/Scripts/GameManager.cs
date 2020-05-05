using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>{}


public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running,
        Pause
    }
    private GameState _currentGameState = GameState.Pregame;
    public EventGameState onChange;
    
    private string currentLevelName = string.Empty;
    private List<AsyncOperation> _loadOperations;

    [SerializeField] private GameObject[] _systemPrefabs;
    private List<GameObject> _instancedSystemPrefabs;
    
    private GameManager _instance;
    
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        InstanciateSystemPrefabs();
    }


    private void Update()
    {
        if (_currentGameState != GameState.Pregame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("HEY MAN");
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
       currentLevelName = levelName;
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


     void InstanciateSystemPrefabs()
    {
        _instancedSystemPrefabs = new List<GameObject>();
        foreach (GameObject go in _systemPrefabs)
            _instancedSystemPrefabs.Add(Instantiate(go));
        
    }

     protected void OnDestroy()
     {
         base.OnDestroy();
         foreach (GameObject go in _instancedSystemPrefabs)
         {
             Destroy(go);
         }
         _instancedSystemPrefabs.Clear();
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
         
         onChange.Invoke(_currentGameState, previous);
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

     public void StartGame()
     {
         
         LoadLevel("SampleScene");
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
