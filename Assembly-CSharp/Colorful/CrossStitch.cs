using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003E RID: 62
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/cross-stitch.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Cross Stitch")]
	public class CrossStitch : BaseEffect
	{
		// Token: 0x060001F4 RID: 500 RVA: 0x0000AE10 File Offset: 0x00009010
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetInt("_StitchSize", this.Size);
			base.Material.SetFloat("_Brightness", this.Brightness);
			int num = this.Invert ? 1 : 0;
			if (this.Pixelize)
			{
				num += 2;
				base.Material.SetFloat("_Scale", (float)source.width / (float)this.Size);
				base.Material.SetFloat("_Ratio", (float)source.width / (float)source.height);
			}
			Graphics.Blit(source, destination, base.Material, num);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000AEAE File Offset: 0x000090AE
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Cross Stitch";
		}

		// Token: 0x04000142 RID: 322
		[Range(1f, 128f)]
		[Tooltip("Works best with power of two values.")]
		public int Size = 8;

		// Token: 0x04000143 RID: 323
		[Range(0f, 10f)]
		[Tooltip("Brightness adjustment. Cross-stitching tends to lower the overall brightness, use this to compensate.")]
		public float Brightness = 1.5f;

		// Token: 0x04000144 RID: 324
		[Tooltip("Inverts the cross-stiching pattern.")]
		public bool Invert;

		// Token: 0x04000145 RID: 325
		[Tooltip("Should the original render be pixelized ?")]
		public bool Pixelize = true;
	}
}
