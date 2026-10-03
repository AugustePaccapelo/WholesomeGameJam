using UnityEngine;
using System.Collections.Generic;

// Author : Auguste Paccapelo

[CreateAssetMenu(fileName = "SOUpgrade", menuName = "ScriptableObject/SOUpgrade", order = 0)]
public class SOUpgrade : ScriptableObject
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _addHP;
    public float HP => _addHP;

    [SerializeField] private float _addPercantageHP;
    public float PercantageHP => _addPercantageHP;

    [SerializeField] private float _addDamage;
    public float Damage => _addDamage;

    [SerializeField] private float _addPercantageDamage;
    public float PercantageDamage => _addPercantageDamage;

    [SerializeField] private float _addAttackSpeed;
    public float AttackSpeed => _addAttackSpeed;

    [SerializeField] private float _addPercantageAttackSpeed;
    public float PercantageAttackSpeed => _addPercantageAttackSpeed;

    [SerializeField] private List<ShootPosInfo> _shootPattern = new List<ShootPosInfo>();
    public List<ShootPosInfo> ShootPattern => new(_shootPattern);

    // ---------- FUNCTIONS ---------- \\
}