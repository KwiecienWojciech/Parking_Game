using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class CarSteering : MonoBehaviour
{
    public static CarSteering Instance { get; private set; }

    private const string HORIZONTAL = "Horizontal";

    private float currentSteerAngle;
    private float horizontalInput;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;

    [SerializeField] private float maxSteerAngle;

    private void Awake()
    {
        Instance = this;    
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleSteering();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);

        //horizontalInput = SteeringWheelUI.Instance.GetClampedValue();
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

}

