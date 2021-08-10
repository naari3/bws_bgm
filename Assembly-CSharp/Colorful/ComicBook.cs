using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200003A RID: 58
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/comic-book.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Comic Book")]
	public class ComicBook : BaseEffect
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000A99C File Offset: 0x00008B9C
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_StripParams", new Vector4(Mathf.Cos(this.StripAngle), Mathf.Sin(this.StripAngle), this.StripLimits.x, this.StripLimits.y));
			base.Material.SetVector("_StripParams2", new Vector3(this.StripDensity * 10f, this.StripThickness, this.Amount));
			base.Material.SetColor("_StripInnerColor", this.StripInnerColor);
			base.Material.SetColor("_StripOuterColor", this.StripOuterColor);
			base.Material.SetColor("_FillColor", this.FillColor);
			base.Material.SetColor("_BackgroundColor", this.BackgroundColor);
			if (this.EdgeDetection)
			{
				base.Material.SetFloat("_EdgeThreshold", 1f / (this.EdgeThreshold * 100f));
				base.Material.SetColor("_EdgeColor", this.EdgeColor);
				Graphics.Blit(source, destination, base.Material, 1);
				return;
			}
			Graphics.Blit(source, destination, base.Material, 0);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Comic Book";
		}

		// Token: 0x0400012A RID: 298
		[Tooltip("Strip orientation in radians.")]
		public float StripAngle = 0.6f;

		// Token: 0x0400012B RID: 299
		[Min(0f)]
		[Tooltip("Amount of strips to draw.")]
		public float StripDensity = 180f;

		// Token: 0x0400012C RID: 300
		[Range(0f, 1f)]
		[Tooltip("Thickness of the inner strip fill.")]
		public float StripThickness = 0.5f;

		// Token: 0x0400012D RID: 301
		public Vector2 StripLimits = new Vector2(0.25f, 0.4f);

		// Token: 0x0400012E RID: 302
		[ColorUsage(false)]
		public Color StripInnerColor = new Color(0.3f, 0.3f, 0.3f);

		// Token: 0x0400012F RID: 303
		[ColorUsage(false)]
		public Color StripOuterColor = new Color(0.8f, 0.8f, 0.8f);

		// Token: 0x04000130 RID: 304
		[ColorUsage(false)]
		public Color FillColor = new Color(0.1f, 0.1f, 0.1f);

		// Token: 0x04000131 RID: 305
		[ColorUsage(false)]
		public Color BackgroundColor = Color.white;

		// Token: 0x04000132 RID: 306
		[Tooltip("Toggle edge detection (slower).")]
		public bool EdgeDetection;

		// Token: 0x04000133 RID: 307
		[Min(0.01f)]
		[Tooltip("Edge detection threshold. Use lower values for more visible edges.")]
		public float EdgeThreshold = 5f;

		// Token: 0x04000134 RID: 308
		[ColorUsage(false)]
		public Color EdgeColor = Color.black;

		// Token: 0x04000135 RID: 309
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
