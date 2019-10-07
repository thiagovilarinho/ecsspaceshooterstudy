using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.NonECS
{
    public class MovementComponent : MonoBehaviour
    {

        [SerializeField]
        private float _speed = 1;

        [SerializeField]
        private Vector2 _nonInputMovDir = new Vector2(1, 1);

        [SerializeField]
        private bool isPlayer = false;


        private Rigidbody rigidyBody = default(Rigidbody);


        // Start is called before the first frame update
        void Start()
        {
            rigidyBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isPlayer)
            {
                Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            }
            else
            {
                Move(_nonInputMovDir);
            }
        }

        private void Move(Vector2 dir)
        {
            rigidyBody.velocity = (dir / Time.deltaTime) * _speed;
        }
    }
}
