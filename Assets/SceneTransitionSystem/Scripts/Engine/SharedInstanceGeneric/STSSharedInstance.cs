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
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class SharedInstanceBasis : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        public bool Initialized = false;
        //-------------------------------------------------------------------------------------------------------------
        public virtual void InitInstance()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class SharedInstanceUnity<K> : SharedInstanceBasis where K : SharedInstanceBasis, new()
    {
        //-------------------------------------------------------------------------------------------------------------
        private static Dictionary<Scene, K> kSharedInstanceBySceneList = new Dictionary<Scene, K>();
        private static K kSharedInstance = null;
        //-------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            //Debug.Log("SharedInstanceUnity<K> Awake() for gameobject named '" + gameObject.name + "'");
            kSharedInstance = this as K;
            //Check if there is already an instance of K
            Scene tScene = gameObject.scene;
            if (kSharedInstanceBySceneList.ContainsKey(tScene) == false)
            {
                //Debug.Log("SharedInstanceUnity<K> Awake() case kSharedInstance == null for gameobject named '" + gameObject.name + "'");
                //if not, set it to this.
                kSharedInstanceBySceneList.Add(tScene, this as K);
                if (Initialized == false)
                {
                    //Debug.Log("SharedInstanceUnity<K> Awake() case kSharedInstance.Initialized == false for gameobject named '" + gameObject.name + "'");
                    // Init Instance
                    InitInstance();
                    // memorize the init instance
                    Initialized = true;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static K LastSharedInstance()
        {
            return kSharedInstance;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static K SharedInstance()
        {
            return SharedInstance(SceneManager.GetActiveScene());
        }
        //-------------------------------------------------------------------------------------------------------------
        public static bool SharedInstanceExists(Scene sScene)
        {
            bool rReturn = kSharedInstanceBySceneList.ContainsKey(sScene);
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static K SharedInstance(Scene sScene)
        {
            K rReturn = null;
            //Debug.Log("SharedInstanceUnity<K> SharedInstance()");
            if (kSharedInstanceBySceneList.ContainsKey(sScene) == false)
            {
				Scene tActual = SceneManager.GetActiveScene();
				SceneManager.SetActiveScene(sScene);
                //Debug.Log("SharedInstanceUnity<K> Singleton() case kSharedInstance == null");
                // I need to create singleton
                GameObject tObjToSpawn;
                //spawn object
                tObjToSpawn = new GameObject(typeof(K).Name + " SharedInstance");
                //Add Components
                tObjToSpawn.AddComponent<K>();
                // keep k_Singleton
                rReturn = tObjToSpawn.GetComponent<K>();
				SceneManager.SetActiveScene(tActual);
            }
            else
            {
                rReturn = kSharedInstanceBySceneList[sScene];
                //Debug.Log("SharedInstanceUnity<K> Singleton() case kSharedInstance != null (exist in gameobject named '" + rReturn.gameObject.name + "')");
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void InitInstance()
        {
            //Debug.Log("SharedInstanceUnity<K> InitInstance() for gameobject named '" + gameObject.name + "'");
            // do something by override
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnDestroy()
        {
            //Debug.Log("SharedInstanceUnity<K> OnDestroy() for gameobject named '" + gameObject.name + "'");
            Scene tScene = gameObject.scene;
            K tThis = this as K;
            if (kSharedInstanceBySceneList.ContainsKey(tScene) == true)
            {
                kSharedInstanceBySceneList.Remove(tScene);
            }
            if (kSharedInstance == tThis)
            {
                kSharedInstance = null;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================