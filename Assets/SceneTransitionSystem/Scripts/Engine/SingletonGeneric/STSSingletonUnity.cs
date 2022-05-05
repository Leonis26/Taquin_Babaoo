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
using UnityEngine;
using UnityEngine.SceneManagement;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSSingletonRoot
    {
        GameObject,
        Component,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSSingletonBasis : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        public bool Initialized = false;
        //-------------------------------------------------------------------------------------------------------------
        public virtual STSSingletonRoot DestroyRoot()
        {
            return STSSingletonRoot.Component;
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void InitInstance()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnSceneLoaded(Scene sScene, LoadSceneMode sMode)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnSceneUnLoaded(Scene sScene)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSSingletonUnity<K> : STSSingletonBasis where K : STSSingletonBasis, new()
    {
        //-------------------------------------------------------------------------------------------------------------
        private static K kSingleton = null;
        //-------------------------------------------------------------------------------------------------------------
        public override STSSingletonRoot DestroyRoot()
        {
            return STSSingletonRoot.Component;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            //Debug.Log("STSSingleton<K> Awake() for gameobject named '" + gameObject.name + "'");
            //Check if there is already an instance of K
            if (kSingleton == null)
            {
                //Debug.Log("STSSingleton<K> Awake() case kSingleton == null for gameobject named '" + gameObject.name + "'");
                //if not, set it to this.
                kSingleton = this as K;
                if (Initialized == false)
                {
                    //Debug.Log("STSSingleton<K> Awake() case kSingleton.Initialized == false for gameobject named '" + gameObject.name + "'");
                    // Init Instance
                    InitInstance();
                    // scene is use on laded new scene
                    SceneManager.sceneLoaded += OnSceneLoaded;
                    // first install in first scene
                    OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
                    // memorize the init instance
                    Initialized = true;
                }
                //Set K's gameobject to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
                DontDestroyOnLoad(gameObject);
            }
            //If instance already exists:
            if (kSingleton != this)
            {
                //Debug.Log("STSSingleton<K> Awake() case kSingleton != this for gameobject named '" + gameObject.name + "'");
                //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
                //Debug.Log("singleton prevent destruction gameobject named '" + gameObject.name + "'");
                if (DestroyRoot() == STSSingletonRoot.Component)
                {
                    Destroy(this);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static bool SingletonExists()
        {
            bool rReturn = kSingleton == null;
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static K Singleton()
        {
            //Debug.Log("STSSingleton<K> Singleton()");
            if (kSingleton == null)
            {
                //Debug.Log("STSSingleton<K> Singleton() case kSingleton == null");
                // I need to create singleton
                GameObject tObjToSpawn;
                //spawn object
                tObjToSpawn = new GameObject(typeof(K).Name + " Singleton");
                //Add Components
                tObjToSpawn.AddComponent<K>();
                // keep k_Singleton
                kSingleton = tObjToSpawn.GetComponent<K>();
            }
            else
            {
                //Debug.Log("STSSingleton<K> Singleton() case kSingleton != null (exist in gameobject named '" + kSingleton.gameObject.name + "')");
            }
            return kSingleton;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void InitInstance()
        {
            //Debug.Log("STSSingleton<K> InitInstance() for gameobject named '" + gameObject.name + "'");
            // do something by override
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnSceneLoaded(Scene sScene, LoadSceneMode sMode)
        {
            //Debug.Log("STSSingleton<K> OnSceneLoaded() for gameobject named '" + gameObject.name + "'");
            // do something by override
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnSceneUnLoaded(Scene sScene)
        {
            //Debug.Log("STSSingleton<K> OnSceneUnLoaded() for gameobject named '" + gameObject.name + "'");
            // do something by override
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OnDestroy()
        {
            //Debug.Log("STSSingleton<K> OnDestroy() for gameobject named '" + gameObject.name + "'");
            if (kSingleton == this)
            {
                kSingleton = null;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================