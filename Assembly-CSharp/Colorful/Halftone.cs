using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200004B RID: 75
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/halftone.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/Halftone")]
	public class Halftone : BaseEffect
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000BD84 File Offset: 0x00009F84
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Center", new Vector2(this.Center.x * (float)source.width, this.Center.y * (float)source.height));
			base.Material.SetVector("_Params", new Vector3(this.Scale, this.DotSize, this.Smoothness));
			Matrix4x4 value = default(Matrix4x4);
			value.SetRow(0, this.CMYKRot(this.Angle + 0.2617994f));
			value.SetRow(1, this.CMYKRot(this.Angle + 1.3089969f));
			value.SetRow(2, this.CMYKRot(this.Angle));
			value.SetRow(3, this.CMYKRot(this.Angle + 0.7853982f));
			base.Material.SetMatrix("_MatRot", value);
			Graphics.Blit(source, destination, base.Material, this.Desaturate ? 1 : 0);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BE94 File Offset: 0x0000A094
		private Vector4 CMYKRot(float angle)
		{
			float num = Mathf.Cos(angle);
			float num2 = Mathf.Sin(angle);
			return new Vector4(num, -num2, num2, num);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Halftone";
		}

		// Token: 0x0400017E RID: 382
		[Min(0f)]
		[Tooltip("Global haltfoning scale.")]
		public float Scale = 12f;

		// Token: 0x0400017F RID: 383
		[Min(0f)]
		[Tooltip("Individual dot size.")]
		public float DotSize = 1.35f;

		// Token: 0x04000180 RID: 384
		[Tooltip("Rotates the dot placement according to the Center point.")]
		public float Angle = 1.2f;

		// Token: 0x04000181 RID: 385
		[Range(0f, 1f)]
		[Tooltip("Dots antialiasing")]
		public float Smoothness = 0.08f;

		// Token: 0x04000182 RID: 386
		[Tooltip("Center point to use for the rotation.")]
		public Vector2 Center = new Vector2(0.5f, 0.5f);

		// Token: 0x04000183 RID: 387
		[Tooltip("Turns the effect black & white.")]
		public bool Desaturate;
	}
}
