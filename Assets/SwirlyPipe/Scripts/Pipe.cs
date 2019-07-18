﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    /// <summary>
    /// 横切面半径
    /// </summary>
    public float pipeRadius;

    /// <summary>
    /// 横切面分割数量
    /// </summary>
    public int pipeSegmentCount;

    /// <summary>
    /// 显示的距离
    /// </summary>
    public float ringDistance;

    /// <summary>
    /// 最大最小的随机半径
    /// </summary>
    public float minCurveRadius, maxCurveRadius;

    /// <summary>
    /// 最大最小的随机分割数量
    /// </summary>
    public int minCurveSegmentCount, maxCurveSegmentCount;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    /// <summary>
    /// 全身的角度
    /// </summary>
    private float curveAngle;

    /// <summary>
    /// 管道半径
    /// </summary>
    private float curveRadius;

    /// <summary>
    /// 管道数量
    /// </summary>
    private int curveSegmentCount;

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Pipe";

        curveRadius = Random.Range(minCurveRadius, maxCurveRadius);
        curveSegmentCount = Random.Range(minCurveSegmentCount, maxCurveSegmentCount);

        SetVertices();
        SetTriangles();
        mesh.RecalculateNormals();
    }

    private void SetVertices()
    {
        vertices = new Vector3[pipeSegmentCount * curveSegmentCount * 4];
        float uStep = ringDistance / curveRadius;
        int iDelta = pipeSegmentCount * 4;
        curveAngle = uStep * curveSegmentCount * Mathf.Rad2Deg;
        CreateFirstRing(uStep);
        for (int u = 2, i = iDelta; u <= curveSegmentCount; u++, i += iDelta)
        {
            CreateQuadRing(u * uStep, i);
        }

        mesh.vertices = vertices;
    }


    private void CreateFirstRing(float u)
    {
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;

        Vector3 vertexA = GetPointOnTorus(0, 0f);
        Vector3 vertexB = GetPointOnTorus(u, 0f);

        for (int v = 1, i = 0; v <= pipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertexA;
            vertices[i + 1] = vertexA = GetPointOnTorus(0, v * vStep);
            vertices[i + 2] = vertexB;
            vertices[i + 3] = vertexB = GetPointOnTorus(u, v * vStep);
        }
    }

    private void CreateQuadRing(float u, int i)
    {
        float vStep = (2f * Mathf.PI) / pipeSegmentCount;
        int ringOffset = pipeSegmentCount * 4;

        Vector3 vertex = GetPointOnTorus(u, 0f);
        //现在用四个点 对应 六个顶点数据
        for (int v = 1; v <= pipeSegmentCount; v++, i += 4)
        {
            vertices[i] = vertices[i - ringOffset + 2];
            vertices[i + 1] = vertices[i - ringOffset + 3];
            vertices[i + 2] = vertex;
            vertices[i + 3] = vertex = GetPointOnTorus(u, v * vStep);
        }
    }

    private void SetTriangles()
    {
        triangles = new int[pipeSegmentCount * curveSegmentCount * 6];
        for (int t = 0, i = 0; t < triangles.Length; t += 6, i += 4)
        {
            triangles[t] = i;
            triangles[t + 1] = triangles[t + 4] = i + 1;
            triangles[t + 2] = triangles[t + 3] = i + 2;
            triangles[t + 5] = i + 3;
        }

        mesh.triangles = triangles;
    }


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
        float uStep = ringDistance / curveRadius;
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

    public void AlignWith(Pipe pipe)
    {
        float relativeRotation = Random.Range(0, curveRadius) * 360f / curveSegmentCount;

        transform.SetParent(pipe.transform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, -pipe.curveAngle);

        transform.Translate(0, pipe.curveRadius, 0f);
        transform.Rotate(relativeRotation, 0, 0);
        transform.Translate(0f, -curveRadius, 0f);
        transform.SetParent(pipe.transform.parent);
    }
}