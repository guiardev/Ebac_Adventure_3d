using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase{

    private GunBase _currentGun;
    //public InputAction shootAction;
    public GunBase gunBase;
    public FlashColor flashColor;

    public Transform gunPosition;



    protected override void Init(){

        base.Init();

        CreateGun();

        //shootAction.performed += ctx => Shoot();
        inputs.Gameplay.Shoot.performed += cts => StartShoot();  // quando essa botão clicado vai chamar método Shoot();
        inputs.Gameplay.Shoot.canceled += cts => CancelShoot();
    }

    private void CreateGun(){

        _currentGun = Instantiate(gunBase, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot(){
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot(){
        Debug.Log("Cancel Shoot");
        _currentGun.StopShoot();
    }
}