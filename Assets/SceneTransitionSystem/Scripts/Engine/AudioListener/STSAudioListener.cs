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
    public partial class STSAudioListener : STSSingletonUnity<STSAudioListener>
    {
        //-------------------------------------------------------------------------------------------------------------
        AudioListener SharedAudioListener;
        GameObject AudioFollowObject;
        Camera DefaultCamera;
        //-------------------------------------------------------------------------------------------------------------
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void OnRuntimeMethodLoad()
        {
            Singleton();
        }
        //-------------------------------------------------------------------------------------------------------------
        // Memory managment
        public override void InitInstance()
        {
            SharedAudioListener = gameObject.AddComponent<AudioListener>();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
            Prevent();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Prevent();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnSceneUnLoaded(Scene scene)
        {
            Prevent();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void Prevent()
        {
            foreach (AudioListener tAudio in FindObjectsOfType<AudioListener>())
            {
                if (tAudio != SharedAudioListener)
                {
                    Destroy(tAudio);
                }
            }
            DefaultCamera = Camera.main;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (AudioFollowObject != null)
            {
                transform.position = AudioFollowObject.transform.position;
            }
            else
            {
                if (DefaultCamera != null)
                {
                    transform.position = DefaultCamera.transform.position;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================