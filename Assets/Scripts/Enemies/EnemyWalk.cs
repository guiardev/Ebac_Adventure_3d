using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{

    public class EnemyWalk : EnemyBase{

        [Header("Waypoints")]
        private int _index = 0;
        public GameObject[] waypoints;
        public float minDistance = 1f, speed = 1f;

        public override void Update() {

            base.Update();

            //fazendo inimigo andar seguindo os waypoints
            if(Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance){

                _index++;

                if(_index >= waypoints.Length){
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
            transform.LookAt(waypoints[_index].transform.position); // fazendo inimigo olhar na direção do waypoints
        }
    }
}