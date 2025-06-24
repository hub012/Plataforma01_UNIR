using System;
using Music;
using UnityEngine;

namespace Scene
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;
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

        private void Update()
        {
            if (Instance == null)
            {
                Instance = FindAnyObjectByType<SceneLoader>();
            }
        }

        public void ChangeScene(string sceneName)
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            CheckSceneMusic(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void CheckSceneMusic(string sceneName)
        {
            switch (sceneName)
            {
                case "Main Menu":
                    Debug.Log("Main Scene");
                    MusicManager.Instance.PlayMainMenu();
                    break;
                case "Level1":
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
//
