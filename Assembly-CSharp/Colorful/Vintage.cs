using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000068 RID: 104
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/vintage.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Vintage (Deprecated)")]
	public class Vintage : LookupFilter
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			if (this.Filter != this.m_CurrentFilter)
			{
				this.m_CurrentFilter = this.Filter;
				if (this.Filter == Vintage.InstragramFilter.None)
				{
					this.LookupTexture = null;
				}
				else
				{
					this.LookupTexture = Resources.Load<Texture2D>("Instagram/" + this.Filter.ToString());
				}
			}
			base.OnRenderImage(source, destination);
		}

		// Token: 0x04000208 RID: 520
		public Vintage.InstragramFilter Filter;

		// Token: 0x04000209 RID: 521
		protected Vintage.InstragramFilter m_CurrentFilter;

		// Token: 0x020000A1 RID: 161
		public enum InstragramFilter
		{
			// Token: 0x040002EE RID: 750
			None,
			// Token: 0x040002EF RID: 751
			F1977,
			// Token: 0x040002F0 RID: 752
			Aden,
			// Token: 0x040002F1 RID: 753
			Amaro,
			// Token: 0x040002F2 RID: 754
			Brannan,
			// Token: 0x040002F3 RID: 755
			Crema,
			// Token: 0x040002F4 RID: 756
			Earlybird,
			// Token: 0x040002F5 RID: 757
			Hefe,
			// Token: 0x040002F6 RID: 758
			Hudson,
			// Token: 0x040002F7 RID: 759
			Inkwell,
			// Token: 0x040002F8 RID: 760
			Juno,
			// Token: 0x040002F9 RID: 761
			Kelvin,
			// Token: 0x040002FA RID: 762
			Lark,
			// Token: 0x040002FB RID: 763
			LoFi,
			// Token: 0x040002FC RID: 764
			Ludwig,
			// Token: 0x040002FD RID: 765
			Mayfair,
			// Token: 0x040002FE RID: 766
			Nashville,
			// Token: 0x040002FF RID: 767
			Perpetua,
			// Token: 0x04000300 RID: 768
			Reyes,
			// Token: 0x04000301 RID: 769
			Rise,
			// Token: 0x04000302 RID: 770
			Sierra,
			// Token: 0x04000303 RID: 771
			Slumber,
			// Token: 0x04000304 RID: 772
			Sutro,
			// Token: 0x04000305 RID: 773
			Toaster,
			// Token: 0x04000306 RID: 774
			Valencia,
			// Token: 0x04000307 RID: 775
			Walden,
			// Token: 0x04000308 RID: 776
			Willow,
			// Token: 0x04000309 RID: 777
			XProII
		}
	}
}
