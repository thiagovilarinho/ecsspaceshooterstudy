using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementJob))]
public class ShootBehavior : MonoBehaviour
{

    [SerializeField]
    private MovementJob _movementJob = default(MovementJob);
    [SerializeField]
    private GameObject _bullet = default(GameObject);

    private List<Transform> _transforms = new List<Transform>();
    private List<Transform> _activeTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
        if (_movementJob == null)
        {
            _movementJob = GetComponent<MovementJob>();
        }

        /* for (int i = 0; i < 10; i++)
        {
            Transform m_transform = Instantiate(_bullet, transform.position, Quaternion.identity).transform;
            _transforms.Add(m_transform);
        } */

        // _movementJob.InitializeTransforms(_transforms.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetBullets();
        }

        if (Input.GetMouseButton(0))
        {
            //_transforms = new List<Transform>();

            Transform m_transform = Instantiate(_bullet, transform.position, Quaternion.identity).transform;
            _transforms.Add(m_transform);

            _movementJob.InitializeTransforms(_transforms.ToArray());
        }
    }

    private void ResetBullets()
    {
        for (int i = 0; i < _transforms.Count; i++)
        {
            _transforms[i].position = transform.position;
        }
    }
}
