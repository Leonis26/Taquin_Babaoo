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
    [CustomEditor(typeof(STSSceneManager))]
    public class STSSceneManagerEditor : Editor
    {
        //-------------------------------------------------------------------------------------------------------------
        SerializedProperty SPDefaultEffectOnEnter;
        SerializedProperty SPDefaultEffectOnExit;
        SerializedProperty SPEffectOnExit;
        //-------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            SPDefaultEffectOnEnter= serializedObject.FindProperty("DefaultEffectOnEnter");
            SPDefaultEffectOnExit = serializedObject.FindProperty("DefaultEffectOnExit");
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            STSSceneManager tTarget = (STSSceneManager)target;
            serializedObject.Update();
            EditorGUILayout.PropertyField(SPDefaultEffectOnEnter);
            EditorGUILayout.PropertyField(SPDefaultEffectOnExit);
            serializedObject.ApplyModifiedProperties();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================

#endif