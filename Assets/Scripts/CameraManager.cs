using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> cameras;

        [SerializeField]
        CameraController controller;

        CameraController lastCameraController;

        [SerializeField] 
        private KeyCode switchCameraKey = KeyCode.C;

        private int activeCameraIndex = 0;
        private ICameraSwitchStrategy switchStrategy;

        void Awake()
        {
            controller = cameras[0].GetComponent<CameraController>();
            switchStrategy = new SequentialCameraSwitchStrategy();
        }

        void Start()
        {
            DisableAllCameras();
            ActivateCamera(activeCameraIndex);
        }

        void Update()
        {
            CheckForCameraSwitch();
        }

        private void CheckForCameraSwitch()
        {
            if (Input.GetKeyDown(switchCameraKey))
            {
                SwitchToNextCamera();
            }
        }

        private void SwitchToNextCamera()
        {
            if (cameras.Count == 0) return;

            lastCameraController = cameras[activeCameraIndex].GetComponent<CameraController>();

            DisableAllCameras();

            activeCameraIndex = switchStrategy.GetNextCameraIndex(activeCameraIndex, cameras.Count);

            controller = cameras[activeCameraIndex].GetComponent<CameraController>();

            controller.SetHorizontalRotation(lastCameraController.GetHorizontalRotation());

            controller.SetVerticalRotation(lastCameraController.GetVerticalRotation());

            ActivateCamera(activeCameraIndex);
        }

        void SetSwitchStrategy(ICameraSwitchStrategy strategy)
        {
            switchStrategy = strategy;
        }

        private void DisableAllCameras()
        {
            foreach (var camera in cameras)
            {
                if(camera != null)
                {
                    camera.SetActive(false);
                }
            }
        }

        private void ActivateCamera(int index)
        {
            if (IsValidIndex(index))
                cameras[index].SetActive(true);
        }

        private void DisableCamera(int index)
        {
            if (IsValidIndex(index))
                cameras[index].SetActive(false);
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < cameras.Count && cameras[index] != null;
        }
    }
}
