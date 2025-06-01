using TMPro;
using UnityEngine;

public class GameWin:MonoBehaviour
{
       [Header("UI")]
       [SerializeField] private TextMeshProUGUI coinText;

       private int currentCoins;
       
       public void BackToMainMenu()
       {
              Scene.SceneManager.Instance.ChangeScene("Main Menu");
       }

       public void UpdateCoinUI()
       {
              currentCoins = Collectables.CollectableManager.Instance.GetCollectedCoins();
       }
       
       void UpdateFinalCoinUI()
       {
              if (coinText != null)
              {
                     coinText.text = currentCoins.ToString();
              }
       }
}