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
using System.Collections;
using System.Collections.Generic;
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSTile
    {
        //-------------------------------------------------------------------------------------------------------------
        public Rect Rectangle;
        public float Purcent;
        public float Speed;
        public float StartDelay;
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSMatrix
    {
        //-------------------------------------------------------------------------------------------------------------
        public STSTile[,] Matrix;
        public List<STSTile> TilesList;
        public float TileCount = 0;
        public int Line;
        public int Column;
        //-------------------------------------------------------------------------------------------------------------
        public void CreateMatrix(int sLine, int sColumn)
        {
            //STSBenchmark.Start();
            Matrix = new STSTile[sLine, sColumn];
            TilesList = new List<STSTile>();
            TileCount = 0;
            Line = sLine;
            Column = sColumn;
            for (int i = 0; i < sLine; i++)
            {
                for (int j = 0; j < sColumn; j++)
                {
                    STSTile tTile = new STSTile();
                    Matrix[i, j] = tTile;
                    TilesList.Add(tTile);
                    TileCount++;
                }
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CreateMatrix(int sLine, int sColumn, Rect sRect)
        {
            //STSBenchmark.Start();
            float tX = sRect.width / sColumn;
            float tY = sRect.height / sLine;
            Matrix = new STSTile[sLine, sColumn];
            TilesList = new List<STSTile>();
            TileCount = 0;
            Line = sLine;
            Column = sColumn;
            for (int i = 0; i < sLine; i++)
            {
                for (int j = 0; j < sColumn; j++)
                {
                    STSTile tTile = new STSTile();//GetTile(i, j);
                    tTile.Rectangle = new Rect(sRect.x + j * tX, sRect.y + i * tY, tX, tY);
                    Matrix[i, j] = tTile;
                    TilesList.Add(tTile);
                    TileCount++;
                }
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void CreateMatrix(int sLine, int sColumn, Rect sRect, float sStartDelayFactor)
        {
            //STSBenchmark.Start();
            float tX = sRect.width / sColumn;
            float tY = sRect.height / sLine;
            Matrix = new STSTile[sLine, sColumn];
            TilesList = new List<STSTile>();
            TileCount = 0;
            Line = sLine;
            Column = sColumn;
            for (int i = 0; i < sLine; i++)
            {
                for (int j = 0; j < sColumn; j++)
                {
                    STSTile tTile = new STSTile();//GetTile(i, j);
                    tTile.Rectangle = new Rect(i * tX, j * tY, tX, tY);
                    tTile.StartDelay = (i * sColumn + j) * sStartDelayFactor;
                    Matrix[i, j] = tTile;
                    TilesList.Add(tTile);
                    TileCount++;
                }
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public STSTile GetTile(int sLine, int sColumn)
        {
            //Debug.Log("sLine = " + sLine +" sColumn = " + sColumn);
            //Debug.Log("Line = " + Matrix.GetLength(0) + " Column = " + Matrix.GetLength(1));
            return Matrix[sLine, sColumn];
        }
        //-------------------------------------------------------------------------------------------------------------
        public void ShuffleList()
        {
            //STSBenchmark.Start();
            int tCount = TilesList.Count;
            for (int i = 0; i < tCount; i++)
            {
                STSTile tTile = TilesList[i];
                TilesList.Remove(tTile);
                TilesList.Insert(Random.Range(0, tCount - 1), tTile);
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OrderList(STSFourCross sDirection, STSClockwise sClosckwise)
        {
            //STSBenchmark.Start();
            switch (sDirection)
            {
                case STSFourCross.Bottom:
                    OrderList(STSNineCross.Bottom, sClosckwise);
                    break;
                case STSFourCross.Top:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSFourCross.Left:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSFourCross.Right:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OrderList(STSFiveCross sDirection, STSClockwise sClosckwise)
        {
            //STSBenchmark.Start();
            switch (sDirection)
            {
                case STSFiveCross.Bottom:
                    OrderList(STSNineCross.Bottom, sClosckwise);
                    break;
                case STSFiveCross.Top:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSFiveCross.Left:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSFiveCross.Right:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSFiveCross.Center:
                    OrderList(STSNineCross.Center, sClosckwise);
                    break;
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
        public void OrderList(STSEightCross sDirection, STSClockwise sClosckwise)
        {
            //STSBenchmark.Start();
            switch (sDirection)
            {
                case STSEightCross.Bottom:
                    OrderList(STSNineCross.Bottom, sClosckwise);
                    break;
                case STSEightCross.Top:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSEightCross.Left:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSEightCross.Right:
                    OrderList(STSNineCross.Top, sClosckwise);
                    break;
                case STSEightCross.TopLeft:
                    OrderList(STSNineCross.TopLeft, sClosckwise);
                    break;
                case STSEightCross.TopRight:
                    OrderList(STSNineCross.TopRight, sClosckwise);
                    break;
                case STSEightCross.BottomLeft:
                    OrderList(STSNineCross.BottomLeft, sClosckwise);
                    break;
                case STSEightCross.BottomRight:
                    OrderList(STSNineCross.BottomRight, sClosckwise);
                    break;
            }
            //STSBenchmark.Finish();
        }

        //-------------------------------------------------------------------------------------------------------------
        public void OrderList(STSNineCross sDirection, STSClockwise sClosckwise)
        {
            //STSBenchmark.Start();
            TilesList = new List<STSTile>();
            switch (sDirection)
            {
                case STSNineCross.Bottom:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.Top:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.Left:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.Right:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.Center:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.TopLeft:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.TopRight:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.BottomLeft:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
                case STSNineCross.BottomRight:
                    if (sClosckwise == STSClockwise.Clockwise)
                    {
                    }
                    else
                    {
                    }
                    break;
            }
            //STSBenchmark.Finish();
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================
