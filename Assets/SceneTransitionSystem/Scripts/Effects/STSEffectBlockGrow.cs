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
    /// <summary>
    /// 
    /// </summary>
    [STSEffectName("Block Grow")]
    // *** Active some parameters in inspector
    [STSTintPrimary()]
    [STSParameterOne("Line Number", 1, 30)]
    [STSParameterTwo("Column Number", 1, 30)]
    [STSNineCross("Block From")]
    [STSClockwise("Block Direction")]
    // ***
    public class STSEffectBlockGrow : STSEffect
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
                int tLine = 0;
                int tColumn = 0;
                switch (FiveCross)
                {
                    case STSFiveCross.Left:
                        {
                            for (int i = 0; i < tIndex; i++)
                            {
                                tColumn = (int)Mathf.Floor((float)i / (float)ParameterOne);
                                tLine = (int)((float)i % ((float)ParameterOne));
                                //Debug.Log("index = "+i+"/"+tIndex+"/ "+TileCount+" ---> loop tLine ="+tLine +" tColumn = " +tColumn);
                                STSTile tTile = Matrix.GetTile(tLine, tColumn);
                                STSDrawQuad.DrawRect(tTile.Rectangle, TintPrimary);
                            }
                            // Draw Alpha tile
                            if (tIndex < Matrix.TileCount)
                            {
                                tColumn = (int)Mathf.Floor((float)tIndex / (float)ParameterOne);
                                tLine = (int)((float)tIndex % ((float)ParameterOne));
                                //Debug.Log("index = " + tIndex + "/" + tIndex + "/ " + TileCount + " ---> loop tLineAlpha =" + tLineAlpha + " tColumnAlpha = " + tColumnAlpha);
                                STSTile tTileAlpha = Matrix.GetTile(tLine, tColumn);
                                float tAlpha = (Purcent * Matrix.TileCount) - (float)tIndex;
                                //Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, tAlpha);
                                Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tAlpha);
                                STSDrawQuad.DrawRect(tTileAlpha.Rectangle, tFadeColorAlpha);
                            }
                        }
                        break;
                    case STSFiveCross.Right:
                        {
                            for (int i = 0; i < tIndex; i++)
                            {
                                tColumn = (int)Mathf.Ceil(ParameterTwo - (float)i / (float)ParameterOne) - 1;
                                tLine = (int)((float)i % ((float)ParameterOne));
                                //Debug.Log("index = "+i+"/"+tIndex+"/ "+TileCount+" ---> loop tLine ="+tLine +" tColumn = " +tColumn);
                                STSTile tTile = Matrix.GetTile(tLine, tColumn);
                                STSDrawQuad.DrawRect(tTile.Rectangle, TintPrimary);
                            }
                            // Draw Alpha tile
                            if (tIndex < Matrix.TileCount)
                            {
                                tColumn = (int)Mathf.Ceil(ParameterTwo - (float)tIndex / (float)ParameterOne) - 1;
                                tLine = (int)((float)tIndex % ((float)ParameterOne));
                                //Debug.Log("index = " + tIndex + "/" + tIndex + "/ " + TileCount + " ---> loop tLineAlpha =" + tLineAlpha + " tColumnAlpha = " + tColumnAlpha);
                                STSTile tTileAlpha = Matrix.GetTile(tLine, tColumn);
                                float tAlpha = (Purcent * Matrix.TileCount) - (float)tIndex;
                                //Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, tAlpha);
                                Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tAlpha);
                                STSDrawQuad.DrawRect(tTileAlpha.Rectangle, tFadeColorAlpha);
                            }
                        }
                        break;
                    case STSFiveCross.Top:
                        {
                            for (int i = 0; i < tIndex; i++)
                            {
                                tLine = (int)Mathf.Floor((float)i / (float)ParameterTwo);
                                tColumn = (int)((float)i % ((float)ParameterTwo));
                                //Debug.Log("index = "+i+"/"+tIndex+"/ "+TileCount+" ---> loop tLine ="+tLine +" tColumn = " +tColumn);
                                STSTile tTile = Matrix.GetTile(tLine, tColumn);
                                STSDrawQuad.DrawRect(tTile.Rectangle, TintPrimary);
                            }
                            // Draw Alpha tile
                            if (tIndex < Matrix.TileCount)
                            {
                                tLine = (int)Mathf.Floor((float)tIndex / (float)ParameterTwo);
                                tColumn = (int)((float)tIndex % ((float)ParameterTwo));
                                //Debug.Log("index = " + tIndex + "/" + tIndex + "/ " + TileCount + " ---> loop tLineAlpha =" + tLineAlpha + " tColumnAlpha = " + tColumnAlpha);
                                STSTile tTileAlpha = Matrix.GetTile(tLine, tColumn);
                                float tAlpha = (Purcent * Matrix.TileCount) - (float)tIndex;
                                //Color tColorLerp = Color.Lerp(TintSecondary, TintPrimary, tAlpha);
                                Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tAlpha);
                                STSDrawQuad.DrawRect(tTileAlpha.Rectangle, tFadeColorAlpha);
                            }
                        }
                        break;
                    case STSFiveCross.Bottom:
                        {
                            for (int i = 0; i < tIndex; i++)
                            {
                                tLine = (int)Mathf.Ceil(ParameterOne - (float)i / (float)ParameterTwo) - 1;
                                tColumn = (int)((float)i % ((float)ParameterTwo));
                                //Debug.Log("index = "+i+"/"+tIndex+"/ "+TileCount+" ---> loop tLine ="+tLine +" tColumn = " +tColumn);
                                STSTile tTile = Matrix.GetTile(tLine, tColumn);
                                STSDrawQuad.DrawRect(tTile.Rectangle, TintPrimary);
                            }
                            // Draw Alpha tile
                            if (tIndex < Matrix.TileCount)
                            {
                                tLine = (int)Mathf.Ceil(ParameterOne - (float)tIndex / (float)ParameterTwo) - 1;
                                tColumn = (int)((float)tIndex % ((float)ParameterTwo));
                                //Debug.Log("index = " + tIndex + "/" + tIndex + "/ " + TileCount + " ---> loop tLineAlpha =" + tLine + " tColumnAlpha = " + tColumn);
                                STSTile tTileAlpha = Matrix.GetTile(tLine, tColumn);
                                float tAlpha = (Purcent * Matrix.TileCount) - (float)tIndex;
                                Color tFadeColorAlpha = new Color(TintPrimary.r, TintPrimary.g, TintPrimary.b, tAlpha * TintPrimary.a);
                                STSDrawQuad.DrawRect(tTileAlpha.Rectangle, tFadeColorAlpha);
                            }
                        }
                        break;
                }
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
