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
    [STSEffectName("Circle/Radiant Block")]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSTintSecondary()]
    [STSParameterOne("Line Number", 1, 30)]
    [STSParameterTwo("Column Number", 1, 30)]
    // ***
    public class STSEffectRadiantBlock : STSEffect
    {
        //-------------------------------------------------------------------------------------------------------------
        private STSMatrix Matrix;
        //-------------------------------------------------------------------------------------------------------------
        public void Prepare(Rect sRect)
        {
            if (ParameterOne < 1)
            {
                ParameterOne = 1;
            }
            if (ParameterTwo < 1)
            {
                ParameterTwo = 1;
            }
            Matrix = new STSMatrix();
            Matrix.CreateMatrix(ParameterOne, ParameterTwo, sRect);
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
            if (Purcent > 0)
            {
                float tPurcent = Purcent * 2;
                Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tPurcent * TintPrimary.a);
                Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, Purcent);

                foreach (STSTile tTile in Matrix.TilesList)
                {
                    STSDrawQuad.DrawRectCenterGradient(tTile.Rectangle, tFadeColorAlpha, tColorLerp);
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================