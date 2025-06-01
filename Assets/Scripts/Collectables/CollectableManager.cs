using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Collectables
{
    public class CollectableManager : MonoBehaviour
    {
        public static CollectableManager Instance;
    
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI coinText; 
    
        [Header("Coin Data")]
        [SerializeField] private int currentCoins = 0;
        
        // Collectable tracking
        private bool hasCollectedCoin = false;
        private bool hasCollectedGem = false;
    
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        void Start()
        {
            UpdateCoinUI();
        }
    
        public void AddCoins(int amount)
        {
            currentCoins += amount;
            UpdateCoinUI();
        }
        
        void UpdateCoinUI()
        {
            if (coinText != null)
            {
                coinText.text = currentCoins.ToString();
            }
        }

        public int GetCollectedCoins()
        {
            return currentCoins;
        }
        
        public void CollectCoin()
        {
            hasCollectedCoin = true;
        }
        public void CollectGem()
        {
            hasCollectedGem = true;
        }
        
        public bool HasCollectedCoin()
        {
            return hasCollectedCoin;
        }
        public bool HasCollectedGem()
        {
            return hasCollectedGem;
        }
    }
}