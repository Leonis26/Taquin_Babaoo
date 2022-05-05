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
    public class STSDrawTriangle
    {
        //-------------------------------------------------------------------------------------------------------------
        static Texture2D kTexture;
        static Material kMaterialUI;
        static string kShaderNameUI = "UI/Default";
        //-------------------------------------------------------------------------------------------------------------
        static STSDrawTriangle()
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
        public static void DrawTriangles(Vector2[] sPoints, Color sColor)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.TRIANGLES);
                GL.Color(sColor);
                foreach (Vector2 tV in sPoints)
                {
                    GL.Vertex3(tV.x, tV.y, 0);
                }
                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawTriangle(Vector2 sA, Vector2 sB, Vector2 sC, Color sColor)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.TRIANGLES);
                GL.Color(sColor);
                GL.Vertex3(sA.x, sA.y, 0);
                GL.Vertex3(sB.x, sB.y, 0);
                GL.Vertex3(sC.x, sC.y, 0);
                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================