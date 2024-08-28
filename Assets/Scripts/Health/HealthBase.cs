using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;



public class HealthBase : MonoBehaviour, IDamageable{

    [SerializeField] private float _currentLife;
    public Action<HealthBase> OnDamage, OnKill;
    public List<UIFillUpdater> uiGunUpdater;
    public float startLife = 10f, damageMultiply = 1;
    public bool destroyOnKill = false;

    private void Awake(){
        Init();
    }

    public void Init(){
        ResetLife();
    }

    public void ResetLife(){
        _currentLife = startLife;
        UpdateUI(); 
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

        _currentLife -= f * damageMultiply; // damageMultiply tendo noção exata que esta mudando.

        if (_currentLife <= 0){
            Kill();
        }

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir){
        Damage(damage);
    }

    private void UpdateUI(){

        if(uiGunUpdater != null){
            uiGunUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration){
        StartCoroutine(ChangeDamageMultiplyCoroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration){
 
        this.damageMultiply = damageMultiply; // this esta mostrando variável da damageMultiply class HealthBase
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }

}