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
    [CustomEditor(typeof(STSIntermission))]
    public class STSIntermissionEditor : Editor
    {
        //-------------------------------------------------------------------------------------------------------------
        SerializedProperty SPStandBySeconds;
        SerializedProperty SPActiveLoadNextScene;
        //-------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            SPStandBySeconds = serializedObject.FindProperty("StandBySeconds");
            SPActiveLoadNextScene = serializedObject.FindProperty("AutoActiveNextScene");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            STSIntermission tTarget = (STSIntermission)target;
            //if (tTarget.gameObject.GetComponent<STSIntermissionInterface>() != null)
            //{
            serializedObject.Update();
            //DrawDefaultInspector();
            //EditorGUILayout.HelpBox("Determine the intermission scene parameters.", MessageType.Info);
            EditorGUILayout.PropertyField(SPStandBySeconds);
            EditorGUILayout.PropertyField(SPActiveLoadNextScene);
            serializedObject.ApplyModifiedProperties();
            //}
            //else
            //{
            //    EditorGUILayout.HelpBox("Need component with interface ISTSTransitionStandBy!", MessageType.Error);
            //}
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================

#endif