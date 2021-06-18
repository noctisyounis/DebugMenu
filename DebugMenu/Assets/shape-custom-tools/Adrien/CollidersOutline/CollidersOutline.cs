using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shapes;
using UnityEngine;

public class CollidersOutline : MonoBehaviour
{
    #region Unity API

    private void Start()
    {
        _boxColliders = new List<BoxCollider>();
        _sphereColliders = new List<SphereCollider>();
        _capsuleColliders = new List<CapsuleCollider>();
        GetTypeOfColliders();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetState();
        }

        // if (!_state) return;
        if (_state)
        {
            DisplayColliders();
            RefreshLists();
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("printList"))
        {
            foreach (var element in _boxColliders)
            {
                Debug.Log(element);
            }
        }
    }

    #endregion


    #region Utils

    private static void RefreshLists()
    {
        _boxColliders.Clear();
        _sphereColliders.Clear();
        _capsuleColliders.Clear();
        GetTypeOfColliders();
    }

    public static void SetState()
    {
        _state = !_state;
    }

    private static List<Collider> GetCollidersInScene()
    {
        List<Collider> collidersInScene = new List<Collider>();

        foreach (var element in Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[])
        {
            collidersInScene.Add(element);
        }

        return collidersInScene;
    }

    private static void GetTypeOfColliders()
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
        }
    }

    #endregion


    #region DrawingShape

    public static void DisplayColliders()
    {
        Camera cam = Camera.main;


        using (Draw.Command(cam))
        {
            foreach (var element in _boxColliders)
            {
                Draw.Cuboid(element.bounds.center, element.transform.rotation, element.bounds.size, Color.red);
            }

            foreach (var element in _sphereColliders)
            {
                Draw.Sphere(element.bounds.center, element.radius, Color.red);
            }

            foreach (var element in _capsuleColliders)
            {
                Draw.Cuboid(element.bounds.center, element.transform.rotation,
                    new Vector3(element.radius * 2, element.height,
                        element.radius * 2), Color.red);
            }
        }
    }

    #endregion


    #region private Members

    private static List<BoxCollider> _boxColliders;
    private static List<SphereCollider> _sphereColliders;
    private static List<CapsuleCollider> _capsuleColliders;
    private static bool _state;

    #endregion
}