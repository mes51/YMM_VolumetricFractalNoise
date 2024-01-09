using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vortice;
using Vortice.Direct2D1;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace YMM_VolumetricFractalNoise
{
    class VolumetricFractalNoiseCustomEffect(IGraphicsDevicesAndContext devices) : D2D1CustomShaderEffectBase(Create<EffectImpl>(devices))
    {
        public Vector4 Color1
        {
            get => GetVector4Value((int)EffectProperty.Color1);
            set => SetValue((int)EffectProperty.Color1, value);
        }

        public Vector4 Color2
        {
            get => GetVector4Value((int)EffectProperty.Color2);
            set => SetValue((int)EffectProperty.Color2, value);
        }

        public float Contrast
        {
            get => GetFloatValue((int)EffectProperty.Contrast);
            set => SetValue((int)EffectProperty.Contrast, value);
        }

        public float Density
        {
            get => GetFloatValue((int)EffectProperty.Density);
            set => SetValue((int)EffectProperty.Density, value);
        }

        public Vector3 NoiseTranslate
        {
            get => GetVector3Value((int)EffectProperty.NoiseTranslate);
            set => SetValue((int)EffectProperty.NoiseTranslate, value);
        }

        public Vector3 NoiseScale
        {
            get => GetVector3Value((int)EffectProperty.NoiseScale);
            set => SetValue((int)EffectProperty.NoiseScale, value);
        }

        public Vector3 NoiseRadian
        {
            get => GetVector3Value((int)EffectProperty.NoiseRadian);
            set => SetValue((int)EffectProperty.NoiseRadian, value);
        }

        public int Octave
        {
            get => GetIntValue((int)EffectProperty.Octave);
            set => SetValue((int)EffectProperty.Octave, value);
        }

        public float Evolution
        {
            get => GetFloatValue((int)EffectProperty.Evolution);
            set => SetValue((int)EffectProperty.Evolution, value);
        }

        public Vector3 FalloffRegion
        {
            get => GetVector3Value((int)EffectProperty.FalloffRegion);
            set => SetValue((int)EffectProperty.FalloffRegion, value);
        }

        public float FalloffPower
        {
            get => GetFloatValue((int)EffectProperty.FalloffPower);
            set => SetValue((int)EffectProperty.FalloffPower, value);
        }

        public Vector3 GeometryPosition
        {
            get => GetVector3Value((int)EffectProperty.GeometryPosition);
            set => SetValue((int)EffectProperty.GeometryPosition, value);
        }

        public Vector3 GeometrySize
        {
            get => GetVector3Value((int)EffectProperty.GeometrySize);
            set => SetValue((int)EffectProperty.GeometrySize, value);
        }

        public Vector3 GeometryRadian
        {
            get => GetVector3Value((int)EffectProperty.GeometryRadian);
            set => SetValue((int)EffectProperty.GeometryRadian, value);
        }

        public float FieldOfView
        {
            get => GetFloatValue((int)EffectProperty.FieldOfView);
            set => SetValue((int)EffectProperty.FieldOfView, value);
        }

        public float CastResolution
        {
            get => GetFloatValue((int)EffectProperty.CastResolution);
            set => SetValue((int)EffectProperty.CastResolution, value);
        }

        public float DistanceAttenuation
        {
            get => GetFloatValue((int)EffectProperty.DistanceAttenuation);
            set => SetValue((int)EffectProperty.DistanceAttenuation, value);
        }

        public int UpdateKey
        {
            get => GetIntValue((int)EffectProperty.UpdateKey);
            set => SetValue((int)EffectProperty.UpdateKey, value);
        }
    }

    [CustomEffect(1)]
    file class EffectImpl : D2D1CustomShaderEffectImplBase<EffectImpl>
    {
        [CustomEffectProperty(PropertyType.Vector4, (int)EffectProperty.Color1)]
        public Vector4 Color1 { get; set; }

        [CustomEffectProperty(PropertyType.Vector4, (int)EffectProperty.Color2)]
        public Vector4 Color2 { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.Contrast)]
        public float Contrast { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.Density)]
        public float Density { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.NoiseTranslate)]
        public Vector3 NoiseTranslate { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.NoiseScale)]
        public Vector3 NoiseScale { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.NoiseRadian)]
        public Vector3 NoiseRadian { get; set; }

        [CustomEffectProperty(PropertyType.Int32, (int)EffectProperty.Octave)]
        public int Octave { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.Evolution)]
        public float Evolution { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.FalloffRegion)]
        public Vector3 FalloffRegion { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.FalloffPower)]
        public float FalloffPower { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.GeometryPosition)]
        public Vector3 GeometryPosition { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.GeometrySize)]
        public Vector3 GeometrySize { get; set; }

        [CustomEffectProperty(PropertyType.Vector3, (int)EffectProperty.GeometryRadian)]
        public Vector3 GeometryRadian { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.FieldOfView)]
        public float FieldOfView { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.CastResolution)]
        public float CastResolution { get; set; }

        [CustomEffectProperty(PropertyType.Float, (int)EffectProperty.DistanceAttenuation)]
        public float DistanceAttenuation { get; set; }

        // 更新用プロパティ
        int updateKey = 0;
        [CustomEffectProperty(PropertyType.Int32, (int)EffectProperty.UpdateKey)]
        public int UpdateKey
        {
            get => updateKey;
            set
            {
                updateKey = value;
                UpdateConstants();
            }
        }

        public EffectImpl() : base(GetShader()) { }

        protected override void UpdateConstants()
        {
            var noiseRotateAndScale = Matrix4x4.CreateScale(NoiseScale.X, NoiseScale.Y, NoiseScale.Z)
                * Matrix4x4.CreateRotationX(-NoiseRadian.X)
                * Matrix4x4.CreateRotationY(-NoiseRadian.Y)
                * Matrix4x4.CreateRotationZ(NoiseRadian.Z);
            var geometryRotation = Matrix4x4.CreateRotationX(-GeometryRadian.X)
                * Matrix4x4.CreateRotationY(-GeometryRadian.Y)
                * Matrix4x4.CreateRotationZ(GeometryRadian.Z);
            Matrix4x4.Invert(noiseRotateAndScale, out var invertedNoiseRotateAndScale);
            Matrix4x4.Invert(geometryRotation, out var invertedGeometryRotation);

            drawInformation?.SetPixelShaderConstantBuffer(
                new EffectParameter(
                    Color1,
                    Color2,
                    NoiseTranslate,
                    invertedNoiseRotateAndScale,
                    Contrast,
                    Density,
                    Octave,
                    Evolution,
                    FalloffRegion,
                    FalloffPower,
                    GeometrySize,
                    GeometryPosition,
                    invertedGeometryRotation,
                    FieldOfView,
                    CastResolution,
                    DistanceAttenuation
                )
            );
        }

        static byte[] GetShader()
        {
            var assembly = typeof(EffectImpl).Assembly;
            using var stream = assembly.GetManifestResourceStream("VolumetricFractalNoise_Shader.cso");
            if (stream != null)
            {
                var result = new byte[stream.Length];
                stream.Read(result, 0, result.Length);
                return result;
            }
            else
            {
                return [];
            }
        }
    }

    file enum EffectProperty : int
    {
        Color1 = 0,
        Color2,
        Contrast,
        Density,
        NoiseTranslate,
        NoiseScale,
        NoiseRadian,
        Octave,
        Evolution,

        FalloffRegion,
        FalloffPower,

        GeometryPosition,
        GeometrySize,
        GeometryRadian,

        FieldOfView,
        CastResolution,
        DistanceAttenuation,

        // 定数更新用プロパティ
        UpdateKey
    }

    [StructLayout(LayoutKind.Sequential)]
    file readonly record struct EffectParameter(
        Vector4 Color1,
        Vector4 Color2,
        Vector3 NoiseTranslate,
        Matrix4x4 NoiseRotateAndScale, 
        float Contrast,
        float Density,
        int Octave,
        float Evolution,
        Vector3 FalloffRegion,
        float FalloffPower,
        Vector3 GeometrySize,
        Vector3 GeometryPosition,
        Matrix4x4 GeometryRotation,
        float FieldOfView,
        float CastResolution,
        float DistanceAttenuation
    )
    {
        public readonly Vector4 Color1 = Color1;

        public readonly Vector4 Color2 = Color2;

        public readonly Vector3 NoiseTranslate = NoiseTranslate;

        readonly int Filler1 = 0;

        public readonly Matrix4x4 NoiseRotateAndScale = NoiseRotateAndScale;

        public readonly float Contrast = Contrast;

        public readonly float Density = Density;

        public readonly int Octave = Octave;

        public readonly float Evolution = Evolution;

        public readonly Vector3 FalloffRegion = FalloffRegion;

        public readonly float FalloffPower = FalloffPower;

        public readonly Vector3 GeometrySize = GeometrySize;

        readonly int Filler2 = 0;

        public readonly Vector3 GeometryPosition = GeometryPosition;

        readonly int Filler3 = 0;

        public readonly Matrix4x4 GeometryRotation = GeometryRotation;

        public readonly float FieldOfView = FieldOfView;

        public readonly float CastResolution = CastResolution;

        public readonly float DistanceAttenuation = DistanceAttenuation;
    }
}
