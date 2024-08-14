using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : Singleton<EffectsManager>{

    [SerializeField] private Vignette _vignette;
    public PostProcessVolume processVolume;

    public float duration = 1f;

    [NaughtyAttributes.Button]    
    public void ChangeVignette(){
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette(){

        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp)){
            _vignette = tmp;
        }

        //criando um variável ColorParameter porque o vignette so vai aceitar ColorParameter
        ColorParameter c = new ColorParameter();

        float time = 0;

        while(time < duration){

            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            //c.value = Color.red;
            time += Time.deltaTime;

            _vignette.color.Override(c); // Override para mudar color do vignette

            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration){

            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            //c.value = Color.red;
            time += Time.deltaTime;

            _vignette.color.Override(c); // Override para mudar color do vignette   

            yield return new WaitForEndOfFrame();
        }

    }

}