using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200005B RID: 91
	[HelpURL("http://www.thomashourdel.com/colorful/doc/other-effects/pixel-matrix.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Other Effects/Pixel Matrix")]
	public class PixelMatrix : BaseEffect
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000D404 File Offset: 0x0000B604
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Params", new Vector4((float)this.Size, Mathf.Floor((float)this.Size / 3f), (float)this.Size - Mathf.Floor((float)this.Size / 3f), this.Brightness));
			Graphics.Blit(source, destination, base.Material, this.BlackBorder ? 1 : 0);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D478 File Offset: 0x0000B678
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/PixelMatrix";
		}

		// Token: 0x040001D1 RID: 465
		[Min(3f)]
		[Tooltip("Tile size. Works best with multiples of 3.")]
		public int Size = 9;

		// Token: 0x040001D2 RID: 466
		[Range(0f, 10f)]
		[Tooltip("Tile brightness booster.")]
		public float Brightness = 1.4f;

		// Token: 0x040001D3 RID: 467
		[Tooltip("Show / hide black borders on every tile.")]
		public bool BlackBorder = true;
	}
}
