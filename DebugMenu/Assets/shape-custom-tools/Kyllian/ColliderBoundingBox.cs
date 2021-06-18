using System.Collections.Generic;
using Shapes;
using UnityEngine;
using DebugAttribute;

public class ColliderBoundingBox : MonoBehaviour
{
    #region Unity API

    private void Start()
    {
        _colliders = new List<Collider>();
        _sphereColliders = new List<SphereCollider>();

        GetTypeOfColliders();
    }

    private void Update()
    {
        if (!_isDisplay) return;

        DrawColliderBoundingBoxes();
    }

    #endregion


    #region Utils

    [DebugMenu("Settings/Gizmos/BoundingBox")]
    public static void SetColliderBoundingBox()
    {
        _isDisplay = !_isDisplay;
        RefreshColliders();
    }

    private static void RefreshColliders()
    {
        _colliders.Clear();
        _sphereColliders.Clear();

        GetTypeOfColliders();
    }

    private static List<Collider> GetCollidersInScene()
    {
        List<Collider> collidersInScene = new List<Collider>();

        foreach (var collider in Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[])
        {
            collidersInScene.Add(collider);
        }

        return collidersInScene;
    }

    private static void GetTypeOfColliders()
    {
        foreach (var collider in GetCollidersInScene())
        {
            if (collider.GetType() == typeof(SphereCollider))
            {
                _sphereColliders.Add((SphereCollider)collider);
            }
            else
            {
                _colliders.Add(collider);
            }
        }
    }

    #endregion


    #region DrawingShape

    private static void DrawColliderBoundingBoxes()
    {
        Camera cam = Camera.main;

        using (Draw.Command(cam))
        {
            Draw.Color = Color.red;

            foreach (var collider in _colliders)
            {
                DrawBoundingBox(collider);
            }

            foreach (var sphereCollider in _sphereColliders)
            {
                DrawBoundingSphere(sphereCollider);
            }
        }
    }

    private static void DrawBoundingSphere(SphereCollider collider)
    {
        Draw.RingThickness = 0.015f;
        Vector3 center = collider.bounds.center;
        float radius = collider.bounds.size.x * 0.5f;

        Draw.DiscGeometry = DiscGeometry.Flat2D;
        Draw.Ring(center, radius);
        Draw.Ring(center, Quaternion.Euler(90, 0, 0), radius);
        Draw.Ring(center, Quaternion.Euler(0, 90, 0), radius);
    }

    private static void DrawBoundingBox(Collider collider)
    {
        Draw.LineThickness = 0.02f;
        PolylinePath[] paths = new PolylinePath[6];

        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new PolylinePath();
        }

        Vector3 halfSize = collider.bounds.size * 0.5f;
        Vector3 center = collider.bounds.center;

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
    private static List<Collider> _colliders;
    private static List<SphereCollider> _sphereColliders;

    #endregion
}