using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    public static StaminaBar instance;

    private float _maxLength;

    [SerializeField] private GameObject fullBar;
    [SerializeField] private GameObject amountBar;
    private RectTransform _amountBarTransform;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _maxLength = fullBar.GetComponent<RectTransform>().rect.width;
        _amountBarTransform = amountBar.GetComponent<RectTransform>();
    }

    public void ChangeAmount(float amount, float maxValue) {
        float percent = maxValue - amount;
        float lenghtAmountBar = _maxLength * percent / 100;
        _amountBarTransform.sizeDelta = new Vector2(lenghtAmountBar, 25f);
    }

}
