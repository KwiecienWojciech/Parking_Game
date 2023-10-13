using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] Rigidbody rb;

    private float timer = 0f;
    public bool isTimerRunning;



    private void Update()
    {
        if (rb.velocity.magnitude > 0.1)
        {
            isTimerRunning = true;
            timer += Time.deltaTime;

            //Debug.Log(rb.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(timer);
            Debug.Log(rb.velocity.magnitude * (18f/5f));
            //Debug.Log(CarSteering.Instance.GetMotorTorque());
        }
    }
}



