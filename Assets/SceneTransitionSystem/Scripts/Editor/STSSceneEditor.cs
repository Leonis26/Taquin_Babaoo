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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [CustomPropertyDrawer(typeof(STSScene))]
    public class STSSceneEditor : PropertyDrawer
    {
        //-------------------------------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty tPath = property.FindPropertyRelative("ScenePath");
            List<string> tPathList = new List<string>();
            tPathList.Add("None");
            foreach (EditorBuildSettingsScene tScene in EditorBuildSettings.scenes)
            {
                tPathList.Add(tScene.path);
            }
            int tPathIndex = tPathList.IndexOf(tPath.stringValue);
            int tPathIndexNext = EditorGUI.Popup(position, property.displayName, tPathIndex, tPathList.ToArray());
            if (tPathIndex != tPathIndexNext)
            {
                if (tPathIndexNext > 0)
                {
                    tPath.stringValue = tPathList[tPathIndexNext];
                }
                else
                {
                    tPath.stringValue = string.Empty;
                }
            }
            //if (tPathIndexNext >0 && tPathIndex != tPathIndexNext)
            //{
            //    tPath.stringValue = EditorBuildSettings.scenes[tPathIndexNext].path;
            //}
            //else
            //{
            //    tPath.stringValue = string.Empty;
            //}
            EditorGUI.EndProperty();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
#endif
