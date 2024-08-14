using System;
using System.Collections;
using System.Collections.Generic;
using Ebac.StateMachine;
using UnityEngine;
using DG.Tweening;

namespace Boss{

    public enum BossAction{
        INIT, IDLE, WALK, ATTACK, DEATH
    }
    
    public class BossBase : MonoBehaviour{

        private StateMachine<BossAction> stateMachine;

        [Header("Animation")]
        public Ease startAnimationEase = Ease.OutBack;
        public float startAnimationDuration = .5f;

        [Header("Attack")]
        public HealthBase healthBase;
        public List<Transform> waypoints;
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f, speed = 5f;

        private void OnValidate(){

            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake(){

            Init();
            OnValidate();

            if(healthBase != null){
                healthBase.OnKill += OnBossKill;
            }
        
            //healthBase.OnDamage += Damage;
        }

        private void Init(){

            //iniciando statos StateMachine
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            Debug.Log("stateMachine " + stateMachine);
            stateMachine.RegisterState(BossAction.INIT, new BossStatesInit());
            stateMachine.RegisterState(BossAction.WALK, new BossStatesWalk());
            stateMachine.RegisterState(BossAction.ATTACK, new BossStatesAttack());
            stateMachine.RegisterState(BossAction.DEATH, new BossStatesDeath());
        }

        private void OnBossKill(HealthBase h){
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK

        public void StartAttack(Action endCallback = null){
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null){

            int attacks = 0;

            while(attacks < attackAmount){
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo); //fazendo o animação
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }

        #endregion

        #region WALK BOSS

        public void GoToRandomPoint(Action onArrive = null){
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null){
            
            //fazendo boss mover com um distancia
            while(Vector3.Distance(transform.position, t.position) > 1f){
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            //if(onArrive != null) onArrive.Invoke();
            onArrive?.Invoke();
        }

        #endregion 

        #region ANIMATION

        public void StartInitAnimation(){
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion

        #region DEBUG

        [NaughtyAttributes.Button]
        private void SwitchInit(){
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk(){
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack(){
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region STATE MACHINE

        public void SwitchState(BossAction state){
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}