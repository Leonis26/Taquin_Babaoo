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
    [STSEffectName("Fade/Fade gradient curve")]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSTintSecondary()]
    [STSAnimationCurve()]
    // ***
    public class STSEffectCurveFadeGradient : STSEffect
    {
        //-------------------------------------------------------------------------------------------------------------
        public override void Draw(Rect sRect)
        {
            //STSBenchmark.Start();
            // Add curve percent calculate
            CurvePurcent = Curve.Evaluate(Purcent);
            if (Purcent > 0)
            {
                // Do drawing with purcent
                Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, CurvePurcent);
                Color tFadeColorAlpha = new Color(tColorLerp.r, tColorLerp.g, tColorLerp.b, CurvePurcent * TintPrimary.a);
                STSDrawQuad.DrawRect(sRect, tFadeColorAlpha);
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================