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
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public partial class STSSceneManager : STSSingletonUnity<STSSceneManager>, STSTransitionInterface, STSIntermissionInterface
    {
        //-------------------------------------------------------------------------------------------------------------
        private List<STSScenesPackage> Historic = new List<STSScenesPackage>();
        private STSScenesPackage DefaultScenesPackage;
        //-------------------------------------------------------------------------------------------------------------
        public static void ResetHistoric()
        {
            Singleton().INTERNAL_Reset();
        }
        //-------------------------------------------------------------------------------------------------------------
        //public static void GoTo(string sKey)
        //{
        //    GoTo(sKey, null);
        //}
        ////-------------------------------------------------------------------------------------------------------------
        //public static void GoTo(string sKey, STSTransitionData sNewData)
        //{
        //}
        //-------------------------------------------------------------------------------------------------------------
        public static void GoBack()
        {
            GoBack(1, null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void GoBack(STSTransitionData sNewData)
        {
            GoBack(1, sNewData);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void GoBack(int sUnstack)
        {
            GoBack(sUnstack, null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void GoBack(int sUnstack, STSTransitionData sNewData)
        {
            for (int ti = 0; ti < sUnstack; ti++)
            {
                if (Singleton().Historic.Count > 0)
                {
                    Singleton().Historic.RemoveAt(Singleton().Historic.Count - 1);
                }
            }
            GoTo(Singleton().Historic.Count - 1, sNewData);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void GoTo(int sHistoricIndex)
        {
            GoTo(sHistoricIndex, null);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void GoTo(int sHistoricIndex, STSTransitionData sNewData)
        {
            if (sHistoricIndex >= 0 && sHistoricIndex < Singleton().Historic.Count)
            {
                Singleton().INTERNAL_Go(Singleton().Historic[sHistoricIndex], sNewData);
            }
            else
            {
                Debug.LogWarning("No scene in historic");
                Singleton().INTERNAL_Go(null, null);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_Reset()
        {
            Historic.Clear();
        }
        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_AddNavigation(string sActiveSceneName, List<string> sScenesNameList, string sIntermissionScene, STSTransitionData sDatas)
        {
            INTERNAL_GetDefaultScenesPackage(); // create default
            STSScenesPackage tScenePackage = new STSScenesPackage(sActiveSceneName, sScenesNameList, sIntermissionScene, sDatas);
            Historic.Add(tScenePackage);
        }
        //-------------------------------------------------------------------------------------------------------------
        private STSScenesPackage INTERNAL_GetDefaultScenesPackage()
        {
            if (DefaultScenesPackage == null)
            {
                if (OriginalScene == null)
                {
                    OriginalScene = new STSScene();
                    Scene tScene = SceneManager.GetActiveScene();
                    if (tScene.path != null)
                    {
                        OriginalScene.ScenePath = tScene.path;
                    }
                }
                DefaultScenesPackage = new STSScenesPackage(OriginalScene.GetSceneShortName(), null, null, null);
            }
            return DefaultScenesPackage;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void INTERNAL_Go(STSScenesPackage sPackage, STSTransitionData sNewData)
        {
            if (sPackage == null)
            {
                sPackage = INTERNAL_GetDefaultScenesPackage();
            }
            List<string> tScenesToRemove = new List<string>();
            for (int tSceneIndex = 0; tSceneIndex < SceneManager.sceneCount; tSceneIndex++)
            {
                Scene tScene = SceneManager.GetSceneAt(tSceneIndex);
                tScenesToRemove.Add(tScene.name);
            }
            if (sNewData == null)
            {
                sNewData = sPackage.Datas;
            }
            Singleton().INTERNAL_ChangeScenes(SceneManager.GetActiveScene().name,
                sPackage.ActiveSceneName,
                sPackage.ScenesNameList,
                tScenesToRemove,
                sPackage.IntermissionScene,
                sNewData,
                false);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================