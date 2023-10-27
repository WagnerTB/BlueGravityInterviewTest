using System;
using Items;
using UnityEngine;

namespace Currency
{
    public class CurrencyController : MonoBehaviour
    {
        public static CurrencyController Instance => _instance;
        private static CurrencyController _instance;
        
        public delegate void CurrencyEvents(int amount, bool isAdding);
        
        public CurrencyEvents OnCurrencyChanged;

        [SerializeField]
        private Item _currencyItem;


        private void Awake()
        {
            _currencyItem = Instantiate(_currencyItem);
        }

        public void AddCurrency(int amount)
        {
            _currencyItem.Amount += amount;
            OnCurrencyChanged?.Invoke(amount, true);
        }

        public void RemoveCurrency(int amount)
        {
            if (_currencyItem.Amount - amount >= 0)
            {
                _currencyItem.Amount -= amount;
                OnCurrencyChanged?.Invoke(amount, false);
            }
        }

        public Item GetCurrencyItem()
        {
            return _currencyItem;
        }
    }
}