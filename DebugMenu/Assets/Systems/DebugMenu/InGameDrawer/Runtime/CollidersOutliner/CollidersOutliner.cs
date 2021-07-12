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
            _boxColliders2D = new List<BoxCollider2D>();
            _circleColliders2D = new List<CircleCollider2D>();
            _capsuleColliders2D = new List<CapsuleCollider2D>();
            _polygonColliders2D = new List<PolygonCollider2D>();
            _boxColliders = new List<BoxCollider>();
            _sphereColliders = new List<SphereCollider>();
            _capsuleColliders = new List<CapsuleCollider>();
            _meshColliders = new List<MeshCollider>();

            GetTypeOfColliders();
            GetTypeOfColliders2D();
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


        #region Main

        private static void RefreshAllCollidersLists()
        {
            _boxColliders2D.Clear();
            _circleColliders2D.Clear();
            _capsuleColliders2D.Clear();
            _polygonColliders2D.Clear();
            _boxColliders.Clear();
            _sphereColliders.Clear();
            _capsuleColliders.Clear();
            _meshColliders.Clear();
            GetTypeOfColliders();
            GetTypeOfColliders2D();
        }

        [DebugMenu("Settings/Gizmos/Show Colliders")]
        public static void SetState()
        {
            _state = !_state;
        }

        private static List<Collider> GetAllCollidersInScene()
        {
            List<Collider> collidersInScene = new List<Collider>();

            foreach (var collider in Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[])
            {
                collidersInScene.Add(collider);
            }

            return collidersInScene;
        }

        private static List<Collider2D> GetAllColliders2DInScene()
        {
            List<Collider2D> colliders2DInScene = new List<Collider2D>();

            foreach (var collider2D in Resources.FindObjectsOfTypeAll(typeof(Collider2D)) as Collider2D[])
            {
                colliders2DInScene.Add(collider2D);
            }

            return colliders2DInScene;
        }

        private static void GetTypeOfColliders2D()
        {
            foreach (var collider2D in GetAllColliders2DInScene())
            {
                if (collider2D.GetType() == typeof(BoxCollider2D))
                {
                    _boxColliders2D.Add((BoxCollider2D) collider2D);
                }
                else if (collider2D.GetType() == typeof(CircleCollider2D))
                {
                    _circleColliders2D.Add((CircleCollider2D) collider2D);
                }
                else if (collider2D.GetType() == typeof(CapsuleCollider2D))
                {
                    _capsuleColliders2D.Add((CapsuleCollider2D) collider2D);
                }
                else if (collider2D.GetType() == typeof(PolygonCollider2D))
                {
                    _polygonColliders2D.Add((PolygonCollider2D) collider2D);
                }
            }
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
                DisplayAllBoxColliders2DInScene();
                DisplayAllCircleColliders2DInScene();
                DisplayAllPolygoneColliders2DInScene();
                DisplayAllBoxCollidersInScene();
                DisplayAllSphereCollidersInScene();
                DisplayAllCapsuleCollidersInScene();
                DisplayAllMeshCollidersInScene();
            }
        }

        public static void DisplayAllBoxColliders2DInScene()
        {
            foreach (var boxCollider2D in _boxColliders2D)
            {
                var rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, boxCollider2D.transform.rotation.eulerAngles.z);

                Vector2 size = boxCollider2D.size;

                Draw.Rectangle(boxCollider2D.bounds.center, rotation, size, boxCollider2D.edgeRadius);
            }
        }

        public static void DisplayAllCircleColliders2DInScene()
        {
            foreach (var circleCollider2D in _circleColliders2D)
            {
                Draw.Disc(circleCollider2D.bounds.center, circleCollider2D.bounds.extents.x);
            }
        }

        public static void DisplayAllPolygoneColliders2DInScene()
        {
            foreach (var polygonCollider2D in _polygonColliders2D)
            {
                var path = new PolylinePath();
                var center = polygonCollider2D.transform.position;
                var pointsPosition = new Vector3[polygonCollider2D.points.Length];
                var points = polygonCollider2D.points;
                var offset = polygonCollider2D.offset;

                for (int i = 0; i < pointsPosition.Length; i++)
                {
                    pointsPosition[i] = new Vector3(points[i].x, points[i].y, 0);
                    pointsPosition[i] += new Vector3(offset.x, offset.y, 0);

                    pointsPosition[i].x *= polygonCollider2D.transform.localScale.x;
                    pointsPosition[i].y *= polygonCollider2D.transform.localScale.y;
                    pointsPosition[i].z *= polygonCollider2D.transform.localScale.z;

                    pointsPosition[i] += center;

                    pointsPosition[i] = CalculateColliderRotation(polygonCollider2D, center, pointsPosition[i]);
                }

                path.AddPoints(pointsPosition);

                Draw.Polyline(path, true);
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
                    verticesPosition[i].x *= meshCollider.transform.localScale.x;
                    verticesPosition[i].y *= meshCollider.transform.localScale.y;
                    verticesPosition[i].z *= meshCollider.transform.localScale.z;

                    verticesPosition[i] += center;

                    verticesPosition[i] = CalculateColliderRotation(meshCollider, center, verticesPosition[i]);
                }

                vertexPath.AddPoints(verticesPosition);

                Draw.Polyline(vertexPath, true);
            }
        }

        #endregion


        #region Utils

        private static Vector3 CalculateColliderRotation(Collider collider, Vector3 center, Vector3 position)
        {
            var newRotation = new Quaternion();
            newRotation.eulerAngles = collider.transform.rotation.eulerAngles;
            return newRotation * (position - center) + center;
        }

        private static Vector3 CalculateColliderRotation(Collider2D collider, Vector3 center, Vector3 position)
        {
            var newRotation = new Quaternion();
            newRotation.eulerAngles = collider.transform.rotation.eulerAngles;
            return newRotation * (position - center) + center;
        }

        #endregion


        #region Private And Protected Members

        private static List<BoxCollider2D> _boxColliders2D;
        private static List<CircleCollider2D> _circleColliders2D;
        private static List<CapsuleCollider2D> _capsuleColliders2D;
        private static List<PolygonCollider2D> _polygonColliders2D;
        private static List<BoxCollider> _boxColliders;
        private static List<SphereCollider> _sphereColliders;
        private static List<CapsuleCollider> _capsuleColliders;
        private static List<MeshCollider> _meshColliders;
        private static bool _state;

        #endregion
    }
}