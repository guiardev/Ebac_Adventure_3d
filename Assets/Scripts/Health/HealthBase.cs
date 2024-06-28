using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour{

    [SerializeField] private float _currentLife;
    public Action<HealthBase> OnDamage, OnKill;
    public float startLife = 10f;
    public bool destroyOnKill = false;

    private void Awake(){
        Init();
    }

    public void Init(){
        ResetLife();
    }

    protected void ResetLife(){
        _currentLife = startLife;
    }

    protected virtual void Kill(){

        if(destroyOnKill){
            Destroy(gameObject, 2f);
        }

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage(){
        Damage(5);
    }

    public void Damage(float f){

        //transform.position -= transform.forward; // fazendo personagem que levou tiro afastar com impacto do tiro

        _currentLife -= f;

        if (_currentLife <= 0){
            Kill();
        }

        OnDamage?.Invoke(this);
    }
}