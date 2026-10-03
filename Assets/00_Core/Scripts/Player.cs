using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

// Author : Auguste Paccapelo

public class Player : MonoBehaviour, ICanOwnProjectile
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Inputs ----- \\
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _shoot;
    [SerializeField] private InputActionReference _cursorMove;

    // ----- Others ----- \\

    [SerializeField] private float _speed = 10.0f;
    private Vector2 _direction;

    // ----- Cursor ----- \\
    [SerializeField] private GameObject _cursorGO;
    // In Degree/Sec
    [SerializeField] private float _cursorRotationSpeed = 5.0f;
    [SerializeField] private float _cursorChargeSpeed = 5.0f;
    private Vector2 _cursorDirection;

    [SerializeField] private float _minCursorDistance = 2.0f;
    [SerializeField] private float _maxCursorDistance = 5.0f;
    private float _cursorDistance;

    [SerializeField] private float _minCharge = 0.5f;
    [SerializeField] private float _maxCharge = 2.0f;
    private float _currentCharge;

    // In degree
    [SerializeField] private float _startAngle = 0.0f;
    private float _currentAngle;

    // ----- Shooting ----- \\

    [SerializeField] private GameObject _arrowPrefab;

    private bool _isShooting;
    // In Attack per Seconds
    [SerializeField] private float _attackSpeed = 0.5f;
    private float _shootTimer = 0;

    private float _damages = 1.0f;

    private List<SOUpgrade> _upgrades;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable()
    {
        _move.action.started += OnMove;
        _move.action.performed += OnMove;
        _move.action.canceled += OnMove;
        
        _cursorMove.action.started += OnCursorMove;
        _cursorMove.action.performed += OnCursorMove;
        _cursorMove.action.canceled += OnCursorMove;

        _shoot.action.started += OnShootStart;
        _shoot.action.canceled += OnShootCanceled;
    }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _currentAngle = _startAngle;
        if (_attackSpeed <= 0) _attackSpeed = float.MinValue;
        _shootTimer = 1 / _attackSpeed;
        MoveCursor();
    }

    private void Update()
    {
        Move();
        MoveCursor();
        Shoot();
    }

    // ----- Interfaces ----- \\

    public float GetDamagesDone()
    {
        return _damages;
    }

    // ----- My Functions ----- \\

    // ----- Movements ----- \\

    private void Move()
    {
        Vector2 velocity = _direction * _speed;

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void MoveCursor()
    {
        _currentAngle += _cursorDirection.x * _cursorRotationSpeed * Time.deltaTime;
        _currentAngle %= 360;

        _currentCharge += _cursorDirection.y * _cursorChargeSpeed * Time.deltaTime;
        _currentCharge = Mathf.Clamp(_currentCharge, _minCharge, _maxCharge);
        
        float distanceLerpWeight = (_currentCharge - _minCharge) / (_maxCharge - _minCharge);
        _cursorDistance = Mathf.Lerp(_minCursorDistance, _maxCursorDistance, distanceLerpWeight);

        _cursorGO.transform.position = transform.position + (Vector3)PolarToCart(_currentAngle, _cursorDistance);
    }

    // ----- Inputs Callbacks ----- \\

    private void OnMove(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnCursorMove(InputAction.CallbackContext context)
    {
        _cursorDirection = context.ReadValue<Vector2>();
    }
    
    private void OnShootStart(InputAction.CallbackContext context)
    {
        _isShooting = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _isShooting = false;
    }
    
    // ----- Others ----- \\

    private void Shoot()
    {
        bool shoot = false;
        float timeBetweenAttacks = 1 / _attackSpeed;

        _shootTimer += Time.deltaTime;

        if (_shootTimer >= timeBetweenAttacks)
        {
            _shootTimer = timeBetweenAttacks;
            shoot = true;
        }

        if (!_isShooting)
        {
            return;
        }

        if (shoot)
        {
            _shootTimer = 0;
            GetShootPattern();
            SpawnArrows();
        }
    }

    private Vector2 PolarToCart(float angle, float distance)
    {
        angle *= Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);
    }

    private float GetVectorAngleDegree(Vector2 vect)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vect.y, vect.x);
    }

    private void GetShootPattern()
    {
        _upgrades = GameManager.Instance.GetUpgrades();
    }

    private void SpawnArrows()
    {
        foreach (SOUpgrade upgrade in _upgrades)
        {
            foreach (ShootPosInfo info in upgrade.ShootPattern)
            {
                GameObject arrow = Instantiate(_arrowPrefab);
                Arrows arrowCompo = arrow.GetComponent<Arrows>();

                Vector2 infoDir = info.direction.normalized;
                float infoAngle = GetVectorAngleDegree(infoDir);
                float angle = _currentAngle + infoAngle;

                Vector2 dir = PolarToCart(angle, 1);

                float offsetAngle = GetVectorAngleDegree(info.posOffSet);
                float offsetDist = info.posOffSet.magnitude;

                Vector2 offset = transform.position + (Vector3)PolarToCart(offsetAngle + angle, offsetDist);

                arrowCompo.Init(this, _currentCharge, dir, offset);
            }
        }
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}