using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGunUpdater : MonoBehaviour{

    private Tween _currTween;
    public Image uiImage;

    [Header("Animation")]
    public Ease ease = Ease.OutBack;
    public float duration = .1f;

    private void OnValidate(){

        if (uiImage == null){
            uiImage = GetComponent<Image>(); 
        }
    }

    public void UpdateValue(float f){
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current){

        if(_currTween != null) _currTween.Kill();
        //10/100 = .1
        //uiImage.fillAmount = 1 - (current / max);
        _currTween = uiImage.DOFillAmount(1 - (current / max), duration).SetEase(ease);

    }

}