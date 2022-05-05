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
    public class STSDrawQuad
    {
        //-------------------------------------------------------------------------------------------------------------
        static Texture2D kTexture;
        static Material kMaterialUI;
        static string kShaderNameUI = "UI/Default";
        //-------------------------------------------------------------------------------------------------------------
        static STSDrawQuad()
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
        public static void DrawQuad(Vector2 sA, Vector2 sB, Vector2 sC, Vector2 sD, Color sColor)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                GL.Begin(GL.QUADS);
                GL.Color(sColor);
                GL.Vertex3(sA.x, sA.y, 0);
                GL.Vertex3(sB.x, sB.y, 0);
                GL.Vertex3(sC.x, sC.y, 0);
                GL.Vertex3(sD.x, sD.y, 0);
                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawRect(Rect sRect, Color sColor)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                // QUADS Method
                GL.Begin(GL.QUADS);
                GL.Color(sColor);
                /*A*/
                GL.Vertex3(sRect.x, sRect.y, 0);
                /*B*/
                GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
                /*C*/
                GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
                /*D*/
                GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);

                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawRectGradient(Rect sRect, Color sColorA, Color sColorB)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                // QUADS Method
                GL.Begin(GL.QUADS);
                GL.Color(sColorA);
                /*A*/
                GL.Vertex3(sRect.x, sRect.y, 0);
                /*B*/
                GL.Color(sColorA);
                GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
                /*C*/
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
                /*D*/
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);

                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawRectCenterGradient(Rect sRect, Color sColorA, Color sColorB)
        {
            if (Event.current.type.Equals(EventType.Repaint))
            {
#if UNITY_EDITOR
                Initialize();
#endif
                GL.PushMatrix();
                kMaterialUI.SetPass(0);
                GL.LoadPixelMatrix();
                // QUADS Method
                GL.Begin(GL.TRIANGLES);

                GL.Color(sColorA);
                GL.Vertex3(sRect.x, sRect.y, 0);
                GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width / 2.0F, sRect.y + sRect.height / 2.0F, 0);

                GL.Color(sColorA);
                GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
                GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width / 2.0F, sRect.y + sRect.height / 2.0F, 0);


                GL.Color(sColorA);
                GL.Vertex3(sRect.x, sRect.y, 0);
                GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width / 2.0F, sRect.y + sRect.height / 2.0F, 0);

                GL.Color(sColorA);
                GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
                GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
                GL.Color(sColorB);
                GL.Vertex3(sRect.x + sRect.width / 2.0F, sRect.y + sRect.height / 2.0F, 0);



                GL.End();
                GL.PopMatrix();
            }
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================