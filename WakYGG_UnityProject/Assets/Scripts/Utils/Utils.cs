using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    ///<summary> new Vector3() 대신 사용</summary>
    public static Vector3 NewVector3(float x = 0, float y = 0, float z = 0)
    {
        return Vector3.right * x + Vector3.up * y + Vector3.forward * z ;
    }
    /// <summary>기본 2D 메테리얼 생성</summary>
    public static Material GetLineMaterial()
    {
        Material lineMat;
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineMat = new Material(shader);
        lineMat.hideFlags = HideFlags.HideAndDontSave;
        lineMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        lineMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        lineMat.SetInt("_ZWrite", 0);
        return lineMat;
    }
}