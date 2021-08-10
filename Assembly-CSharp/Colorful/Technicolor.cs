using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000064 RID: 100
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/technicolor.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Technicolor")]
	public class Technicolor : BaseEffect
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000DD10 File Offset: 0x0000BF10
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetFloat("_Exposure", 8f - this.Exposure);
			base.Material.SetVector("_Balance", Vector3.one - this.Balance);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DD96 File Offset: 0x0000BF96
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Technicolor";
		}

		// Token: 0x040001FB RID: 507
		[Range(0f, 8f)]
		public float Exposure = 4f;

		// Token: 0x040001FC RID: 508
		public Vector3 Balance = new Vector3(0.25f, 0.25f, 0.25f);

		// Token: 0x040001FD RID: 509
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 0.5f;
	}
}
