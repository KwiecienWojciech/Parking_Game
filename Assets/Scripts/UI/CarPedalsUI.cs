using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarPedalsUI : MonoBehaviour
{
    public static CarPedalsUI Instance { get; private set; }

    public bool isGasPressed;
    public bool isBreakPressed;

    [SerializeField] private GameObject gasButton;
    [SerializeField] private GameObject breakButton;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetUpGasButton();
        SetUpBreakButton();
    }

    private void SetUpGasButton()
    {
        EventTrigger gasButtonTrigger = gasButton.AddComponent<EventTrigger>();

        var pointerGasButtonDown = new EventTrigger.Entry();
        pointerGasButtonDown.eventID = EventTriggerType.PointerDown;
        pointerGasButtonDown.callback.AddListener((e) => OnClickGasButtonDown());

        var pointerGasButtonUp = new EventTrigger.Entry();
        pointerGasButtonUp.eventID = EventTriggerType.PointerUp;
        pointerGasButtonUp.callback.AddListener((e) => OnClickGasButtonUp());

        gasButtonTrigger.triggers.Add(pointerGasButtonDown);
        gasButtonTrigger.triggers.Add(pointerGasButtonUp);
    }

    private void SetUpBreakButton()
    {
        EventTrigger breakButtonTrigger = breakButton.AddComponent<EventTrigger>();

        var pointerBreakButtonDown = new EventTrigger.Entry();
        pointerBreakButtonDown.eventID = EventTriggerType.PointerDown;
        pointerBreakButtonDown.callback.AddListener((e) => OnClickBreakButtonDown());

        var pointerBreakButtonUp = new EventTrigger.Entry();
        pointerBreakButtonUp.eventID = EventTriggerType.PointerUp;
        pointerBreakButtonUp.callback.AddListener((e) => OnClickBreakButtonUp());

        breakButtonTrigger.triggers.Add(pointerBreakButtonDown);
        breakButtonTrigger.triggers.Add(pointerBreakButtonUp);
    }

    private void OnClickGasButtonDown()
    {
        isGasPressed = true;

    }
    
    private void OnClickGasButtonUp()
    {
        isGasPressed = false;
    }

    private void OnClickBreakButtonDown()
    {
        isBreakPressed = true;
    }

    private void OnClickBreakButtonUp()
    {
        isBreakPressed = false;
    }

    public bool isGasButtonPressed()
    {
        return isGasPressed;
    }

    public bool isBreakButtonPressed()
    {
        return isBreakPressed;
    }
}
