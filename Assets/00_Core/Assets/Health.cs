using System;
using UnityEngine;

// Author : Auguste Paccapelo

public class Health : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _maxHealth;
    private float _health;

    [SerializeField] private CustomSliderUI _healthSlider;

    public event Action<float> OnDamageTaken;
    public event Action OnDeath;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start()
    {
        _health = _maxHealth;
        UpdateSlider();
    }

    private void Update() { }

    // ----- My Functions ----- \\

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        
        UpdateSlider();
        OnDamageTaken?.Invoke(damage);

        if (_health == 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void UpdateSlider()
    {
        _healthSlider.Value = _health / _maxHealth;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}