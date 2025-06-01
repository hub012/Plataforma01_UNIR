using System;
using Enemy;
using Enemy.UI;
using Player.UI;
using UnityEngine;

public class WinGame:MonoBehaviour
{
    private float life;
    
    private void Awake()
    {
        life = GetComponent<Goblin>().Life;
    }

    private void Update()
    {
        if (life <= 0)
        {
            YouWon();
        }
    }

    public void YouWon()
    {
        Scene.SceneManager.Instance.ChangeScene("WinScene");
    }
}