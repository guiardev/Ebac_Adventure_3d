using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour{

    public List<string> tagsToHit;
    public float timeToDestroy = 2f, speed = 50f;
    public int damageAmount = 1;

    private void Awake() {
        Destroy(gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col){

        foreach (var t in tagsToHit){

            if(col.transform.tag == t){
                
                var damageable = col.transform.GetComponent<IDamageable>();

                if(damageable != null){

                    //fazendo personagem que levar tiro sentir impacto do tiro na direção oposta
                    //0,0,35
                    Vector3 dir = col.transform.position - transform.position;
                    //0,0,1
                    dir = -dir.normalized;

                    dir.y = 0; // zerando posição y para personagem atingindo nao subir

                    damageable.Damage(damageAmount, dir);
                    Destroy(gameObject);
                }

                break;
            }
        }

    }
}