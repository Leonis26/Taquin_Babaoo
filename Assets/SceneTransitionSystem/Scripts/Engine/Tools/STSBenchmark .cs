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
using System.Diagnostics;

//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSBenchmark
    {
        //-------------------------------------------------------------------------------------------------------------
        public static Dictionary<string, DateTime> cStartDico = new Dictionary<string, DateTime>();
        public static Dictionary<string, int> cCounterDico = new Dictionary<string, int>();
        public static Dictionary<string, float> cMaxDico = new Dictionary<string, float>();
        public static Dictionary<string, float> cMaxGranDico = new Dictionary<string, float>();
        public static Dictionary<string, string> cTagDico = new Dictionary<string, string>();
        //-------------------------------------------------------------------------------------------------------------
        public static void ResetAll()
        {
            cStartDico = new Dictionary<string, DateTime>();
            cCounterDico = new Dictionary<string, int>();
            cTagDico = new Dictionary<string, string>();
        }
        //-------------------------------------------------------------------------------------------------------------
        protected static string GetKey()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(2);
            string tMethod = sf.GetMethod().DeclaringType.Name + " "+ sf.GetMethod().Name;
            return tMethod;
        }
        //-------------------------------------------------------------------------------------------------------------
        protected static UnityEngine.Object GetObject()
        {
            //StackTrace st = new StackTrace();
            //StackFrame sf = st.GetFrame(2);
            UnityEngine.Object sObject = null;
            return sObject;
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Start()
        {
            Start(GetKey());
        }
        //-------------------------------------------------------------------------------------------------------------
        static float kMaxDefault = 0.010f;
        static float kMaxPerOperationDefault = 0.001f;
        //-------------------------------------------------------------------------------------------------------------
        public static void Start(string sKey)
        {
            if (cStartDico.ContainsKey(sKey) == true)
            {
                cStartDico[sKey] = DateTime.Now;
                cCounterDico[sKey] = 0;
                cTagDico[sKey] = string.Empty;
                cMaxDico[sKey] = kMaxDefault;
                cMaxGranDico[sKey] = kMaxPerOperationDefault;
            }
            else
            {
                cStartDico.Add(sKey, DateTime.Now);
                cCounterDico.Add(sKey, 0);
                cTagDico.Add(sKey, string.Empty);
                cMaxDico.Add(sKey, kMaxDefault);
                cMaxGranDico.Add(sKey,kMaxPerOperationDefault);
            }
            UnityEngine.Debug.Log("benchmark : '" + sKey + " start now!");
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Tag(string sTag)
        {
            Tag(GetKey(), sTag);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Tag(string sKey, string sTag)
        {
            if (cStartDico.ContainsKey(sKey) == true)
            {
                cTagDico[sKey] = sTag;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Max(float sMax)
        {
            Max(GetKey(), sMax);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Max(string sKey, float sMax)
        {
            if (cStartDico.ContainsKey(sKey) == true)
            {
                cMaxDico[sKey] = sMax;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void MaxPerOperation(float sMax)
        {
            MaxPerOperation(GetKey(), sMax);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void MaxPerOperation(string sKey, float sMax)
        {
            if (cStartDico.ContainsKey(sKey) == true)
            {
                cMaxGranDico[sKey] = sMax;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Increment(int sVal = 1)
        {
            Increment(GetKey(), sVal);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void Increment(string sKey, int sVal = 1)
        {
            if (cStartDico.ContainsKey(sKey) == true)
            {
                cCounterDico[sKey] = cCounterDico[sKey] + sVal;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static double Finish(bool sWithDebug = true)
        {
            return Finish(GetKey(), sWithDebug);
        }
        //-------------------------------------------------------------------------------------------------------------
        public static double Finish(string sKey, bool sWithDebug = true)
        {
            double rDelta = 0;
            if (cStartDico.ContainsKey(sKey) == true)
            {
                double tStart = STSDateHelper.ConvertToTimestamp(cStartDico[sKey]);
                int tCounter = cCounterDico[sKey];
                string tTag = cTagDico[sKey];
                if (string.IsNullOrEmpty(tTag) == false)
                {
                    tTag = " (tag : "+tTag+")";
                }
                float tMax = cMaxDico[sKey];
                float tMaxGranule = cMaxGranDico[sKey];
                cStartDico.Remove(sKey);
                cCounterDico.Remove(sKey);
                cTagDico.Remove(sKey);
                cMaxDico.Remove(sKey);
                cMaxGranDico.Remove(sKey);
                double tFinish = STSDateHelper.ConvertToTimestamp(DateTime.Now);
                rDelta = tFinish - tStart;
                string tMaxColor = "black";
                if (rDelta >= tMax)
                {
                    tMaxColor = "red";
                }
                if (sWithDebug == true)
                {
                    if (tCounter == 1)
                    {
                        UnityEngine.Debug.Log("benchmark : '" + sKey +"'"+tTag+ " execute " + tCounter +
                         " operation in <color=" + tMaxColor + ">" +
                         rDelta.ToString("F3") + " seconds </color>");
                    }
                    else if (tCounter > 1)
                    {
                        double tGranule = rDelta / tCounter;
                        string tMaxGranuleColor = "black";
                        if (tGranule >= tMaxGranule)
                        {
                            tMaxGranuleColor = "red";
                        }
                        UnityEngine.Debug.Log("benchmark : '" + sKey + "'"+tTag+ " execute " + tCounter +
                         " operations in <color=" + tMaxColor + ">" + rDelta.ToString("F3") +
                         " seconds </color>(<color="+tMaxGranuleColor+">" + tGranule.ToString("F5") +
                         " seconds per operation</color>)");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("benchmark : '" + sKey + "'"+tTag+ " execute in <color="+tMaxColor+">" +
                         rDelta.ToString("F3") + " seconds </color>");
                    }
                }
            }
            else
            {
                if (sWithDebug == true)
                {
                    UnityEngine.Debug.Log("benchmark : error '" + sKey + "' has no start value");
                }
            }
            return rDelta;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================