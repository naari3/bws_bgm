using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005A RID: 90
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/pixelate.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Pixelate")]
	public class Pixelate : BaseEffect
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000D364 File Offset: 0x0000B564
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			float x = this.Scale;
			if (this.Mode == Pixelate.SizeMode.PixelPerfect)
			{
				x = (float)source.width / this.Scale;
			}
			base.Material.SetVector("_Params", new Vector2(x, this.AutomaticRatio ? ((float)source.width / (float)source.height) : this.Ratio));
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Pixelate";
		}

		// Token: 0x040001CD RID: 461
		[Range(1f, 1024f)]
		[Tooltip("Scale of an individual pixel. Depends on the Mode used.")]
		public float Scale = 80f;

		// Token: 0x040001CE RID: 462
		[Tooltip("Turn this on to automatically compute the aspect ratio needed for squared pixels.")]
		public bool AutomaticRatio = true;

		// Token: 0x040001CF RID: 463
		[Tooltip("Custom aspect ratio.")]
		public float Ratio = 1f;

		// Token: 0x040001D0 RID: 464
		[Tooltip("Used for the Scale field.")]
		public Pixelate.SizeMode Mode;

		// Token: 0x0200009C RID: 156
		public enum SizeMode
		{
			// Token: 0x040002D9 RID: 729
			ResolutionIndependent,
			// Token: 0x040002DA RID: 730
			PixelPerfect
		}
	}
}
