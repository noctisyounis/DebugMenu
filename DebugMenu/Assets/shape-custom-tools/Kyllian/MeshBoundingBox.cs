using System.Collections.Generic;
using Shapes;
using UnityEngine;
using DebugAttribute;

public class MeshBoundingBox : MonoBehaviour
{
    #region Unity API

    private void Start()
    {
        _meshFilter = new List<MeshFilter>();

        GetMeshInScene();
    }

    private void Update()
    {
        if (!_isDisplay) return;

        DrawMeshBoundingBoxes();
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Meshes"))
        {
            foreach (var item in _meshFilter)
            {
                Debug.Log(item);
            }
        }
    }

    #endregion


    #region Utils

    [DebugMenu("Settings/Gizmos/MeshBoundingBox")]
    public static void SetMeshBoundingBox()
    {
        _isDisplay = !_isDisplay;
        RefreshMeshes();
    }

    private static void RefreshMeshes()
    {
        _meshFilter.Clear();
        GetMeshInScene();
    }

    private static void GetMeshInScene()
    {
        foreach (var meshFilter in FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[])
        {
            _meshFilter.Add(meshFilter);
        }
    }

    #endregion


    #region DrawingShape

    private static void DrawMeshBoundingBoxes()
    {
        Camera cam = Camera.main;

        using (Draw.Command(cam))
        {
            Draw.Color = Color.yellow;

            foreach (var meshFilter in _meshFilter)
            {
                DrawBoundingBox(meshFilter);
            }
        }
    }

    private static void DrawBoundingBox(MeshFilter meshFilter)
    {
        Draw.LineThickness = 0.02f;
        PolylinePath[] paths = new PolylinePath[6];

        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new PolylinePath();
        }

        Vector3 halfSize = meshFilter.mesh.bounds.size * 0.5f;
        Vector3 center = meshFilter.transform.position;

        Vector3 upFrontRightVertices = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
        Vector3 upFrontLeftVertices = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
        Vector3 upBackRightVertices = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        Vector3 upBackLeftVertices = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        Vector3 downFrontRightVertices = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
        Vector3 downFrontLeftVertices = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
        Vector3 downBackRightVertices = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        Vector3 downBackLeftVertices = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);

        paths[0].AddPoints(new Vector3[] { upFrontRightVertices, upFrontLeftVertices, upBackLeftVertices, upBackRightVertices });
        paths[1].AddPoints(new Vector3[] { downFrontRightVertices, downFrontLeftVertices, downBackLeftVertices, downBackRightVertices });
        paths[2].AddPoints(new Vector3[] { upFrontRightVertices, upFrontLeftVertices, downFrontLeftVertices, downFrontRightVertices });
        paths[3].AddPoints(new Vector3[] { upBackRightVertices, upBackLeftVertices, downBackLeftVertices, downBackRightVertices });
        paths[4].AddPoints(new Vector3[] { upFrontLeftVertices, upBackLeftVertices, downBackLeftVertices, downFrontLeftVertices });
        paths[5].AddPoints(new Vector3[] { upFrontRightVertices, upBackRightVertices, downBackRightVertices, downFrontRightVertices });

        for (int i = 0; i < paths.Length; i++)
        {
            Draw.Polyline(paths[i], true);
        }
    }

    #endregion


    #region private Members

    private static bool _isDisplay;
    private static List<MeshFilter> _meshFilter;

    #endregion
}