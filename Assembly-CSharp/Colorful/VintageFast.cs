using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000069 RID: 105
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/vintage-fast.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Vintage")]
	public class VintageFast : LookupFilter3D
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000E024 File Offset: 0x0000C224
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
					this.LookupTexture = Resources.Load<Texture2D>("InstagramFast/" + this.Filter.ToString());
				}
			}
			base.OnRenderImage(source, destination);
		}

		// Token: 0x0400020A RID: 522
		public Vintage.InstragramFilter Filter;

		// Token: 0x0400020B RID: 523
		protected Vintage.InstragramFilter m_CurrentFilter;
	}
}
