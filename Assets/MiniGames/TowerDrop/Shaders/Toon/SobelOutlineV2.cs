using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(OutlineRenderer), PostProcessEvent.BeforeStack, "Custom/OutlineV2")]
public sealed class SobelOutlineV2 : PostProcessEffectSettings
{ 
    public FloatParameter edgeExp = new FloatParameter { value = 1.0f };
    public FloatParameter sampleDist = new FloatParameter { value = 1.0f };

    [Range(0.0f, 1.0f)]
    public FloatParameter fadeStength = new FloatParameter { value = 0.0f };
    public ColorParameter fadeColor = new ColorParameter { value = Color.white };

}

public sealed class OutlineRenderer : PostProcessEffectRenderer<SobelOutlineV2>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/SobelOutline"));

        sheet.properties.SetFloat("_FadeStength", settings.fadeStength);
        sheet.properties.SetFloat("_SampleDistance", settings.sampleDist);
        sheet.properties.SetVector("_FadeColor", settings.fadeColor.value);
        sheet.properties.SetFloat("_Exponent", settings.edgeExp);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}