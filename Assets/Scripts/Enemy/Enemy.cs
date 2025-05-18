using System;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public Animator enemyAnimator;
        private StateMachine _stateMachine;
        private void Update()
        {
            Debug.Log("Im the enemy ");
        }
    }
}