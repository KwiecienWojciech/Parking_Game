using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;


public class GearBox : MonoBehaviour
{
    [SerializeField] private Slider gearboxSlider;

    public bool isParked;
    public bool isSetGearReverse;
    public bool isSetGearNeutral;
    public bool isSetGearDrive;

    private enum DrivingMode
    {
        Parked,
        ReverseMode,
        NeutralMode,
        DriveMode   
    }

    private void Start()
    {
        gearboxSlider.value = 2;
    }

    private void Update()
    {
        SetDrivingMode();
    }
    private void SetDrivingMode()
    {
        DrivingMode mode = (DrivingMode)gearboxSlider.value;
        SetDrivingMode(mode);
    }

    private void SetDrivingMode(DrivingMode mode)
    {
        isSetGearNeutral = mode == DrivingMode.NeutralMode;
        isSetGearReverse = mode == DrivingMode.ReverseMode;
        isSetGearDrive = mode == DrivingMode.DriveMode;
        isParked = mode == DrivingMode.Parked;
    }
    public bool IsGearDrive()
    {
        return isSetGearDrive;
    }
    public bool IsGearReverse()
    {
        return isSetGearReverse;
        
    }
    public bool IsGearNeutral()
    {
        return isSetGearNeutral;
    }
    public bool IsCarParked()
    {
        return isParked;
    }

}
