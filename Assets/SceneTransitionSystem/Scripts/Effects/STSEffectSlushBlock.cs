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
    [STSEffectName("Slush Block")]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSParameterOne("Line Number", 1, 30)]
    [STSParameterTwo("Column Number", 1, 30)]
    [STSClockwise()]
    // ***
    public class STSEffectSlushBlock : STSEffect
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
                float tWidthPurcent = Matrix.TilesList[0].Rectangle.width * Purcent;
                float tWidth = Matrix.TilesList[0].Rectangle.width;
                float tHeight = Matrix.TilesList[0].Rectangle.height;
                if (Clockwise == STSClockwise.Clockwise)
                {
                    foreach (STSTile tTile in Matrix.TilesList)
                    {
                        Vector2 tAa = new Vector2(tTile.Rectangle.x, tTile.Rectangle.y);
                        Vector2 tAb = new Vector2(tTile.Rectangle.x, tTile.Rectangle.y + tHeight);
                        Vector2 tAc = new Vector2(tTile.Rectangle.x + tWidthPurcent, tTile.Rectangle.y);

                        Vector2 tBa = new Vector2(tTile.Rectangle.x + tWidth, tTile.Rectangle.y);
                        Vector2 tBb = new Vector2(tTile.Rectangle.x + tWidth, tTile.Rectangle.y + tHeight);
                        Vector2 tBc = new Vector2(tTile.Rectangle.x + tWidth - tWidthPurcent, tTile.Rectangle.y + tHeight);

                        STSDrawTriangle.DrawTriangle(tAa, tAb, tAc, TintPrimary);
                        STSDrawTriangle.DrawTriangle(tBa, tBb, tBc, TintPrimary);
                    }
                }
                else
                {
                    foreach (STSTile tTile in Matrix.TilesList)
                    {
                        Vector2 tAa = new Vector2(tTile.Rectangle.x, tTile.Rectangle.y);
                        Vector2 tAb = new Vector2(tTile.Rectangle.x, tTile.Rectangle.y + tHeight);
                        Vector2 tAc = new Vector2(tTile.Rectangle.x + tWidthPurcent, tTile.Rectangle.y + tHeight);

                        Vector2 tBa = new Vector2(tTile.Rectangle.x + tWidth, tTile.Rectangle.y);
                        Vector2 tBb = new Vector2(tTile.Rectangle.x + tWidth, tTile.Rectangle.y + tHeight);
                        Vector2 tBc = new Vector2(tTile.Rectangle.x + tWidth - tWidthPurcent, tTile.Rectangle.y);

                        STSDrawTriangle.DrawTriangle(tAa, tAb, tAc, TintPrimary);
                        STSDrawTriangle.DrawTriangle(tBa, tBb, tBc, TintPrimary);
                    }
                }
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================