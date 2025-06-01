using UnityEngine;

namespace Map
{
    public class DeathZone :  MonoBehaviour

    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            if (!other.CompareTag("Player")) return;
            if (Scene.SceneManager.Instance != null)
            {
                Scene.SceneManager.Instance.ChangeScene("GameOver");
            }
        }

   
    }
}