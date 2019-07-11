using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    /// <summary>
    /// 内圆环半径 管道半径
    /// </summary>
    public float curveRadius, pipeRadius;

    /// <summary>
    /// 内圆环管道分割数量 管道分割数量
    /// </summary>
    public int curveSegmentCount, pipeSegmentCount;


    private Vector3 GetPointOnTorus(float u, float v)
    {
        Vector3 p;
        float r = curveRadius + pipeRadius * Mathf.Cos(v);
        p.x = r * Mathf.Sin(u);
        p.y = r * Mathf.Cos(u);
        p.z = pipeRadius * Mathf.Sin(v);
        return p;
    }

    private void OnDrawGizmos()
    {
        float uStep = (2f * Mathf.PI) / curveSegmentCount;
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;

        for (int u = 0; u < curveSegmentCount; u++)
        {
            for (int v = 0; v < pipeSegmentCount; v++)
            {
                Vector3 point = GetPointOnTorus(u * uStep, v * vStep);
                Gizmos.color = new Color(
                    1f,
                    (float) v / pipeSegmentCount,
                    (float) u / curveSegmentCount);
                Gizmos.DrawSphere(point, 0.1f);
                
            }
        }
    }
}