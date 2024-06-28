using System.Collections;
using System.Collections.Generic;
using Ebac.StateMachine;
using UnityEngine;


namespace Boss{

    public class BossStateBase : StateBase{

        protected BossBase boss;

        public override void OnStateEnter(params object[] objs){

            base.OnStateEnter(objs);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStatesInit : BossStateBase{

        public override void OnStateEnter(params object[] objs) {

            base.OnStateEnter(objs);
            boss.StartInitAnimation();
            //Debug.Log("Boss: " + boss);
        }
    }

    public class BossStatesWalk : BossStateBase{

        public override void OnStateEnter(params object[] objs){

            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);
        }

        private void OnArrive(){// mudando stato boss quando ele chegar no destino
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit(){
            Debug.Log("Exit Attack");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStatesAttack : BossStateBase{

        public override void OnStateEnter(params object[] objs){

            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks(){
            boss.SwitchState(BossAction.WALK);
        }

        public override void OnStateExit(){
            Debug.Log("Exit Walk");
            base.OnStateExit();
            boss.StopAllCoroutines();
        }
    }

    public class BossStatesDeath : BossStateBase{

        public override void OnStateEnter(params object[] objs){
            Debug.Log("Enter Death");
            base.OnStateEnter(objs);
            boss.transform.localScale = Vector3.one * .2f;
        }
    }
}