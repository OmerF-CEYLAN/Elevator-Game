using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CameraController : MonoBehaviour
    {
        [SerializeField]
        protected float sensitivity;

        [SerializeField]
        protected float maxVerticalAngle;

        [SerializeField]
        protected Transform playerTransform;

        protected float horizontalRotation, verticalRotation;

        protected void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        protected virtual void Update()
        {
            ProcessMouseInput();
            ApplyCameraRotation();
        }

        protected void ProcessMouseInput()
        {
            horizontalRotation += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            verticalRotation -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        }

        protected abstract void ApplyCameraRotation();

        public void SetHorizontalRotation(float rotation)
        {
            horizontalRotation = rotation;
        }
        public void SetVerticalRotation(float rotation)
        {
            verticalRotation = rotation;
        }

        public float GetHorizontalRotation()
        {
            return horizontalRotation;
        }
        public float GetVerticalRotation()
        {
            return verticalRotation;
        }
    }
}
