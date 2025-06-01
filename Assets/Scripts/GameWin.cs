using System;
using TMPro;
using UnityEngine;

public class GameWin:MonoBehaviour
{
       [Header("UI")]
       [SerializeField] private TextMeshProUGUI coinText;

       private int currentCoins;
       
       public void BackToMainMenu()
       {
              Scene.SceneLoader.Instance.ChangeScene("Main Menu");
       }

       private void Update()
       {
              UpdateCoinUI();
              UpdateFinalCoinUI();
       }

       public void UpdateCoinUI()
       {
              currentCoins = Collectables.CollectableManager.Instance.GetCollectedCoins();
       }
       
       void UpdateFinalCoinUI()
       {
              Debug.Log("monedas: "+currentCoins);
              if (coinText != null)
              {
                     coinText.text = currentCoins.ToString();
              }
       }
}