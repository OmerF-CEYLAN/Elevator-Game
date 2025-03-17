using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface ICameraSwitchStrategy
    {
        int GetNextCameraIndex(int currentIndex, int totalCameras);
    }

}
