using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000053 RID: 83
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/levels.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Levels")]
	public class Levels : BaseEffect
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Mode == Levels.ColorMode.Monochrome)
			{
				base.Material.SetVector("_InputMin", new Vector4(this.InputL.x / 255f, this.InputL.x / 255f, this.InputL.x / 255f, 1f));
				base.Material.SetVector("_InputMax", new Vector4(this.InputL.y / 255f, this.InputL.y / 255f, this.InputL.y / 255f, 1f));
				base.Material.SetVector("_InputGamma", new Vector4(this.InputL.z, this.InputL.z, this.InputL.z, 1f));
				base.Material.SetVector("_OutputMin", new Vector4(this.OutputL.x / 255f, this.OutputL.x / 255f, this.OutputL.x / 255f, 1f));
				base.Material.SetVector("_OutputMax", new Vector4(this.OutputL.y / 255f, this.OutputL.y / 255f, this.OutputL.y / 255f, 1f));
			}
			else
			{
				base.Material.SetVector("_InputMin", new Vector4(this.InputR.x / 255f, this.InputG.x / 255f, this.InputB.x / 255f, 1f));
				base.Material.SetVector("_InputMax", new Vector4(this.InputR.y / 255f, this.InputG.y / 255f, this.InputB.y / 255f, 1f));
				base.Material.SetVector("_InputGamma", new Vector4(this.InputR.z, this.InputG.z, this.InputB.z, 1f));
				base.Material.SetVector("_OutputMin", new Vector4(this.OutputR.x / 255f, this.OutputG.x / 255f, this.OutputB.x / 255f, 1f));
				base.Material.SetVector("_OutputMax", new Vector4(this.OutputR.y / 255f, this.OutputG.y / 255f, this.OutputB.y / 255f, 1f));
			}
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Levels";
		}

		// Token: 0x040001AC RID: 428
		public Levels.ColorMode Mode;

		// Token: 0x040001AD RID: 429
		public Vector3 InputL = new Vector3(0f, 255f, 1f);

		// Token: 0x040001AE RID: 430
		public Vector3 InputR = new Vector3(0f, 255f, 1f);

		// Token: 0x040001AF RID: 431
		public Vector3 InputG = new Vector3(0f, 255f, 1f);

		// Token: 0x040001B0 RID: 432
		public Vector3 InputB = new Vector3(0f, 255f, 1f);

		// Token: 0x040001B1 RID: 433
		public Vector2 OutputL = new Vector2(0f, 255f);

		// Token: 0x040001B2 RID: 434
		public Vector2 OutputR = new Vector2(0f, 255f);

		// Token: 0x040001B3 RID: 435
		public Vector2 OutputG = new Vector2(0f, 255f);

		// Token: 0x040001B4 RID: 436
		public Vector2 OutputB = new Vector2(0f, 255f);

		// Token: 0x02000099 RID: 153
		public enum ColorMode
		{
			// Token: 0x040002B2 RID: 690
			Monochrome,
			// Token: 0x040002B3 RID: 691
			RGB
		}
	}
}
