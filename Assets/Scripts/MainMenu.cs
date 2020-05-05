using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Track the animation component
    // Track the animation clips for fadein fadeout
    // Functions that can receive animations events
    // Functions to play fadein fadeout animations

    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    private void Start()
    {
        GameManager.Instance.onChange.AddListener(HandleGameStateChanged);
    }

    private void HandleGameStateChanged(GameManager.GameState currentGameState, GameManager.GameState previousGameState)
    {
        if (previousGameState == GameManager.GameState.Pregame && currentGameState == GameManager.GameState.Running)
        {
            FadeOut();
        }
        
        if (previousGameState == GameManager.GameState.Pregame && currentGameState == GameManager.GameState.Running)
        {
            FadeIn();
        }
    
    }

    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FadeOut Complete");
    }

    public void OnFadeInComplete()
    {
        Debug.LogWarning("FadeIn Complete");
        UIManager.Instance.SetDummyCameraActive(true);
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActive(false);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();


    }
}
