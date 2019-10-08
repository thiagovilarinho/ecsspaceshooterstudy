using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.Core
{
    [ExecuteInEditMode]
    public class Poolable : MonoBehaviour
    {
        public int size;

        public int Key { get { return gameObject.GetHashCode(); } }
    }
}

