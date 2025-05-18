using System;
using UnityEngine;

namespace Enemy
{
    public class Goblin: Enemy
    {
        protected override void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            Debug.Log("Im a Goblin");
        }
    }
}