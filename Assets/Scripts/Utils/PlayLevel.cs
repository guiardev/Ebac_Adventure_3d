using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour{

    public TextMeshProUGUI uiTxtName;

    // Start is called before the first frame update
    void Start(){
        SaveManager.Instance.fileLoadedAction += OnLoad;
    }

    public void OnLoad(SaveSetup setup){
        uiTxtName.text = "Play " + (setup.lastLevel + 1);    
    }

    private void OnDestroy() {
        SaveManager.Instance.fileLoadedAction -= OnLoad;
    }
}
