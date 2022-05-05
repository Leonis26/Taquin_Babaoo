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
    [STSEffectNameAttribute("Checkout")]
    // *** Remove some parameters in inspector
    // No remove
    // ***
    public class STSEffectCheckout : STSEffect
    {
        //-------------------------------------------------------------------------------------------------------------
        public void Prepare(Rect sRect)
        {
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void PrepareEffectEnter(Rect sRect)
        {
            // Prepare your datas to draw
            Prepare(sRect);
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void PrepareEffectExit(Rect sRect)
        {
            // Prepare your datas to draw
            Prepare(sRect);
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Draw(Rect sRect)
        {
            // Do drawing with purcent
            float tWidth = sRect.width * Purcent;
            float tHeight = sRect.height * Purcent;
            int tRadius = (int)Mathf.Max(tWidth, tHeight);
            //float tX = sRect.position.x + (sRect.width - tWidth) / 2.0F;
            //float tY = sRect.position.y + (sRect.height - tHeight) / 2.0F;
            STSDrawing.DrawCircle(sRect.center, tRadius, TintPrimary, tWidth, ParameterOne);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================