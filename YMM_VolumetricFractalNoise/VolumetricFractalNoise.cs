using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin.Effects;

namespace YMM_VolumetricFractalNoise
{
    [VideoEffect("Volumetric FractalNoise", ["加工"], [], IsAviUtlSupported = false)]
    public class VolumetricFractalNoise : VideoEffectBase
    {
        public override string Label => "Volumetric FractalNoise";

        [Display(GroupName = "ノイズ", AutoGenerateField = true)]
        public NoisePropertyGroup NoiseProperty { get; } = new NoisePropertyGroup();

        [Display(GroupName = "ノイズの表示領域", AutoGenerateField = true)]
        public GeometryPropertyGroup GeometryProperty { get; } = new GeometryPropertyGroup();

        [Display(GroupName = "境界でのノイズの減衰", AutoGenerateField = true)]
        public FalloffPropertyGroup FalloffProperty { get; } = new FalloffPropertyGroup();

        [Display(GroupName = "レンダリング設定", AutoGenerateField = true)]
        public RenderPropertyGroup RenderProperty { get; } = new RenderPropertyGroup();

        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            return [];
        }

        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new VolumetricFractalNoiseProcessor(devices, this);
        }

        protected override IEnumerable<IAnimatable> GetAnimatables()
        {
            return new IAnimatable[] { NoiseProperty, FalloffProperty, GeometryProperty, RenderProperty };
        }
    }
}
