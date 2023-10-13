using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(() => StartGame());
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
