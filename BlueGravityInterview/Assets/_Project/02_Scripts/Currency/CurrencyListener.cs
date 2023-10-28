using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

namespace Currency
{
    public class CurrencyListener : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currencyText;
        private void Start()
        {
            CurrencyController.Instance.OnCurrencyChanged += UpdateCurrency;
            _currencyText.text = CurrencyController.Instance.GetCurrencyItem().Amount.ToString();
        }

        private void UpdateCurrency(int amount, bool isAdding,int finalAmount)
        {
            _currencyText.text = finalAmount.ToString();
        }
    }
}