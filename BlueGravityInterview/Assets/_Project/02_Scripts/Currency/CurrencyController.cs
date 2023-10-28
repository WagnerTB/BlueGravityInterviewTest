using System;
using Items;
using UnityEngine;

namespace Currency
{
    public class CurrencyController : MonoBehaviour
    {
        public static CurrencyController Instance => _instance;
        private static CurrencyController _instance;

        public delegate void CurrencyEvents(int amount, bool isAdding, int finalAmount);

        public CurrencyEvents OnCurrencyChanged;

        [SerializeField]
        private Item _currencyItem;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _currencyItem = Instantiate(_currencyItem);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddCurrency(int amount)
        {
            _currencyItem.Amount += amount;
            OnCurrencyChanged?.Invoke(amount, true, _currencyItem.Amount);
        }

        public void RemoveCurrency(int amount)
        {
            if (_currencyItem.Amount - amount >= 0)
            {
                _currencyItem.Amount -= amount;
                OnCurrencyChanged?.Invoke(amount, false, _currencyItem.Amount);
            }
        }

        public Item GetCurrencyItem()
        {
            return _currencyItem;
        }
    }
}