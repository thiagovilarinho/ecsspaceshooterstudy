using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class ScreenClamperComponent : MonoBehaviour
    {
        private Transform _transform = default(Transform);

        [SerializeField]
        private Vector2 _maxValue = new Vector2(1, 1);

        [SerializeField]
        private Vector2 _minValue = new Vector2(1, 1);

        private Vector4 _clampValue = new Vector4(0, 0, 0, 0);

        private Vector3 _clampedVector = new Vector3(0, 0, 0);


        // Start is called before the first frame update
        void Start()
        {
            _transform = transform;
            _clampedVector = _transform.position;

            var vertExtent = Camera.main.orthographicSize;
            var horzExtent = vertExtent * Screen.width / Screen.height;


            _clampValue = new Vector4(-horzExtent * _minValue.x,
                                     horzExtent * _maxValue.x,
                                    -vertExtent * _minValue.y,
                                     vertExtent * _minValue.x);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            _clampedVector = transform.position;

            _clampedVector.x = Mathf.Clamp(_clampedVector.x, _clampValue.x, _clampValue.y);
            _clampedVector.y = Mathf.Clamp(_clampedVector.y, _clampValue.z, _clampValue.w);

            transform.position = _clampedVector;

        }
    }
}
