using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameActionButtons : MonoBehaviour
{

    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() => RestartGame());
        exitButton.onClick.AddListener(() => ExitGame());
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

}
