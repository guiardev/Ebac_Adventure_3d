using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase{

    private float _currentShoots;
    private bool _recharging = false;

    public List<UIFillUpdater> uIGunUpdaters;

    public float maxShoot = 5f, timeToRecharge = 1f;

    private void Awake() {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine(){

        /*
        //esperar um tempo em cada tiro
        while (true){
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
        */

        if (_recharging) yield break; //  yield break returna um quebra

        while(true){

            //verificando os tiros se for menor que o maxShoot
            if (_currentShoots < maxShoot){
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge(){

        if (_currentShoots >= maxShoot){
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge(){
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }


    IEnumerator RechargeCoroutine(){

        float time = 0;

        //recarregando balas por um tempo
        while(time <= timeToRecharge){

            time += Time.deltaTime;
            Debug.Log("Rechargin: " + time);
            uIGunUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }

        _currentShoots = 0;
        _recharging = false;
    }

    private void UpdateUI(){
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs(){
        uIGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdater  >().ToList();
    }

}