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
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// STS Constants. Use to declare all constants.
    /// </summary>
    public static class STSConstants
    {
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The name of the transition controller game object in the root scene.
        /// </summary>
        public static string K_TRANSITION_CONTROLLER_OBJECT_NAME = "STSControllerObject";
        /// <summary>
        /// the name for default object transition
        /// </summary>
        public static string K_TRANSITION_DEFAULT_OBJECT_NAME = "STSTransitionDefault";
        /// <summary>
        /// the name for default object Intermission
        /// </summary>
        public static string K_TRANSITION_Intermission_OBJECT_NAME = "STSIntermissionDefault";
        /// <summary>
        /// The scene name key use in payload as dictionary key.
        /// </summary>
        public static string K_SCENE_NAME_KEY = "SceneNameKey_872d7fe";
        /// <summary>
        /// The scene name key use in payload as dictionary key.
        /// </summary>
        public static string K_Intermission_SCENE_NAME_KEY = "IntermissionSceneNameKey_88jk7fe";
        /// <summary>
        /// The load mode key use in payload as dictionary key.
        /// </summary>
        public static string K_LOAD_MODE_KEY = "LoadModeKey_j7vhv8e";
        /// <summary>
        /// The payload data key use in payload as dictionary key.
        /// </summary>
        public static string K_PAYLOAD_DATA_KEY = "PayloadDataKey_33h52fe";
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// editor infos if no little preview
        /// </summary>
        public static string K_NO_LITTLE_PREVIEW = "No little preview";
        /// <summary>
        /// editor infos if no big preview
        /// </summary>
        public static string K_NO_BIG_PREVIEW = "No big preview";
        /// <summary>
        /// editor infos big preview
        /// </summary>
        public static string K_SHOW_BIG_PREVIEW = "Big preview";
        /// <summary>
        /// editor infos run big preview
        /// </summary>
        public static string K_RUN_BIG_PREVIEW = "Run big preview";
        /// <summary>
        /// Button text to get more effects
        /// </summary>
        public static string K_ASSET_STORE = "GET MORE EFFECTS";
        /// <summary>
        /// 
        /// </summary>
        //public static string K_ASSET_STORE_URL = string.Empty;
        public static string K_ASSET_STORE_URL = "https://assetstore.unity.com/?q=STSEffect&orderBy=0";
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================