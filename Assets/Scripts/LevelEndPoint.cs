using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class LevelEndPoint : MonoBehaviour
{

    [SerializeField] private Transform carTransform;
    [SerializeField] private GameObject unfinishedGameObject;
    [SerializeField] private GameObject finishedGameObject;


    private float timer = 3f;
    private float timerMax = 3f;

    private void LevelFinished()
    {
        if (Distance() < 1f)
        {
            unfinishedGameObject.SetActive(false);
            finishedGameObject.SetActive(true);
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                Destroy(carTransform.gameObject);
                //SceneManager.LoadScene(1);
            }
        }
        else
        {
            timer = timerMax;
            unfinishedGameObject.SetActive(true);
            finishedGameObject.SetActive(false);
        }
    }

    private float Distance()
    {
        Vector3 endPointPosition = this.transform.position;

        Vector3 carPosition = carTransform.position;

        float distance = Vector3.Distance(endPointPosition, carPosition);

        return distance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelFinished();
        }
    } 
}
