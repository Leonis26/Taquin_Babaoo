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
        public static void ActiveScene(string sSceneNameToActive, string sIntermissionSceneName = null, STSTransitionData sTransitionData = null)
        {
            Singleton().INTERNAL_ActiveScene(SceneManager.GetActiveScene().name, sSceneNameToActive, sIntermissionSceneName, sTransitionData);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_ActiveScene(string sActualActiveSceneName, string sSceneNameToActive, string sIntermissionSceneName = null, STSTransitionData sTransitionData = null)
        {
            if (TransitionInProgress == false)
            {
                List<string> tAllScenesListS = new List<string>();
                tAllScenesListS.Add(sActualActiveSceneName);
                tAllScenesListS.Add(sSceneNameToActive);
                tAllScenesListS.Add(sIntermissionSceneName);
                List<string> tAllScenesList = new List<string>();
                foreach (string tScen in tAllScenesListS)
                {
                    if (string.IsNullOrEmpty(tScen) == false)
                    {
                        if (tAllScenesList.Contains(tScen) == false)
                        {
                            tAllScenesList.Add(tScen);
                        }
                    }
                }
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
                if (tScenes.Contains(sActualActiveSceneName) && tScenes.Contains(sSceneNameToActive))
                {
                    if (string.IsNullOrEmpty(sIntermissionSceneName))
                    {
                        StartCoroutine(INTERNAL_ActiveSceneWithoutIntermissionAsync(sTransitionData, sActualActiveSceneName, sSceneNameToActive));
                    }
                    else
                    {
                        StartCoroutine(INTERNAL_ActiveSceneWithIntermissionAsync(sTransitionData, sActualActiveSceneName, sSceneNameToActive, sIntermissionSceneName));
                    }
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
        private IEnumerator INTERNAL_ActiveSceneWithoutIntermissionAsync(STSTransitionData sTransitionData, string sActualActiveSceneName, string sSceneNameToActive)
        {
            TransitionInProgress = true;
            STSTransition tActualSceneParams = GetTransitionsParams(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransitionInterface[] tActualSceneInterfaced = GetTransitionInterface(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransitionInterface[] tOtherSceneInterfaced = GetOtherTransitionInterface(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransition tNextSceneParams = GetTransitionsParams(SceneManager.GetSceneByName(sSceneNameToActive));
            STSTransitionInterface[] tNextSceneInterfaced = GetTransitionInterface(SceneManager.GetSceneByName(sSceneNameToActive));
            EventSystemPrevent(false);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneDisable(sTransitionData);
            }
            AnimationTransitionOut(tActualSceneParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitStart(sTransitionData, tActualSceneParams.EffectOnExit, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionExitStart(sTransitionData, tActualSceneParams.EffectOnExit, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, false);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sSceneNameToActive));
            CameraPrevent(true);
            AudioListenerPrevent(true);
            AnimationTransitionIn(tNextSceneParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tNextSceneParams.EffectOnEnter, tNextSceneParams.InterEffectDuration, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tNextSceneParams.EffectOnEnter, tNextSceneParams.InterEffectDuration, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, false);
            }
            EventSystemPrevent(true);
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneEnable(sTransitionData);
            }
            TransitionInProgress = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        private IEnumerator INTERNAL_ActiveSceneWithIntermissionAsync(STSTransitionData sTransitionData, string sActualActiveSceneName, string sSceneNameToActive, string sIntermissionSceneName)
        {
            TransitionInProgress = true;
            STSTransition tActualSceneParams = GetTransitionsParams(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransitionInterface[] tActualSceneInterfaced = GetTransitionInterface(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransitionInterface[] tOtherSceneInterfaced = GetOtherTransitionInterface(SceneManager.GetSceneByName(sActualActiveSceneName));
            STSTransition tNextSceneParams = GetTransitionsParams(SceneManager.GetSceneByName(sSceneNameToActive));
            STSTransitionInterface[] tNextSceneInterfaced = GetTransitionInterface(SceneManager.GetSceneByName(sSceneNameToActive));
            EventSystemPrevent(false);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneDisable(sTransitionData);
            }
            AnimationTransitionOut(tActualSceneParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitStart(sTransitionData, tActualSceneParams.EffectOnExit, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionExitStart(sTransitionData, tActualSceneParams.EffectOnExit, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tActualSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, false);
            }
            // load transition scene async
            AsyncOperation tAsynchroneLoadIntermissionOperation;
            tAsynchroneLoadIntermissionOperation = SceneManager.LoadSceneAsync(sIntermissionSceneName, LoadSceneMode.Additive);
            tAsynchroneLoadIntermissionOperation.allowSceneActivation = false;
            while (tAsynchroneLoadIntermissionOperation.progress < 0.9f)
            {
                yield return null;
            }
            // Intermission scene will be active
            tAsynchroneLoadIntermissionOperation.allowSceneActivation = true;
            while (!tAsynchroneLoadIntermissionOperation.isDone)
            {
                yield return null;
            }
            // get Transition Scene
            Scene tIntermissionScene = SceneManager.GetSceneByName(sIntermissionSceneName);
            // Active the next scene as root scene 
            SceneManager.SetActiveScene(tIntermissionScene);
            // disable audiolistener of preview scene
            CameraPrevent(true);
            AudioListenerPrevent(true);
            // get params
            STSTransition tIntermissionSceneParams = GetTransitionsParams(tIntermissionScene);
            STSTransitionInterface[] tIntermissionSceneInterfaced = GetTransitionInterface(tIntermissionScene);
            STSIntermissionInterface[] tIntermissionInterfaced = GetIntermissionInterface(tIntermissionScene);
            // disable the user interactions until fadein 
            EventSystemEnable(tIntermissionScene, false);
            // Intermission scene is loaded
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneLoaded(sTransitionData);
            }
            // animation in Go!
            AnimationTransitionIn(tIntermissionSceneParams, sTransitionData);
            // animation in
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tIntermissionSceneParams.EffectOnEnter,tIntermissionSceneParams.InterEffectDuration, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tIntermissionSceneParams.EffectOnEnter,tIntermissionSceneParams.InterEffectDuration, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            // animation in Finish
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, false);
            }
            // enable the user interactions 
            EventSystemEnable(tIntermissionScene, true);
            // enable the user interactions 
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneEnable(sTransitionData);
            }
            // start stand by
            STSIntermission tIntermissionSceneStandBy = GetStandByParams(tIntermissionScene);
            foreach (STSIntermissionInterface tInterfaced in tIntermissionInterfaced)
            {
                tInterfaced.OnStandByStart(tIntermissionSceneStandBy);
            }
            StandBy();
            while (StandByIsProgressing(tIntermissionSceneStandBy))
            {
                yield return null;
            }
            // send call back for standby finished
            foreach (STSIntermissionInterface tInterfaced in tIntermissionInterfaced)
            {
                tInterfaced.OnStandByFinish(tIntermissionSceneStandBy);
            }
            // Waiting to load the next Scene
            while (WaitingToLauchNextScene(tIntermissionSceneStandBy))
            {
                //Debug.Log ("StandByIsNotFinished loop");
                yield return null;
            }
            // stanby is finish
            // disable user interactions on the transition scene
            EventSystemEnable(tIntermissionScene, false);


            // disable user interactions on the Intermission scene
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneDisable(sTransitionData);
            }
            // Intermission scene Transition Out GO! 
            AnimationTransitionOut(tIntermissionSceneParams, sTransitionData);
            // Intermission scene Transition Out start 
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData,tIntermissionSceneParams.EffectOnExit ,tIntermissionSceneParams.InterEffectDuration, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData,tIntermissionSceneParams.EffectOnExit ,tIntermissionSceneParams.InterEffectDuration, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            // Intermission scene Transition Out finished! 
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionExitFinish(sTransitionData, false);
            }
            // fadeout is finish
            // will unloaded the Intermission scene
            foreach (STSTransitionInterface tInterfaced in tIntermissionSceneInterfaced)
            {
                tInterfaced.OnTransitionSceneWillUnloaded(sTransitionData);
            }
            AsyncOperation tAsynchroneUnloadTransition;
            tAsynchroneUnloadTransition = SceneManager.UnloadSceneAsync(sIntermissionSceneName);
            while (tAsynchroneUnloadTransition.progress < 0.9f)
            {
                yield return null;
            }
            while (!tAsynchroneUnloadTransition.isDone)
            {
                yield return null;
            }
            CameraPrevent(true);
            AudioListenerPrevent(true);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sSceneNameToActive));
            AnimationTransitionIn(tNextSceneParams, sTransitionData);
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tNextSceneParams.EffectOnEnter,tNextSceneParams.InterEffectDuration, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterStart(sTransitionData, tNextSceneParams.EffectOnEnter,tNextSceneParams.InterEffectDuration, false);
            }
            while (AnimationFinished() == false)
            {
                yield return null;
            }
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, true);
            }
            foreach (STSTransitionInterface tInterfaced in tOtherSceneInterfaced)
            {
                tInterfaced.OnTransitionEnterFinish(sTransitionData, false);
            }
            EventSystemPrevent(true);
            foreach (STSTransitionInterface tInterfaced in tNextSceneInterfaced)
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