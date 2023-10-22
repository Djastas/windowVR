using System.Collections;
using System.Collections.Generic;
using BrainyJunior.MyGame.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _main.scripts
{
    public class RobotAudioController : MonoBehaviour
    {
        [SerializeField] private List<BackgroundMusicManager.AudioObject> audioClips;
        [SerializeField] private List<BackgroundMusicManager.AudioObject> randomAudioClips;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float maxTime;
        [SerializeField] private float minTime;

        private int _currentIndex;

        private void Start()
        {
            StartCoroutine(CallMethod());
            Play("hello");
        }

        public void Play(string id)
        {
            var tmp = audioClips.Find(obj => obj.id == id) ?? randomAudioClips.Find(obj => obj.id == id);
            if (tmp.isPlay) return;
            tmp.isPlay = true;
            audioSource.clip = tmp.audio;
            audioSource.Play();
        }
        
        IEnumerator CallMethod()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minTime, maxTime)); // Wait for a random time between 0 and the maximum time
                while (audioSource.isPlaying)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                if (randomAudioClips.Count <= _currentIndex) { yield break; }
                var tmp = randomAudioClips[_currentIndex];
                _currentIndex++;

              
                Play(tmp.id); 
            }
        }
    }
}
