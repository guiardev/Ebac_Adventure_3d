using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour{

    public MeshRenderer meshRenderer;

    [Header("Setup")]
    private Tween _currentTween;
    private Color _defaultColor;
    public Color color = Color.red;
    public float duration = .1f;


    private void Start() {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash(){// _EmissionColor e um variável do Material.

        if(!_currentTween.IsActive()){
            _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo); // LoopType e para fazer piscar quando inimigo for atingindo
        }
    }    
}