using System.Collections.Generic;
using Shapes;
using UnityEngine;

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

    public static void SetColliderBoundingBox()
    {
        _isDisplay = !_isDisplay;
    }

    private List<Collider> GetCollidersInScene()
    {
        List<Collider> collidersInScene = new List<Collider>();

        foreach (var collider in Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[])
        {
            collidersInScene.Add(collider);
        }

        return collidersInScene;
    }

    private void GetTypeOfColliders()
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
        Draw.DiscRadius = collider.radius;
        Draw.RingThickness = 0.015f;

        Draw.Ring(collider.bounds.center);
        Draw.Ring(collider.bounds.center, Quaternion.Euler(90, 0, 0));
        Draw.Ring(collider.bounds.center, Quaternion.Euler(0, 90, 0));

        Draw.DiscGeometry = DiscGeometry.Billboard;
        Draw.Ring(collider.bounds.center);
    }

    private static void DrawBoundingBox(Collider collider)
    {
        Draw.LineThickness = 0.02f;
        PolylinePath[] paths = new PolylinePath[6];

        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = new PolylinePath();
        }

        var halfSize = collider.bounds.size * 0.5f;
        var center = collider.bounds.center;

        var upFrontRightVertices = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
        var upFrontLeftVertices = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
        var upBackRightVertices = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        var upBackLeftVertices = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        var downFrontRightVertices = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
        var downFrontLeftVertices = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
        var downBackRightVertices = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        var downBackLeftVertices = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);

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