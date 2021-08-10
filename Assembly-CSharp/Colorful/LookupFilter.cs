using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000055 RID: 85
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/lookup-filter.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Lookup Filter (Deprecated)")]
	public class LookupFilter : BaseEffect
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.LookupTexture == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetTexture("_LookupTex", this.LookupTexture);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material, CLib.IsLinearColorSpace() ? 1 : 0);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000CB80 File Offset: 0x0000AD80
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Lookup Filter (Deprecated)";
		}

		// Token: 0x040001B9 RID: 441
		[Tooltip("The lookup texture to apply. Read the documentation to learn how to create one.")]
		public Texture LookupTexture;

		// Token: 0x040001BA RID: 442
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
