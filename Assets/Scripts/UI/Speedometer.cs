using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private GameObject carGameObject;
    [SerializeField] private TextMeshProUGUI speedometerText;


    private float carVelocity;

    private Rigidbody rb;

    private void Awake()
    {
        rb = carGameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetCarVelocity();
    }

    private void GetCarVelocity()
    {
        carVelocity = Mathf.Floor(rb.velocity.magnitude * (18f/5f));

        speedometerText.text = carVelocity.ToString() + " km/h";
    }

}
