using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour{

    public float timeToDestroy = 2f, speed = 50f;
    public int damageAmount = 1;

    private void Awake() {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col) {

        
    }
}