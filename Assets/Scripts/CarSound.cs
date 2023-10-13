using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    private float pitchFromCar;
    private float currentSpeed;

    private Rigidbody carRb;
    private AudioSource carAudio;

    void Start()
    {
        carAudio = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude;
        pitchFromCar = carRb.velocity.magnitude / 60f;

        if (currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
