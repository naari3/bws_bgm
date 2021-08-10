using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000031 RID: 49
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/analog-tv.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Analog TV")]
	public class AnalogTV : BaseEffect
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00009F85 File Offset: 0x00008185
		protected virtual void Update()
		{
			if (this.AutomaticPhase)
			{
				if (this.Phase > 10f)
				{
					this.Phase = 1f;
				}
				this.Phase += Time.deltaTime * 0.25f;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00009FC0 File Offset: 0x000081C0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Params1", new Vector4(this.NoiseIntensity, this.ScanlinesIntensity, (float)this.ScanlinesCount, this.ScanlinesOffset));
			base.Material.SetVector("_Params2", new Vector4(this.Phase, this.Distortion, this.CubicDistortion, this.Scale));
			int num = this.VerticalScanlines ? 2 : 0;
			num += (this.ConvertToGrayscale ? 1 : 0);
			Graphics.Blit(source, destination, base.Material, num);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A052 File Offset: 0x00008252
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Analog TV";
		}

		// Token: 0x04000105 RID: 261
		[Tooltip("Automatically animate the Phase value.")]
		public bool AutomaticPhase = true;

		// Token: 0x04000106 RID: 262
		[Tooltip("Current noise phase. Consider this a seed value.")]
		public float Phase = 0.5f;

		// Token: 0x04000107 RID: 263
		[Tooltip("Convert the original render to black & white.")]
		public bool ConvertToGrayscale;

		// Token: 0x04000108 RID: 264
		[Range(0f, 1f)]
		[Tooltip("Noise brightness. Will impact the scanlines visibility.")]
		public float NoiseIntensity = 0.5f;

		// Token: 0x04000109 RID: 265
		[Range(0f, 10f)]
		[Tooltip("Scanline brightness. Depends on the NoiseIntensity value.")]
		public float ScanlinesIntensity = 2f;

		// Token: 0x0400010A RID: 266
		[Range(0f, 4096f)]
		[Tooltip("The number of scanlines to draw.")]
		public int ScanlinesCount = 768;

		// Token: 0x0400010B RID: 267
		[Tooltip("Scanline offset. Gives a cool screen scanning effect when animated.")]
		public float ScanlinesOffset;

		// Token: 0x0400010C RID: 268
		[Tooltip("Uses vertical scanlines.")]
		public bool VerticalScanlines;

		// Token: 0x0400010D RID: 269
		[Range(-2f, 2f)]
		[Tooltip("Spherical distortion factor.")]
		public float Distortion = 0.2f;

		// Token: 0x0400010E RID: 270
		[Range(-2f, 2f)]
		[Tooltip("Cubic distortion factor.")]
		public float CubicDistortion = 0.6f;

		// Token: 0x0400010F RID: 271
		[Range(0.01f, 2f)]
		[Tooltip("Helps avoid screen streching on borders when working with heavy distortions.")]
		public float Scale = 0.8f;
	}
}
