using UnityEngine;
using UnityEngine.UI;

// Author : Auguste Paccapelo

public class CustomSliderUI : MonoBehaviour
{
    // ---------- VARIABLES ---------- \\

    // ----- Prefabs & Assets ----- \\

    // ----- Objects ----- \\

    // ----- Others ----- \\

    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;
    [SerializeField] private float _value;
    public float Value
    {
        get => _value;
        set => UpdateValue(value);
    }

    [SerializeField] private RectMask2D _rectMask;

    // ---------- FUNCTIONS ---------- \\

    // ----- Buil-in ----- \\

    private void OnValidate()
    {
        UpdateValue(_value);
    }

    private void OnEnable() { }

    private void OnDisable() { }

    private void Awake() { }

    private void Start() { }

    private void Update() { }

    // ----- My Functions ----- \\

    private void UpdateValue(float value)
    {
        _value = Mathf.Clamp(value, _minValue, _maxValue);
        UpdateMask();
    }

    private void UpdateMask()
    {
        float width = _rectMask.rectTransform.rect.width;
        float weight = MyMath.InverseLerp(_value, _minValue, _maxValue);
        Vector4 padding = _rectMask.padding;
        padding.z = Mathf.Lerp(width, 0, weight);
        _rectMask.padding = padding;
    }

    // ----- Destructor ----- \\

    private void OnDestroy() { }
}