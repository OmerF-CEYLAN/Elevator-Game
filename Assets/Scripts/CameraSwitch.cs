using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    List<GameObject> cameras;

    int activeCam;

    void Start()
    {
        DisableCameras();

        cameras[activeCam].SetActive(true);
    }

    void Update()
    {
        SwitchCameras();
    }

    void SwitchCameras()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < cameras.Count; i++)
            {
                cameras[i].SetActive(!cameras[i].activeSelf);
            }
        }
    }

    void DisableCameras()
    {
        for(int i = 0;i < cameras.Count;i++)
        {
            cameras[i].SetActive(false);
        }
    }
}
