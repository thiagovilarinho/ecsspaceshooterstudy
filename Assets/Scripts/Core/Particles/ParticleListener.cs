using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Core
{
    public class ParticleListener : MonoBehaviour
    {
        public Action ParticleStopped;

        public void OnParticleSystemStopped()
        {
            ParticleStopped?.Invoke();
        }
    }
}
