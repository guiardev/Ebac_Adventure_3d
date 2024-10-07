using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>{

    private List<AudioSource> _audioSourceList;
    private int _index = 0;

    public int poolSize = 10;

    private void Start() {
        CreatePool();
    }
    
    private void CreatePool(){

        _audioSourceList = new List<AudioSource>(); // create component 

        //creates several audios
        for (int i = 0; i < poolSize; i++){
            CreateAudioSourceItem();
        }

    }

    // criando vários AudioSource para quando tocar vários audio cada um vai ficar tocando no sua AudioSource e nao fica so um object audio
    private void CreateAudioSourceItem(){

        GameObject go = new GameObject("SFX_Pool");  // colocando dentro object
        go.transform.SetParent(gameObject.transform);

        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType){


        if(sfxType == SFXType.NAME) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType); // var create automatic to receive

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++; // update _index
        if (_index >= _audioSourceList.Count) _index = 0;

    }

}