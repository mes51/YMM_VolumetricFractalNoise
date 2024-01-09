using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vortice.Direct2D1;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace YMM_VolumetricFractalNoise
{
    class VolumetricFractalNoiseProcessor : IVideoEffectProcessor
    {
        public ID2D1Image Output => OutputInternal ?? Input ?? throw new NullReferenceException();

        ID2D1Image? Input { get; set; }

        ID2D1Image? OutputInternal { get; set; }

        VolumetricFractalNoiseCustomEffect Effect { get; set; }

        VolumetricFractalNoise Item { get; }

        int PrevPropertyHashCode { get; set; }

        public VolumetricFractalNoiseProcessor(IGraphicsDevicesAndContext devices, VolumetricFractalNoise item)
        {
            Item = item;
            Effect = new VolumetricFractalNoiseCustomEffect(devices);
            if (Effect.IsEnabled)
            {
                OutputInternal = Effect.Output;
            }
        }

        public void ClearInput()
        {
            if (Effect.IsEnabled)
            {
                Effect.SetInput(0, null, true);
            }
        }

        public void SetInput(ID2D1Image input)
        {
            Input = input;
            if (Effect.IsEnabled)
            {
                Effect.SetInput(0, input, true);
            }
        }

        public DrawDescription Update(EffectDescription effectDescription)
        {
            if (!Effect.IsEnabled)
            {
                return effectDescription.DrawDescription;
            }

            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;

            var color1 = Item.NoiseProperty.Color1;
            var color2 = Item.NoiseProperty.Color2;
            var noiseTranslate = new Vector3(
                (float)Item.NoiseProperty.TranslateX.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.TranslateY.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.TranslateZ.GetValue(frame, length, fps)
            );
            var noiseScale = new Vector3(
                (float)Item.NoiseProperty.ScaleX.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.ScaleY.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.ScaleZ.GetValue(frame, length, fps)
            );
            var noiseRotate = new Vector3(
                (float)Item.NoiseProperty.RotateX.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.RotateY.GetValue(frame, length, fps),
                (float)Item.NoiseProperty.RotateZ.GetValue(frame, length, fps)
            );
            var contrast = (float)Item.NoiseProperty.Contrast.GetValue(frame, length, fps);
            var density = (float)Item.NoiseProperty.Density.GetValue(frame, length, fps);
            var octave = (int)Item.NoiseProperty.Octave.GetValue(frame, length, fps);
            var evolution = (float)Item.NoiseProperty.Evolution.GetValue(frame, length, fps);

            var fallofRegion = new Vector3(
                (float)Item.FalloffProperty.SizeX.GetValue(frame, length, fps),
                (float)Item.FalloffProperty.SizeY.GetValue(frame, length, fps),
                (float)Item.FalloffProperty.SizeZ.GetValue(frame, length, fps)
            );
            var falloffPower = (float)Item.FalloffProperty.Power.GetValue(frame, length, fps);

            var geometrySize = new Vector3(
                (float)Item.GeometryProperty.SizeX.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.SizeY.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.SizeZ.GetValue(frame, length, fps)
            );
            var geometryPosition = new Vector3(
                (float)Item.GeometryProperty.TranslateX.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.TranslateY.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.TranslateZ.GetValue(frame, length, fps)
            );
            var geometryRotate = new Vector3(
                (float)Item.GeometryProperty.RotateX.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.RotateY.GetValue(frame, length, fps),
                (float)Item.GeometryProperty.RotateZ.GetValue(frame, length, fps)
            );

            var fieldOfView = (float)Item.RenderProperty.FieldOfView.GetValue(frame, length, fps);
            var castResolution = (float)Item.RenderProperty.CastResolution.GetValue(frame, length, fps);
            var distanceAttenuation = (float)Item.RenderProperty.DistanceAttenuation.GetValue(frame, length, fps);

            var hashCode = new HashCode();
            hashCode.Add(color1);
            hashCode.Add(color2);
            hashCode.Add(noiseTranslate);
            hashCode.Add(noiseScale);
            hashCode.Add(noiseRotate);
            hashCode.Add(contrast);
            hashCode.Add(density);
            hashCode.Add(octave);
            hashCode.Add(evolution);
            hashCode.Add(fallofRegion);
            hashCode.Add(falloffPower);
            hashCode.Add(geometrySize);
            hashCode.Add(geometryPosition);
            hashCode.Add(geometryRotate);
            hashCode.Add(fieldOfView);
            hashCode.Add(castResolution);
            hashCode.Add(distanceAttenuation);

            var newHashCode = hashCode.ToHashCode();
            if (PrevPropertyHashCode != newHashCode)
            {
                var color1Vec = new Vector4(color1.R, color1.G, color1.B, color1.A) / 255.0F;
                var color2Vec = new Vector4(color2.R, color2.G, color2.B, color2.A) / 255.0F;
                Effect.Color1 = color1Vec * new Vector4(color1Vec.W, color1Vec.W, color1Vec.W, 1.0F);
                Effect.Color2 = color2Vec * new Vector4(color2Vec.W, color2Vec.W, color2Vec.W, 1.0F);
                Effect.NoiseTranslate = noiseTranslate;
                Effect.NoiseScale = noiseScale * 0.01F;
                Effect.NoiseRadian = noiseRotate / 180.0F * MathF.PI;
                Effect.Contrast = contrast * 0.01F;
                Effect.Density = density;
                Effect.Octave = octave;
                Effect.Evolution = evolution;
                Effect.FalloffRegion = fallofRegion;
                Effect.FalloffPower = falloffPower;
                Effect.GeometrySize = geometrySize;
                Effect.GeometryPosition = geometryPosition;
                Effect.GeometryRadian = geometryRotate / 180.0F * MathF.PI;
                Effect.FieldOfView = fieldOfView / 180.0F * MathF.PI;
                Effect.CastResolution = castResolution * 0.1F;
                Effect.DistanceAttenuation = distanceAttenuation * 0.01F;

                Effect.UpdateKey = newHashCode;
                PrevPropertyHashCode = newHashCode;
            }

            return effectDescription.DrawDescription;
        }

        public void Dispose()
        {
            Output?.Dispose();
            if (Effect.IsEnabled)
            {
                Effect.SetInput(0, null, true);
            }
            Effect.Dispose();
        }
    }
}
