using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLights : MonoBehaviour
{
    [SerializeField] private List<GameObject> brakingLightsGameObjects;
    [SerializeField] private List<GameObject> reverseLightsGameObjects;

    private GearBox gearBox;
    private CarEngine carEngine;

    private void Awake()
    {
        carEngine = GetComponent<CarEngine>();
        gearBox = GetComponent<GearBox>();
    }

    private void Start()
    {
        SetLightsDisctive(brakingLightsGameObjects);
        SetLightsDisctive(reverseLightsGameObjects);

        carEngine.OnBraking += CarEngine_OnBraking;
        carEngine.OnReverseDriving += CarEngine_OnReverseDriving;
    }

    private void CarEngine_OnReverseDriving(object sender, System.EventArgs e)
    {
        if (gearBox.IsGearReverse())
        {
            SetLightsActive(reverseLightsGameObjects);
        }
        else
        {
            SetLightsDisctive(reverseLightsGameObjects);
        }
    }

    private void CarEngine_OnBraking(object sender, System.EventArgs e)
    {
        if (carEngine.IsCarBraking() || CarPedalsUI.Instance.isBreakButtonPressed())
        {
            SetLightsActive(brakingLightsGameObjects);
        }
        else
        {
            SetLightsDisctive(brakingLightsGameObjects);
        }
    }

    private void SetLightsActive(List<GameObject> lightsList)
    {
        foreach (GameObject lights in lightsList)
        {
            lights.SetActive(true);
        }
    }
    private void SetLightsDisctive(List<GameObject> lightsList)
    {
        foreach (GameObject lights in lightsList)
        {
            lights.SetActive(false);
        }
    }


}
