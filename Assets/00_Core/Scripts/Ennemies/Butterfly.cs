using UnityEngine;

// Author : Auguste Paccapelo

public class Butterfly : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    private float _speed;

    [SerializeField] private float _minRotationSpeed;
    [SerializeField] private float _maxRotationSpeed;
    private float _rotationSpeed;

    private float _currentAngle;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
        _currentAngle = Random.Range(0, 360);
    }

    private void Update()
    {
        Move();
    }

    // ----- My Functions ----- \\

    private void Move()
    {
        _currentAngle += _rotationSpeed * Time.deltaTime;
        Vector2 vel = MyMath.PolarToCart(_currentAngle, _speed);
        transform.localPosition = (Vector3)vel;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}