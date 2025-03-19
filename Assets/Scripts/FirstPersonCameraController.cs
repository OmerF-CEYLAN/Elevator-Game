using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class FirstPersonCameraController : CameraController
    {
        protected override void ApplyCameraRotation()
        {
            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

            if(playerTransform != null)
            {
                playerTransform.rotation = Quaternion.Euler(0f,horizontalRotation,0f);
            }
        }
    }

}