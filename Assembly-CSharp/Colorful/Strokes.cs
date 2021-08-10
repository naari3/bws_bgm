using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000063 RID: 99
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/strokes.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Strokes")]
	public class Strokes : BaseEffect
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float num = this.Scaling / (float)source.height;
			base.Material.SetVector("_Params1", new Vector4(this.Amplitude, this.Frequency, num, this.MaxThickness * num));
			base.Material.SetVector("_Params2", new Vector3(this.RedLuminance, this.GreenLuminance, this.BlueLuminance));
			base.Material.SetVector("_Params3", new Vector2(this.Threshold, this.Harshness));
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Strokes";
		}

		// Token: 0x040001F1 RID: 497
		public Strokes.ColorMode Mode;

		// Token: 0x040001F2 RID: 498
		[Range(0f, 0.04f)]
		[Tooltip("Stroke rotation, or wave pattern amplitude.")]
		public float Amplitude = 0.025f;

		// Token: 0x040001F3 RID: 499
		[Range(0f, 20f)]
		[Tooltip("Wave pattern frequency (higher means more waves).")]
		public float Frequency = 10f;

		// Token: 0x040001F4 RID: 500
		[Range(4f, 12f)]
		[Tooltip("Global scaling.")]
		public float Scaling = 7.5f;

		// Token: 0x040001F5 RID: 501
		[Range(0.1f, 0.5f)]
		[Tooltip("Stroke maximum thickness.")]
		public float MaxThickness = 0.2f;

		// Token: 0x040001F6 RID: 502
		[Range(0f, 1f)]
		[Tooltip("Contribution threshold (higher means more continous strokes).")]
		public float Threshold = 0.7f;

		// Token: 0x040001F7 RID: 503
		[Range(-0.3f, 0.3f)]
		[Tooltip("Stroke pressure.")]
		public float Harshness;

		// Token: 0x040001F8 RID: 504
		[Range(0f, 1f)]
		[Tooltip("Amount of red to contribute to the strokes.")]
		public float RedLuminance = 0.299f;

		// Token: 0x040001F9 RID: 505
		[Range(0f, 1f)]
		[Tooltip("Amount of green to contribute to the strokes.")]
		public float GreenLuminance = 0.587f;

		// Token: 0x040001FA RID: 506
		[Range(0f, 1f)]
		[Tooltip("Amount of blue to contribute to the strokes.")]
		public float BlueLuminance = 0.114f;

		// Token: 0x020000A0 RID: 160
		public enum ColorMode
		{
			// Token: 0x040002E7 RID: 743
			BlackAndWhite,
			// Token: 0x040002E8 RID: 744
			WhiteAndBlack,
			// Token: 0x040002E9 RID: 745
			ColorAndWhite,
			// Token: 0x040002EA RID: 746
			ColorAndBlack,
			// Token: 0x040002EB RID: 747
			WhiteAndColor,
			// Token: 0x040002EC RID: 748
			BlackAndColor
		}
	}
}
