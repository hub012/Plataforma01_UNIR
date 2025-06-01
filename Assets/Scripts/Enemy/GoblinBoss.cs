using UnityEngine;

namespace Enemy
{
    public class GoblinBoss : Enemy
    {
        [Header("Audio")]
        [SerializeField] private AudioClip damageSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private float soundVolume = 0.7f;
        
    }
}