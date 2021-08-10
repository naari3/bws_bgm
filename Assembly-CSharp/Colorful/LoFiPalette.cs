using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000054 RID: 84
	[HelpURL("http://www.thomashourdel.com/colorful/doc/artistic-effects/lofi-palette.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Artistic Effects/LoFi Palette")]
	public class LoFiPalette : LookupFilter3D
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Palette != this.m_CurrentPreset)
			{
				this.m_CurrentPreset = this.Palette;
				if (this.Palette == LoFiPalette.Preset.None)
				{
					this.LookupTexture = null;
				}
				else
				{
					this.LookupTexture = Resources.Load<Texture2D>("LoFiPalettes/" + this.Palette.ToString());
				}
			}
			if (this.LookupTexture == null || this.Amount <= 0f)
			{
				Graphics.Blit(source, destination);
				return;
			}
			if (this.m_Use2DLut || this.ForceCompatibility)
			{
				this.RenderLut2D(source, destination);
				return;
			}
			this.RenderLut3D(source, destination);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000C958 File Offset: 0x0000AB58
		protected override void RenderLut2D(RenderTexture source, RenderTexture destination)
		{
			float num = Mathf.Sqrt((float)this.LookupTexture.width);
			base.Material.SetTexture("_LookupTex", this.LookupTexture);
			base.Material.SetVector("_Params1", new Vector3(1f / (float)this.LookupTexture.width, 1f / (float)this.LookupTexture.height, num - 1f));
			base.Material.SetVector("_Params2", new Vector2(this.Amount, this.PixelSize));
			int pass = (this.Pixelize ? 6 : 4) + (CLib.IsLinearColorSpace() ? 1 : 0);
			Graphics.Blit(source, destination, base.Material, pass);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000CA20 File Offset: 0x0000AC20
		protected override void RenderLut3D(RenderTexture source, RenderTexture destination)
		{
			if (this.LookupTexture.name != this.m_BaseTextureName)
			{
				base.ConvertBaseTexture();
			}
			if (this.m_Lut3D == null)
			{
				base.SetIdentityLut();
			}
			this.m_Lut3D.filterMode = FilterMode.Point;
			base.Material.SetTexture("_LookupTex", this.m_Lut3D);
			float num = (float)this.m_Lut3D.width;
			base.Material.SetVector("_Params", new Vector4((num - 1f) / (1f * num), 1f / (2f * num), this.Amount, this.PixelSize));
			int pass = (this.Pixelize ? 2 : 0) + (CLib.IsLinearColorSpace() ? 1 : 0);
			Graphics.Blit(source, destination, base.Material, pass);
		}

		// Token: 0x040001B5 RID: 437
		public LoFiPalette.Preset Palette;

		// Token: 0x040001B6 RID: 438
		[Tooltip("Pixelize the display.")]
		public bool Pixelize = true;

		// Token: 0x040001B7 RID: 439
		[Tooltip("The display height in pixels.")]
		public float PixelSize = 128f;

		// Token: 0x040001B8 RID: 440
		protected LoFiPalette.Preset m_CurrentPreset;

		// Token: 0x0200009A RID: 154
		public enum Preset
		{
			// Token: 0x040002B5 RID: 693
			None,
			// Token: 0x040002B6 RID: 694
			AmstradCPC = 2,
			// Token: 0x040002B7 RID: 695
			CGA,
			// Token: 0x040002B8 RID: 696
			Commodore64,
			// Token: 0x040002B9 RID: 697
			CommodorePlus,
			// Token: 0x040002BA RID: 698
			EGA,
			// Token: 0x040002BB RID: 699
			GameBoy,
			// Token: 0x040002BC RID: 700
			MacOS16,
			// Token: 0x040002BD RID: 701
			MacOS256,
			// Token: 0x040002BE RID: 702
			MasterSystem,
			// Token: 0x040002BF RID: 703
			RiscOS16,
			// Token: 0x040002C0 RID: 704
			Teletex,
			// Token: 0x040002C1 RID: 705
			Windows16,
			// Token: 0x040002C2 RID: 706
			Windows256,
			// Token: 0x040002C3 RID: 707
			ZXSpectrum,
			// Token: 0x040002C4 RID: 708
			Andrae = 17,
			// Token: 0x040002C5 RID: 709
			Anodomani,
			// Token: 0x040002C6 RID: 710
			Crayolo,
			// Token: 0x040002C7 RID: 711
			DB16,
			// Token: 0x040002C8 RID: 712
			DB32,
			// Token: 0x040002C9 RID: 713
			DJinn,
			// Token: 0x040002CA RID: 714
			DrazileA,
			// Token: 0x040002CB RID: 715
			DrazileB,
			// Token: 0x040002CC RID: 716
			DrazileC,
			// Token: 0x040002CD RID: 717
			Eggy,
			// Token: 0x040002CE RID: 718
			FinlalA,
			// Token: 0x040002CF RID: 719
			FinlalB,
			// Token: 0x040002D0 RID: 720
			Hapiel,
			// Token: 0x040002D1 RID: 721
			PavanzA,
			// Token: 0x040002D2 RID: 722
			PavanzB,
			// Token: 0x040002D3 RID: 723
			Peyton,
			// Token: 0x040002D4 RID: 724
			SpeedyCube
		}
	}
}
