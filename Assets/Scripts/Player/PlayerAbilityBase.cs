using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityBase : MonoBehaviour{

    protected Player player;

    protected Inputs inputs;

    private void OnValidate() {
        
        if(player == null){
            player = GetComponent<Player>(); // chegando player esta cena
        }
    }

    private void Start() {


        //class Inputs foi criada pelo package que foi instalado input system e na aba inputs a action criei action shoot no binding X = keyboard
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnEnable() {

        if(inputs != null){
            inputs.Enable();
        }
    }

    private void OnDisable() {
        inputs.Disable();
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    protected virtual void Init(){

    }

    protected virtual void RegisterListeners(){

    }

    protected virtual void RemoveListeners(){

    }
}