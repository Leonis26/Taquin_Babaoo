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

//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class STSSceneManager : STSSingletonUnity<STSSceneManager>, STSTransitionInterface, STSIntermissionInterface
    {
        //-------------------------------------------------------------------------------------------------------------
        public static void TransitionSimulate(STSTransitionData sTransitionData = null, STSDelegate sDelegate = null)
        {
            Singleton().INTERNAL_PlayEffectWithCallBackTransition(SceneManager.GetActiveScene(), sTransitionData, sDelegate);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void TransitionSimulate(string sSceneName, STSTransitionData sTransitionData = null, STSDelegate sDelegate = null)
        {
            Singleton().INTERNAL_PlayEffectWithCallBackScene(sSceneName, sTransitionData, sDelegate);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_PlayEffectWithCallBackScene(string sSceneName, STSTransitionData sTransitionData = null, STSDelegate sDelegate = null)
        {
            if (TransitionInProgress == false)
            {
                List<string> tAllScenesList = new List<string>();
                tAllScenesList.Add(sSceneName);
                if (ScenesAreAllInBuild(tAllScenesList) == false)
                {
                    Debug.LogWarning(K_SCENE_UNKNOW);
                    return;
                }
                List<string> tScenes = new List<string>();
                for (int tSceneIndex = 0; tSceneIndex < SceneManager.sceneCount; tSceneIndex++)
                {
                    Scene tScene = SceneManager.GetSceneAt(tSceneIndex);
                    tScenes.Add(tScene.name);
                }
                if (tScenes.Contains(sSceneName))
                {
                    Scene tScene = SceneManager.GetSceneByName(sSceneName);
                    StartCoroutine(INTERNAL_PlayEffectWithCallBackSceneAsync(tScene, sTransitionData, sDelegate));
                }
                else
                {
                    Debug.LogWarning(K_SCENE_MUST_BY_LOADED);
                }
            }
            else
            {
                Debug.LogWarning(K_TRANSITION_IN_PROGRESS);
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_PlayEffectWithCallBackTransition(Scene sScene, STSTransitionData sTransitionData = null, STSDelegate sDelegate = null)
        {
            if (TransitionInProgress == false)
            {
                StartCoroutine(INTERNAL_PlayEffectWithCallBackSceneAsync(sScene, sTransitionData, sDelegate));
            }
            else
            {
                Debug.LogWarning(K_TRANSITION_IN_PROGRESS);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private IEnumerator INTERNAL_PlayEffectWithCallBackSceneAsync(Scene sScene, STSTransitionData sTransitionData = null, STSDelegate sDelegate = null)
        {
            TransitionInProgress = true;
            EventSystemPrevent(false);
            STSTransition tTransitionParams = GetTransitionsParams(sScene);
            STSTransitionInterface[] tActualSceneInterfaced = GetTransitionInterface(sScene);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneDisable(sTransitionData);
            }
            AnimationTransitionOut(tTransitionParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitStart(sTransitionData, tTransitionParams.EffectOnExit, true);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, true);
            }
            if (sDelegate != null)
            {
                sDelegate(sTransitionData);
            }
            AnimationTransitionIn(tTransitionParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tTransitionParams.EffectOnEnter, tTransitionParams.InterEffectDuration, true);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, true);
            }
            EventSystemPrevent(true);
            CameraPrevent(true);
            AudioListenerPrevent(true);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneEnable(sTransitionData);
            }
            TransitionInProgress = false;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================