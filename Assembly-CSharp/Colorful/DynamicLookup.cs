using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000042 RID: 66
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/dynamic-lookup.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Dynamic Lookup")]
	public class DynamicLookup : BaseEffect
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000B148 File Offset: 0x00009348
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetColor("_White", this.White);
			base.Material.SetColor("_Black", this.Black);
			base.Material.SetColor("_Red", this.Red);
			base.Material.SetColor("_Green", this.Green);
			base.Material.SetColor("_Blue", this.Blue);
			base.Material.SetColor("_Yellow", this.Yellow);
			base.Material.SetColor("_Magenta", this.Magenta);
			base.Material.SetColor("_Cyan", this.Cyan);
			base.Material.SetFloat("_Amount", this.Amount);
			Graphics.Blit(source, destination, base.Material, CLib.IsLinearColorSpace() ? 1 : 0);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000B233 File Offset: 0x00009433
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/DynamicLookup";
		}

		// Token: 0x04000153 RID: 339
		[ColorUsage(false)]
		public Color White = new Color(1f, 1f, 1f);

		// Token: 0x04000154 RID: 340
		[ColorUsage(false)]
		public Color Black = new Color(0f, 0f, 0f);

		// Token: 0x04000155 RID: 341
		[ColorUsage(false)]
		public Color Red = new Color(1f, 0f, 0f);

		// Token: 0x04000156 RID: 342
		[ColorUsage(false)]
		public Color Green = new Color(0f, 1f, 0f);

		// Token: 0x04000157 RID: 343
		[ColorUsage(false)]
		public Color Blue = new Color(0f, 0f, 1f);

		// Token: 0x04000158 RID: 344
		[ColorUsage(false)]
		public Color Yellow = new Color(1f, 1f, 0f);

		// Token: 0x04000159 RID: 345
		[ColorUsage(false)]
		public Color Magenta = new Color(1f, 0f, 1f);

		// Token: 0x0400015A RID: 346
		[ColorUsage(false)]
		public Color Cyan = new Color(0f, 1f, 1f);

		// Token: 0x0400015B RID: 347
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
