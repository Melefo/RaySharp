using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace RaySharp.Textures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Shader : IDisposable
    {
        [DllImport(Constants.dllName)]
        private static extern Shader LoadShader(string vsFileName, string fsFileName);
        [DllImport(Constants.dllName)]
        private static extern Shader LoadShaderFromMemory(string vsCode, string fsCode);
        [DllImport(Constants.dllName)]
        private static extern int GetShaderLocation(Shader shader, string uniformName);
        [DllImport(Constants.dllName)]
        private static extern int GetShaderLocationAttrib(Shader shader, string attribName);
        [DllImport(Constants.dllName)]
        private static extern void SetShaderValueV(Shader shader, ShaderLocationIndex locIndex, IntPtr value, ShaderUniformDataType uniformType, int count);
        [DllImport(Constants.dllName)]
        private static extern void SetShaderValueMatrix(Shader shader, ShaderLocationIndex locIndex, Matrix4x4 mat);
        [DllImport(Constants.dllName)]
        private static extern void SetShaderValueTexture(Shader shader, ShaderLocationIndex locIndex, Texture2D texture);
        [DllImport(Constants.dllName)]
        private static extern void UnloadShader(Shader shader);

        [DllImport(Constants.dllName)]
        private static extern void BeginShaderMode(Shader shader);
        [DllImport(Constants.dllName)]
        private static extern void EndShaderMode();

        /// <summary>
        /// Shader location index
        /// </summary>
        public enum ShaderLocationIndex : int
        {
            /// <summary>
            /// Shader location: vertex attribute: position
            /// </summary>
            VERTEX_POSITION = 0,
            /// <summary>
            /// Shader location: vertex attribute: texcoord01
            /// </summary>
            VERTEX_TEXCOORD01 = 1,
            /// <summary>
            /// Shader location: vertex attribute: texcoord02
            /// </summary>
            VERTEX_TEXCOORD02 = 2,
            /// <summary>
            /// Shader location: vertex attribute: normal
            /// </summary>
            VERTEX_NORMAL = 3,
            /// <summary>
            /// Shader location: vertex attribute: tangent
            /// </summary>
            VERTEX_TANGENT = 4,
            /// <summary>
            /// Shader location: vertex attribute: color
            /// </summary>
            VERTEX_COLOR = 5,
            /// <summary>
            /// Shader location: matrix uniform: model-view-projection
            /// </summary>
            MATRIX_MVP = 6,
            /// <summary>
            /// Shader location: matrix uniform: view (camera transform)
            /// </summary>
            MATRIX_VIEW = 7,
            /// <summary>
            /// Shader location: matrix uniform: projection
            /// </summary>
            MATRIX_PROJECTION = 8,
            /// <summary>
            /// Shader location: matrix uniform: model (transform)
            /// </summary>
            MATRIX_MODEL = 9,
            /// <summary>
            /// Shader location: matrix uniform: normal
            /// </summary>
            MATRIX_NORMAL = 10,
            /// <summary>
            /// Shader location: vector uniform: view
            /// </summary>
            VECTOR_VIEW = 11,
            /// <summary>
            /// Shader location: vector uniform: diffuse color
            /// </summary>
            COLOR_DIFFUSE = 12,
            /// <summary>
            /// Shader location: vector uniform: specular color
            /// </summary>
            COLOR_SPECULAR = 13,
            /// <summary>
            /// Shader location: vector uniform: ambient color
            /// </summary>
            COLOR_AMBIENT = 14,
            /// <summary>
            /// Shader location: sampler2d texture: albedo (same as: MAP_DIFFUSE)
            /// </summary>
            MAP_ALBEDO = 15,
            /// <summary>
            /// Shader location: sampler2d texture: metalness (same as: MAP_SPECULAR)
            /// </summary>
            MAP_METALNESS = 16,
            /// <summary>
            /// Shader location: sampler2d texture: normal
            /// </summary>
            MAP_NORMAL = 17,
            /// <summary>
            /// Shader location: sampler2d texture: roughness
            /// </summary>
            MAP_ROUGHNESS = 18,
            /// <summary>
            /// Shader location: sampler2d texture: occlusion
            /// </summary>
            MAP_OCCLUSION = 19,
            /// <summary>
            /// Shader location: sampler2d texture: emission
            /// </summary>
            MAP_EMISSION = 20,
            /// <summary>
            /// Shader location: sampler2d texture: height
            /// </summary>
            MAP_HEIGHT = 21,
            /// <summary>
            /// Shader location: samplerCube texture: cubemap
            /// </summary>
            MAP_CUBEMAP = 22,
            /// <summary>
            /// Shader location: samplerCube texture: irradiance
            /// </summary>
            MAP_IRRADIANCE = 23,
            /// <summary>
            /// Shader location: samplerCube texture: prefilter
            /// </summary>
            MAP_PREFILTER = 24,
            /// <summary>
            /// Shader location: sampler2d texture: brdf
            /// </summary>
            MAP_BRDF = 25,
            /// <summary>
            /// Shader location: sampler2d texture: albedo (same as: MAP_ALBEDO)
            /// </summary>
            MAP_DIFFUSE = MAP_ALBEDO,
            /// <summary>
            /// Shader location: sampler2d texture: metalness (same as: MAP_METALNESS)
            /// </summary>
            MAP_SPECULAR = MAP_METALNESS
        }

        /// <summary>
        /// Shader uniform data type
        /// </summary>
        public enum ShaderUniformDataType : int
        {
            /// <summary>
            /// Shader uniform type: float
            /// </summary>
            FLOAT = 0,
            /// <summary>
            /// Shader uniform type: vec2 (2 float)
            /// </summary>
            VEC2 = 1,
            /// <summary>
            /// Shader uniform type: vec3 (3 float)
            /// </summary>
            VEC3 = 2,
            /// <summary>
            /// Shader uniform type: vec4 (4 float)
            /// </summary>
            VEC4 = 3,
            /// <summary>
            /// Shader uniform type: int
            /// </summary>
            INT = 4,
            /// <summary>
            /// Shader uniform type: ivec2 (2 int)
            /// </summary>
            IVEC2 = 5,
            /// <summary>
            /// Shader uniform type: ivec3 (3 int)
            /// </summary>
            IVEC3 = 6,
            /// <summary>
            /// Shader uniform type: ivec4 (4 int)
            /// </summary>
            IVEC4 = 7,
            /// <summary>
            /// Shader uniform type: sampler2d
            /// </summary>
            SAMPLER2D = 8
        }

        /// <summary>
        /// Shader program id
        /// </summary>
        public uint Id { get; private set; }
        /// <summary>
        /// Shader locations array (MAX_SHADER_LOCATIONS, IntPtr)
        /// </summary>
        public IntPtr Locs { get; private set; }

        /// <summary>
        /// Load shader from files/code strings and bind default locations
        /// </summary>
        /// <param name="vs">Vertex shader filepath/code string</param>
        /// <param name="fs">Framgent shader filepath/code string</param>
        /// <param name="file">Is parameter a filepath</param>
        public Shader(string vs, string fs, bool file = true)
        {
            Shader shader = file ? LoadShader(vs, fs) : LoadShaderFromMemory(vs, fs);

            Id = shader.Id;
            Locs = shader.Locs;
        }

        /// <summary>
        /// Unload shader from GPU memory (VRAM)
        /// </summary>
        public void Dispose()
        {
            UnloadShader(this);
            Id = 0;
            Locs = IntPtr.Zero;
        }

        /// <summary>
        /// Begin custom shader drawing
        /// </summary>
        public void Begin() => BeginShaderMode(this);
        /// <summary>
        /// End custom shader drawing (use default shader)
        /// </summary>
        public void End() => EndShaderMode();

        /// <summary>
        /// Get shader uniform location
        /// </summary>
        /// <param name="uniformName">Uniform name</param>
        /// <returns>Uniform location</returns>
        public int GetLocation(string uniformName) => GetShaderLocation(this, uniformName);
        /// <summary>
        /// Get shader attribute location
        /// </summary>
        /// <param name="attribName">Attribute name</param>
        /// <returns>Attribue location</returns>
        public int GetLocationAttrib(string attribName) => GetShaderLocationAttrib(this, attribName);
        /// <summary>
        /// Set shader uniform value vector
        /// </summary>
        /// <param name="locIndex">Location index</param>
        /// <param name="value">new value(s)</param>
        /// <param name="uniformType">Uniform type</param>
        /// <param name="count">Number of values</param>
        public void SetValue(ShaderLocationIndex locIndex, IntPtr value, ShaderUniformDataType uniformType, int count = 1) => SetShaderValueV(this, locIndex, value, uniformType, count);
        /// <summary>
        /// Set shader uniform value (matrix 4x4)
        /// </summary>
        /// <param name="locIndex">Location index</param>
        /// <param name="mat">Matrix</param>
        public void SetValueMatrix(ShaderLocationIndex locIndex, Matrix4x4 mat) => SetShaderValueMatrix(this, locIndex, mat);
        /// <summary>
        /// Set shader uniform value for texture (sampler2d)
        /// </summary>
        /// <param name="locIndex">Location index</param>
        /// <param name="texture">Texture</param>
        public void SetValueTexture(ShaderLocationIndex locIndex, Texture2D texture) => SetShaderValueTexture(this, locIndex, texture);
    }
}
