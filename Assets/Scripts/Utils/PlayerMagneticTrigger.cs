using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class PlayerMagneticTrigger : MonoBehaviour{

    private void OnTriggerEnter(Collider col) {

        ItemCollectableBase i = col.transform.GetComponent<ItemCollectableBase>(); // fazendo item seguir player

        if(i != null){
            i.gameObject.AddComponent<Magnetic>(); // player estive perto vai ativar magnetic
        }
    }
}