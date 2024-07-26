using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour{

    private string checkpointKey = "CheckpointKey";
    private bool checkpointActive = false;
    public MeshRenderer meshRenderer;
    public int key = 01;

    private void OnTriggerEnter(Collider col){

        if(!checkpointActive && col.transform.tag == "Player"){
            Checkpoint();
        }
    }

    private void Checkpoint(){
        TurnItOn();
        SaveCheckpoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn(){
        meshRenderer.material.SetColor("_EmissionColor", Color.white);  // _EmissionColor e um variável do Material em Select shader
    }

    [NaughtyAttributes.Button]
    private void TurnItOff(){
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);  // _EmissionColor e um variável do Material em Select shader
    }

    private void SaveCheckpoint(){

        // if(PlayerPrefs.GetInt(checkpointKey, 0) > key){
        //     PlayerPrefs.SetInt(checkpointKey, key);
        // }

        CheckpointManager.Instance.SaveCheckPoint(key);

        checkpointActive = true;
    }
}