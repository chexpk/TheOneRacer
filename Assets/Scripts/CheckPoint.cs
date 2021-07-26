using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    GhostController ghostController;
    bool isPointMarked = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MarkPoint(true);
            Debug.Log("запись началась");
            ghostController.StartRecord();
        }
    }

    void MarkPoint(bool status)
    {
        isPointMarked = status;
    }
}
