using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class NameInput : MonoBehaviour
{

    public static NameInput Instance {get; private set;}

    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TextMeshProUGUI welcomeTextMeshPro;

    private string playerNameInput;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        welcomeTextMeshPro.text = PlayerPrefs.GetString("welcome_user_name");
    }

    public void CreateUserName()
    {
        welcomeTextMeshPro.text = "Welcome " + nameInputField.text;
        PlayerPrefs.SetString("welcome_user_name", welcomeTextMeshPro.text);
        PlayerPrefs.Save();

        playerNameInput = nameInputField.text;
        PlayerPrefs.SetString("user_name", playerNameInput);
        PlayerPrefs.Save();

        nameInputField.text = null; 
    }

    public void ShowTouchKeyboard()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public string GetPlayerInputName()
    {
        string playerName = PlayerPrefs.GetString("user_name");
        
        return playerName;
    }


}
