using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000058 RID: 88
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/noise.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Noise")]
	public class Noise : BaseEffect
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000D202 File Offset: 0x0000B402
		protected virtual void Update()
		{
			if (this.Animate)
			{
				if (this.Seed > 10f)
				{
					this.Seed = 0.5f;
				}
				this.Seed += Time.deltaTime * 0.25f;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D23C File Offset: 0x0000B43C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Params", new Vector3(this.Seed, this.Strength, this.LumContribution));
			int num = (this.Mode == Noise.ColorMode.Monochrome) ? 0 : 1;
			num += ((this.LumContribution > 0f) ? 2 : 0);
			Graphics.Blit(source, destination, base.Material, num);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D2A4 File Offset: 0x0000B4A4
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Noise";
		}

		// Token: 0x040001C6 RID: 454
		[Tooltip("Black & white or colored noise.")]
		public Noise.ColorMode Mode;

		// Token: 0x040001C7 RID: 455
		[Tooltip("Automatically increment the seed to animate the noise.")]
		public bool Animate = true;

		// Token: 0x040001C8 RID: 456
		[Tooltip("A number used to initialize the noise generator.")]
		public float Seed = 0.5f;

		// Token: 0x040001C9 RID: 457
		[Range(0f, 1f)]
		[Tooltip("Strength used to apply the noise. 0 means no noise at all, 1 is full noise.")]
		public float Strength = 0.12f;

		// Token: 0x040001CA RID: 458
		[Range(0f, 1f)]
		[Tooltip("Reduce the noise visibility in luminous areas.")]
		public float LumContribution;

		// Token: 0x0200009B RID: 155
		public enum ColorMode
		{
			// Token: 0x040002D6 RID: 726
			Monochrome,
			// Token: 0x040002D7 RID: 727
			RGB
		}
	}
}
