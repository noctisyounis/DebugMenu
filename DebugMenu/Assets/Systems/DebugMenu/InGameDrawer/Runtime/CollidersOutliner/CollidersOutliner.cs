using System.Collections.Generic;
using Shapes;
using UnityEngine;
using DebugAttribute;

namespace DebugMenu.InGameDrawer.CollidersOutliner
{
    public class CollidersOutliner : MonoBehaviour
    {
        #region Unity API

        private void Start()
        {
            _boxColliders = new List<BoxCollider>();
            _sphereColliders = new List<SphereCollider>();
            _capsuleColliders = new List<CapsuleCollider>();
            _meshColliders = new List<MeshCollider>();

            GetTypeOfColliders();
            SetShapesColor(new Color(0, 1, 0, 0.2f));
            SetShapesBlendMode(ShapesBlendMode.Transparent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SetState();
            }

            if (_state)
            {
                DisplayAllCollidersInScene();
                RefreshAllCollidersLists();
            }
        }
        
        #endregion


        #region Utils

        private static void RefreshAllCollidersLists()
        {
            _boxColliders.Clear();
            _sphereColliders.Clear();
            _capsuleColliders.Clear();
            _meshColliders.Clear();
            GetTypeOfColliders();
        }

        [DebugMenu("Settings/Gizmos/Show Colliders")]
        public static void SetState()
        {
            _state = !_state;
        }

        private static List<Collider> GetAllCollidersInScene()
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
            foreach (var collider in GetAllCollidersInScene())
            {
                if (collider.GetType() == typeof(BoxCollider))
                {
                    _boxColliders.Add((BoxCollider) collider);
                }
                else if (collider.GetType() == typeof(SphereCollider))
                {
                    _sphereColliders.Add((SphereCollider) collider);
                }
                else if (collider.GetType() == typeof(CapsuleCollider))
                {
                    _capsuleColliders.Add((CapsuleCollider) collider);
                }
                else if (collider.GetType() == typeof(MeshCollider))
                {
                    _meshColliders.Add((MeshCollider) collider);
                }
            }
        }

        private static void SetShapesColor(Color color)
        {   
            Draw.Color = color;
        }

        private static void SetShapesBlendMode(ShapesBlendMode blendMode)
        {
            Draw.BlendMode = ShapesBlendMode.Transparent;
        }

        #endregion


        #region DrawingShape
        public static void DisplayAllCollidersInScene()
        {
            Camera cam = Camera.main;

            using (Draw.Command(cam))
            {
                DisplayAllBoxCollidersInScene();
                DisplayAllSphereCollidersInScene();
                DisplayAllCapsuleCollidersInScene();
                DisplayAllMeshCollidersInScene();
            }
        }

        public static void DisplayAllBoxCollidersInScene()
        {
            foreach (var boxCollider in _boxColliders)
            {
                var boxSize = new Vector3(boxCollider.size.x * boxCollider.transform.localScale.x,
                                          boxCollider.size.y * boxCollider.transform.localScale.y,
                                          boxCollider.size.z * boxCollider.transform.localScale.z);

                Draw.Cuboid(boxCollider.bounds.center, boxCollider.transform.rotation, boxSize);
            }
        }

        public static void DisplayAllSphereCollidersInScene()
        {
            foreach (var sphereCollider in _sphereColliders)
            {
                Draw.Sphere(sphereCollider.bounds.center, sphereCollider.bounds.extents.x);
            }
        }

        public static void DisplayAllCapsuleCollidersInScene()
        {
            foreach (var capsuleCollider in _capsuleColliders)
            {
                var localScale = capsuleCollider.transform.localScale;
                var maxLocalScaleAxis = Mathf.Max(localScale.x, localScale.z);

                var capsuleSize = new Vector3(capsuleCollider.radius * maxLocalScaleAxis * 2,
                                              capsuleCollider.height * localScale.y,
                                              capsuleCollider.radius * maxLocalScaleAxis * 2);

                Draw.Cuboid(capsuleCollider.bounds.center,
                            capsuleCollider.transform.rotation,
                            capsuleSize);
            }
        }

        public static void DisplayAllMeshCollidersInScene()
        {
            foreach (var meshCollider in _meshColliders)
            {
                if (!meshCollider.sharedMesh.isReadable) return;

                var vertexPath = new PolylinePath();
                var center = meshCollider.bounds.center;
                var verticesPosition = meshCollider.sharedMesh.vertices;

                for (int i = 0; i < verticesPosition.Length; i++)
                {
                    verticesPosition[i] += center;

                    var newRotation = new Quaternion();
                    newRotation.eulerAngles = meshCollider.transform.rotation.eulerAngles;
                    verticesPosition[i] = newRotation * (verticesPosition[i] - center) + center;
                    

                    //verticesPosition[i].x *= meshCollider.transform.localScale.x;
                    //verticesPosition[i].y *= meshCollider.transform.localScale.y;
                    //verticesPosition[i].z *= meshCollider.transform.localScale.z;
                }

                vertexPath.AddPoints(verticesPosition);

                Draw.Polyline(vertexPath);
            }
        }

        #endregion


        #region Private And Protected Members

        private static List<BoxCollider> _boxColliders;
        private static List<SphereCollider> _sphereColliders;
        private static List<CapsuleCollider> _capsuleColliders;
        private static List<MeshCollider> _meshColliders;
        private static bool _state;

        #endregion
    }
}