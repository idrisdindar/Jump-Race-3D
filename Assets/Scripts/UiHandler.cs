using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{
    [SerializeField] private GameObject gamePlayUI, gameOverUI, endLevelUI, gameStartUI;
    private static UiHandler _instance;

    public Slider slider;
    public static UiHandler Instance { get { return _instance; } }

    public bool isGameStarted = false;
    public bool isGameEnded = false;
    public bool IsGameStarted { get => isGameStarted;}
    public bool IsGameEnded { get => isGameEnded;}

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void StartGameButton()
    {
        isGameStarted = true;
        isGameEnded = false;
        gameStartUI.SetActive(false);
        gamePlayUI.SetActive(true);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLeveLButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        isGameEnded = true;
        gamePlayUI.SetActive(false);
        Invoke("PopLoseCanvas", .3f);
    }
    void PopLoseCanvas()
    {
        gameOverUI.SetActive(true);
    }
    public void LevelFinish()
    {
        if (!isGameEnded)
        {
            isGameEnded = true;
            Invoke("PopWinCanvas", .5f);
        }
    }
    void PopWinCanvas()
    {
        endLevelUI.SetActive(true);
    }

    public void SetupSlider(int trampolineCount)
    {
        slider.maxValue = trampolineCount;
    }
    public void SetSliderValue(Trampoline trampoline)
    {
        Trampolines trampolines = trampoline.GetComponentInParent<Trampolines>();
        slider.value = System.Array.IndexOf(trampolines.trampolines, trampoline);
    }
}
