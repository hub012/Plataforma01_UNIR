using UnityEngine;

namespace Map
{
    public class DeathZone :  MonoBehaviour

    {
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            if (!other.CompareTag("Player")) return;
            if (Scene.SceneLoader.Instance != null)
            {
                Scene.SceneLoader.Instance.ChangeScene("GameOver");
            }
        }

   
    }
}