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
using System.IO;

//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public partial class STSSceneManager : STSSingletonUnity<STSSceneManager>, STSTransitionInterface, STSIntermissionInterface
    {
        //-------------------------------------------------------------------------------------------------------------
        const string K_SCENE_MUST_BY_LOADED = "Scene must be loaded!";
        const string K_SCENE_UNKNOW = "Some scenes are not in build!";
        const string K_TRANSITION_IN_PROGRESS = "Transition in progress";
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Prevent multi transition by using this state
        /// </summary>
        private bool TransitionInProgress = false;
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Return if transition is in progress or not. 
        /// </summary>
        /// <returns></returns>
        static public bool InTransition()
        {
            bool rReturn = Singleton().TransitionInProgress;
            if (rReturn == false)
            {
                Debug.Log("#### STSSceneManager NOT IN TRANSITION");
            }
            else
            {
                Debug.Log("#### STSSceneManager IN TRANSITION");
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        // prevent user actions during the transition
        private bool PreventUserInteractions = true;
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectType DefaultEffectOnEnter;
        public STSEffectType DefaultEffectOnExit;
        private STSEffect EffectType;
        //-------------------------------------------------------------------------------------------------------------
        private float StandByTimer;
        private bool LauchNextScene = false;
        private bool StandByInProgress = false;
        public STSScene OriginalScene;
        //-------------------------------------------------------------------------------------------------------------
        // Memory managment
        public override void InitInstance()
        {
            //Debug.Log("STSSceneManager InitInstance()");
            //if (gameObject.GetComponent<STSTransition>() == null)
            //{
            //    gameObject.AddComponent<STSTransition>();
            //}
            //if (gameObject.GetComponent<STSIntermission>() == null)
            //{
            //    gameObject.AddComponent<STSIntermission>();
            //}
        }
        //-------------------------------------------------------------------------------------------------------------
        private void Start()
        {
            //Debug.Log("<color=red>START</color>");
            if (OriginalScene == null)
            {
                OriginalScene = new STSScene();
                Scene tScene = SceneManager.GetActiveScene();
                if (tScene.path != null)
                {
                    OriginalScene.ScenePath = tScene.path;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        // toolbox method
        private void AudioListenerPrevent(bool sEnable)
        {
            //Debug.Log("STSSceneManager AudioListenerPrevent()");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene tScene = SceneManager.GetSceneAt(i);
                //if (tScene.isLoaded)
                {
                    AudioListenerEnable(tScene, false);
                }
            }
            AudioListenerEnable(SceneManager.GetActiveScene(), sEnable);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void AudioListenerEnable(Scene sScene, bool sEnable)
        {
            //Debug.Log("STSSceneManager AudioListenerEnable()");
            // if (sScene.isLoaded)
            {
                AudioListener tAudioListener = null;
                GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
                foreach (GameObject tObject in tAllRootObjects)
                {
                    if (tObject.GetComponent<AudioListener>() != null)
                    {
                        tAudioListener = tObject.GetComponent<AudioListener>();
                    }
                }
                if (tAudioListener != null)
                {
                    tAudioListener.enabled = sEnable;
                }
                else
                {
                    //Debug.Log ("No <AudioListener> type component found in the root Objects. Becarefull!");
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void EventSystemPrevent(bool sEnable)
        {
            //Debug.Log("STSSceneManager EventSystemPrevent()");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene tScene = SceneManager.GetSceneAt(i);
                if (tScene.isLoaded)
                {
                    EventSystemEnable(tScene, false);
                }
            }
            EventSystemEnable(SceneManager.GetActiveScene(), sEnable);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void EventSystemEnable(Scene sScene, bool sEnable)
        {
            //Debug.Log("STSSceneManager EventSystemEnable()");
            if (PreventUserInteractions == true)
            {
                EventSystem tEventSystem = null;
                if (string.IsNullOrEmpty(sScene.name) == false)
                {
                    GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
                    foreach (GameObject tObject in tAllRootObjects)
                    {
                        if (tObject.GetComponent<EventSystem>() != null)
                        {
                            tEventSystem = tObject.GetComponent<EventSystem>();
                        }
                    }
                    if (tEventSystem != null)
                    {
                        tEventSystem.enabled = sEnable;
                    }
                }
                else
                {
                    Debug.LogWarning("EventSystemEnable() - Scene is null");
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void CameraPrevent(bool sEnable)
        {
            //Debug.Log("STSSceneManager EventSystemPrevent()");
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene tScene = SceneManager.GetSceneAt(i);
                if (tScene.isLoaded)
                {
                    CameraPreventEnable(tScene, false);
                }
            }
            CameraPreventEnable(SceneManager.GetActiveScene(), sEnable);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void CameraPreventEnable(Scene sScene, bool sEnable)
        {
            //Debug.Log("STSSceneManager EventSystemEnable()");
            if (PreventUserInteractions == true)
            {
                Camera tCameraSystem = null;
                if (string.IsNullOrEmpty(sScene.name) == false)
                {
                    GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
                    foreach (GameObject tObject in tAllRootObjects)
                    {
                        if (tObject.GetComponent<Camera>() != null)
                        {
                            tCameraSystem = tObject.GetComponent<Camera>();
                        }
                    }
                    if (tCameraSystem != null)
                    {
                        tCameraSystem.enabled = sEnable;
                    }
                }
                else
                {
                    Debug.LogWarning("CameraEnable() - Scene is null");
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private STSIntermissionInterface[] GetIntermissionInterface(Scene sScene)
        {
            List<STSIntermissionInterface> rReturn = new List<STSIntermissionInterface>();
            GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
            foreach (GameObject tObject in tAllRootObjects)
            {
                STSIntermissionInterface tScript = tObject.GetComponent<STSIntermissionInterface>();
                if (tScript != null)
                {
                    rReturn.Add(tScript);
                }
            }
            return rReturn.ToArray();
            //return FindObjectsOfType(typeof(STSIntermissionInterface)) as STSIntermissionInterface[];
        }
        //-------------------------------------------------------------------------------------------------------------
        private STSTransitionInterface[] GetTransitionInterface(Scene sScene)
        {
            List<STSTransitionInterface> rReturn = new List<STSTransitionInterface>();
            GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
            foreach (GameObject tObject in tAllRootObjects)
            {
                STSTransitionInterface tScript = tObject.GetComponent<STSTransitionInterface>();
                if (tScript != null)
                {
                    rReturn.Add(tScript);
                }
            }
            return rReturn.ToArray();
            //return FindObjectsOfType(typeof(STSTransitionInterface)) as STSTransitionInterface[];
        }
        //-------------------------------------------------------------------------------------------------------------
        private STSTransitionInterface[] GetOtherTransitionInterface(Scene sExceptScene)
        {
            List<STSTransitionInterface> rReturn = new List<STSTransitionInterface>();
            foreach (Scene tScene in GetAllLoadedScenes())
            {
                if (tScene != sExceptScene)
                {
                    GameObject[] tAllRootObjects = tScene.GetRootGameObjects();
                    foreach (GameObject tObject in tAllRootObjects)
                    {
                        STSTransitionInterface tScript = tObject.GetComponent<STSTransitionInterface>();
                        if (tScript != null)
                        {
                            rReturn.Add(tScript);
                        }
                    }
                }
            }
            return rReturn.ToArray();
            //return FindObjectsOfType(typeof(STSTransitionInterface)) as STSTransitionInterface[];
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTransition GetTransitionsParams(Scene sScene)
        {
            STSTransition tTransitionParametersScript;
            if (STSTransition.SharedInstanceExists(sScene))
            {
                //Debug.LogWarning("tTransitionParametersScript exists");
                tTransitionParametersScript = STSTransition.SharedInstance(sScene);
            }
            else
            {
                //Debug.LogWarning("tTransitionParametersScript not exists");
                tTransitionParametersScript = STSTransition.SharedInstance(sScene);
                if (DefaultEffectOnEnter != null)
                {
                    tTransitionParametersScript.EffectOnEnter = DefaultEffectOnEnter.Dupplicate();
                }
                else
                {
                    tTransitionParametersScript.EffectOnEnter = STSEffectType.Default.Dupplicate();
                }
                if (DefaultEffectOnEnter != null)
                {
                    tTransitionParametersScript.EffectOnExit = DefaultEffectOnExit.Dupplicate();
                }
                else
                {
                    tTransitionParametersScript.EffectOnExit = STSEffectType.Default.Dupplicate();
                }
            }
            /*
			//Debug.Log("STSSceneManager GetTransitionsParams()");
			STSTransition tTransitionParametersScript = null;
			GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
			// quick solution?!
			foreach (GameObject tObject in tAllRootObjects)
			{
				if (tObject.GetComponent<STSTransition>() != null)
				{
					tTransitionParametersScript = tObject.GetComponent<STSTransition>();
					break;
				}
			}
			// slower solution?!
			if (tTransitionParametersScript == null)
			{
				foreach (GameObject tObject in tAllRootObjects)
				{
					if (tObject.GetComponentInChildren<STSTransition>() != null)
					{
						tTransitionParametersScript = tObject.GetComponent<STSTransition>();
						break;
					}
				}
			}
			// no solution?!
			if (tTransitionParametersScript == null)
			{
				Scene tActual = SceneManager.GetActiveScene();
				SceneManager.SetActiveScene(sScene);
				// create Game Object?
				//Debug.Log ("NO PARAMS");
				GameObject tObjToSpawn = new GameObject(STSConstants.K_TRANSITION_DEFAULT_OBJECT_NAME);
				tObjToSpawn.AddComponent<STSSceneController>();
				tTransitionParametersScript = tObjToSpawn.AddComponent<STSTransition>();
				if (DefaultEffectOnEnter != null)
				{
					tTransitionParametersScript.EffectOnEnter = DefaultEffectOnEnter.Dupplicate();
				}
				else
				{
					tTransitionParametersScript.EffectOnEnter = STSEffectType.Default.Dupplicate();
				}
				if (DefaultEffectOnEnter != null)
				{
					tTransitionParametersScript.EffectOnExit = DefaultEffectOnExit.Dupplicate();
				}
				else
				{
					tTransitionParametersScript.EffectOnExit = STSEffectType.Default.Dupplicate();
				}
				SceneManager.SetActiveScene(tActual);
			}
            */
            return tTransitionParametersScript;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSIntermission GetStandByParams(Scene sScene)
        {
            STSIntermission tTransitionStandByScript;
            if (STSIntermission.SharedInstanceExists(sScene))
            {
                tTransitionStandByScript = STSIntermission.SharedInstance(sScene);
            }
            else
            {
                tTransitionStandByScript = STSIntermission.SharedInstance(sScene);
                tTransitionStandByScript.StandBySeconds = 5.0f;
                tTransitionStandByScript.AutoActiveNextScene = true;
            }
            /*
            //Debug.Log("STSSceneManager GetStandByParams()");
            STSIntermission tTransitionStandByScript = null;
            GameObject[] tAllRootObjects = sScene.GetRootGameObjects();
            // quick solution?!
            foreach (GameObject tObject in tAllRootObjects)
            {
                if (tObject.GetComponent<STSIntermission>() != null)
                {
                    tTransitionStandByScript = tObject.GetComponent<STSIntermission>();
                    break;
                }
            }
            // slower solution?!
            if (tTransitionStandByScript == null)
            {
                foreach (GameObject tObject in tAllRootObjects)
                {
                    if (tObject.GetComponentInChildren<STSIntermission>() != null)
                    {
                        tTransitionStandByScript = tObject.GetComponent<STSIntermission>();
                        break;
                    }
                }
            }
            // no solution?!
            if (tTransitionStandByScript == null)
            {
                Scene tActual = SceneManager.GetActiveScene();
                SceneManager.SetActiveScene(sScene);
                GameObject tObjToSpawn = new GameObject(STSConstants.K_TRANSITION_Intermission_OBJECT_NAME);
                tObjToSpawn.AddComponent<STSSceneIntermissionController>();
                tTransitionStandByScript = tObjToSpawn.AddComponent<STSIntermission>();
                tTransitionStandByScript.StandBySeconds = 5.0f;
                tTransitionStandByScript.AutoLoadNextScene = true;
                SceneManager.SetActiveScene(tActual);
            }
            */
            return tTransitionStandByScript;
        }
        //private int m_DrawDepth = -1000;
        //-------------------------------------------------------------------------------------------------------------
        private bool AnimationProgress()
        {
            return EffectType.AnimIsPlaying;
        }
        //-------------------------------------------------------------------------------------------------------------
        private bool AnimationFinished()
        {
            return EffectType.AnimIsFinished;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void AnimationTransitionIn(STSTransition sThisSceneParameters, STSTransitionData sDatas)
        {
            Color tOldColor = Color.black;
            float tInterlude = 0;
            if (EffectType != null)
            {
                // I get the old value of
                tOldColor = new Color(EffectType.TintPrimary.r, EffectType.TintPrimary.g, EffectType.TintPrimary.b, EffectType.TintPrimary.a);
                tInterlude = sThisSceneParameters.InterEffectDuration;
            }
            EffectType = sThisSceneParameters.EffectOnEnter.GetEffect();
            if (EffectType == null)
            {
                EffectType = new STSEffectFade();
            }
            STSEffectMoreInfos sMoreInfos = null;
            if (sDatas != null)
            {
                sMoreInfos = sDatas.EffectMoreInfos;
            }
            EffectType.StartEffectEnter(new Rect(0, 0, Screen.width, Screen.height), tOldColor, tInterlude, sMoreInfos);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void AnimationTransitionOut(STSTransition sThisSceneParameters, STSTransitionData sDatas)
        {
            EffectType = sThisSceneParameters.EffectOnExit.GetEffect();
            if (EffectType == null)
            {
                EffectType = new STSEffectFade();
            }
            STSEffectMoreInfos sMoreInfos = null;
            if (sDatas != null)
            {
                sMoreInfos = sDatas.EffectMoreInfos;
            }
            EffectType.StartEffectExit(new Rect(0, 0, Screen.width, Screen.height), sMoreInfos);
        }
        //-------------------------------------------------------------------------------------------------------------
        private void StandBy()
        {
            StandByTimer = 0.0f;
            StandByInProgress = true;
            LauchNextScene = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        private bool StandByIsProgressing(STSIntermission sIntermissionSceneStandBy)
        {
            StandByTimer += Time.deltaTime;
            if (StandByTimer >= sIntermissionSceneStandBy.StandBySeconds)
            {
                StandByInProgress = false;
            }
            return StandByInProgress;
        }
        //-------------------------------------------------------------------------------------------------------------
        private bool WaitingToLauchNextScene(STSIntermission sIntermissionSceneStandBy)
        {
            if (sIntermissionSceneStandBy.AutoActiveNextScene == true)
            {
                LauchNextScene = true;
            }
            return !LauchNextScene;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void FinishStandBy()
        {
            LauchNextScene = true;
            StandByInProgress = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        void OnGUI()
        {
            if (EffectType != null)
            {
                EffectType.DrawMaster(new Rect(0, 0, Screen.width, Screen.height));
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private bool ScenesAreAllInBuild(List<string> sScenesList)
        {
            bool rReturn = true;
            List<string> tScenesInBuildList = new List<string>();
            for (int tIndex = 0; tIndex < SceneManager.sceneCountInBuildSettings; tIndex++)
            {
                string tScenePath = SceneUtility.GetScenePathByBuildIndex(tIndex);
                string sSceneInBuild = Path.GetFileNameWithoutExtension(tScenePath);
                //Debug.Log("scene ["+tIndex + "] => " + sSceneInBuild + " IN BUILD!");
                tScenesInBuildList.Add(sSceneInBuild);
            }
            foreach (string tScene in sScenesList)
            {
                if (tScenesInBuildList.Contains(tScene) == false)
                {
                    Debug.LogWarning("Scene '" + tScene + "' NOT IN BUILD! STOP THE TRAIN? (array is '" + string.Join("','", sScenesList) + "')");
                    rReturn = false;
                    break;
                }
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionEnterStart(STSTransitionData sData, STSEffectType sEffect, float sInterludeDuration, bool sActiveScene)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionEnterFinish(STSTransitionData sData, bool sActiveScene)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionExitStart(STSTransitionData sData, STSEffectType sEffect, bool sActiveScene)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionExitFinish(STSTransitionData sData, bool sActiveScene)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionSceneLoaded(STSTransitionData sData)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionSceneEnable(STSTransitionData sData)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionSceneDisable(STSTransitionData sData)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnTransitionSceneWillUnloaded(STSTransitionData sData)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadNextSceneStart(STSTransitionData sData, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadingNextScenePercent(STSTransitionData sData, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadNextSceneFinish(STSTransitionData sData, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnStandByStart(STSIntermission sStandBy)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnStandByFinish(STSIntermission sStandBy)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadingSceneStart(STSTransitionData sData, string sSceneName, int SceneNumber, float sScenePercent, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadingScenePercent(STSTransitionData sData, string sSceneName, int SceneNumber, float sScenePercent, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnLoadingSceneFinish(STSTransitionData sData, string sSceneName, int SceneNumber, float sScenePercent, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnSceneAllReadyLoaded(STSTransitionData sData, string sSceneName, int SceneNumber, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnUnloadScene(STSTransitionData sData, string sSceneName, int SceneNumber, float sPercent)
        {
            //throw new System.NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================