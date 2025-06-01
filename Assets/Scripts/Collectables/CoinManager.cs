using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Collectables
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager Instance;
    
        [Header("UI")]
        [SerializeField] private TextMeshProUGUI coinText; 
    
        [Header("Coin Data")]
        [SerializeField] private int currentCoins = 0;
    
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
    }
}