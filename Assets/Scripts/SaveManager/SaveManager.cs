using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>{

    private SaveSetup _saveSetup;

    protected override void Awake() {

        _saveSetup = new SaveSetup(); // uma nova instância SaveSetup
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "guigamer";

        base.Awake();
    }

    #region SAVE

    [NaughtyAttributes.Button]
    private void Save (){
        
        /*
        SaveSetup setup = new SaveSetup(); // uma nova instância SaveSetup
        setup.lastLevel = 2;
        setup.playerName = "guigamer";
        */

        // transformando método SaveSetup e json
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);

        Debug.Log(setupToJson);

        SaveFile(setupToJson);
    }

    public void SaveName(string text){
        _saveSetup.playerName = text;
        Save();
    }

    public void SaveLastLevel(int level){
        _saveSetup.lastLevel = level;
        Save();
    }

    #endregion


    private void SaveFile(string json){

        // dataPath sava o arquivo dentro do projeto
        // persistentDataPath sava o arquivo na pasta do usuário do sistema operacional
        // streamingAssetsPath salva dentro da pasta StreamingAssets que vai estar na pasta do projeto no Assets
        string path = Application.streamingAssetsPath + "/save.txt";    

        // string fileLoaded = "";

        // if(File.Exists(path)){
        //     fileLoaded = File.ReadAllText(path);
        // }

        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne(){
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void SaveLevelFive(){
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup{

    public int lastLevel;
    public string playerName;
}