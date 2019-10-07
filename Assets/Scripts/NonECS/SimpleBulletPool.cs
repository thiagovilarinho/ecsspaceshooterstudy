using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class SimpleBulletPool : MonoBehaviour
    {
        public static SimpleBulletPool instance;
        public GameObject poolObject;

        [SerializeField]
        private int _poolSize = 50;

        private GameObject[] _pool;

        private int _currentIndex = 1;

        public GameObject GetGameObject()
        {
            _currentIndex++;

            if (_currentIndex > _poolSize)
            {
                _currentIndex = 1;
            }

            return _pool[_currentIndex - 1];
        }

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            _pool = new GameObject[_poolSize];
            for (int i = 0; i < _poolSize; i++)
            {
                _pool[i] = GameObject.Instantiate(poolObject);
                _pool[i].SetActive(false);
            }
        }
    }

}
