using System;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001D RID: 29
	public class uMyGUI_PopupTexturePicker : uMyGUI_PopupText
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000604B File Offset: 0x0000424B
		public override void Hide()
		{
			base.Hide();
			if (this.m_picker != null)
			{
				this.m_picker.ButtonCallback = null;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006070 File Offset: 0x00004270
		public virtual uMyGUI_PopupText SetPicker(Texture2D[] p_textures, int p_selectedIndex, Action<int> p_buttonCallback)
		{
			if (this.m_picker != null)
			{
				this.m_picker.ButtonCallback = delegate(int p_clickedIndex)
				{
					p_buttonCallback(p_clickedIndex);
					this.Hide();
				};
				this.m_picker.SetTextures(p_textures, p_selectedIndex);
			}
			return this;
		}

		// Token: 0x04000090 RID: 144
		[SerializeField]
		protected uMyGUI_TexturePicker m_picker;
	}
}
