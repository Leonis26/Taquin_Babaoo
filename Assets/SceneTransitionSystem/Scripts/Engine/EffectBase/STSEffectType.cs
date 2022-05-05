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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSNoSmallPreviewAttribute : Attribute
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSNoBigPreviewAttribute : Attribute
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSAnimationCurveAttribute : Attribute
    {
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSEffectNameAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string EffectName;
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectNameAttribute(string sEffectName)
        {
            this.EffectName = sEffectName;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTintPrimaryAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Tint Primary";
        //-------------------------------------------------------------------------------------------------------------
        public STSTintPrimaryAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTintPrimaryAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTintSecondaryAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Tint Secondary";
        //-------------------------------------------------------------------------------------------------------------
        public STSTintSecondaryAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTintSecondaryAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTexturePrimaryAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Texture Primary";
        //-------------------------------------------------------------------------------------------------------------
        public STSTexturePrimaryAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTexturePrimaryAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTextureSecondaryAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Texture Secondary";
        //-------------------------------------------------------------------------------------------------------------
        public STSTextureSecondaryAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTextureSecondaryAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSParameterOneAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Parameter One";
        public bool Slider = false;
        public int Min;
        public int Max;
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterOneAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterOneAttribute(string sEntitlement, int sMin, int sMax)
        {
            this.Slider = true;
            this.Entitlement = sEntitlement;
            this.Min = sMin;
            this.Max = sMax;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterOneAttribute(string sEntitlement)
        {
            this.Slider = false;
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSParameterTwoAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Parameter Two";
        public bool Slider = false;
        public int Min;
        public int Max;
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterTwoAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterTwoAttribute(string sEntitlement, int sMin, int sMax)
        {
            this.Slider = true;
            this.Entitlement = sEntitlement;
            this.Min = sMin;
            this.Max = sMax;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterTwoAttribute(string sEntitlement)
        {
            this.Slider = false;
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSParameterThreeAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Parameter Three";
        public bool Slider = false;
        public int Min;
        public int Max;
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterThreeAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterThreeAttribute(string sEntitlement, int sMin, int sMax)
        {
            this.Slider = true;
            this.Entitlement = sEntitlement;
            this.Min = sMin;
            this.Max = sMax;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSParameterThreeAttribute(string sEntitlement)
        {
            this.Slider = false;
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSOffsetAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Offset";
        //-------------------------------------------------------------------------------------------------------------
        public STSOffsetAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSOffsetAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTwoCrossAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Orientation";
        //-------------------------------------------------------------------------------------------------------------
        public STSTwoCrossAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTwoCrossAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSFourCrossAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Four cross";
        //-------------------------------------------------------------------------------------------------------------
        public STSFourCrossAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSFourCrossAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSFiveCrossAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Five cross";
        //-------------------------------------------------------------------------------------------------------------
        public STSFiveCrossAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSFiveCrossAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSEightCrossAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Eight cross";
        //-------------------------------------------------------------------------------------------------------------
        public STSEightCrossAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSEightCrossAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSClockwiseAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Clockwise";
        //-------------------------------------------------------------------------------------------------------------
        public STSClockwiseAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSClockwiseAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSNineCrossAttribute : Attribute
    {
        //-------------------------------------------------------------------------------------------------------------
        public string Entitlement = "Nine cross";
        //-------------------------------------------------------------------------------------------------------------
        public STSNineCrossAttribute()
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSNineCrossAttribute(string sEntitlement)
        {
            this.Entitlement = sEntitlement;
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSFourCross : int
    {
        Top,
        Bottom,
        Right,
        Left,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSFiveCross : int
    {
        Top,
        Bottom,
        Right,
        Left,
        Center,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSEightCross : int
    {
        Top,
        Bottom,
        Right,
        Left,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSClockwise : int
    {
        Clockwise,
        Counterclockwise
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSTwoCross : int
    {
        Vertical,
        Horizontal
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public enum STSNineCross : int
    {
        Top,
        Bottom,
        Right,
        Left,
        Center,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [Serializable]
    public class STSEffectBase
    {
        //[SerializeField]
        public string EffectName = "";
        public AnimationCurve Curve;
        public Color TintPrimary = Color.black;
        public Color TintSecondary = Color.black;
        public Texture2D TexturePrimary = null;
        public Texture2D TextureSecondary = null;
        public Vector2 Offset;

        public STSTwoCross TwoCross;
        public STSFourCross FourCross;
        public STSFiveCross FiveCross;
        public STSEightCross EightCross;
        public STSNineCross NineCross;

        public STSClockwise Clockwise;

        public int ParameterOne;
        public int ParameterTwo;
        public int ParameterThree;

        public float Duration = 1.0F;
        public float Purcent = 0.0F;
        public float CurvePurcent = 0.0F;
        //-------------------------------------------------------------------------------------------------------------
        // Pseudo private... pblic but not in the inspector
        public int Direction = 0; // 0 is unknow; -1 go from 1.0F to 0.0F purcent; 1 go from 0.0F to 1.0F purcent
        public float AnimPurcent = 0.0F;
        public bool AnimIsPlaying = false;
        public bool AnimIsFinished = false;


        public Color OldColor;
        public float ColorDuration = 0.0F;
        public float ColorPurcent = 0.0F;
        public bool ColorIsPlaying = false;
        public bool ColorIsFinished = false;
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [Serializable]
    public class STSEffectType : STSEffectBase
    {
        //-------------------------------------------------------------------------------------------------------------
        public static List<GUIContent> kEffectContentList = new List<GUIContent>();
        public static List<Type> kEffectTypeList = new List<Type>();
        public static List<string> kEffectNameList = new List<string>();
        //-------------------------------------------------------------------------------------------------------------
        public static STSEffectType Default = new STSEffectType(STSEffectFade.K_FADE_NAME, Color.black, 1.0F);
        public static STSEffectType QuickDefault = new STSEffectType(STSEffectFade.K_FADE_NAME, Color.black, 0.50F);
        public static STSEffectType Flash = new STSEffectType(STSEffectFade.K_FADE_NAME, Color.white, 0.50F);
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectType()
        {
            Curve = AnimationCurve.Linear(0.0F, 0.0F, 1.0F, 1.0F);
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSEffect GetEffect()
        {
            STSEffect rReturn = null;
            int tIndex = STSEffectType.kEffectNameList.IndexOf(EffectName);
            if (tIndex < 0 || tIndex >= STSEffectType.kEffectNameList.Count())
            {
                rReturn = new STSEffectFade();
            }
            else
            {
                Type tEffectType = STSEffectType.kEffectTypeList[tIndex];
                rReturn = (STSEffect)Activator.CreateInstance(tEffectType);
                // Copy Param
                rReturn.CopyFrom(this);
            }
            return rReturn;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectType(string sString, Color sColor, float sDuration)
        {
            EffectName = sString;
            TintPrimary = sColor;
            Duration = sDuration;
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectType(STSEffectType sObject)
        {
            CopyFrom(sObject);
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSEffectType Dupplicate()
        {
            STSEffectType tCopy = new STSEffectType(this);
            return tCopy;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CopyFrom(STSEffectType sObject)
        {
            EffectName = sObject.EffectName;
            TintPrimary = sObject.TintPrimary;
            TintSecondary = sObject.TintSecondary;
            TexturePrimary = sObject.TexturePrimary;
            TextureSecondary = sObject.TextureSecondary;

            ParameterOne = sObject.ParameterOne;
            ParameterTwo = sObject.ParameterTwo;
            ParameterThree = sObject.ParameterThree;

            TwoCross = sObject.TwoCross;
            FourCross = sObject.FourCross;
            FiveCross = sObject.FiveCross;
            EightCross = sObject.EightCross;
            NineCross = sObject.NineCross;

            Clockwise = sObject.Clockwise;

            Duration = sObject.Duration;
            Purcent = sObject.Purcent;
            if (sObject.Curve != null)
            {
                Curve = new AnimationCurve(sObject.Curve.keys);
            }
            else
            {
                Curve = AnimationCurve.Linear(0.0F, 0.0F, 1.0F, 1.0F);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        static STSEffectType()
        {
            Type[] tAllTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            Type[] tAllNWDTypes = (from System.Type type in tAllTypes
                                   where type.IsSubclassOf(typeof(STSEffect))
                                   select type).ToArray();
            // reccord all the effect type
            foreach (Type tType in tAllNWDTypes)
            {
                string tEntitlement = tType.Name;
                if (tType.GetCustomAttributes(typeof(STSEffectNameAttribute), true).Length > 0)
                {
                    foreach (STSEffectNameAttribute tReference in tType.GetCustomAttributes(typeof(STSEffectNameAttribute), true))
                    {
                        tEntitlement = tReference.EffectName;
                    }
                }
                if (string.IsNullOrEmpty(tEntitlement))
                {
                    tEntitlement = tType.Name;
                }
                //MethodInfo tMethodInfo = tType.GetMethod("EffectName", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                //if (tMethodInfo != null)
                //{
                //    tEntitlement = tMethodInfo.Invoke(null, null) as string;
                //}
                kEffectContentList.Add(new GUIContent(tEntitlement));
                kEffectTypeList.Add(tType);
                kEffectNameList.Add(tEntitlement);
            }
            // Add Default
            kEffectNameList.Insert(0, "");
            kEffectTypeList.Insert(0, typeof(STSEffectFade));
            kEffectContentList.Insert(0, new GUIContent("Default"));
        }

        public static explicit operator STSEffectType(UnityEngine.Object v)
        {
            throw new NotImplementedException();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [STSEffectNameAttribute("Default effect")]
    // *** Remove some parameters in inspector
    // No remove
    // ***
    public class STSEffect : STSEffectType
    {
        //-------------------------------------------------------------------------------------------------------------
        public void EstimateCurvePurcent()
        {
            CurvePurcent = Curve.Evaluate(Purcent);
        }
        //-------------------------------------------------------------------------------------------------------------
        public void EstimatePurcent()
        {
            if (AnimPurcent < 1.0F || AnimPurcent >= 0.0F)
            {
                AnimPurcent += (Time.deltaTime) / Duration;
                if (AnimPurcent > 1.0F)
                {
                    AnimPurcent = 1.0F;
                    //AnimIsPlaying = false;
                    AnimIsFinished = true;
                }
                if (AnimPurcent < 0.0F)
                {
                    // IMPOSSIBLE
                    AnimPurcent = 0.0F;
                    //AnimIsPlaying = false;
                    AnimIsFinished = true;
                }
            }
            switch (Direction)
            {
                case 0:
                    // no direction do nothing ... on error
                    break;
                case 1:
                    Purcent = AnimPurcent;
                    break;
                case -1:
                    Purcent = 1 - AnimPurcent;
                    break;
            }
            //Debug.Log("STSEffect EstimatePurcent() => AnimPurcent = " + AnimPurcent.ToString("F3"));
            //Debug.Log("STSEffect EstimatePurcent() => Purcent = " + Purcent.ToString("F3"));
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StartEffectEnter(Rect sRect, Color sOldColor, float sInterEffectDelay, STSEffectMoreInfos sEffectMoreInfos)
        {
            PrepareEffectEnter(sRect);
            ColorIsPlaying = false;
            if (sInterEffectDelay > 0)
            {
                // I need do to a color transitionpublic float 
                ColorPurcent = 0.0F;
                ColorDuration = sInterEffectDelay;
                OldColor = sOldColor;
                ColorIsPlaying = true;
                ColorIsFinished = false;
            }
            else
            {
                ColorIsFinished = true;
            }
            AnimPurcent = 0.0F;
            Direction = -1;
            AnimIsPlaying = true;
            AnimIsFinished = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StartEffectExit(Rect sRect, STSEffectMoreInfos sEffectMoreInfos)
        {
            PrepareEffectExit(sRect);
            AnimPurcent = 0.0F;
            Direction = 1;
            AnimIsPlaying = true;
            AnimIsFinished = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void PauseEffect()
        {
            AnimIsPlaying = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void StopEffect()
        {
            AnimPurcent = 1.0F;
            AnimIsPlaying = false;
            AnimIsFinished = true;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ResetEffect()
        {
            AnimPurcent = 0.0F;
            Direction = 0;
            AnimIsPlaying = false;
            AnimIsFinished = false;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void DrawMaster(Rect sRect)
        {
            //if (Event.current.type.Equals(EventType.Repaint))
            //{
            // Do drawing
            if (ColorIsFinished == false)
            {
                ColorPurcent += (Time.deltaTime) / ColorDuration;
                Color tColor = Color.Lerp(OldColor, TintPrimary, ColorPurcent);
                STSDrawQuad.DrawRect(sRect, tColor);
                if (ColorPurcent >= 1)
                {
                    ColorIsPlaying = false;
                    ColorIsFinished = true;
                }
            }
            else
            {
                if (AnimIsPlaying == true) // play animation
                {
                    // estimate purcent
                    EstimatePurcent();
                    //EstimateCurvePurcent();
                    Draw(sRect);
                }
            }
            //}
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void PrepareEffectEnter(Rect sRect)
        {
            // Prepare your datas to draw
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void PrepareEffectExit(Rect sRect)
        {
            // Prepare your datas to draw
        }
        //-------------------------------------------------------------------------------------------------------------
        public virtual void Draw(Rect sRect)
        {
            // Do drawing with purcent
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================