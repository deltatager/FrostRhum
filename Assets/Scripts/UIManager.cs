using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once InconsistentNaming
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PauseMenu pauseMenu;
    private void Start()
    {
        GameManager.Instance.onGameStateChange.AddListener(HandleGameStateChange);
    }

    private void HandleGameStateChange(GameManager.GameState current, GameManager.GameState previous)
    {
        pauseMenu.gameObject.SetActive(current == GameManager.GameState.Pause);
    }
}