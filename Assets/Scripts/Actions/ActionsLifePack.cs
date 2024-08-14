using System.Collections;
using System.Collections.Generic;
using Itens;
using UnityEngine;

public class ActionsLifePack : MonoBehaviour{

    public KeyCode keyCode = KeyCode.L;
    public SOInt soInt;

    // Start is called before the first frame update
    void Start(){
        soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
    }

    private void RecoverLife(){

        if(soInt.value > 0){
            ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            Player.Instance.healthBase.ResetLife();
        }
    }


    private void Update() {

        if(Input.GetKeyDown(keyCode)){
            RecoverLife();
        }   
    }
}