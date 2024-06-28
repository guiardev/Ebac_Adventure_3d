using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour{

    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    private Tween _currentTween;
    private Color _defaultColor;
    public Color color = Color.red;
    public float duration = .1f;

    private void OnValidate() {

        // método OnValidate seve para pegar componentes
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    /*
    private void Start() {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }
    */

    [NaughtyAttributes.Button]
    public void Flash(){// _EmissionColor e um variável do Material.

        if(meshRenderer != null && !_currentTween.IsActive()){
            // LoopType e para fazer piscar quando inimigo for atingindo
            _currentTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }

        if(skinnedMeshRenderer != null && !_currentTween.IsActive()){
            _currentTween = skinnedMeshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }

    }    
}