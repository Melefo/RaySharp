using RaySharp.Textures;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Mesh : IDisposable
    {
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshPoly(int sides, float radius);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshPlane(float width, float length, int resX, int resZ);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshCube(float width, float height, float length);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshSphere(float radius, int rings, int slices);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshHemiSphere(float radius, int rings, int slices);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshCylinder(float radius, float height, int slices);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshTorus(float radius, float size, int radSeg, int sides);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshKnot(float radius, float size, int radSeg, int sides);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshHeightmap(Image heightmap, Vector3 size);
        [DllImport(Constants.dllName)]
        private static extern Mesh GenMeshCubicmap(Image cubicmap, Vector3 cubeSize);
        [DllImport(Constants.dllName)]
        private static extern void UploadMesh(ref Mesh mesh, bool dynamic);
        [DllImport(Constants.dllName)]
        private static extern void UpdateMeshBuffer(Mesh mesh, int index, IntPtr data, int dataSize, int offset);
        [DllImport(Constants.dllName)]
        private static extern void DrawMesh(Mesh mesh, Material material, Matrix4x4 transform);
        [DllImport(Constants.dllName)]
        private static extern void DrawMeshInstanced(Mesh mesh, Material material, Matrix4x4[] transforms, int instances);
        [DllImport(Constants.dllName)]
        private static extern void UnloadMesh(Mesh mesh);
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern bool ExportMesh(Mesh mesh, string fileName);

        /// <summary>
        /// Number of vertices stored in arrays
        /// </summary>
        public int VertexCount { get; private set; }
        /// <summary>
        /// Number of triangles stored (indexed or not)
        /// </summary>
        public int TriangleCount { get; private set; }
        /// <summary>
        /// Vertex position (XYZ - 3 components per vertex) (shader-location = 0)
        /// </summary>
        public IntPtr Vertices { get; private set; }
        /// <summary>
        /// Vertex texture coordinates (UV - 2 components per vertex) (shader-location = 1)
        /// </summary>
        public IntPtr Texcoords { get; private set; }
        /// <summary>
        /// Vertex second texture coordinates (useful for lightmaps) (shader-location = 5)
        /// </summary>
        public IntPtr Texcoords2 { get; private set; }
        /// <summary>
        /// Vertex normals (XYZ - 3 components per vertex) (shader-location = 2)
        /// </summary>
        public IntPtr Normals { get; private set; }
        /// <summary>
        /// Vertex tangents (XYZW - 4 components per vertex) (shader-location = 4)
        /// </summary>
        public IntPtr Tangents { get; private set; }
        /// <summary>
        /// Vertex colors (RGBA - 4 components per vertex) (shader-location = 3)
        /// </summary>
        public IntPtr Colors { get; private set; }
        /// <summary>
        /// Vertex indices (in case vertex data comes indexed)
        /// </summary>
        public IntPtr Indices { get; private set; }
        /// <summary>
        /// Animated vertex positions (after bones transformations)
        /// </summary>
        public IntPtr AnimVertices { get; private set; }
        /// <summary>
        /// Animated normals (after bones transformations)
        /// </summary>
        public IntPtr AnimNormals { get; private set; }
        /// <summary>
        /// Vertex bone ids, up to 4 bones influence by vertex (skinning)
        /// </summary>
        public IntPtr BoneIds { get; private set; }
        /// <summary>
        /// Vertex bone weight, up to 4 bones influence by vertex (skinning)
        /// </summary>
        public IntPtr BoneWeights { get; private set; }
        /// <summary>
        /// OpenGL Vertex Array Object id
        /// </summary>
        public uint VaoId { get; private set; }
        /// <summary>
        /// OpenGL Vertex Buffer Objects id (default vertex data)
        /// </summary>
        public IntPtr VboId { get; private set; }

        /// <summary>
        /// Generate cubes-based map/heightmap mesh from image data
        /// </summary>
        /// <param name="map">Map image</param>
        /// <param name="size">Mesh size</param>
        /// <param name="cubic">If map a cubes-based map</param>
        public Mesh(Image map, Vector3 size, bool cubic = false)
        {
            var mesh = cubic ? GenMeshCubicmap(map, size) : GenMeshHeightmap(map, size);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate torus mesh
        /// </summary>
        /// <param name="radius">Torus radius</param>
        /// <param name="size">Torus size</param>
        /// <param name="radSeg">Torus segments</param>
        /// <param name="sides">Torus sides</param>
        /// <param name="knot">Generate a trefoil knot torus</param>
        public Mesh(float radius, float size, int radSeg, int sides, bool knot = false)
        {
            var mesh = knot ? GenMeshKnot(radius, size, radSeg, sides) : GenMeshTorus(radius, size, radSeg, sides);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate cylinder mesh
        /// </summary>
        /// <param name="radius">Cylinder radius</param>
        /// <param name="height">Cylinder height</param>
        /// <param name="slices">Cylinder slices</param>
        public Mesh(float radius, float height, int slices)
        {
            var mesh = GenMeshCylinder(radius, height, slices);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate sphere mesh (standard sphere)
        /// </summary>
        /// <param name="radius">Sphere radius</param>
        /// <param name="rings">Shpere rings</param>
        /// <param name="slices">Sphere slices</param>
        /// <param name="hemi">Generate a half-sphere (no bottom cap)</param>
        public Mesh(float radius, int rings, int slices, bool hemi = false)
        {
            var mesh = hemi ? GenMeshHemiSphere(radius, rings, slices) : GenMeshSphere(radius, rings, slices);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate cuboid mesh
        /// </summary>
        /// <param name="width">Cube width</param>
        /// <param name="height">Cube height</param>
        /// <param name="length">Cube length</param>
        public Mesh(float width, float height, float length)
        {
            var mesh = GenMeshCube(width, height, length);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate plane mesh (with subdivisions)
        /// </summary>
        /// <param name="width">Plane width</param>
        /// <param name="length">Plane length</param>
        /// <param name="resX">Resolution X axis</param>
        /// <param name="resZ">Resolution Z axis</param>
        public Mesh(float width, float length, int resX, int resZ)
        {
            var mesh = GenMeshPlane(width, length, resX, resZ);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Generate polygonal mesh
        /// </summary>
        /// <param name="sides">Number of sides</param>
        /// <param name="radius">Radius of polygon</param>
        public Mesh(int sides, float radius)
        {
            var mesh = GenMeshPoly(sides, radius);

            VertexCount = mesh.VertexCount;
            TriangleCount = mesh.TriangleCount;
            Vertices = mesh.Vertices;
            Texcoords = mesh.Texcoords;
            Texcoords2 = mesh.Texcoords2;
            Normals = mesh.Normals;
            Tangents = mesh.Tangents;
            Colors = mesh.Colors;
            Indices = mesh.Indices;
            AnimVertices = mesh.AnimVertices;
            AnimNormals = mesh.AnimNormals;
            BoneIds = mesh.BoneIds;
            BoneWeights = mesh.BoneWeights;
            VaoId = mesh.VaoId;
            VboId = mesh.VboId;
        }

        /// <summary>
        /// Unload mesh data from CPU and GPU
        /// </summary>
        public void Dispose()
        {
            UnloadMesh(this);
            VertexCount = 0;
            TriangleCount = 0;
            Vertices = IntPtr.Zero;
            Texcoords = IntPtr.Zero;
            Texcoords2 = IntPtr.Zero;
            Normals = IntPtr.Zero;
            Tangents = IntPtr.Zero;
            Colors = IntPtr.Zero;
            Indices = IntPtr.Zero;
            AnimVertices = IntPtr.Zero;
            AnimNormals = IntPtr.Zero;
            BoneIds = IntPtr.Zero;
            BoneWeights = IntPtr.Zero;
            VaoId = 0;
            VboId = IntPtr.Zero;
        }

        /// <summary>
        /// Export mesh data to file, returns true on success
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <returns>returns true on success</returns>
        public bool Export(string fileName) => ExportMesh(this, fileName);

        /// <summary>
        /// Draw a 3d mesh with material and transform
        /// </summary>
        /// <param name="material">Mesh material</param>
        /// <param name="transform">Mesh transform</param>
        public void Draw(Material material, Matrix4x4 transform) => DrawMesh(this, material, transform);
        /// <summary>
        /// Draw multiple mesh instances with material and different transforms
        /// </summary>
        /// <param name="material">Mesh material</param>
        /// <param name="transforms">Mesh transforms</param>
        /// <param name="instances">Number of instances</param>
        public void DrawInstanced(Material material, Matrix4x4[] transforms, int instances) => DrawMeshInstanced(this, material, transforms, instances);

        /// <summary>
        /// Upload vertex data into GPU and provided VAO/VBO ids
        /// </summary>
        /// <param name="dynamic"></param>
        public void Upload(bool dynamic) => UploadMesh(ref this, dynamic);
        /// <summary>
        /// Update mesh vertex data in GPU for a specific buffer index
        /// </summary>
        /// <param name="index">Buffer index</param>
        /// <param name="data">Buffer data</param>
        /// <param name="dataSize">Buffer size</param>
        /// <param name="offset">Data offset</param>
        public void UploadBuffer(int index, IntPtr data, int dataSize, int offset) => UpdateMeshBuffer(this, index, data, dataSize, offset);
    }
}
