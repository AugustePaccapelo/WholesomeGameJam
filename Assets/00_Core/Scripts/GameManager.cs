using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

// Author : Auguste Paccapelo

public class GameManager : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Singleton ----- \\

    public static GameManager Instance {get; private set;}

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private SOUpgrade _baseUpgrade;
    private List<SOUpgrade> _upgrades = new List<SOUpgrade>();

    [Serializable]
    private class ShootUpgradesPatternsKeyVal
    {
        public UpgradesShootPatterns key;
        public SOUpgrade value;
    }
    [SerializeField] private List<ShootUpgradesPatternsKeyVal> _shootUpgradesPatterns = new();

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake()
    {
        // Singleton
        if (Instance != null)
        {
            Debug.Log(nameof(GameManager) + " Instance already exist, destorying last added.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _upgrades.Add(_baseUpgrade);

        _upgrades.AddRange(_shootUpgradesPatterns.Select(a => a.value));
    }

    void Update() { }

    // ----- My Functions ----- \\

    public List<SOUpgrade> GetUpgrades()
    {
        return new(_upgrades);
    }

    // ----- Destructor ----- \\

    protected virtual void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}