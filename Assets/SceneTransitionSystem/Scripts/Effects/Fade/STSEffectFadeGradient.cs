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
    [STSEffectName("Fade/Fade gradient")]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSTintSecondary()]
    // ***
    public class STSEffectFadeGradient : STSEffect
    {
        //-------------------------------------------------------------------------------------------------------------
        public override void Draw(Rect sRect)
        {
            if (Purcent > 0)
            {
                // Do drawing with purcent
                Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, Purcent);
                Color tFadeColorAlpha = new Color(tColorLerp.r, tColorLerp.g, tColorLerp.b, Purcent * TintPrimary.a);
                STSDrawQuad.DrawRect(sRect, tFadeColorAlpha);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================