using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour{

    private MusicSetup _currentMusicSetup;

    public MusicType musicType;
    public AudioSource audioSource;

    private void Start() {
        Play();
    }

    private void Play(){

        //_currentMusicSetup = SoundManager.Instance.GetSFXByType(musicType);

       // audioSource.clip = _currentMusicSetup.audioClip;
       //audioSource.Play();
    }
   
}