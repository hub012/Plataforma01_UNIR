using UnityEngine;

public class GameOver:MonoBehaviour
{
       public void BackToMainMenu()
       {
              Scene.SceneManager.Instance.ChangeScene("Main Menu");
       } 
}