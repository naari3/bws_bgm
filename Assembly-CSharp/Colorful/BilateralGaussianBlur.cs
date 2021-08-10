using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000032 RID: 50
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/bilateral-gaussian-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Bilateral Gaussian Blur")]
	public class BilateralGaussianBlur : BaseEffect
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000A0C3 File Offset: 0x000082C3
		protected override void Start()
		{
			base.Start();
			base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A0E0 File Offset: 0x000082E0
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetFloat("_Threshold", this.Threshold / 10000f);
			if (this.Passes <= 0 || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.Amount < 1f)
			{
				RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
				if (this.Passes == 1)
				{
					this.OnePassBlur(source, temporary);
				}
				else
				{
					this.MultiPassBlur(source, temporary);
				}
				base.Material.SetTexture("_Blurred", temporary);
				base.Material.SetFloat("_Amount", this.Amount);
				Graphics.Blit(source, destination, base.Material, 1);
				RenderTexture.ReleaseTemporary(temporary);
				return;
			}
			if (this.Passes == 1)
			{
				this.OnePassBlur(source, destination);
				return;
			}
			this.MultiPassBlur(source, destination);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A1B8 File Offset: 0x000083B8
		protected virtual void OnePassBlur(RenderTexture source, RenderTexture destination)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			base.Material.SetVector("_Direction", new Vector2(1f / (float)source.width, 0f));
			Graphics.Blit(source, temporary, base.Material, 0);
			base.Material.SetVector("_Direction", new Vector2(0f, 1f / (float)source.height));
			Graphics.Blit(temporary, destination, base.Material, 0);
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A258 File Offset: 0x00008458
		protected virtual void MultiPassBlur(RenderTexture source, RenderTexture destination)
		{
			Vector2 v = new Vector2(1f / (float)source.width, 0f);
			Vector2 v2 = new Vector2(0f, 1f / (float)source.height);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
			base.Material.SetVector("_Direction", v);
			Graphics.Blit(source, temporary, base.Material, 0);
			base.Material.SetVector("_Direction", v2);
			Graphics.Blit(temporary, temporary2, base.Material, 0);
			temporary.DiscardContents();
			for (int i = 1; i < this.Passes; i++)
			{
				base.Material.SetVector("_Direction", v);
				Graphics.Blit(temporary2, temporary, base.Material, 0);
				temporary2.DiscardContents();
				base.Material.SetVector("_Direction", v2);
				Graphics.Blit(temporary, temporary2, base.Material, 0);
				temporary.DiscardContents();
			}
			Graphics.Blit(temporary2, destination);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A393 File Offset: 0x00008593
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Bilateral Gaussian Blur";
		}

		// Token: 0x04000110 RID: 272
		[Range(0f, 10f)]
		[Tooltip("Add more passes to get a smoother blur. Beware that each pass will slow down the effect.")]
		public int Passes = 1;

		// Token: 0x04000111 RID: 273
		[Range(0.04f, 1f)]
		[Tooltip("Adjusts the blur \"sharpness\" around edges")]
		public float Threshold = 0.05f;

		// Token: 0x04000112 RID: 274
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
