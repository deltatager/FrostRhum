using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private Camera _camera;


    private void Start()
    {
        GameManager.Instance.onChange.AddListener(HandleGameStateChange);
    }

    private void HandleGameStateChange(GameManager.GameState current, GameManager.GameState previous)
    {
        _pauseMenu.gameObject.SetActive(current == GameManager.GameState.Pause);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Pregame)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
           // _mainMenu.FadeOut();
            
        }
    }


    public void SetDummyCameraActive(bool active)
    {
        _camera.gameObject.SetActive(active);
    }
}
