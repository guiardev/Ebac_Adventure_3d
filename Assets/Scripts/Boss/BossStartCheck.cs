using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour{

    public GameObject bossCamera;
    public Color gizmoColor = Color.yellow;
    public string tagToCheck = "Player";

    private void Awake() {
        bossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider col) {
        
        if(col.transform.tag == tagToCheck){
            TurnCameraOn();
        }
    }

    private void TurnCameraOn(){
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos() {

        Gizmos.color = gizmoColor;                  
        Gizmos.DrawSphere(transform.position, transform.localScale.y); // transform.localScale.y para Gizmos pegar tamanho collider     
    }

}