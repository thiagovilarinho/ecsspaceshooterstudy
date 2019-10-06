using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class MovementJob : MonoBehaviour
{
    //[SerializeField]
    // private float _speed = 10;

    [SerializeField]
    private float2 _movementAxis = new float2(0, 0);

    private TransformAccessArray _transformAccessArray;
    private NativeArray<float2> _mov;

    private JobHandle _jobHandle;

    public Transform[] Transforms { get; set; }

    // private void OnEnable()
    // {

    // }

    public void InitializeTransforms(Transform[] transforms)
    {
        DisposeNativeArray();

        _transformAccessArray = new TransformAccessArray(transforms.Length);
        _transformAccessArray.SetTransforms(transforms);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!_transformAccessArray.isCreated)
        {
            return;
        }

        _mov = new NativeArray<float2>(_transformAccessArray.length, Allocator.TempJob);

        for (int i = 0; i < _mov.Length; i++)
        {
            _mov[i] = _movementAxis;
        }

        MovementParallelJobTransforms movementParallelJobTransforms = new MovementParallelJobTransforms
        {
            deltaTime = Time.deltaTime,
            //speed = _speed,
            mov = _mov //new float2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
        };

        _jobHandle = movementParallelJobTransforms.Schedule(_transformAccessArray);

        _jobHandle.Complete();
        _mov.Dispose();

    }

    private void OnDisable()
    {
        DisposeNativeArray();
    }

    private void DisposeNativeArray()
    {
        if (_transformAccessArray.isCreated)
        {
            _transformAccessArray.Dispose();
        }
    }

    [BurstCompile]
    public struct MovementParallelJobTransforms : IJobParallelForTransform
    {
        [ReadOnly] public float deltaTime;
        //public float speed;

        public NativeArray<float2> mov;

        public void Execute(int index, TransformAccess transform)
        {
            mov[index] *= deltaTime;
            transform.position += new Vector3(mov[index].x, mov[index].y, 0);
        }
    }

}
