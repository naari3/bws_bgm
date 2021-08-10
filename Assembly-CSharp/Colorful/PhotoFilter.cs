using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000059 RID: 89
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/photo-filter.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Photo Filter")]
	public class PhotoFilter : BaseEffect
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Density <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			base.Material.SetColor("_RGB", this.Color);
			base.Material.SetFloat("_Density", this.Density);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D32B File Offset: 0x0000B52B
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Photo Filter";
		}

		// Token: 0x040001CB RID: 459
		[ColorUsage(false)]
		[Tooltip("Lens filter color.")]
		public Color Color = new Color(1f, 0.5f, 0.2f, 1f);

		// Token: 0x040001CC RID: 460
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Density = 0.35f;
	}
}
