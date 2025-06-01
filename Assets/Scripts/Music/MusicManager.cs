using System.Collections;
using UnityEngine;

namespace Music
{
    public class MusicManager : MonoBehaviour
    {
        // Make it a singleton so we can access from anywhere
        public static MusicManager Instance;
    
        [Header("Music Tracks")]
        [SerializeField] private AudioClip mainMenuMusic;
        [SerializeField] private AudioClip level1Music;
        [SerializeField] private AudioClip gameOverMusic;
        [SerializeField] private AudioClip successMusic;
        [SerializeField] private AudioClip bossMusic;
    
        [Header("Settings")]
        [SerializeField] private float fadeSpeed = 1f;
        [SerializeField] private float musicVolume = 0.7f;
    
        private AudioSource audioSource;
        private Coroutine fadeCoroutine;
    
        void Awake()
        {
            
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
            
                // Set up audio source
                audioSource.loop = true;
                audioSource.volume = musicVolume;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        void Start()
        {
            PlayMainMenu();
        }
    
        public void PlayMainMenu()
        {
            PlayMusic(mainMenuMusic);
            Debug.Log("Playing Main Menu Music");
        }
    
        public void PlayLevel1()
        {
            PlayMusic(level1Music);
            Debug.Log("Playing Level 1 Music");
        }
    
        public void PlayGameOver()
        {
            PlayMusic(gameOverMusic);
            Debug.Log("Playing Game Over Music");
        }
    
        public void PlaySuccess()
        {
            PlayMusic(successMusic);
            Debug.Log("Playing Success Music");
        }
    
        public void PlayBoss()
        {
            PlayMusic(bossMusic);
            Debug.Log("Playing Boss Music");
        }
    
        public void StopMusic()
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeOut());
        }
    
        public void SetVolume(float newVolume)
        {
            musicVolume = Mathf.Clamp01(newVolume);
            audioSource.volume = musicVolume;
        }
    
        // PRIVATE METHODS
    
        private void PlayMusic(AudioClip newClip)
        {
            if (newClip == null)
            {
                Debug.LogWarning("Music clip is missing!");
                return;
            }
        
            // If same music is already playing, don't restart it
            if (audioSource.clip == newClip && audioSource.isPlaying)
            {
                return;
            }
        
            // Stop any fade that's happening
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
        
            // Start the fade transition
            fadeCoroutine = StartCoroutine(FadeToNewMusic(newClip));
        }
    
        private IEnumerator FadeToNewMusic(AudioClip newClip)
        {
            // Fade out current music
            if (audioSource.isPlaying)
            {
                while (audioSource.volume > 0)
                {
                    audioSource.volume -= fadeSpeed * Time.deltaTime;
                    yield return null;
                }
            }
        
            // Switch to new music
            audioSource.Stop();
            audioSource.clip = newClip;
            audioSource.Play();
        
            // Fade in new music
            while (audioSource.volume < musicVolume)
            {
                audioSource.volume += fadeSpeed * Time.deltaTime;
                yield return null;
            }
        
            audioSource.volume = musicVolume;
        }
    
        private IEnumerator FadeOut()
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= fadeSpeed * Time.deltaTime;
                yield return null;
            }
        
            audioSource.Stop();
        }
    }
}