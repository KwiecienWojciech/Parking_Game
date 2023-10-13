using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class GearBoxUI : MonoBehaviour
{
    public static GearBoxUI Instance { get; private set; }

    [SerializeField] private Slider gearboxSlider;
    [SerializeField] List<TextMeshProUGUI> gearsTextList;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitEventsSystem();
    }
    private void Update()
    {
        SetDrivingModeOnGearBoxUI();

    }

    void InitEventsSystem()
    {
        EventTrigger events = gearboxSlider.gameObject.GetComponent<EventTrigger>();

        if (events == null)
            events = gearboxSlider.gameObject.AddComponent<EventTrigger>();

        if (events.triggers == null)
            events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData>  functionCall = new UnityAction<BaseEventData>(ReleaseEvent);//
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = callback;

        events.triggers.Add(entry);
    }
    private void SetDrivingModeOnGearBoxUI()
    {
        if (gearboxSlider.value == 0)
        {
            MarkWorkingDrivingMode("P");
        }
        if (gearboxSlider.value == 1)
        {
            MarkWorkingDrivingMode("R");
        }
        if (gearboxSlider.value == 2)
        {
            MarkWorkingDrivingMode("N");
        }
        if (gearboxSlider.value == 3)
        {
            MarkWorkingDrivingMode("D");
        }
    }

    private void MarkWorkingDrivingMode(string gearName)
    {
        foreach (TextMeshProUGUI gearText in gearsTextList)
        {
            if (gearText.name == gearName)
            {
                gearText.color = Color.green;
            }
            else
            {
                gearText.color = Color.white;
            }
        }
    }

    public void SetSliderDisactive()
    {
        gearboxSlider.enabled = false;
    }

    public void SetSliderActive()
    {
        gearboxSlider.enabled = true;
    }

    private void ReleaseEvent(BaseEventData eventData)
    {
        if (gearboxSlider.value <= 0.3f)
        {
            gearboxSlider.value = 0;
        }
        if (gearboxSlider.value > 0.3f && gearboxSlider.value <= 1.45f)
        {
            gearboxSlider.value = 1;
        }
        if (gearboxSlider.value > 1.45f && gearboxSlider.value <= 2.5f )
        {
            gearboxSlider.value = 2;
        }
        if (gearboxSlider.value > 2.5f)
        {
            gearboxSlider.value = 3;
        }
    }
}
