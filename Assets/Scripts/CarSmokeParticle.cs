using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSmokeParticle : MonoBehaviour
{
    private const string VERTICAL = "Vertical";

    private ParticleSystem smokeParticleSystem;
    [SerializeField] private AnimationCurve smokeParticleSystemSimulationSpeedCurve;


    private void Awake()
    {
        smokeParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var main = smokeParticleSystem.main;

        main.simulationSpeed = smokeParticleSystemSimulationSpeedCurve.Evaluate(Input.GetAxis(VERTICAL));

    }
}
