using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200006A RID: 106
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/wave-distortion.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Wave Distortion")]
	public class WaveDistortion : BaseEffect
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000E094 File Offset: 0x0000C294
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = CLib.Frac(this.Phase);
			if (num == 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector4(this.Amplitude, this.Waves, this.ColorGlitch, num));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Wave Distortion";
		}

		// Token: 0x0400020C RID: 524
		[Range(0f, 1f)]
		[Tooltip("Wave amplitude.")]
		public float Amplitude = 0.6f;

		// Token: 0x0400020D RID: 525
		[Tooltip("Amount of waves.")]
		public float Waves = 5f;

		// Token: 0x0400020E RID: 526
		[Range(0f, 5f)]
		[Tooltip("Amount of color shifting.")]
		public float ColorGlitch = 0.35f;

		// Token: 0x0400020F RID: 527
		[Tooltip("Distortion state. Think of it as a bell curve going from 0 to 1, with 0.5 being the highest point.")]
		public float Phase = 0.35f;
	}
}
