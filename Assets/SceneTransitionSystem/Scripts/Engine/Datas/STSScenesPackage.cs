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

//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSScenesPackage
    {
        //-------------------------------------------------------------------------------------------------------------
        public string ActiveSceneName;
        public List<string> ScenesNameList = new List<string>();
        public string IntermissionScene;
        public STSTransitionData Datas;
        //-------------------------------------------------------------------------------------------------------------
        public STSScenesPackage(string sActiveSceneName, List<string> sScenesNameList, string sIntermissionScene, STSTransitionData sDatas)
        {
            ActiveSceneName = sActiveSceneName;
            ScenesNameList = sScenesNameList;
            IntermissionScene = sIntermissionScene;
            Datas = sDatas;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================