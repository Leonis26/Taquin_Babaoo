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
    [STSEffectName("Octo/Octo Random")]
    [STSNoSmallPreview]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSParameterOne("Line Number", 1, 30)]
    [STSParameterTwo("Column Number", 1, 30)]
    // ***
    public class STSEffectOctoRandom : STSEffect
    {
        //-------------------------------------------------------------------------------------------------------------
        private STSMatrix Matrix;
        //-------------------------------------------------------------------------------------------------------------
        public void Prepare(Rect sRect)
        {
            //Debug.Log("STSEffectFadeLine Prepare()");
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
            Matrix.ShuffleList();
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void PrepareEffectEnter(Rect sRect)
        {
            //Debug.Log("STSEffectFadeLine PrepareEffectEnter()");
            // Prepare your datas to draw
            Prepare(sRect);
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void PrepareEffectExit(Rect sRect)
        {
            //Debug.Log("STSEffectFadeLine PrepareEffectExit()");
            // Prepare your datas to draw
            Prepare(sRect);
        }
        //-------------------------------------------------------------------------------------------------------------
        public override void Draw(Rect sRect)
        {
            //STSBenchmark.Start();
            if (Purcent > 0)
            {
                //Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, Purcent);
                int tIndex = (int)Mathf.Floor(Purcent * Matrix.TileCount);
                //Debug.Log("tIndex = " + tIndex + " on TileCount) = "+TileCount);
                // draw all fill tiles
                for (int i = 0; i < tIndex; i++)
                {
                    STSTile tTile = Matrix.TilesList[i];
                    //STSTransitionDrawing.DrawRect(tTile.Rectangle, TintPrimary);
                    STSDrawCircle.DrawCircle(tTile.Rectangle.center, tTile.Rectangle.width, 2, TintPrimary);
                }
                // Draw Alpha tile
                if (tIndex < Matrix.TileCount)
                {
                    STSTile tTileAlpha = Matrix.TilesList[tIndex];
                    float tAlpha = (Purcent * Matrix.TileCount) - (float)tIndex;
                    //Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, tAlpha);
                    //Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tAlpha*TintPrimary.a);
                    //STSTransitionDrawing.DrawRect(tTileAlpha.Rectangle, tFadeColorAlpha);

                    STSDrawCircle.DrawCircle(tTileAlpha.Rectangle.center, tTileAlpha.Rectangle.width * tAlpha, 2, TintPrimary);
                }
            }
            //STSBenchmark.Finish();
        }
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================