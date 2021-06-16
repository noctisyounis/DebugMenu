using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shapes;
using UnityEngine;

public class CollidersOutline : ImmediateModeShapeDrawer
{
    #region Unity API

    private void Start()
    {
        _boxColliders = new List<BoxCollider>();
        _sphereColliders = new List<SphereCollider>();
        _capsuleColliders = new List<CapsuleCollider>();
        _meshColliders = new List<MeshCollider>();
        GetTypeOfColliders();
    }

    private void OnGUI()
    {
        GUILayout.Label($"colliders : {GetCollidersInScene().Count}");
    }

    #endregion


    #region Utils

    private List<Collider> GetCollidersInScene()
    {
        List<Collider> collidersInScene = new List<Collider>();

        foreach (var element in Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[])
        {
            collidersInScene.Add(element);
        }

        return collidersInScene;
    }

    private void GetTypeOfColliders()
    {
        foreach (var element in GetCollidersInScene())
        {
            if (element.GetType() == typeof(BoxCollider))
            {
                _boxColliders.Add((BoxCollider) element);
            }
            else if (element.GetType() == typeof(SphereCollider))
            {
                _sphereColliders.Add((SphereCollider) element);
            }
            else if (element.GetType() == typeof(CapsuleCollider))
            {
                _capsuleColliders.Add((CapsuleCollider) element);
            }
            else if (element.GetType() == typeof(MeshCollider))
            {
                _meshColliders.Add((MeshCollider) element);
            }
        }
    }

    #endregion


    #region DrawingShape

    public override void DrawShapes(Camera cam)
    {
        using (Draw.Command(cam))
        {
            // set up static parameters. these are used for all following Draw.Line calls
            Draw.LineGeometry = LineGeometry.Volumetric3D;
            Draw.LineThicknessSpace = ThicknessSpace.Pixels;
            Draw.LineThickness = 4; // 4px wide

            // set static parameter to draw in the local space of this object
            Draw.Matrix = transform.localToWorldMatrix;

            foreach (var element in _boxColliders)
            {
                Draw.Cuboid(element.bounds.center, element.bounds.size, Color.red);
            }

            foreach (var element in _sphereColliders)
            {
                Draw.Sphere(element.bounds.center, element.radius);
            }

            foreach (var element in _capsuleColliders)
            {
                Draw.Cuboid(element.bounds.center, element.bounds.size, Color.red);
            }
        }
    }

    private void Test()
    {
    }

    #endregion


    #region private Members

    private List<BoxCollider> _boxColliders;
    private List<SphereCollider> _sphereColliders;
    private List<CapsuleCollider> _capsuleColliders;
    private List<MeshCollider> _meshColliders;

    #endregion
}