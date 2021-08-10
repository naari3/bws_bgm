using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000045 RID: 69
	[HelpURL("http://www.thomashourdel.com/colorful/doc/blur-effects/gaussian-blur.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Blur Effects/Gaussian Blur")]
	public class GaussianBlur : BaseEffect
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000B4B4 File Offset: 0x000096B4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
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

		// Token: 0x0600020A RID: 522 RVA: 0x0000B570 File Offset: 0x00009770
		protected virtual void OnePassBlur(RenderTexture source, RenderTexture destination)
		{
			int num = Mathf.FloorToInt((float)source.width / this.Downscaling);
			int num2 = Mathf.FloorToInt((float)source.height / this.Downscaling);
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, source.format);
			base.Material.SetVector("_Direction", new Vector2(1f / (float)num, 0f));
			Graphics.Blit(source, temporary, base.Material, 0);
			base.Material.SetVector("_Direction", new Vector2(0f, 1f / (float)num2));
			Graphics.Blit(temporary, destination, base.Material, 0);
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B624 File Offset: 0x00009824
		protected virtual void MultiPassBlur(RenderTexture source, RenderTexture destination)
		{
			int num = Mathf.FloorToInt((float)source.width / this.Downscaling);
			int num2 = Mathf.FloorToInt((float)source.height / this.Downscaling);
			Vector2 v = new Vector2(1f / (float)num, 0f);
			Vector2 v2 = new Vector2(0f, 1f / (float)num2);
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, source.format);
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0, source.format);
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

		// Token: 0x0600020C RID: 524 RVA: 0x0000B778 File Offset: 0x00009978
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Gaussian Blur";
		}

		// Token: 0x04000165 RID: 357
		[Range(0f, 10f)]
		[Tooltip("Amount of blurring pass to apply.")]
		public int Passes = 1;

		// Token: 0x04000166 RID: 358
		[Range(1f, 16f)]
		[Tooltip("Downscales the result for faster processing or heavier blur.")]
		public float Downscaling = 1f;

		// Token: 0x04000167 RID: 359
		[Range(0f, 1f)]
		[Tooltip("Blending factor.")]
		public float Amount = 1f;
	}
}
