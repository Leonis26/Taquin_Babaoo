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
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using SceneTransitionSystem;

//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSSceneButton : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        public STSScene ActiveScene;
        public STSScene IntermissionScene;
        public STSScene[] AdditionnalScenes;
        //-------------------------------------------------------------------------------------------------------------
        public void RunTransition()
        {
            Debug.Log("STSSceneButton RunTransition()");
            STSSceneManager.ReplaceAllByScenes(ActiveScene, AdditionnalScenes, IntermissionScene);
        }
        //-------------------------------------------------------------------------------------------------------------

    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================