using System;
using Enemy;
using Enemy.UI;
using Player.UI;
using UnityEngine;

public class WinGame:MonoBehaviour
{
    private float life;
    [SerializeField] private Goblin goblin;

    private void OnEnable()
    {
        goblin.OnDeath += YouWon;
    }

    private void OnDisable()
    {
        goblin.OnDeath -= YouWon;
    }
    


    public static void YouWon()
    {
        Scene.SceneLoader.Instance.ChangeScene("WinScene");
    }
}