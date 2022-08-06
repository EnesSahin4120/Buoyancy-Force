using UnityEngine;

public class SwimmingObject : MonoBehaviour
{
    [SerializeField] private WaterSettings _waterSettings;
    [SerializeField] private float _fallingSpeed;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;

    private float _cubeBottomY
    {
        get
        {
            return _meshRenderer.bounds.min.y;
        }
    }
    private float _cubeTopY
    {
        get
        {
            return _meshRenderer.bounds.max.y;
        }
    }
    private float _cubeSurfaceArea
    {
        get
        {
            return transform.localScale.x * transform.localScale.z;
        }
    }
    private float _hb
    {
        get
        {
            return Mathf.Abs(_cubeBottomY - _waterSettings.WaterPos_Y);
        }
    }
    private float _ht
    {
        get
        {
            return Mathf.Abs(_cubeTopY - _waterSettings.WaterPos_Y);
        }
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector3(0, -_fallingSpeed, 0);
    }

    private void FixedUpdate() 
    {
        if (_cubeBottomY < _waterSettings.WaterPos_Y)
            _rb.AddForce(Vector3.up * UpwardForceMagnitude(),ForceMode.Force);

        if (_cubeTopY < _waterSettings.WaterPos_Y)
            _rb.AddForce(-Vector3.up * DownwardForceMagnitude(),ForceMode.Force);
    }

    private float UpwardForceMagnitude() 
    {
        return Mathf.Abs(_waterSettings.BuoyancyFactor * _hb * _cubeSurfaceArea);
    }

    private float DownwardForceMagnitude()
    {
        return Mathf.Abs(_waterSettings.BuoyancyFactor * _ht * _cubeSurfaceArea);
    }
}
