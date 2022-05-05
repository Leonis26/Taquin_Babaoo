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
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [CustomPropertyDrawer(typeof(STSEffectType))]
    public class STSEffectTypeEditor : PropertyDrawer
    {
        ////-------------------------------------------------------------------------------------------------------------
        public static Texture2D kImagePreviewA = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewA.png"));
        public static Texture2D kImagePreviewB = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewB.png"));
        public static Texture2D kImagePreviewC = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewC.png"));
        public static Texture2D kImagePreviewD = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewD.png"));
        //-------------------------------------------------------------------------------------------------------------
        public float kSizePreview = 80.0F;
        public float kSizePreviewPopup = 30.0F;
        public float kSizePreviewButton = 120.0F;
        //-------------------------------------------------------------------------------------------------------------
        static GUIStyle tMiniButtonStyle;
        static GUIStyle tPopupFieldStyle;
        static GUIStyle tColorFieldStyle;
        static GUIStyle tTextfieldStyle;
        static GUIStyle tObjectFieldStyle;
        static GUIStyle tNumberFieldStyle;
        static GUIStyle tNoPreviewFieldStyle;
        //-------------------------------------------------------------------------------------------------------------
        const float kMarge = 4.0F;
        //-------------------------------------------------------------------------------------------------------------
        //float LocalPurcent = 0.0F;
        STSEffect rSmallPreview = null;
        STSEffect rBigPreview = null;
        Rect OldRect;
        int SelectedPreview = 0;
        //-------------------------------------------------------------------------------------------------------------
        static STSEffectTypeEditor()
        {
            tMiniButtonStyle = new GUIStyle(EditorStyles.miniButton);
            tMiniButtonStyle.fixedHeight = tMiniButtonStyle.CalcHeight(new GUIContent("A"), 100);

            tPopupFieldStyle = new GUIStyle(EditorStyles.popup);
            tPopupFieldStyle.fixedHeight = tPopupFieldStyle.CalcHeight(new GUIContent("A"), 100);

            tColorFieldStyle = new GUIStyle(EditorStyles.colorField);
            tColorFieldStyle.fixedHeight = tColorFieldStyle.CalcHeight(new GUIContent("A"), 100);

            tTextfieldStyle = new GUIStyle(EditorStyles.textField);
            tTextfieldStyle.fixedHeight = tTextfieldStyle.CalcHeight(new GUIContent("A"), 100);

            tObjectFieldStyle = new GUIStyle(EditorStyles.objectField);
            tObjectFieldStyle.fixedHeight = tObjectFieldStyle.CalcHeight(new GUIContent("A"), 100);

            tNumberFieldStyle = new GUIStyle(EditorStyles.numberField);
            tNumberFieldStyle.fixedHeight = tNumberFieldStyle.CalcHeight(new GUIContent("A"), 100);

            tNoPreviewFieldStyle = new GUIStyle(EditorStyles.boldLabel);
            tNoPreviewFieldStyle.alignment = TextAnchor.MiddleCenter;
            tNoPreviewFieldStyle.normal.textColor = Color.red;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float tH = 0.0F;

            SerializedProperty tEffectName = property.FindPropertyRelative("EffectName");
            string tValue = tEffectName.stringValue;
            int tIndex = STSEffectType.kEffectNameList.IndexOf(tValue);
            if (tIndex < 0 || tIndex >= STSEffectType.kEffectNameList.Count())
            {
                tIndex = 0;
            }
            Type tEffectType = STSEffectType.kEffectTypeList[tIndex];

            // Effet selectio by popup
            tH += tPopupFieldStyle.fixedHeight + kMarge;

            // Tint Primary
            if (tEffectType.GetCustomAttributes(typeof(STSTintPrimaryAttribute), true).Length > 0)
            {
                tH += tColorFieldStyle.fixedHeight + kMarge;
            }

            // Tint Secondary
            if (tEffectType.GetCustomAttributes(typeof(STSTintSecondaryAttribute), true).Length > 0)
            {
                tH += tColorFieldStyle.fixedHeight + kMarge;
            }

            // Texture Primary
            if (tEffectType.GetCustomAttributes(typeof(STSTexturePrimaryAttribute), true).Length > 0)
            {
                tH += tObjectFieldStyle.fixedHeight + kMarge;
            }

            // Texture Secondary
            if (tEffectType.GetCustomAttributes(typeof(STSTextureSecondaryAttribute), true).Length > 0)
            {
                tH += tObjectFieldStyle.fixedHeight + kMarge;
            }

            // Parameter One
            if (tEffectType.GetCustomAttributes(typeof(STSParameterOneAttribute), true).Length > 0)
            {
                tH += tNumberFieldStyle.fixedHeight + kMarge;
            }

            // Parameter Two
            if (tEffectType.GetCustomAttributes(typeof(STSParameterTwoAttribute), true).Length > 0)
            {
                tH += tNumberFieldStyle.fixedHeight + kMarge;
            }

            // Parameter Three
            if (tEffectType.GetCustomAttributes(typeof(STSParameterThreeAttribute), true).Length > 0)
            {
                tH += tNumberFieldStyle.fixedHeight + kMarge;
            }

            // Offset
            if (tEffectType.GetCustomAttributes(typeof(STSOffsetAttribute), true).Length > 0)
            {
                tH += tNumberFieldStyle.fixedHeight + kMarge;
            }

            // TwoCross
            if (tEffectType.GetCustomAttributes(typeof(STSTwoCrossAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            // FourCross
            if (tEffectType.GetCustomAttributes(typeof(STSFourCrossAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            // FiveCross
            if (tEffectType.GetCustomAttributes(typeof(STSFiveCrossAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            //EightCross
            if (tEffectType.GetCustomAttributes(typeof(STSEightCrossAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            // NineCross
            if (tEffectType.GetCustomAttributes(typeof(STSNineCrossAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            // ClockwiseCross
            if (tEffectType.GetCustomAttributes(typeof(STSClockwiseAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }


            //  Animantion Curve
            if (tEffectType.GetCustomAttributes(typeof(STSAnimationCurveAttribute), true).Length > 0)
            {
                tH += tPopupFieldStyle.fixedHeight + kMarge;
            }

            // Duration
            tH += tNumberFieldStyle.fixedHeight + kMarge;

            // Purcent
            tH += tNumberFieldStyle.fixedHeight + kMarge;

            // Purcent
            tH += kSizePreview + kMarge;

            return tH;
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float tY = position.y;

            EditorGUI.BeginProperty(position, label, property);
            bool tAutoInstallPreview = false;
            bool tAutoPreparePreview = false;
            EditorGUI.BeginChangeCheck();
            Rect tRect = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
            SerializedProperty tEffectName = property.FindPropertyRelative("EffectName");
            string tValue = tEffectName.stringValue;
            int tIndex = STSEffectType.kEffectNameList.IndexOf(tValue);
            if (tIndex < 0 || tIndex >= STSEffectType.kEffectNameList.Count())
            {
                tIndex = 0;
            }
            int tIndexNew = EditorGUI.Popup(tRect, new GUIContent(property.displayName), tIndex, STSEffectType.kEffectContentList.ToArray());
            if (EditorGUI.EndChangeCheck() == true)
            {
                if (tIndexNew != tIndex)
                {
                    tValue = STSEffectType.kEffectNameList[tIndexNew];
                    tEffectName.stringValue = tValue;
                    tEffectName.serializedObject.ApplyModifiedProperties();
                    rSmallPreview = null;
                    rBigPreview = null;
                }
                tAutoInstallPreview = true;
            }
            tY += tPopupFieldStyle.fixedHeight + kMarge;
            //bool tNewReturn = false;
            if (rSmallPreview == null)
            {
                string tName = tEffectName.stringValue;
                int tIndexDD = STSEffectType.kEffectNameList.IndexOf(tName);
                if (tIndexDD < 0 || tIndexDD >= STSEffectType.kEffectNameList.Count())
                {
                    rSmallPreview = new STSEffectFade();
                }
                else
                {
                    Type tEffectTypeDD = STSEffectType.kEffectTypeList[tIndexDD];
                    rSmallPreview = (STSEffect)Activator.CreateInstance(tEffectTypeDD);
                }
                //tNewReturn = true;
            }
            if (rBigPreview == null)
            {
                string tName = tEffectName.stringValue;
                int tIndexDD = STSEffectType.kEffectNameList.IndexOf(tName);
                if (tIndexDD < 0 || tIndexDD >= STSEffectType.kEffectNameList.Count())
                {
                    rBigPreview = new STSEffectFade();
                }
                else
                {
                    Type tEffectTypeDD = STSEffectType.kEffectTypeList[tIndexDD];
                    rBigPreview = (STSEffect)Activator.CreateInstance(tEffectTypeDD);
                }
                //tNewReturn = true;
            }
            Type tEffectType = STSEffectType.kEffectTypeList[tIndexNew];

            EditorGUI.indentLevel++;

            // Draw Parameter;

            EditorGUI.BeginChangeCheck();

            // Primary Tint
            if (tEffectType.GetCustomAttributes(typeof(STSTintPrimaryAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSTintPrimaryAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSTintPrimaryAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectTintPrimary = new Rect(position.x, tY, position.width, tColorFieldStyle.fixedHeight);
                SerializedProperty tTintPrimary = property.FindPropertyRelative("TintPrimary");
                EditorGUI.PropertyField(tRectTintPrimary, tTintPrimary, tEntitlement, false);
                tY += tColorFieldStyle.fixedHeight + kMarge;
                rSmallPreview.TintPrimary = tTintPrimary.colorValue;
                rBigPreview.TintPrimary = tTintPrimary.colorValue;
            }

            // Secondary Tint
            if (tEffectType.GetCustomAttributes(typeof(STSTintSecondaryAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSTintSecondaryAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSTintSecondaryAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectTintSecondary = new Rect(position.x, tY, position.width, tColorFieldStyle.fixedHeight);
                SerializedProperty tTintSecondary = property.FindPropertyRelative("TintSecondary");
                EditorGUI.PropertyField(tRectTintSecondary, tTintSecondary, tEntitlement, false);
                tY += tColorFieldStyle.fixedHeight + kMarge;
                rSmallPreview.TintSecondary = tTintSecondary.colorValue;
                rBigPreview.TintSecondary = tTintSecondary.colorValue;
            }

            // Primary Texture
            if (tEffectType.GetCustomAttributes(typeof(STSTexturePrimaryAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSTexturePrimaryAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSTexturePrimaryAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectTexturePrimary = new Rect(position.x, tY, position.width, tObjectFieldStyle.fixedHeight);
                SerializedProperty tTexturePrimary = property.FindPropertyRelative("TexturePrimary");
                EditorGUI.PropertyField(tRectTexturePrimary, tTexturePrimary, tEntitlement, false);
                tY += tObjectFieldStyle.fixedHeight + kMarge;
                rSmallPreview.TexturePrimary = (Texture2D)tTexturePrimary.objectReferenceValue;
                rBigPreview.TexturePrimary = (Texture2D)tTexturePrimary.objectReferenceValue;
            }

            // Secondary Texture
            if (tEffectType.GetCustomAttributes(typeof(STSTextureSecondaryAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSTextureSecondaryAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSTextureSecondaryAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectTextureSecondary = new Rect(position.x, tY, position.width, tObjectFieldStyle.fixedHeight);
                SerializedProperty tTextureSecondary = property.FindPropertyRelative("TextureSecondary");
                EditorGUI.PropertyField(tRectTextureSecondary, tTextureSecondary, tEntitlement, false);
                tY += tObjectFieldStyle.fixedHeight + kMarge;
                rSmallPreview.TextureSecondary = (Texture2D)tTextureSecondary.objectReferenceValue;
                rBigPreview.TextureSecondary = (Texture2D)tTextureSecondary.objectReferenceValue;
            }

            // Secondary Texture
            if (tEffectType.GetCustomAttributes(typeof(STSAnimationCurveAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = new GUIContent("Animation curve");
                //foreach (STSAnimationCurveAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSAnimationCurveAttribute), true))
                //{
                //    tEntitlement = new GUIContent(tAtt.Entitlement);
                //}
                Rect tRectCurve = new Rect(position.x, tY, position.width, tObjectFieldStyle.fixedHeight);
                SerializedProperty tCurve = property.FindPropertyRelative("Curve");
                EditorGUI.PropertyField(tRectCurve, tCurve, tEntitlement, false);
                tY += tObjectFieldStyle.fixedHeight + kMarge;
                AnimationCurve tCurveAnim = (AnimationCurve)tCurve.animationCurveValue;
                rSmallPreview.Curve = new AnimationCurve(tCurveAnim.keys);
                rBigPreview.Curve = new AnimationCurve(tCurveAnim.keys);
            }

            // Parameter One
            if (tEffectType.GetCustomAttributes(typeof(STSParameterOneAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                bool tSlider = false;
                int tSliderMin = 0;
                int tSliderMax = 0;
                foreach (STSParameterOneAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSParameterOneAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                    tSlider = tAtt.Slider;
                    tSliderMin = tAtt.Min;
                    tSliderMax = tAtt.Max;
                }
                if (tSlider == true)
                {
                    Rect tRectParameterOne = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                    SerializedProperty tParameterOne = property.FindPropertyRelative("ParameterOne");
                    if (tParameterOne.intValue > tSliderMax)
                    {
                        tParameterOne.intValue = tSliderMax;
                    }
                    if (tParameterOne.intValue < tSliderMin)
                    {
                        tParameterOne.intValue = tSliderMin;
                    }
                    EditorGUI.IntSlider(tRectParameterOne, tParameterOne, tSliderMin, tSliderMax, tEntitlement);
                    tY += tPopupFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterOne = tParameterOne.intValue;
                    rBigPreview.ParameterOne = tParameterOne.intValue;
                }
                else
                {
                    Rect tRectParameterOne = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
                    SerializedProperty tParameterOne = property.FindPropertyRelative("ParameterOne");
                    EditorGUI.PropertyField(tRectParameterOne, tParameterOne, tEntitlement, false);
                    tY += tNumberFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterOne = tParameterOne.intValue;
                    rBigPreview.ParameterOne = tParameterOne.intValue;
                }
            }

            // Parameter Two
            if (tEffectType.GetCustomAttributes(typeof(STSParameterTwoAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                bool tSlider = false;
                int tSliderMin = 0;
                int tSliderMax = 0;
                foreach (STSParameterTwoAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSParameterTwoAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                    tSlider = tAtt.Slider;
                    tSliderMin = tAtt.Min;
                    tSliderMax = tAtt.Max;
                }
                if (tSlider == true)
                {
                    Rect tRectParameterTwo = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                    SerializedProperty tParameterTwo = property.FindPropertyRelative("ParameterTwo");
                    if (tParameterTwo.intValue > tSliderMax)
                    {
                        tParameterTwo.intValue = tSliderMax;
                    }
                    if (tParameterTwo.intValue < tSliderMin)
                    {
                        tParameterTwo.intValue = tSliderMin;
                    }
                    EditorGUI.IntSlider(tRectParameterTwo, tParameterTwo, tSliderMin, tSliderMax, tEntitlement);
                    tY += tPopupFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterTwo = tParameterTwo.intValue;
                    rBigPreview.ParameterTwo = tParameterTwo.intValue;
                }
                else
                {
                    Rect tRectParameterTwo = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
                    SerializedProperty tParameterTwo = property.FindPropertyRelative("ParameterTwo");
                    EditorGUI.PropertyField(tRectParameterTwo, tParameterTwo, tEntitlement, false);
                    tY += tNumberFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterTwo = tParameterTwo.intValue;
                    rBigPreview.ParameterTwo = tParameterTwo.intValue;
                }
            }

            // Parameter Three
            if (tEffectType.GetCustomAttributes(typeof(STSParameterThreeAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                bool tSlider = false;
                int tSliderMin = 0;
                int tSliderMax = 0;
                foreach (STSParameterThreeAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSParameterThreeAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                    tSlider = tAtt.Slider;
                    tSliderMin = tAtt.Min;
                    tSliderMax = tAtt.Max;
                }
                if (tSlider == true)
                {
                    Rect tRectParameterThree = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                    SerializedProperty tParameterThree = property.FindPropertyRelative("ParameterThree");
                    if (tParameterThree.intValue > tSliderMax)
                    {
                        tParameterThree.intValue = tSliderMax;
                    }
                    if (tParameterThree.intValue < tSliderMin)
                    {
                        tParameterThree.intValue = tSliderMin;
                    }
                    EditorGUI.IntSlider(tRectParameterThree, tParameterThree, tSliderMin, tSliderMax, tEntitlement);
                    tY += tPopupFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterThree = tParameterThree.intValue;
                    rBigPreview.ParameterThree = tParameterThree.intValue;
                }
                else
                {
                    Rect tRectParameterThree = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
                    SerializedProperty tParameterThree = property.FindPropertyRelative("ParameterThree");
                    EditorGUI.PropertyField(tRectParameterThree, tParameterThree, tEntitlement, false);
                    tY += tNumberFieldStyle.fixedHeight + kMarge;
                    rSmallPreview.ParameterThree = tParameterThree.intValue;
                    rBigPreview.ParameterThree = tParameterThree.intValue;
                }
            }

            // Offset
            if (tEffectType.GetCustomAttributes(typeof(STSOffsetAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSOffsetAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSOffsetAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectOffset = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
                SerializedProperty tOffset = property.FindPropertyRelative("Offset");
                EditorGUI.PropertyField(tRectOffset, tOffset, tEntitlement, false);
                tY += tNumberFieldStyle.fixedHeight + kMarge;
                rSmallPreview.Offset = tOffset.vector2Value;
                rBigPreview.Offset = tOffset.vector2Value;
            }

            // TwoCross
            if (tEffectType.GetCustomAttributes(typeof(STSTwoCrossAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSTwoCrossAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSTwoCrossAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectTwoCross = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tTwoCross = property.FindPropertyRelative("TwoCross");
                EditorGUI.PropertyField(tRectTwoCross, tTwoCross, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.TwoCross = (STSTwoCross)tTwoCross.intValue;
                rBigPreview.TwoCross = (STSTwoCross)tTwoCross.intValue;
            }

            // FourCross
            if (tEffectType.GetCustomAttributes(typeof(STSFourCrossAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSFourCrossAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSFourCrossAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectFourCross = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tFourCross = property.FindPropertyRelative("FourCross");
                EditorGUI.PropertyField(tRectFourCross, tFourCross, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.FourCross = (STSFourCross)tFourCross.intValue;
                rBigPreview.FourCross = (STSFourCross)tFourCross.intValue;
            }

            // FiveCross
            if (tEffectType.GetCustomAttributes(typeof(STSFiveCrossAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSFiveCrossAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSFiveCrossAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectFiveCross = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tFiveCross = property.FindPropertyRelative("FiveCross");
                EditorGUI.PropertyField(tRectFiveCross, tFiveCross, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.FiveCross = (STSFiveCross)tFiveCross.intValue;
                rBigPreview.FiveCross = (STSFiveCross)tFiveCross.intValue;
            }

            // EightCross
            if (tEffectType.GetCustomAttributes(typeof(STSEightCrossAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSEightCrossAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSEightCrossAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectEightCross = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tEightCross = property.FindPropertyRelative("EightCross");
                EditorGUI.PropertyField(tRectEightCross, tEightCross, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.EightCross = (STSEightCross)tEightCross.intValue;
                rBigPreview.EightCross = (STSEightCross)tEightCross.intValue;
            }

            // NineCross
            if (tEffectType.GetCustomAttributes(typeof(STSNineCrossAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSNineCrossAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSNineCrossAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectNineCross = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tNineCross = property.FindPropertyRelative("NineCross");
                EditorGUI.PropertyField(tRectNineCross, tNineCross, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.NineCross = (STSNineCross)tNineCross.intValue;
                rBigPreview.NineCross = (STSNineCross)tNineCross.intValue;
            }

            // Clockwise
            if (tEffectType.GetCustomAttributes(typeof(STSClockwiseAttribute), true).Length > 0)
            {
                GUIContent tEntitlement = null;
                foreach (STSClockwiseAttribute tAtt in tEffectType.GetCustomAttributes(typeof(STSClockwiseAttribute), true))
                {
                    tEntitlement = new GUIContent(tAtt.Entitlement);
                }
                Rect tRectClockwise = new Rect(position.x, tY, position.width, tPopupFieldStyle.fixedHeight);
                SerializedProperty tClockwise = property.FindPropertyRelative("Clockwise");
                EditorGUI.PropertyField(tRectClockwise, tClockwise, tEntitlement, false);
                tY += tPopupFieldStyle.fixedHeight + kMarge;
                rSmallPreview.Clockwise = (STSClockwise)tClockwise.intValue;
                rBigPreview.Clockwise = (STSClockwise)tClockwise.intValue;
            }

            if (EditorGUI.EndChangeCheck() == true)
            {
                //tNewReturn = true;
                tAutoInstallPreview = true;
                tAutoPreparePreview = true;
            }
            EditorGUI.BeginChangeCheck();
            // Duration
            Rect tRectDuration = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
            SerializedProperty tDuration = property.FindPropertyRelative("Duration");
            //EditorGUI.PropertyField(tRectDuration, tDuration, false);
            EditorGUI.Slider(tRectDuration, tDuration, 0.1F, 10.0F);
            tY += tNumberFieldStyle.fixedHeight + kMarge;
            rBigPreview.Duration = tDuration.floatValue;

            // Purcent
            Rect tRectPurcent = new Rect(position.x, tY, position.width, tNumberFieldStyle.fixedHeight);
            SerializedProperty tPurcent = property.FindPropertyRelative("Purcent");
            EditorGUI.Slider(tRectPurcent, tPurcent, 0.0F, 1.0F, new GUIContent("Preview"));
            tY += tNumberFieldStyle.fixedHeight + kMarge;
            rSmallPreview.Purcent = tPurcent.floatValue;
            rBigPreview.Purcent = tPurcent.floatValue;

            // Finish sub layput
            EditorGUI.indentLevel--;

            // Select the small background
            SelectedPreview = EditorGUI.IntPopup(new Rect(position.x + position.width - kSizePreviewPopup - kMarge - kSizePreview * 2, tY, kSizePreviewPopup, tPopupFieldStyle.fixedHeight),
            SelectedPreview, new string[] { "A", "B", "C", "D", "…" }, new int[] { 0, 1, 2, 3, 999 });

            if (EditorGUI.EndChangeCheck() == true)
            {
                //tNewReturn = true;
                tAutoInstallPreview = true;
            }

            Rect tPreviewRect = new Rect(position.x + position.width - kSizePreview * 2, tY, kSizePreview * 2, kSizePreview);

            //If rect change redraw effect
            if (OldRect.y != tPreviewRect.y || OldRect.width != tPreviewRect.width || OldRect.x != tPreviewRect.x)
            {
                rSmallPreview.PrepareEffectExit(tPreviewRect);
            }
            OldRect = tPreviewRect;

            // button for big preview
            if (GUI.Button(new Rect(position.x + position.width - kSizePreviewButton - kMarge - kSizePreview * 2, tY + tPopupFieldStyle.fixedHeight + kMarge, kSizePreviewButton, tPopupFieldStyle.fixedHeight), STSConstants.K_SHOW_BIG_PREVIEW, tMiniButtonStyle))
            {
                STSEffectPreview.EffectPreviewShow();
                STSEffectPreview.kEffectPreview.SetEffect(rBigPreview);
                STSEffectPreview.kEffectPreview.EffectPrepare();
            }
            // button to run in big preview
            if (GUI.Button(new Rect(position.x + position.width - kSizePreviewButton - kMarge - kSizePreview * 2, tY + 2 * (tPopupFieldStyle.fixedHeight + kMarge), kSizePreviewButton, tPopupFieldStyle.fixedHeight), STSConstants.K_RUN_BIG_PREVIEW, tMiniButtonStyle))
            {
                STSEffectPreview.EffectPreviewShow();
                STSEffectPreview.kEffectPreview.SetEffect(rBigPreview);
                STSEffectPreview.kEffectPreview.EffectPrepare();
                STSEffectPreview.kEffectPreview.EffectRun(tDuration.floatValue);
            }

            if (string.IsNullOrEmpty(STSConstants.K_ASSET_STORE_URL) == false)
            {
                // Get More effects
                Color tOldColor = GUI.contentColor;
                GUI.backgroundColor = Color.red;
                if (GUI.Button(new Rect(position.x + position.width - kSizePreviewButton - kMarge - kSizePreview * 2, tY + 3 * (tPopupFieldStyle.fixedHeight + kMarge), kSizePreviewButton, tPopupFieldStyle.fixedHeight), STSConstants.K_ASSET_STORE, tMiniButtonStyle))
                {
                    //AssetStore.Open(STSConstants.K_ASSET_STORE_URL);
                    Application.OpenURL(STSConstants.K_ASSET_STORE_URL);
                }
                GUI.backgroundColor = tOldColor;
            }

            // If AutoInstallPreview?
            if (tAutoInstallPreview == true)
            {
                rSmallPreview.PrepareEffectExit(tPreviewRect);
                if (STSEffectPreview.kEffectPreview != null)
                {
                    STSEffectPreview.kEffectPreview.SetEffect(rBigPreview);
                }
            }
            if (tAutoPreparePreview == true)
            {
                if (STSEffectPreview.kEffectPreview != null)
                {
                    STSEffectPreview.kEffectPreview.EffectPrepare();
                }
            }
            // Draw big preview 
            if (STSEffectPreview.kEffectPreview != null)
            {
                if (tEffectType.GetCustomAttributes(typeof(STSNoBigPreviewAttribute), true).Length == 0)
                {
                    STSEffectPreview.kEffectPreview.NoPreview = false;
                    STSEffectPreview.kEffectPreview.Repaint();
                }
                else
                {
                    STSEffectPreview.kEffectPreview.NoPreview = true;
                }
            }
            //Draw local small preview
            if (SelectedPreview == 0)
            {
                GUI.DrawTexture(tPreviewRect, kImagePreviewA);
            }
            else if (SelectedPreview == 1)
            {
                GUI.DrawTexture(tPreviewRect, kImagePreviewB);
            }
            else if (SelectedPreview == 2)
            {
                GUI.DrawTexture(tPreviewRect, kImagePreviewC);
            }
            else if (SelectedPreview == 3)
            {
                GUI.DrawTexture(tPreviewRect, kImagePreviewD);
            }
            else
            {
                STSDrawQuad.DrawRect(tPreviewRect, Color.white);
                STSDrawCircle.DrawCircle(tPreviewRect.center, tPreviewRect.height / 2.0F, 64, Color.black);
            }
            // TODO : Add image in background
            // draw preview

            if (tEffectType.GetCustomAttributes(typeof(STSNoSmallPreviewAttribute), true).Length == 0)
            {

                rSmallPreview.EstimateCurvePurcent();
                rSmallPreview.Draw(tPreviewRect);
            }
            else
            {
                GUI.Label(tPreviewRect, new GUIContent(STSConstants.K_NO_LITTLE_PREVIEW), tNoPreviewFieldStyle);
            }


            EditorGUI.EndProperty();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================

#endif