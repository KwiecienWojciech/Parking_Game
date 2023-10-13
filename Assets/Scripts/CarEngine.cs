using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class CarEngine : MonoBehaviour
{
    public event EventHandler OnBraking;
    public event EventHandler OnReverseDriving;

    private const string VERTICAL = "Vertical";

    private float verticalInputThreshold = 0.1f;
    private float verticalInput;
    private float currentBreakFroce;
    private bool isBraking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    [SerializeField] private float dampenPress = 0f;
    [SerializeField] private float sensitivity = 2f;

    private Rigidbody rb;
    private GearBox gearBox;

    private void Awake()
    {
        gearBox = GetComponent<GearBox>();

        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
    private void Update()
    {
        GetTouchPedalsInput();

        //Debug.Log(rb.velocity.magnitude * (18f/5f));

        Debug.Log(frontRightWheelCollider.motorTorque);

        //Debug.Log(verticalInput);

        //Debug.Log(GetEngineSpeed());

    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        UpdateWheels();
        ApplyAirResistance();
    }
    private void GetInput()
    {
        verticalInput = Input.GetAxis(VERTICAL);

        isBraking = Input.GetKey(KeyCode.Space);

        if (CarPedalsUI.Instance.isGasButtonPressed())
        {
            verticalInput += dampenPress;
        }
        if (CarPedalsUI.Instance.isBreakButtonPressed())
        {
            verticalInput -= dampenPress;
        }
    }
    private void GetTouchPedalsInput()
    {
        if (CarPedalsUI.Instance.isGasButtonPressed())
        {
            dampenPress += sensitivity * Time.deltaTime;
        }
        if (CarPedalsUI.Instance.isBreakButtonPressed())
        {
            dampenPress -= sensitivity * Time.deltaTime;
        }
        dampenPress = Mathf.Clamp01(dampenPress);
    }
    private void HandleMotor()
    {
        OnReverseDriving?.Invoke(this, EventArgs.Empty);
        OnBraking?.Invoke(this, EventArgs.Empty);

        if (gearBox.IsGearDrive())
        {
            ManagementDrivingForward(verticalInput, verticalInputThreshold);
        }
        if (gearBox.IsGearReverse())
        {          
            ManagementDrivingReverse(verticalInput, verticalInputThreshold);
        }
        if (gearBox.IsGearNeutral())
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
        }
        if (gearBox.IsCarParked())
        {
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
        }
        if (rb.velocity.magnitude < 0.1f)
        {
            GearBoxUI.Instance.SetSliderActive();
        }
        ManagementCarBraking();
    }
    public void SetMotorTorqueAfterBreakPedalRealase(float motorTorque)
    {
        frontLeftWheelCollider.motorTorque = motorTorque;
        frontRightWheelCollider.motorTorque = motorTorque;
    }
    public void IncreasingSpeedByPressingGasPedal(float verticalInput)
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
    }
    public void ManagementDrivingForward(float verticalInput, float verticalInputThreshold)
    {
        SetMotorTorqueAfterBreakPedalRealase(50f);
        
        if (verticalInput > verticalInputThreshold)
        {
            IncreasingSpeedByPressingGasPedal(verticalInput);

            if (frontLeftWheelCollider.motorTorque > 0 && frontRightWheelCollider.motorTorque > 0)
            {
                GearBoxUI.Instance.SetSliderDisactive();
            }
        }
        else
        {
            if (rb.velocity.magnitude > 0.1 && currentBreakFroce == 0f)
            {
                Deacceleration(-200f);
            }
        }
    }
    public void ManagementDrivingReverse(float verticalInput, float verticalInputThreshold)
    {
        
        SetMotorTorqueAfterBreakPedalRealase(-50f);
        
        if (verticalInput > verticalInputThreshold)
        {
            IncreasingSpeedByPressingGasPedal(-verticalInput);

            if (frontLeftWheelCollider.motorTorque < 0 && frontRightWheelCollider.motorTorque < 0)
            {
                GearBoxUI.Instance.SetSliderDisactive();
            }
        }
        else
        {
            if (rb.velocity.magnitude > 0.1 && currentBreakFroce == 0f)
            {
                Deacceleration(200f);
            }
        } 
    }

    public void ManagementCarBraking()
    {
        currentBreakFroce = isBraking || CarPedalsUI.Instance.isBreakButtonPressed() ? breakForce : 0f;

        ApplyBreaking();
    }
    public void Deacceleration(float decelerationMultiplier)
    {
        Vector3 force = new Vector3(0, 0, 1) * decelerationMultiplier;
        rb.AddForce(force);
    }
    public void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakFroce;
        backLeftWheelCollider.brakeTorque = currentBreakFroce;
        frontRightWheelCollider.brakeTorque = currentBreakFroce;
        backRightWheelCollider.brakeTorque = currentBreakFroce; 
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }

    private void ApplyAirResistance()
    {
        float constantCoefficient = -0.3f;
        float carArea = 2.5f;
        float airDesinty = 1.225f;
        float speed = rb.velocity.magnitude;

        Vector3 airResistanceDragForce = constantCoefficient * carArea * airDesinty * speed * speed * rb.velocity.normalized;

        rb.AddForce(airResistanceDragForce);

        //Debug.Log(airResistanceDragForce);
    }
    public bool IsCarBraking()
    {
        return isBraking;
    }

}
