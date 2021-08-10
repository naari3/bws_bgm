using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000039 RID: 57
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/chromatic-aberration.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Chromatic Aberration")]
	public class ChromaticAberration : BaseEffect
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x0000A918 File Offset: 0x00008B18
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Refraction", new Vector3(this.RedRefraction, this.GreenRefraction, this.BlueRefraction));
			Graphics.Blit(source, destination, base.Material, this.PreserveAlpha ? 1 : 0);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A96A File Offset: 0x00008B6A
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Chromatic Aberration";
		}

		// Token: 0x04000126 RID: 294
		[Range(0.9f, 1.1f)]
		[Tooltip("Indice of refraction for the red channel.")]
		public float RedRefraction = 1f;

		// Token: 0x04000127 RID: 295
		[Range(0.9f, 1.1f)]
		[Tooltip("Indice of refraction for the green channel.")]
		public float GreenRefraction = 1.005f;

		// Token: 0x04000128 RID: 296
		[Range(0.9f, 1.1f)]
		[Tooltip("Indice of refraction for the blue channel.")]
		public float BlueRefraction = 1.01f;

		// Token: 0x04000129 RID: 297
		[Tooltip("Enable this option if you need the effect to keep the alpha channel from the original render (some effects like Glow will need it). Disable it otherwise for better performances.")]
		public bool PreserveAlpha;
	}
}
