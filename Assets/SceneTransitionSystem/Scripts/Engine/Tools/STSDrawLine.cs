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
    public class STSDrawLine
    {
        //-------------------------------------------------------------------------------------------------------------
        static Texture2D kTexture;
        static Material kMaterial;
        static string kShaderName = "UI/Default";
        //-------------------------------------------------------------------------------------------------------------
        static STSDrawLine()
        {
            Initialize();
        }
        //-------------------------------------------------------------------------------------------------------------
        static void Initialize()
        {
            if (kMaterial == null)
            {
                kMaterial = new Material(Shader.Find(kShaderName));
            }
            if (kTexture == null)
            {
                kTexture = new Texture2D(1, 1);
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public static void DrawLines(Vector2[] sPoints, Color sColor, float sWwidth, bool sAntiAlias)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterial.SetPass(0);
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
        public static void DrawLine(Vector2 sA, Vector2 sB, Color sColor, float sWidth, bool sAntiAlias)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterial.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.LINES);
                GL.Color(sColor);
                GL.Vertex3(sA.x, sA.y, 0);
                GL.Vertex3(sB.x, sB.y, 0);
                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================