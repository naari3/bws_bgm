using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000061 RID: 97
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/sharpen.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Sharpen")]
	public class Sharpen : BaseEffect
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000D988 File Offset: 0x0000BB88
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Strength <= 0f || (this.Clamp <= 0f && this.Mode == Sharpen.Algorithm.TypeA))
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Params", new Vector4(this.Strength, this.Clamp, 1f / (float)source.width, 1f / (float)source.height));
			Graphics.Blit(source, destination, base.Material, (int)this.Mode);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DA0D File Offset: 0x0000BC0D
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Sharpen";
		}

		// Token: 0x040001EB RID: 491
		[Tooltip("Sharpening algorithm to use.")]
		public Sharpen.Algorithm Mode = Sharpen.Algorithm.TypeB;

		// Token: 0x040001EC RID: 492
		[Range(0f, 5f)]
		[Tooltip("Sharpening Strength.")]
		public float Strength = 0.6f;

		// Token: 0x040001ED RID: 493
		[Range(0f, 1f)]
		[Tooltip("Limits the amount of sharpening a pixel will receive.")]
		public float Clamp = 0.05f;

		// Token: 0x0200009F RID: 159
		public enum Algorithm
		{
			// Token: 0x040002E4 RID: 740
			TypeA,
			// Token: 0x040002E5 RID: 741
			TypeB
		}
	}
}
