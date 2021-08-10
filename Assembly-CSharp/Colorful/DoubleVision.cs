using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000041 RID: 65
	[HelpURL("http://www.thomashourdel.com/colorful/doc/camera-effects/double-vision.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Camera Effects/Double Vision")]
	public class DoubleVision : BaseEffect
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000B080 File Offset: 0x00009280
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Amount <= 0f || this.Displace == Vector2.zero)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetVector("_Displace", new Vector2(this.Displace.x / (float)source.width, this.Displace.y / (float)source.height));
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000B117 File Offset: 0x00009317
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Double Vision";
		}

		// Token: 0x04000151 RID: 337
		[Tooltip("Diploplia strength.")]
		public Vector2 Displace = new Vector2(0.7f, 0f);

		// Token: 0x04000152 RID: 338
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
