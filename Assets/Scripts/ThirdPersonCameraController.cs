using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ThirdPersonCameraController : CameraController
    {
        float storedHorizontalRotation, storedVerticalRotation, freeLookHorizontalRotation;

        [SerializeField]
        KeyCode freeLookKeyCode = KeyCode.LeftAlt;

        [SerializeField]
        private Transform cameraRig;

        bool isFreeLooking = false;

        protected override void Update()
        {
            ProcessMouseInput();
            HandleFreeLook();
            ApplyCameraRotation();
        }


        protected override void ApplyCameraRotation()
        {
            if(cameraRig == null || playerTransform == null) return;

            if(isFreeLooking)
            {
                cameraRig.localRotation = Quaternion.Euler(verticalRotation,horizontalRotation,0f);
            }
            else
            {
                cameraRig.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
                playerTransform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
            }
        }

        void EnterFreeLook()
        {
            isFreeLooking = true;
            storedHorizontalRotation = horizontalRotation;
            storedVerticalRotation = verticalRotation;
            freeLookHorizontalRotation = 0f;
            horizontalRotation = 0f;
            verticalRotation = 0f;
        }

        void ExitFreeLook()
        {
            isFreeLooking = false;
            horizontalRotation = storedHorizontalRotation;
            verticalRotation = storedVerticalRotation;
        }

        void HandleFreeLook()
        {
            if(Input.GetKeyDown(freeLookKeyCode))
            {
                EnterFreeLook();
            }
            else if(Input.GetKeyUp(freeLookKeyCode))
            {
                ExitFreeLook();
            }
        }
    }
}
