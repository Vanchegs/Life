using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class VHS_RenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material;
        public float scanlineIntensity = 0.1f;
        public int scanlineCount = 100;
        public float noiseIntensity = 0.2f;
        public float vignetteIntensity = 0.5f;
        public float colorShift = 0.1f;
        public float glitchIntensity = 0.1f;
        public float glitchFrequency = 10f;
        public float vhsDistortion = 0.1f;
    }

    public Settings settings = new Settings();
    private VHSRenderPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new VHSRenderPass(settings);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.material == null) return;
        renderer.EnqueuePass(m_ScriptablePass);
    }

    private class VHSRenderPass : ScriptableRenderPass
    {
        private Material material;
        private Settings settings;

        // Кэшируем ID для быстрого доступа
        private static readonly int ScanlineIntensityID = Shader.PropertyToID("_ScanlineIntensity");
        private static readonly int ScanlineCountID = Shader.PropertyToID("_ScanlineCount");
        private static readonly int NoiseIntensityID = Shader.PropertyToID("_NoiseIntensity");
        private static readonly int VignetteIntensityID = Shader.PropertyToID("_VignetteIntensity");
        private static readonly int ColorShiftID = Shader.PropertyToID("_ColorShift");
        private static readonly int GlitchIntensityID = Shader.PropertyToID("_GlitchIntensity");
        private static readonly int GlitchFrequencyID = Shader.PropertyToID("_GlitchFrequency");
        private static readonly int VHSDistortionID = Shader.PropertyToID("_VHSDistortion");

        private class PassData
        {
            public TextureHandle source;
            public TextureHandle destination;
            public Material material;
            public Settings settings;
        }

        public VHSRenderPass(Settings settings)
        {
            this.settings = settings;
            this.material = settings.material;
            this.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (material == null) return;

            UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
            UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();

            // Получаем исходный цвет камеры
            TextureHandle source = resourceData.activeColorTexture;
            
            // Создаем временную текстуру (куда будем писать результат)
            TextureHandle destination = UniversalRenderer.CreateRenderGraphTexture(
                renderGraph,
                cameraData.cameraTargetDescriptor,
                "_TempVHSTexture",
                true
            );

            using (var builder = renderGraph.AddRasterRenderPass<PassData>("VHS_Effect", out var passData))
            {
                passData.source = source;
                passData.destination = destination;
                passData.material = material;
                passData.settings = settings;

                builder.UseTexture(source, AccessFlags.Read);
                builder.UseTexture(destination, AccessFlags.Write);
                builder.SetRenderAttachment(destination, 0);

                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                {
                    // Настраиваем материал ДО блита
                    data.material.SetFloat(ScanlineIntensityID, data.settings.scanlineIntensity);
                    data.material.SetInt(ScanlineCountID, data.settings.scanlineCount);
                    data.material.SetFloat(NoiseIntensityID, data.settings.noiseIntensity);
                    data.material.SetFloat(VignetteIntensityID, data.settings.vignetteIntensity);
                    data.material.SetFloat(ColorShiftID, data.settings.colorShift);
                    data.material.SetFloat(GlitchIntensityID, data.settings.glitchIntensity);
                    data.material.SetFloat(GlitchFrequencyID, data.settings.glitchFrequency);
                    data.material.SetFloat(VHSDistortionID, data.settings.vhsDistortion);

                    // 1. Копируем исходный экран в destination
                    Blitter.BlitTexture(context.cmd, data.source, Vector4.one, data.material, 0);
                });
            }
        }
    }
}