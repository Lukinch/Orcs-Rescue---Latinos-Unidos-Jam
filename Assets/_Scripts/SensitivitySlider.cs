using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private Slider _sliderToUpdate;
    [SerializeField] private TextMeshProUGUI _textToUpdate;
    [SerializeField] private SensitivityAxis _sensitivityType;

    private float _maxSliderValue = 1;

    enum SensitivityAxis
    {
        Horizontal,
        Vertical
    }

    void Start()
    {
        _sliderToUpdate.value = GetValue();
        Debug.Log($"Final {_sensitivityType} slider value: {_sliderToUpdate.value}");

        UpdateText();
    }

    private void UpdateText()
    {
        float percentage = (_sliderToUpdate.value / _maxSliderValue) * 100;
        string toText = percentage.ToString("0");
        _textToUpdate.text = $"{toText}%";
    }

    float GetValue()
    {
        if (_sensitivityType == SensitivityAxis.Horizontal)
        {
            Debug.Log($"{PlayerManager.Instance.HorizontalSensitivity}");
            return PlayerManager.Instance.HorizontalSensitivity;
        }
        else
        {
            Debug.Log($"{PlayerManager.Instance.VerticalSensitivity}");
            return PlayerManager.Instance.VerticalSensitivity;
        }
    }

    public void SetSensitivity(float value)
    {
        if (_sensitivityType == SensitivityAxis.Horizontal)
        {
            PlayerManager.Instance.HorizontalSensitivity = value;
        }
        else
        {
            PlayerManager.Instance.VerticalSensitivity = value;
        }

        UpdateText();
    }
}
