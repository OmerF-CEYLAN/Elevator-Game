using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class SequentialCameraSwitchStrategy : ICameraSwitchStrategy
    {
        public int GetNextCameraIndex(int currentIndex, int totalCameras)
        {
            return (currentIndex + 1) % totalCameras; //If the camera is not the last one on the list then next but if the camera is last one then first.
        }
    }
}
