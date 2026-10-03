using UnityEngine;

// Author : Auguste Paccapelo

public class Arrows : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _minSpeed = 0;
    [SerializeField] private float _maxSpeed = 20.0f;

    private Vector2 _velocity;
    private ICanOwnProjectile _owner;

    [SerializeField] private float _gravity = 8.0f;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update()
    {
        Move();
    }

    // ----- My Functions ----- \\

    public void Init(ICanOwnProjectile owner, float shootForce, Vector2 direction, Vector2 position)
    {
        _owner = owner;
        transform.position = position;

        float speed = Mathf.Lerp(_minSpeed, _maxSpeed, shootForce);

        _velocity = direction * speed;
    }

    private void Move()
    {
        _velocity += Vector2.down * _gravity * Time.deltaTime;

        transform.position += (Vector3)_velocity * Time.deltaTime;

        Vector3 angles = transform.eulerAngles;
        Vector2 dir = _velocity.normalized;
        angles.z = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        transform.eulerAngles = angles;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}