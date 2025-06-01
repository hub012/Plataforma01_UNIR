using UnityEngine;

public class GameOver:MonoBehaviour
{
       public void BackToMainMenu()
       {
              Scene.SceneLoader.Instance.ChangeScene("Main Menu");
       } 
}