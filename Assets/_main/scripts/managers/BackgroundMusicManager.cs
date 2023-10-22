  using System;
  using System.Collections;
  using System.Collections.Generic;
  using StvDEV.StarterPack;
  using UnityEngine;
  using Random = UnityEngine.Random;

  namespace BrainyJunior.MyGame.Scripts.Managers
  {
      public class BackgroundMusicManager : MonoBehaviourSingleton<BackgroundMusicManager> {
          
          [SerializeField] private List<AudioObject> audioList;
          
          [SerializeField] private List<AudioObject> backgroundMusics;
          
          [SerializeField] private GameObject audioSourcePrefab;
          [SerializeField] private bool playBackgroundMusicOnStart;

          [HideInInspector] public AudioSource background;

          private List<AudioSource> _audioSources;
          
          [SerializeField] private float fadeOutTime;
          [SerializeField] private float fadeInTime;
          [SerializeField] private float musicChangeInterval;
          
          private float _remainingTime;


          protected override void Start()
          {
              
              _audioSources = new List<AudioSource>();
              if (playBackgroundMusicOnStart)
              {
                  PlayAudioById("background", true , true);
              }
          }

          private void Update()
          {
              // Wait for the specified interval before changing the music
              _remainingTime -= Time.deltaTime;
              if (_remainingTime >= musicChangeInterval)
              {
                  // ChangeMusic(background , );
              }
          }

          public int PlayAudioById(string id, bool loop = false , bool isBackground = false)
          {
              AudioObject audioObj;
              if (isBackground)
              {
                  audioObj = backgroundMusics.Find(obj => obj.id == id);
                  if (audioObj != null) SetBackgroundMusic(audioObj);
                  
              }
              else
              {
                  audioObj = audioList.Find(obj => obj.id == id);
                  
                  if (audioObj != null)
                  {
                      return PlayAudioObject(audioObj,loop);
                  }
              }
             
 
              Debug.LogWarning("name of the audio track incorrect");

              return 0;
          }
         
          

          public int PlayAudioObject(AudioObject audioObject, bool loop = false)
          {
              var audioSource = Instantiate(audioSourcePrefab,gameObject.transform);
              
              _audioSources.Add(audioSource.GetComponent<AudioSource>());
              _audioSources[^1].gameObject.name = audioObject.id;
              _audioSources[^1].loop = loop;
              _audioSources[^1].clip = audioObject.audio;
              _audioSources[^1].Play();
              
              return _audioSources.Count - 1;
          }

          public void SetBackgroundMusic(AudioObject audioObject)
          {
              if (background == null)
              {
                  var audioSource = Instantiate(audioSourcePrefab,gameObject.transform);
                  background = audioSource.GetComponent<AudioSource>();
              }
              
              
              background.gameObject.name = audioObject.id;
              background.clip = audioObject.audio;
              background.Play();
              // ChangeMusic(background, audioObject.audio);
          }

       
          
          
          IEnumerator ChangeMusic(AudioSource audioSource, AudioClip clip)
          {
              // Fade out the current music over the specified time
              float elapsedTime = 0.0f;
              float originalVolume = audioSource.volume;

              while (elapsedTime < fadeOutTime)
              {
                  elapsedTime += Time.deltaTime;
                  audioSource.volume = Mathf.Lerp(originalVolume, 0.0f, elapsedTime / fadeOutTime);
                  yield return null;
              }


              // Change the audio source's clip to the new music

              audioSource.clip = clip;
              audioSource.Play();

              // Fade in the new music over the specified time
              elapsedTime = 0.0f;
              while (elapsedTime < fadeInTime)
              {
                  elapsedTime += Time.deltaTime;
                  audioSource.volume = Mathf.Lerp(0.0f, originalVolume, elapsedTime / fadeInTime);
                  yield return null;
              }
          }
          
          
          
          public void DeleteAudioNum(int num)
          {
              Destroy(_audioSources[num].gameObject);
          }
          public void StopAudioNum(int num)
          {
              _audioSources[num].Stop();
          }
          
          public void PlayAudioNum(int num)
          {
              _audioSources[num].Play();
          }
          
          AudioClip GetRandomBackgroundMusicClip()
          {
              return backgroundMusics[Random.Range(0, backgroundMusics.Count)].audio;
          }
          

          [Serializable]
          public class AudioObject {
              public string id;
              public AudioClip audio;
              public bool isPlay;
          }
      }
  }

