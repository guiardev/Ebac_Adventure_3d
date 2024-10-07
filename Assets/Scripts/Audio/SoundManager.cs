using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SoundManager : Singleton<SoundManager>{

    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;

    public void PlayMusicByType(MusicType musicType){

        // buscando music
        var music = GetMusicByType(musicType);

        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType){

        // buscando music
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType sfxType){

        // buscando audio
        return sfxSetups.Find(i => i.sfxStype == sfxType);
    }

}

public enum MusicType{
    TYPE_01, TYPE_02, TYPE_03
}

[System.Serializable]
public class MusicSetup{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType{
    NAME, TYPE_01, TYPE_02, TYPE_03
}

[System.Serializable]
public class SFXSetup{
    public SFXType sfxStype;
    public AudioClip audioClip;
}