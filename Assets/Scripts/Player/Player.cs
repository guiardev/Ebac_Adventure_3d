using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using UnityEngine;
using Cloth;

public class Player : Singleton<Player> {

    private float _vSpeed = 0f;
    private bool _alive = true, _jumping = false;

    public Animator animator;
    public List<Collider> colliders;

    public KeyCode jumpKeyCode = KeyCode.Space;
    public CharacterController characterController;
    public float speed = 1f, turnSpeed = 1f, jumpSpeed = 15f, gravity = -9.8f;

    [Header("Life")]
    public HealthBase healthBase;
    public UIFillUpdater uiFillUpdater;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    #region VALIDATE

    private void OnValidate() {

        if(healthBase == null){ 
            healthBase = GetComponent<HealthBase>();
        }
    }

    protected override void Awake() {

        base.Awake();

        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    #endregion

    #region LIFE

    private void OnKill(HealthBase h){

        if(_alive){

            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false); // tirando colisão personagem

            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive(){

        _alive = true;

        healthBase.ResetLife();
        animator.SetTrigger("Revive");

        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    private void TurnOnColliders(){
        colliders.ForEach(i => i.enabled = true); // ativando colisão personagem        
    }

    public void Damage(HealthBase h){
        flashColors.ForEach(i => i.Flash()); // fazendo personagem piscar quando for atingindo
        EffectsManager.Instance.ChangeVignette(); // fazendo efeito na tela quando personagem toma dano, o effect e PostProcessing
    }

    #endregion

    // Update is called once per frame
    void Update(){
        
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded){

            if(_jumping){
                _jumping = false;
                animator.SetTrigger("Land");
                Debug.Log("animator " + animator);
            }
    
            _vSpeed = 0;

            if(Input.GetKeyDown(jumpKeyCode)){


                _vSpeed = jumpSpeed;

                if(!_jumping){  
                    _jumping = true;
                    animator.SetTrigger("Jump");
                    Debug.Log("animator " + animator);
                }
            }
        }

        _vSpeed -= gravity * Time.deltaTime; // fazendo gravidade funcionar
        speedVector.y = _vSpeed;

        var isWalking = inputAxisVertical != 0;
        if(isWalking){
            
            if(Input.GetKey(keyRun)){

                speedVector *= speedRun;
                animator.speed = speedRun;
            }else{
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);  //verificar input Vertical

        /*
        //verificar input Vertical
        if(inputAxisVertical != 0){
            animator.SetBool("Run", true);
        }else{
            animator.SetBool("Run", false);
        }
        */

    }

    [NaughtyAttributes.Button]
    public void Respawn(){

        if(CheckpointManager.Instance.HasCheckpoint()){
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration){
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration){

        var defaultSpeed = speed;
        speed = localSpeed;

        yield return new WaitForSeconds(duration);

        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration){
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration){

        //var defaultSpeed = speed;
        //speed = localSpeed;

        _clothChanger.ChangeTexture(setup);

        yield return new WaitForSeconds(duration);

        //speed = defaultSpeed;
        _clothChanger.ResetTexture();
    }

}