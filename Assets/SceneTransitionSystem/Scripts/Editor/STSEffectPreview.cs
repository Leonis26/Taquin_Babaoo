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
using UnityEngine;
using UnityEditor;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSEffectPreview : EditorWindow
    {
        //-------------------------------------------------------------------------------------------------------------
        //[MenuItem(NWDConstants.K_MENU_ENVIRONMENT_SYNC, false, 62)]
        //-------------------------------------------------------------------------------------------------------------
        STSEffect Effect = null;
        float Duration = 1.0F;
        bool IsPlaying = false;
        float Delta = 0.0F;
        Texture2D Background = null;
        public bool NoPreview = false;
        static GUIStyle tNoPreviewFieldStyle;
        int SelectedPreview = 0;
        Rect LastPosition;
        //-------------------------------------------------------------------------------------------------------------
        public static STSEffectPreview kEffectPreview;
        //-------------------------------------------------------------------------------------------------------------
        public static STSEffectPreview EffectPreviewShow()
        {
            if (kEffectPreview == null)
            {
                kEffectPreview = EditorWindow.GetWindow(typeof(STSEffectPreview)) as STSEffectPreview;
            }
            kEffectPreview.ShowUtility();
            //kEffectPreview.Focus();
            return kEffectPreview;
        }
        //-------------------------------------------------------------------------------------------------------------
        private void OnDestroy()
        {
            kEffectPreview = null;
        }
        //-------------------------------------------------------------------------------------------------------------
        void Start()
        {
            //Debug.Log("Start");
            tNoPreviewFieldStyle = new GUIStyle(EditorStyles.boldLabel);
            tNoPreviewFieldStyle.alignment = TextAnchor.MiddleCenter;
            tNoPreviewFieldStyle.normal.textColor = Color.red;
            LastPosition = position;
        }
        //-------------------------------------------------------------------------------------------------------------
        double editorDeltaTime = 0f;
        double lastTimeSinceStartup = 0f;
        //-------------------------------------------------------------------------------------------------------------
        private void SetEditorDeltaTime()
        {
            if (lastTimeSinceStartup == 0f)
            {
                lastTimeSinceStartup = EditorApplication.timeSinceStartup;
            }
            editorDeltaTime = EditorApplication.timeSinceStartup - lastTimeSinceStartup;
            lastTimeSinceStartup = EditorApplication.timeSinceStartup;
        }
        //-------------------------------------------------------------------------------------------------------------
        void Update()
        {
            //Debug.Log("Update");
            if (IsPlaying == true)
            {
                SetEditorDeltaTime();
                Delta += (float)editorDeltaTime;
                //Debug.Log("Update IsPlaying == true Delta = " + Delta.ToString("F3") + "  /" + Duration.ToString("F3"));
                if (Delta <= Duration)
                {
                    if (Effect != null)
                    {
                        Effect.EstimatePurcent();
                        Repaint();
                        //Debug.Log("Update effect Purcent = " + Effect.Purcent.ToString("F3"));
                        // Repaint();
                    }
                    else
                    {
                        //Debug.Log("Update effect is null");
                        IsPlaying = false;
                    }
                }
                else
                {
                    //Debug.Log("Update play is finish");
                    IsPlaying = false;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckResize()
        {
            if (LastPosition.x != position.x || LastPosition.y != position.y || LastPosition.width != position.width || LastPosition.height != position.height)
            {
                LastPosition = position;
                EffectPrepare();
                Repaint();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void EffectPrepare()
        {
            if (IsPlaying == false)
            {
                if (Effect != null)
                {
                    Effect.PrepareEffectExit(new Rect(0, 0, position.width, position.height));
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void EffectRun(float sDuration)
        {
            //Debug.Log("EffectRun sDuration" + sDuration.ToString("F3"));
            if (Effect != null)
            {
                //Debug.Log("EffectRun is not null");
                Effect.Purcent = 0;
                Duration = sDuration;
                IsPlaying = true;
                Delta = 0.0F;
                //Debug.Log("Update IsPlaying == true Delta = " + Delta.ToString("F3") + "  /" + Duration.ToString("F3"));
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetEffect(STSEffect sEffect)
        {
            if (IsPlaying == false)
            {
                //Debug.Log("SetEffect");
                if (sEffect != Effect)
                {
                    Effect = sEffect;
                    EffectPrepare();
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetEffectPurcent(float sPurcent)
        {
            if (IsPlaying == false)
            {
                if (Effect != null)
                {
                    Effect.Purcent = sPurcent;
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        void OnGUI()
        {
            //Debug.Log("OnGUI");
            CheckResize();
            Rect ThisRect = new Rect(0, 0, position.width, position.height);
            if (Background == null)
            {
                //Background = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewA.png"));
                STSDrawQuad.DrawRect(ThisRect, Color.white);
                STSDrawCircle.DrawCircle(ThisRect.center, ThisRect.height / 2.0F, 64, Color.black);
            }
            if (Background != null)
            {
                GUI.DrawTexture(ThisRect, Background);
            }
            if (NoPreview == true)
            {
                GUI.Label(ThisRect, new GUIContent(STSConstants.K_NO_BIG_PREVIEW), tNoPreviewFieldStyle);
            }
            int tSelectedPreviewNew = EditorGUILayout.IntPopup(SelectedPreview, new string[] { "A", "B", "C", "D", "…" }, new int[] { 0, 1, 2, 3, 999 });
            if (tSelectedPreviewNew != SelectedPreview)
            {
                SelectedPreview = tSelectedPreviewNew;
                if (SelectedPreview == 0)
                {
                    Background = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewA.png"));
                }
                else if (SelectedPreview == 1)
                {
                    Background = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewB.png"));
                }
                else if (SelectedPreview == 2)
                {
                    Background = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewC.png"));
                }
                else if (SelectedPreview == 3)
                {
                    Background = AssetDatabase.LoadAssetAtPath<Texture2D>(STSFindPackage.PathOfPackage("/Scripts/Editor/Resources/STSPreviewD.png"));
                }
                else
                {
                    Background = null;
                }
            }
            if (Effect != null)
            {
                //Debug.Log("effect is drawinf with purcent " + Effect.Purcent);
                Effect.EstimateCurvePurcent();
                Effect.Draw(ThisRect);
            }
            else
            {
                //Debug.Log("effect is null");
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
