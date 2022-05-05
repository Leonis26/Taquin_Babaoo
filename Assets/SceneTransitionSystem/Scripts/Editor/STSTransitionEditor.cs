//=====================================================================================================================
//
//  ideMobi 2019©
//
//  Author		Kortex (Jean-François CONTART) 
//  Email		jfcontart@idemobi.com
//  Project 	SceneTransitionSystem for Unity3D
//
//  All rights reserved by ideMobi
//
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [CustomEditor(typeof(STSTransition))]
    public class STSTransitionEditor : Editor
    {
        //-------------------------------------------------------------------------------------------------------------
        SerializedProperty SPEffectOnEnter;
        SerializedProperty SPInterEffectDuration;
        SerializedProperty SPEffectOnExit;
        //-------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            SPEffectOnEnter= serializedObject.FindProperty("EffectOnEnter");
            SPInterEffectDuration = serializedObject.FindProperty("InterEffectDuration");
            SPEffectOnExit= serializedObject.FindProperty("EffectOnExit");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            STSTransition tTarget = (STSTransition)target;
            //if (tTarget.gameObject.GetComponent<STSTransitionInterface>() != null)
            //{
            serializedObject.Update();
            //DrawDefaultInspector();
            //EditorGUILayout.HelpBox("Determine effects scene parameters.", MessageType.Info);
            EditorGUILayout.PropertyField(SPEffectOnEnter);
            EditorGUILayout.PropertyField(SPEffectOnExit);
            EditorGUILayout.PropertyField(SPInterEffectDuration);
            serializedObject.ApplyModifiedProperties();
            //}
            //else
            //{
            //    EditorGUILayout.HelpBox("Need component with interface ISTSTransitionParameters!", MessageType.Error);
            //}
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================

#endif