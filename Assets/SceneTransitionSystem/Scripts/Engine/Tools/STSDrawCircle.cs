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
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
//=====================================================================================================================
namespace SceneTransitionSystem
{
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public class STSDrawCircle
    {
        //-------------------------------------------------------------------------------------------------------------
        static Texture2D kTexture;
        static Material kMaterialUI;
        static string kShaderNameUI = "UI/Default";
        //-------------------------------------------------------------------------------------------------------------
        static STSDrawCircle()
        {
            Initialize();
        }
        //-------------------------------------------------------------------------------------------------------------
        static void Initialize()
        {
            if (kMaterialUI == null)
            {
                kMaterialUI = new Material(Shader.Find(kShaderNameUI));
            }
            if (kTexture == null)
            {
                kTexture = new Texture2D(1, 1);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawCircle(Vector2 sCenter, float sRadius, uint sSegmentPerQuarter, Color sColor)
        {
            if (sSegmentPerQuarter < 1)
            {
                sSegmentPerQuarter = 1;
            }
            uint tTriangles = (sSegmentPerQuarter + 1) * 4 * 3;
            Vector2[] tList = new Vector2[tTriangles];
            // Create Circle points triangles around this center
            // Put in DrawTriangles methods
            int tCounter = 0;
            float tRadIncrement = Mathf.PI / (2.0F * (float)sSegmentPerQuarter);
            //Debug.Log("tRadIncrement " + tRadIncrement);
            //Debug.Log("cos " + Mathf.Cos(tRadIncrement));
            //Debug.Log("sin " + Mathf.Sin(tRadIncrement));
            // Add First Segment
            tList[tCounter++] = sCenter;
            tList[tCounter++] = new Vector2(sCenter.x + sRadius, sCenter.y);
            Vector2 tOriginalPoint = new Vector2(sCenter.x + Mathf.Cos(tRadIncrement) * sRadius, sCenter.y - Mathf.Sin(tRadIncrement) * sRadius);
            Vector2 tNextPoint = tOriginalPoint;
            tList[tCounter++] = tNextPoint;
            uint tSeg = (sSegmentPerQuarter * 4) - 1;
            for (int i = 1; i <= tSeg; i++)
            {
                float tR = tRadIncrement * i;
                //Debug.Log("tRadIncrement <" +i+">"+ tR.ToString());
                //Debug.Log("cos " + Mathf.Cos(tR));
                //Debug.Log("sin " + Mathf.Sin(tR));
                // Add next Segment
                tList[tCounter++] = sCenter;
                tList[tCounter++] = tNextPoint;
                tNextPoint = new Vector2(sCenter.x + Mathf.Cos(tR) * sRadius, sCenter.y - Mathf.Sin(tR) * sRadius);
                tList[tCounter++] = tNextPoint;
            }
            tList[tCounter++] = sCenter;
            tList[tCounter++] = tNextPoint;
            tList[tCounter++] = new Vector2(sCenter.x + sRadius, sCenter.y);


#if UNITY_EDITOR
            Initialize();
#endif
            GL.PushMatrix();
            kMaterialUI.SetPass(0);
            GL.LoadPixelMatrix();
            //GL.Viewport(sViewPortRect);

            GL.Begin(GL.TRIANGLES);
            GL.Color(sColor);
            foreach (Vector2 tV in tList)
            {
                GL.Vertex3(tV.x, tV.y, 0);
            }
            GL.End();
            GL.PopMatrix();
        }
        //-------------------------------------------------------------------------------------------------------------


        //        //-------------------------------------------------------------------------------------------------------------
        //        public static void DrawCircle(Vector2 sCenter, float sRadius, uint sSegmentPerQuarter, Color sColor, Rect sViewPortRect)
        //        {
        //            if (sSegmentPerQuarter < 1)
        //            {
        //                sSegmentPerQuarter = 1;
        //            }
        //            uint tTriangles = (sSegmentPerQuarter + 1) * 4 * 3;
        //            Vector2[] tList = new Vector2[tTriangles];
        //            // Create Circle points triangles around this center
        //            // Put in DrawTriangles methods
        //            int tCounter = 0;
        //            float tRadIncrement = Mathf.PI / (2.0F * (float)sSegmentPerQuarter);
        //            //Debug.Log("tRadIncrement " + tRadIncrement);
        //            //Debug.Log("cos " + Mathf.Cos(tRadIncrement));
        //            //Debug.Log("sin " + Mathf.Sin(tRadIncrement));
        //            // Add First Segment
        //            tList[tCounter++] = sCenter;
        //            tList[tCounter++] = new Vector2(sCenter.x + sRadius, sCenter.y);
        //            Vector2 tOriginalPoint = new Vector2(sCenter.x + Mathf.Cos(tRadIncrement) * sRadius, sCenter.y - Mathf.Sin(tRadIncrement) * sRadius);
        //            Vector2 tNextPoint = tOriginalPoint;
        //            tList[tCounter++] = tNextPoint;
        //            uint tSeg = (sSegmentPerQuarter * 4) - 1;
        //            for (int i = 1; i <= tSeg; i++)
        //            {
        //                float tR = tRadIncrement * i;
        //                //Debug.Log("tRadIncrement <" +i+">"+ tR.ToString());
        //                //Debug.Log("cos " + Mathf.Cos(tR));
        //                //Debug.Log("sin " + Mathf.Sin(tR));
        //                // Add next Segment
        //                tList[tCounter++] = sCenter;
        //                tList[tCounter++] = tNextPoint;
        //                tNextPoint = new Vector2(sCenter.x + Mathf.Cos(tR) * sRadius, sCenter.y - Mathf.Sin(tR) * sRadius);
        //                tList[tCounter++] = tNextPoint;
        //            }
        //            tList[tCounter++] = sCenter;
        //            tList[tCounter++] = tNextPoint;
        //            tList[tCounter++] = new Vector2(sCenter.x + sRadius, sCenter.y);


        //#if UNITY_EDITOR
        //            Initialize();
        //#endif
        //    GL.PushMatrix();
        //    kMaterialUI.SetPass(0);
        //    GL.LoadPixelMatrix();
        //    GL.Viewport(sViewPortRect);

        //    GL.Begin(GL.TRIANGLES);
        //    GL.Color(sColor);
        //    foreach (Vector2 tV in tList)
        //    {
        //        GL.Vertex3(tV.x, tV.y, 0);
        //    }
        //    GL.End();
        //    GL.PopMatrix();
        //    GL.Viewport(new Rect(0, 0, Screen.width, Screen.height));
        //}
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================