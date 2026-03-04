using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : SingletonBase<CoinCollector>
    {
        [SerializeField] private int coinsPerSpeedBoost = 10;
        [SerializeField] private int maxCoinAmountForBoost = 200;

        public event Action OnSpeedIncreased;
        public event Action OnNewCoinCollected;
        
        public int CoinAmount { get; private set; } = 0;
        
        public void AddCoin()
        {
            CoinAmount++;
            OnNewCoinCollected?.Invoke();
            if (CoinAmount % coinsPerSpeedBoost == 0 && CoinAmount < maxCoinAmountForBoost )
                OnSpeedIncreased?.Invoke();
        }
        
        protected override void Awake()
        {
            base.Awake();
            Debug.Log("CoinCollector loader");
        }
    }
}