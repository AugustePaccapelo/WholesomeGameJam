using UnityEngine;

// Author : Auguste Paccapelo

public class ButterflySwarm : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _speed;
    private Vector2 _direction;

    [SerializeField] private float _maxAngleDiff = 45f;

    private float _lifeTime;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _direction = Vector2.right;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        Move();
    }

    // ----- My Functions ----- \\

    private void Move()
    {
        float angleDiff = Mathf.Sin(_lifeTime) * _maxAngleDiff;
        float baseAngle = MyMath.GetVectorAngleDegree(_direction);
        float angle = baseAngle + angleDiff;

        Vector2 dir = MyMath.PolarToCart(angle, 1);
        Vector2 vel = dir * _speed;
        transform.position += (Vector3)vel * Time.deltaTime;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}