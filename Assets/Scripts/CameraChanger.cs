using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public GameObject [] cameras;
    public int activeCamera;

    void Start()
    {
        //либо метод загрузки или иная камера
        activeCamera = 0;

        foreach(GameObject cam in cameras)
        {
            cam.SetActive(false);
        }

        cameras[activeCamera].SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
            ChangeCamera();
    }

    void ChangeCamera()
    {
        cameras[activeCamera].SetActive(false);

        if(activeCamera + 1 >= cameras.Length)
            activeCamera = 0;
        else
            activeCamera += 1;

        cameras[activeCamera].SetActive(true);
    }
}
