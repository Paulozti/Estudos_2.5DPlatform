using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        UiManager.onPlayerOutOfLives += RestartGame;
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
