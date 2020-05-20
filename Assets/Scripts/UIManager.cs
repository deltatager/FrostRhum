using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable once InconsistentNaming
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private GameObject inventoryMenu;
    
    
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.onGameStateChange.AddListener(HandleGameStateChange);
        
        
    }
    
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        
        
    }
    
    

    private void HandleGameStateChange(GameManager.GameState current, GameManager.GameState previous)
    {
        pauseMenu.gameObject.SetActive(current == GameManager.GameState.Pause);
        inventoryMenu.gameObject.SetActive(current == GameManager.GameState.Inventory);

        if (previous == GameManager.GameState.Running && current == GameManager.GameState.Inventory)
        {
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        }
        
        
    }
}