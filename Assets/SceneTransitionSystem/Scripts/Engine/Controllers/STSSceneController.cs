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
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSSceneDebugColor
    {
        //-------------------------------------------------------------------------------------------------------------
        black,
        red,
        green,
        yellow,
        blue,
        gray,
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSSceneController : MonoBehaviour, STSTransitionInterface
    {
        //-------------------------------------------------------------------------------------------------------------
        [Header("Debug Mode")]
        public bool ActiveLog = false;
        public STSSceneDebugColor LogTagColor = STSSceneDebugColor.black;
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionSceneLoaded(STSTransitionData sData)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionSceneLoaded()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionEnterFinish(STSTransitionData sData, bool sActiveScene)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionEnterFinish()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionEnterStart(STSTransitionData sData, STSEffectType sEffect, float sInterludeDuration, bool sActiveScene)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionEnterStart()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionSceneEnable(STSTransitionData sData)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionSceneEnable()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionSceneDisable(STSTransitionData sData)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionSceneDisable()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionExitStart(STSTransitionData sData, STSEffectType sEffect, bool sActiveScene)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionExitStart()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionExitFinish(STSTransitionData sData, bool sActiveScene)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionExitFinish()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void OnTransitionSceneWillUnloaded(STSTransitionData sData)
        {
            if (ActiveLog == true)
            {
                Debug.Log("<color=" + LogTagColor.ToString() + ">" + this.gameObject.scene.name + "</color> OnTransitionSceneWillUnloaded()");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================