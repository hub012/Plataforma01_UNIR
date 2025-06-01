using System;
using Music;
using UnityEngine;

namespace Scene
{
    public class SceneManager : MonoBehaviour
    {
        public static SceneManager Instance;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ChangeScene(string sceneName)
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            CheckSceneMusic(sceneName);
        }

        public void CheckSceneMusic(string sceneName)
        {
            switch (sceneName)
            {
                case "Main Menu":
                    Debug.Log("Main Scene");
                    MusicManager.Instance.PlayMainMenu();
                    break;
                case "SampleScene":
                    Debug.Log("Level 1");
                    MusicManager.Instance.PlayLevel1();
                    break;
                case "GameOver":
                    MusicManager.Instance.PlayGameOver();
                    Debug.Log("GameOver");
                    break;
                case "WinGame":
                    Debug.Log("WinGame");
                    break;
                default:
                    break;
            }
        }
    }
}