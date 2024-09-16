using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy{
    
    public class EnemyBase : MonoBehaviour, IDamageable{

        private Player _player;
        [SerializeField] private float _currentLife;
        public FlashColor flashColor;
        public Collider collider;
        public ParticleSystem particleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public Ease startAnimationEase = Ease.OutBack;
        public float startAnimationDuration = .2f;
        public bool startWithBornAnimation = true;

        private void Awake() {
            Init();
        }

        private void Start() {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife(){
            _currentLife = startLife;
        }

        protected virtual void Init(){
            ResetLife();

            if(startWithBornAnimation){
                BornAnimation();
            }
        }

        protected virtual void Kill(){
            OnKill();
        }

        protected virtual void OnKill(){

            if (collider != null) collider.enabled = false;
            Destroy(gameObject, 2f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f){

            if(flashColor != null){
                flashColor.Flash();
            }

            if(particleSystem != null){
               particleSystem.Emit(15);    
            }

            transform.position -= transform.forward; // fazendo personagem que levou tiro afastar com impacto do tiro

            _currentLife -= f;

            if(_currentLife <= 0){
               Kill(); 
            }
        }

        #region ANIMATION

        private void BornAnimation(){
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType){
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        /*
        //debug
        private void Update() {
            
            if(Input.GetKeyDown(KeyCode.T)){
                OnDamage(5f);
            }
        }
        */

        public void Damage(float damage){
            Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir){
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision col) {

            Player p = col.transform.GetComponent<Player>();

            if(p != null){
               p.healthBase.Damage(30);
            }
        }
        
        public virtual void Update(){

            if(lookAtPlayer){ // lookAtPlayer = true
                transform.LookAt(_player.transform.position); //fazendo inimigo olhar para player
            }
        }
    }
}