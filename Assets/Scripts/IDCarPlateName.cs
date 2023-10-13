using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IDCarPlateName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI carPlateName;

    private string playerBackIDPlateName;

    private void Update()
    {
        playerBackIDPlateName = NameInput.Instance.GetPlayerInputName();

        carPlateName.text = playerBackIDPlateName;

        Debug.Log(playerBackIDPlateName);
    }
}
