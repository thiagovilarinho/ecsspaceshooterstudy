using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimpleSpace.Core
{
    /// <summary>
    /// Simple PoolManager to manage all poolable objects on the scene
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        /// <summary>
        /// A list of pool objects to initilize them when the game starts
        /// </summary>
        public List<AssetReference> poolObjects = new List<AssetReference>();

        /// <summary>
        /// A dictionary to separate all objects and be able to get them using a key value
        /// </summary>
        private readonly Dictionary<int, List<GameObject>> _listPools = new Dictionary<int, List<GameObject>>();

        protected override void Awake()
        {
            base.Awake();

            Initialize();
        }

        /// <summary>
        /// Initializes the pool.
        /// </summary>
        private void Initialize()
        {

            for (int i = 0; i < poolObjects.Count; i++)
            {
                //Create a new list of this specific object
                var _poolInstObj = new List<GameObject>();
                poolObjects[i].LoadAssetAsync<GameObject>() .Completed += handle =>
                {
                    var result = handle.Result.GetComponent<Poolable>();

                    //Instantiate all objects based on specified size
                    for (int s = 0; s < result.size; s++)
                    {
                        var poolObject = Instantiate(result.gameObject);
                        poolObject.gameObject.SetActive(false);
                        poolObject.transform.parent = transform;
                        _poolInstObj.Add(poolObject);
                    }

                    //Register into the dictionary this new type of object with his name as key
                    _listPools.Add(result.Key, _poolInstObj);
                };

            }
        }


        /// <summary>
        /// Get a referenced object from the pool, if there is no object will return a new one and grow the pool size
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="startActive">if set to <c>true</c> [start active].</param>
        /// <returns></returns>
        public GameObject TryPool(GameObject obj, bool startActive = true)
        {
            var tempObj = obj;

            //Check if exist a type of the object using the object name as a key
            if (_listPools.ContainsKey(tempObj.GetHashCode()))
            {
                for (int i = 0; i < _listPools[tempObj.GetHashCode()].Count; i++)
                {

                    //Get the disabled one and return
                    if (!_listPools[tempObj.GetHashCode()][i].activeSelf)
                    {
                        tempObj = _listPools[tempObj.GetHashCode()][i];

                        tempObj.SetActive(startActive);
                        tempObj.transform.parent = null;

                        return tempObj;
                    }
                }

                // if there is no more objects available create a new one and grow the pool
                tempObj = Instantiate(obj);

                tempObj.SetActive(startActive);

                tempObj.transform.parent = null;

                Grow(tempObj, obj.GetHashCode());

                return tempObj;
            }

            //If there is no object type existent on the pool create a new list of the new type of object
            var _poolInstObj = new List<GameObject>();

            tempObj = Instantiate(obj);

            _poolInstObj.Add(tempObj);

            _listPools.Add(obj.GetHashCode(), _poolInstObj);

            return tempObj;
        }

        /// <summary>
        /// Try to get a component in get pool object.
        /// </summary>
        /// <typeparam name="T">Type of get component</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="startActive">if set to <c>true</c> [start active].</param>
        /// <returns></returns>
        public T TryGetPool<T>(GameObject obj, bool startActive = true)
        {
            var temp = TryPool(obj, startActive);

            T instance = temp.GetComponent<T>();

            return instance;
        }

        /// <summary>
        /// Grows the pool based on specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void Grow(GameObject obj, int size = 1)
        {
            Grow(obj, obj.GetHashCode(), size);
        }


        /// <summary>
        /// Can Grows the pool based on specified size and specified key.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="key">The key.</param>
        /// <param name="size">The size.</param>
        public void Grow(GameObject obj, int key, int size = 1)
        {

            if (size <= 0)
            {
                Debug.LogWarning("Can't grow with size 0 or less, please set a size bigger than 0");
                return;
            }

            if (_listPools.ContainsKey(key))
            {
                for (int i = 0; i < size; i++)
                {
                    _listPools[key].Add(obj);
                }

                Debug.Log("I'm Growing " + obj.name);
            }
            else
            {
                Debug.Log("There is no kind of this" + obj.name + "object on pool, use TryPool Instead");

            }

        }

        /// <summary>
        /// Returns the specified object back into the pool.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.parent = transform;
            obj.transform.position = new Vector3(obj.transform.position.x + 100, 0);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            for (int i = 0; i < poolObjects.Count; i++)
            {
                poolObjects[i].ReleaseAsset();
            }
        }
    }

}


