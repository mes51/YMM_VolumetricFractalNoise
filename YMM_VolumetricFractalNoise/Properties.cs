using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;

namespace YMM_VolumetricFractalNoise
{
    public class NoisePropertyGroup : Animatable
    {
        Color color1 = Colors.White;
        [Display(Name = "色1", Description = "ノイズの色1")]
        [ColorPicker]
        public Color Color1
        {
            get => color1;
            set
            {
                Set(ref color1, value);
            }
        }

        Color color2 = new Color();
        [Display(Name = "色2", Description = "ノイズの色2")]
        [ColorPicker]
        public Color Color2
        {
            get => color2;
            set
            {
                Set(ref color2, value);
            }
        }

        [Display(Name = "コントラスト", Description = "ノイズのコントラスト")]
        [AnimationSlider("F1", "%", 0.0, 100.0)]
        public Animation Contrast { get; } = new Animation(100.0, 0.0, 500.0);

        [Display(Name = "密度", Description = "ノイズのコントラスト")]
        [AnimationSlider("F2", "", 0.0, 10.0)]
        public Animation Density { get; } = new Animation(1.0, 0.0, 10.0);

        [Display(Name = "X位置", Description = "X位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateX { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Y位置", Description = "Y位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateY { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Z位置", Description = "Z位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateZ { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Xスケール", Description = "Xスケール")]
        [AnimationSlider("F2", "%", 0.0, 100.0)]
        public Animation ScaleX { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "Yスケール", Description = "Yスケール")]
        [AnimationSlider("F2", "%", 0.0, 100.0)]
        public Animation ScaleY { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "Zスケール", Description = "Zスケール")]
        [AnimationSlider("F2", "%", 0.0, 100.0)]
        public Animation ScaleZ { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "X回転", Description = "X回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateX { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Y回転", Description = "Y回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateY { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Z回転", Description = "Z回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateZ { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "複雑度", Description = "複雑度")]
        [AnimationSlider("F0", "", 1.0, 20.0)]
        public Animation Octave { get; } = new Animation(6.0, 1.0, 20.0);

        [Display(Name = "展開", Description = "展開")]
        [AnimationSlider("F1", "°", -36.0, 36.0)]
        public Animation Evolution { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        protected override IEnumerable<IAnimatable> GetAnimatables()
        {
            return new IAnimatable[] { Contrast, Density, TranslateX, TranslateY, TranslateZ, ScaleX, ScaleY, ScaleZ, RotateX, RotateY, RotateZ, Octave, Evolution };
        }
    }

    public class GeometryPropertyGroup : Animatable
    {
        [Display(Name = "幅", Description = "幅")]
        [AnimationSlider("F2", "", 0.0, 100.0)]
        public Animation SizeX { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "高さ", Description = "高さ")]
        [AnimationSlider("F2", "", 0.0, 100.0)]
        public Animation SizeY { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "奥行き", Description = "奥行き")]
        [AnimationSlider("F2", "", 0.0, 100.0)]
        public Animation SizeZ { get; } = new Animation(100.0, 0.0, float.MaxValue);

        [Display(Name = "X位置", Description = "X位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateX { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Y位置", Description = "Y位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateY { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Z位置", Description = "Z位置")]
        [AnimationSlider("F1", "", -1000.0, 1000.0)]
        public Animation TranslateZ { get; } = new Animation(500.0, float.MinValue, float.MaxValue);

        [Display(Name = "X回転", Description = "X回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateX { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Y回転", Description = "Y回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateY { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        [Display(Name = "Z回転", Description = "Z回転")]
        [AnimationSlider("F1", "°", -360.0, 360.0)]
        public Animation RotateZ { get; } = new Animation(0.0, float.MinValue, float.MaxValue);

        protected override IEnumerable<IAnimatable> GetAnimatables()
        {
            return new IAnimatable[] { TranslateX, TranslateY, TranslateZ, SizeX, SizeY, SizeZ, RotateX, RotateY, RotateZ };
        }
    }

    public class FalloffPropertyGroup : Animatable
    {
        [Display(Name = "X", Description = "X")]
        [AnimationSlider("F1", "", 0.0, 100.0)]
        public Animation SizeX { get; } = new Animation(0.0, 0.0, float.MaxValue);

        [Display(Name = "Y", Description = "Y")]
        [AnimationSlider("F1", "", 0.0, 100.0)]
        public Animation SizeY { get; } = new Animation(0.0, 0.0, float.MaxValue);

        [Display(Name = "Z", Description = "Z")]
        [AnimationSlider("F1", "", 0.0, 100.0)]
        public Animation SizeZ { get; } = new Animation(0.0, 0.0, float.MaxValue);

        [Display(Name = "減衰量", Description = "減衰量")]
        [AnimationSlider("F1", "", 0.0, 100.0)]
        public Animation Power { get; } = new Animation(0.0, 0.0, 1000.0);

        protected override IEnumerable<IAnimatable> GetAnimatables()
        {
            return new IAnimatable[] { SizeX, SizeY, SizeZ, Power };
        }
    }

    public class RenderPropertyGroup : Animatable
    {
        [Display(Name = "視野角", Description = "視野角")]
        [AnimationSlider("F3", "°", 1.0, 180.0)]
        public Animation FieldOfView { get; } = new Animation(54.4322, 1.0, 180.0);

        [Display(Name = "レンダリング粒度", Description = "レンダリング粒度")]
        [AnimationSlider("F0", "", 1.0, 100.0)]
        public Animation CastResolution { get; } = new Animation(10.0, 1.0, 10000.0);

        [Display(Name = "視点からの減衰率", Description = "視点からの減衰率")]
        [AnimationSlider("F1", "", 0.0, 100.0)]
        public Animation DistanceAttenuation { get; } = new Animation(0.0, 0.0, 10000.0);

        protected override IEnumerable<IAnimatable> GetAnimatables()
        {
            return new IAnimatable[] { FieldOfView, CastResolution, DistanceAttenuation };
        }
    }
}
