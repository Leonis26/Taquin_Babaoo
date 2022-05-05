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
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    [RequireComponent(typeof(Image), typeof(CanvasGroup))]
    [ExecuteInEditMode]
    public class STSScreenGauge : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------------------------------
        [Header("Connect images")]
        public Image ImageBackground;
        public Image ImageFill;
        public Image ImageOverLay;
        //-------------------------------------------------------------------------------------------------------------
        [Header("Expand zone")]
        public bool HorizontalExpand = true;
        public float HorizontalMin = 30.0f;
        public bool VerticalExpand = false;
        public float VerticalMin = 30.0f;
        //-------------------------------------------------------------------------------------------------------------
        [Header("Expand value")]
        [Range(0.0F, 1.0F)]
        public float HorizontalValue = 1.0F;
        [Range(0.0F, 1.0F)]
        public float VerticalValue = 1.0F;
        //-------------------------------------------------------------------------------------------------------------
        [Header("Animation")]
        public bool Smooth = true;
        public float Speed = 1.0F;
        public float SpeedHidden = 0.10F;
        private bool Hidden = false;
        private CanvasGroup Layer;
        float HorizontalValueInit = 0.0F;
        float VerticalValueInit = 0.0F;
        float HorizontalValueTarget = 1.0F;
        float VerticalValueTarget = 1.0F;
        float DeltaTimeCounter = 0.0F;
        //-------------------------------------------------------------------------------------------------------------
        public void SetHidden(bool sValue)
        {
            Hidden = sValue;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckHorizontalValue()
        {
            if (HorizontalValue > 1.0F)
            {
                HorizontalValue = 1.0F;
            }
            else if (HorizontalValue < 0)
            {
                HorizontalValue = 0.0F;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CheckVerticalValue()
        {
            if (VerticalValue > 1.0F)
            {
                VerticalValue = 1.0F;
            }
            else if (VerticalValue < 0)
            {
                VerticalValue = 0.0F;
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetHorizontalValue(float sHorizontalValue, bool sRegress = false)
        {
            if (sHorizontalValue < HorizontalValue && sRegress == false)
            {
                sHorizontalValue = HorizontalValue;
            }
            HorizontalValueInit = HorizontalValue;
            HorizontalValueTarget = sHorizontalValue;
            DeltaTimeCounter = 0.0F;
        }
        //-------------------------------------------------------------------------------------------------------------
        public void SetVerticalValue(float sVerticalValue, bool sRegress = false)
        {
            if (sVerticalValue < VerticalValue && sRegress == false)
            {
                sVerticalValue = VerticalValue;
            }
            VerticalValueInit = VerticalValue;
            VerticalValueTarget = sVerticalValue;
            DeltaTimeCounter = 0.0F;
        }
        //-------------------------------------------------------------------------------------------------------------
        void OnEnable()
        {
            Layer = gameObject.GetComponent<CanvasGroup>();
            ReDraw();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void Update()
        {
            if (Application.isPlaying == true)
            {
                if (Smooth == true)
                {
                    DeltaTimeCounter += Time.deltaTime * Speed;
                    HorizontalValue = Mathf.Lerp(HorizontalValueInit, HorizontalValueTarget, DeltaTimeCounter);
                    VerticalValue = Mathf.Lerp(VerticalValueInit, VerticalValueTarget, DeltaTimeCounter);
                    if (Hidden == true && Layer.alpha > 0.0F)
                    {
                        Layer.alpha -= Time.deltaTime * SpeedHidden;
                    }
                    else if (Hidden == false && Layer.alpha < 1.0F)
                    {
                        Layer.alpha += Time.deltaTime * SpeedHidden;
                    }
                }
                else
                {
                    HorizontalValue = HorizontalValueTarget;
                    VerticalValue = VerticalValueTarget;
                }
            }
            ReDraw();
        }
        //-------------------------------------------------------------------------------------------------------------
        void ReDraw()
        {
            if (ImageBackground != null)
            {
                CheckHorizontalValue();
                CheckVerticalValue();
                Rect tRect = ImageBackground.rectTransform.rect;
                float tW = tRect.width;
                if (HorizontalExpand)
                {
                    tW = HorizontalMin + (tRect.width - HorizontalMin) * HorizontalValue;
                }
                float tH = tRect.height;
                if (VerticalExpand)
                {
                    tH = VerticalMin + (tRect.height - VerticalMin) * VerticalValue;
                }
                ImageFill.rectTransform.sizeDelta = new Vector2(tW, tH);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
