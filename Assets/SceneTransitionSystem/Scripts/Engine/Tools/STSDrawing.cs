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
    public class STSDrawing
    {
        //-------------------------------------------------------------------------------------------------------------
        private static Texture2D aaLineTex = null;
        private static Texture2D lineTex = null;
        private static Material blitMaterial = null;
        private static Material blendMaterial = null;
        private static Rect lineRect = new Rect(0, 0, 1, 1);
        //-------------------------------------------------------------------------------------------------------------
        static Texture2D kTexture;
        static Material tMat;
        static string ShaderName = "UI/Default";
        //-------------------------------------------------------------------------------------------------------------
        static STSDrawing()
        {
            Initialize();
            tMat = new Material(Shader.Find(ShaderName));
            if (kTexture == null)
            {
                kTexture = new Texture2D(1, 1);
            }
        }
        //-------------------------------------------------------------------------------------------------------------
        //public static void DrawCircle(Vector2 sCenter, float sRadius, uint sSegmentPerQuarter, Color sColor)
        //{
        //    if (sSegmentPerQuarter < 1)
        //    {
        //        sSegmentPerQuarter = 1;
        //    }
        //    uint tTriangles = (sSegmentPerQuarter+1) * 4 * 3;
        //    Vector2[] tList = new Vector2[tTriangles];
        //    // Create Circle points triangles around this center
        //    // Put in DrawTriangles methods
        //    int tCounter = 0;
        //    float tRadIncrement = Mathf.PI / (2.0F * (float)sSegmentPerQuarter);
        //    //Debug.Log("tRadIncrement " + tRadIncrement);
        //    //Debug.Log("cos " + Mathf.Cos(tRadIncrement));
        //    //Debug.Log("sin " + Mathf.Sin(tRadIncrement));
        //    // Add First Segment
        //    tList[tCounter++] = sCenter;
        //    tList[tCounter++] = new Vector2(sCenter.x +sRadius, sCenter.y );
        //    Vector2 tOriginalPoint= new Vector2(sCenter.x + Mathf.Cos(tRadIncrement) * sRadius, sCenter.y - Mathf.Sin(tRadIncrement) * sRadius);
        //    Vector2 tNextPoint = tOriginalPoint;
        //    tList[tCounter++] = tNextPoint;
        //    uint tSeg = (sSegmentPerQuarter * 4)-1;
        //    for (int i = 1; i <= tSeg; i++)
        //    {
        //        float tR = tRadIncrement * i;
        //        //Debug.Log("tRadIncrement <" +i+">"+ tR.ToString());
        //        //Debug.Log("cos " + Mathf.Cos(tR));
        //        //Debug.Log("sin " + Mathf.Sin(tR));
        //        // Add next Segment
        //        tList[tCounter++] = sCenter;
        //        tList[tCounter++] = tNextPoint;
        //        tNextPoint = new Vector2(sCenter.x + Mathf.Cos(tR) * sRadius, sCenter.y - Mathf.Sin(tR) * sRadius);
        //        tList[tCounter++] = tNextPoint;
        //    }
        //    tList[tCounter++] = sCenter;
        //    tList[tCounter++] = tNextPoint;
        //    tList[tCounter++] = new Vector2(sCenter.x + sRadius, sCenter.y);
        //    STSDrawTriangle.DrawTriangles(tList, sColor);
        //}
        //-------------------------------------------------------------------------------------------------------------
        //public static void DrawTriangles(Vector2[] sPoints, Color sColor)
        //{
        //    if (Event.current.type.Equals(EventType.Repaint))
        //    {
        //        if (tMat == null)
        //        {
        //            tMat = new Material(Shader.Find(ShaderName));
        //        }
        //        GL.PushMatrix();
        //        tMat.SetPass(0);
        //        GL.LoadPixelMatrix();
        //        GL.Begin(GL.TRIANGLES);
        //        GL.Color(sColor);
        //        foreach (Vector2 tV in sPoints)
        //        {
        //            GL.Vertex3(tV.x, tV.y, 0);
        //        }
        //        GL.End();
        //        GL.PopMatrix();
        //    }
        //}
        ////-------------------------------------------------------------------------------------------------------------
        //public static void DrawTriangle(Vector2 sA, Vector2 sB, Vector2 sC, Color sColor)
        //{
        //    if (Event.current.type.Equals(EventType.Repaint))
        //    {
        //        if (tMat == null)
        //        {
        //            tMat = new Material(Shader.Find(ShaderName));
        //        }
        //        GL.PushMatrix();
        //        tMat.SetPass(0);
        //        GL.LoadPixelMatrix();
        //        GL.Begin(GL.TRIANGLES);
        //        GL.Color(sColor);
        //        GL.Vertex3(sA.x, sA.y, 0);
        //        GL.Vertex3(sB.x, sB.y, 0);
        //        GL.Vertex3(sC.x, sC.y, 0);
        //        GL.End();
        //        GL.PopMatrix();
        //    }
        //}
        //-------------------------------------------------------------------------------------------------------------
        //public static void DrawQuad(Vector2 sA, Vector2 sB, Vector2 sC, Vector2 sD, Color sColor)
        //{
        //    if (Event.current.type.Equals(EventType.Repaint))
        //    {
        //        if (tMat == null)
        //        {
        //            tMat = new Material(Shader.Find(ShaderName));
        //        }
        //        GL.PushMatrix();
        //        tMat.SetPass(0);
        //        GL.LoadPixelMatrix();
        //        GL.Begin(GL.QUADS);
        //        GL.Color(sColor);
        //        GL.Vertex3(sA.x, sA.y, 0);
        //        GL.Vertex3(sB.x, sB.y, 0);
        //        GL.Vertex3(sC.x, sC.y, 0);
        //        GL.Vertex3(sD.x, sD.y, 0);
        //        GL.End();
        //        GL.PopMatrix();
        //    }
        //}
        //-------------------------------------------------------------------------------------------------------------
        //public static void DrawRect(Rect sRect, Color sColor)
        //{

        //    //DrawQuad(new Vector2(sRect.x, sRect.y),
        //    //new Vector2(sRect.x, sRect.y + sRect.height),
        //    //new Vector2(sRect.x + sRect.width, sRect.y + sRect.height),
        //    //new Vector2(sRect.x + sRect.width, sRect.y), sColor);
        //    if (Event.current.type.Equals(EventType.Repaint))
        //    {
        //        if (tMat == null)
        //        {
        //            tMat = new Material(Shader.Find(ShaderName));
        //        }
        //        GL.PushMatrix();
        //        tMat.SetPass(0);
        //        GL.LoadPixelMatrix();
        //        // TRIANGLES Method
        //        //GL.Begin(GL.TRIANGLES);
        //        //GL.Color(sColor);
        //        ///*A*/
        //        //GL.Vertex3(sRect.x, sRect.y, 0);
        //        ///*B*/
        //        //GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
        //        ///*C*/
        //        //GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
        //        ///*D*/
        //        //GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);
        //        ///*C*/
        //        //GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
        //        ///*A*/
        //        //GL.Vertex3(sRect.x, sRect.y, 0);
        //        // QUADS Method
        //        GL.Begin(GL.QUADS);
        //        GL.Color(sColor);
        //        /*A*/
        //        GL.Vertex3(sRect.x, sRect.y, 0);
        //        /*B*/
        //        GL.Vertex3(sRect.x, sRect.y + sRect.height, 0);
        //        /*C*/
        //        GL.Vertex3(sRect.x + sRect.width, sRect.y + sRect.height, 0);
        //        /*D*/
        //        GL.Vertex3(sRect.x + sRect.width, sRect.y, 0);
        //        GL.End();
        //        GL.PopMatrix();
        //    }
        //}
        //-------------------------------------------------------------------------------------------------------------
        //public static void DrawRect(Rect sRect, Color sColor)
        //{
        //#if UNITY_EDITOR
        //            //EditorGUI.DrawRect(position, color); // replace by Graphics.DrawTexture
        //#endif
        //if (kTexture == null)
        //{
        //    kTexture = new Texture2D(1, 1);
        //}
        //if (tMat == null)
        //{
        //    tMat = new Material(Shader.Find(ShaderName));
        //}
        ////Texture2D tTexture = new Texture2D(1, 1);
        //kTexture.SetPixel(0, 0, color);
        //kTexture.Apply();
        ////GUI.DrawTexture(position, kTexture);
        //if (Event.current.type.Equals(EventType.Repaint)) /// move in draw master
        //{
        //    //Graphics.DrawTexture(position, kTexture);

        //    //GL.PushMatrix();
        //    //tMat.SetPass(0);
        //    //GL.LoadPixelMatrix();
        //    //GL.Begin(GL.QUADS);
        //    //GL.Color(color);
        //    //GL.Vertex3(position.x, position.y, 0);
        //    //GL.Vertex3(position.x, position.y + position.height, 0);
        //    //GL.Vertex3(position.x + position.width, position.y + position.height, 0);
        //    //GL.Vertex3(position.x + position.width, position.y, 0);
        //    //GL.End();
        //    //GL.PopMatrix();

        //    GL.PushMatrix();
        //    tMat.SetPass(0);
        //    GL.LoadPixelMatrix();
        //    GL.Begin(GL.TRIANGLES);
        //    GL.Color(color);
        //    GL.Vertex3(position.x, position.y, 0);
        //    GL.Vertex3(position.x, position.y + position.height, 0);
        //    GL.Vertex3(position.x + position.width, position.y + position.height, 0);
        //    //GL.Vertex3(position.x + position.width, position.y, 0);
        //    GL.End();
        //    GL.PopMatrix();
        //}
        //}
        //-------------------------------------------------------------------------------------------------------------
        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
        {
            // Normally the static initializer does this, but to handle texture reinitialization
            // after editor play mode stops we need this check in the Editor.
#if UNITY_EDITOR
            if (!lineTex)
            {
                Initialize();
            }
#endif

            // Note that theta = atan2(dy, dx) is the angle we want to rotate by, but instead
            // of calculating the angle we just use the sine (dy/len) and cosine (dx/len).
            float dx = pointB.x - pointA.x;
            float dy = pointB.y - pointA.y;
            float len = Mathf.Sqrt(dx * dx + dy * dy);

            // Early out on tiny lines to avoid divide by zero.
            // Plus what's the point of drawing a line 1/1000th of a pixel long??
            if (len < 0.001f)
            {
                return;
            }

            // Pick texture and material (and tweak width) based on anti-alias setting.
            Texture2D tex;
            // Material mat;
            if (antiAlias)
            {
                // Multiplying by three is fine for anti-aliasing width-1 lines, but make a wide "fringe"
                // for thicker lines, which may or may not be desirable.
                width = width * 3.0f;
                tex = aaLineTex;
                //   mat = blendMaterial;
            }
            else
            {
                tex = lineTex;
                //  mat = blitMaterial;
            }

            float wdx = width * dy / len;
            float wdy = width * dx / len;

            Matrix4x4 matrix = Matrix4x4.identity;
            matrix.m00 = dx;
            matrix.m01 = -wdx;
            matrix.m03 = pointA.x + 0.5f * wdx;
            matrix.m10 = dy;
            matrix.m11 = wdy;
            matrix.m13 = pointA.y - 0.5f * wdy;

            // Use GL matrix and Graphics.DrawTexture rather than GUI.matrix and GUI.DrawTexture,
            // for better performance. (Setting GUI.matrix is slow, and GUI.DrawTexture is just a
            // wrapper on Graphics.DrawTexture.)
            GL.PushMatrix();
            GL.MultMatrix(matrix);
            //Graphics.DrawTexture(lineRect, tex, lineRect, 0, 0, 0, 0, color, mat);
            //Replaced by:
            GUI.color = color;//this and...
            GUI.DrawTexture(lineRect, tex);//this

            GL.PopMatrix();
        }

        public static void DrawCircle(Vector2 center, int radius, Color color, float width, int segmentsPerQuarter)
        {
            DrawCircle(center, radius, color, width, false, segmentsPerQuarter);
        }

        public static void DrawCircle(Vector2 center, int radius, Color color, float width, bool antiAlias, int segmentsPerQuarter)
        {
            float rh = (float)radius / 2;

            Vector2 p1 = new Vector2(center.x, center.y - radius);
            Vector2 p1_tan_a = new Vector2(center.x - rh, center.y - radius);
            Vector2 p1_tan_b = new Vector2(center.x + rh, center.y - radius);

            Vector2 p2 = new Vector2(center.x + radius, center.y);
            Vector2 p2_tan_a = new Vector2(center.x + radius, center.y - rh);
            Vector2 p2_tan_b = new Vector2(center.x + radius, center.y + rh);

            Vector2 p3 = new Vector2(center.x, center.y + radius);
            Vector2 p3_tan_a = new Vector2(center.x - rh, center.y + radius);
            Vector2 p3_tan_b = new Vector2(center.x + rh, center.y + radius);

            Vector2 p4 = new Vector2(center.x - radius, center.y);
            Vector2 p4_tan_a = new Vector2(center.x - radius, center.y - rh);
            Vector2 p4_tan_b = new Vector2(center.x - radius, center.y + rh);

            DrawBezierLine(p1, p1_tan_b, p2, p2_tan_a, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(p2, p2_tan_b, p3, p3_tan_b, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(p3, p3_tan_a, p4, p4_tan_b, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(p4, p4_tan_a, p1, p1_tan_a, color, width, antiAlias, segmentsPerQuarter);
        }

        // Other than method name, DrawBezierLine is unchanged from Linusmartensson's original implementation.
        public static void DrawBezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
        {
            Vector2 lastV = CubeBezier(start, startTangent, end, endTangent, 0);
            for (int i = 1; i < segments + 1; ++i)
            {
                Vector2 v = CubeBezier(start, startTangent, end, endTangent, i / (float)segments);
                STSDrawing.DrawLine(lastV, v, color, width, antiAlias);
                lastV = v;
            }
        }


        private static Vector2 CubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t)
        {
            float rt = 1 - t;
            return rt * rt * rt * s + 3 * rt * rt * t * st + 3 * rt * t * t * et + t * t * t * e;
        }

        // This static initializer works for runtime, but apparently isn't called when
        // Editor play mode stops, so DrawLine will re-initialize if needed.

        private static void Initialize()
        {
            if (lineTex == null)
            {
                lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                lineTex.SetPixel(0, 1, Color.white);
                lineTex.Apply();
            }
            if (aaLineTex == null)
            {
                // TODO: better anti-aliasing of wide lines with a larger texture? or use Graphics.DrawTexture with border settings
                aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
                aaLineTex.SetPixel(0, 0, new Color(1, 1, 1, 0));
                aaLineTex.SetPixel(0, 1, Color.white);
                aaLineTex.SetPixel(0, 2, new Color(1, 1, 1, 0));
                aaLineTex.Apply();
            }

            // GUI.blitMaterial and GUI.blendMaterial are used internally by GUI.DrawTexture,
            // depending on the alphaBlend parameter. Use reflection to "borrow" these references.
            blitMaterial = (Material)typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
            blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        }
        //-------------------------------------------------------------------------------------------------------------
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================