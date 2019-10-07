using UnityEngine;

namespace SimpleSpace
{
    public struct ScreenBoundary
    {
        private float _screenBound;
        public float GetBoundaries()
        {
            return _screenBound;
        }

        public bool TouchBound(float target)
        {
            return Mathf.Abs(target) > _screenBound * 1.25f ;
        }
        public void Initialize()
        {
            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;

            _screenBound = horzExtent;
        }
    }
}
