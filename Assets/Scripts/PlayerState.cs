using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class PlayerState
    {
        public abstract void EnterState(PlayerController player);
        public abstract void UpdateState(PlayerController player);
        public abstract void ExitState(PlayerController player);


    }
}
