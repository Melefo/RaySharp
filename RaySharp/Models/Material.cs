
using RaySharp.Textures;
using System;
using System.Runtime.InteropServices;

namespace RaySharp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Material : IDisposable
    {
        [DllImport(Constants.dllName, CharSet = CharSet.Ansi)]
        private static extern unsafe Material* LoadMaterials(string fileName, ref int materialCount);
        [DllImport(Constants.dllName)]
        private static extern Material LoadMaterialDefault();
        [DllImport(Constants.dllName)]
        private static extern void UnloadMaterial(Material material);
        [DllImport(Constants.dllName)]
        private static extern void SetMaterialTexture(ref Material material, MaterialMapIndex mapType, Texture2D texture);
        //[DllImport(Constants.dllName)]
        //private static extern void SetModelMeshMaterial(Model* model, int meshId, int materialId);                               // Set material for a mesh

        /// <summary>
        /// Material map index
        /// </summary>
        public enum MaterialMapIndex : int
        {
            /// <summary>
            /// Albedo material (same as: DIFFUSE)
            /// </summary>
            ALBEDO = 0,
            /// <summary>
            /// Metalness material (same as: SPECULAR)
            /// </summary>
            METALNESS = 1,
            /// <summary>
            /// Normal material
            /// </summary>
            NORMAL = 2,
            /// <summary>
            /// Roughness material
            /// </summary>
            ROUGHNESS = 3,
            /// <summary>
            /// Ambient occlusion material
            /// </summary>
            OCCLUSION = 4,
            /// <summary>
            /// Emission material
            /// </summary>
            EMISSION = 5,
            /// <summary>
            /// Heightmap material
            /// </summary>
            HEIGHT = 6,
            /// <summary>
            /// Cubemap material
            /// </summary>
            /// <remarks>
            /// Uses GL_TEXTURE_CUBE_MAP
            /// </remarks>
            CUBEMAP = 7,
            /// <summary>
            /// Irradiance material
            /// </summary>
            /// <remarks>
            /// Uses GL_TEXTURE_CUBE_MAP
            /// </remarks>
            IRRADIANCE = 8,
            /// <summary>
            /// Prefilter material 
            /// </summary>
            /// <remarks>
            /// Uses GL_TEXTURE_CUBE_MAP
            /// </remarks>
            PREFILTER = 9,
            /// <summary>
            /// Brdg material
            /// </summary>
            BRDF = 10,
            /// <summary>
            /// Albedo material (same as: ALBEDO)
            /// </summary>
            DIFFUSE = ALBEDO,
            /// <summary>
            /// Metalness material (same as: METALNESS)
            /// </summary>
            SPECULAR = METALNESS,
        }

        /// <summary>
        /// Load default material (Supports: DIFFUSE, SPECULAR, NORMAL maps)
        /// </summary>
        public static Material Default => LoadMaterialDefault();
        /// <summary>
        /// Material shader
        /// </summary>
        public Shader Shader { get; }
        /// <summary>
        /// Material maps
        /// </summary>
        public IntPtr Maps { get; private set; }
        /// <summary>
        /// Material generic parameters (if required)
        /// </summary>
        public IntPtr Param { get; private set; }

        /// <summary>
        /// Load materials from model file
        /// </summary>
        /// <param name="fileName">Filepath</param>
        /// <returns>All models inside file</returns>
        public static unsafe Material[] LoadMaterials(string fileName)
        {
            int materialCount = 0;
            Material* materials = LoadMaterials(fileName, ref materialCount);
            var array = new Material[materialCount];

            for (int i = 0; i < materialCount; i++)
                array[i] = materials[i];
            return array;
        }

        /// <summary>
        /// Unload material from GPU memory (VRAM)
        /// </summary>
        public void Dispose()
        {
            UnloadMaterial(this);
            Shader.Dispose();
            Maps = IntPtr.Zero;
            Param = IntPtr.Zero;
        }

        /// <summary>
        /// Set texture for a material map type (MATERIAL_MAP_DIFFUSE, MATERIAL_MAP_SPECULAR...)
        /// </summary>
        /// <param name="mapType"></param>
        /// <param name="texture"></param>
        public void SetTexture(MaterialMapIndex mapType, Texture2D texture) => SetMaterialTexture(ref this, mapType, texture);
    }
}
