using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(FSM))]
public class StateMachineEditor : Editor{

    public bool showFoldout;

    public override void OnInspectorGUI(){

        base.OnInspectorGUI();

        FSM fsm = (FSM)target;

        EditorGUILayout.LabelField("State Machine");
        EditorGUILayout.Space(30);

        if (fsm.stateMachine == null){
            return;
        }

        if (fsm.stateMachine.CurrentState != null){
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout){

            if (fsm.stateMachine.dictionaryState != null){

                var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
                var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

                for (int i = 0; i < keys.Length; i++){
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}