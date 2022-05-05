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
using System;
using System.Collections.Generic;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// Transition scene system
    /// </summary>
    //-----------------------------------------------------------------------------------------------------------------
    public partial class STSTransitionData
    {
        public string Level;
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// <summary>
    /// Transition scene system
    /// </summary>
    //-----------------------------------------------------------------------------------------------------------------
    public partial class STSTransitionData
    {
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectMoreInfos EffectMoreInfos;
        //-------------------------------------------------------------------------------------------------------------
        public string InternalName;
        public string Title;
        public string Subtitle;
        //-------------------------------------------------------------------------------------------------------------
        private Dictionary<string, object> DictionaryAsPayload = new Dictionary<string, object>();
        //-------------------------------------------------------------------------------------------------------------
        public STSTransitionData()
        {
            // Empty!
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTransitionData(string sInternalName)
        {
            InternalName = sInternalName;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTransitionData(string sInternalName, string sTitle, string sSubtitle, string sLevel)
        {
            InternalName = sInternalName;
            Title = sTitle;
            Subtitle = sSubtitle;
            Level = sLevel;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTransitionData(string sInternalName, string sTitle, string sSubtitle, string sLevel, Dictionary<string, object> sDictionaryAsPayload)
        {
            InternalName = sInternalName;
            Title = sTitle;
            Subtitle = sSubtitle;
            Level = sLevel;
            DictionaryAsPayload = sDictionaryAsPayload;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ClearPayLoad()
        {
            DictionaryAsPayload.Clear();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void AddObjectForKeyInPayload(string sKey, object sObject)
        {
            if (DictionaryAsPayload == null)
            {
                DictionaryAsPayload = new Dictionary<string, object>();
            }
            if (DictionaryAsPayload.ContainsKey(sKey))
            {
                DictionaryAsPayload[sKey] = sObject;
            }
            else
            {
                DictionaryAsPayload.Add(sKey, sObject);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        //public void AddObjectInPayload(object sObject)
        //{
        //    if (ListAsPayload == null)
        //    {
        //        ListAsPayload = new List<object>();
        //    }
        //    ListAsPayload.Add(sObject);
        //}
        //-------------------------------------------------------------------------------------------------------------
        public bool HasKey(string sKey)
        {
            bool rValue = false;
            if (DictionaryAsPayload.ContainsKey(sKey))
            {
                rValue = true;
            }
            return rValue;
        }
        //-------------------------------------------------------------------------------------------------------------
        public object GetObject(string sKey)
        {
            object tValue = null;
            if (DictionaryAsPayload.TryGetValue(sKey, out tValue))
            {
                return tValue;
            }
            return null;
        }
        //-------------------------------------------------------------------------------------------------------------
        public bool GetBool(string sKey, bool sDefault = false)
        {
            object tValue;
            if (DictionaryAsPayload.TryGetValue(sKey, out tValue))
            {
                return Convert.ToBoolean(tValue);
            }
            return sDefault;
        }
        //-------------------------------------------------------------------------------------------------------------
        public string GetString(string sKey, string sDefault = "")
        {
            object tValue;
            if (DictionaryAsPayload.TryGetValue(sKey, out tValue))
            {
                return Convert.ToString(tValue);
            }
            return sDefault;
        }
        //-------------------------------------------------------------------------------------------------------------
        public int GetInt(string sKey, int sDefault = -1)
        {
            object tValue;
            if (DictionaryAsPayload.TryGetValue(sKey, out tValue))
            {
                return Convert.ToInt32(tValue);
            }
            return sDefault;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
