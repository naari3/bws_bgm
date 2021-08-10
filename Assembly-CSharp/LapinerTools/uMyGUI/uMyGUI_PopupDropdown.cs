using System;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001A RID: 26
	public class uMyGUI_PopupDropdown : uMyGUI_PopupText
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00005910 File Offset: 0x00003B10
		public override void Show()
		{
			base.Show();
			if (this.m_dropdown != null)
			{
				this.m_dropdown.Select(-1);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005932 File Offset: 0x00003B32
		public override void Hide()
		{
			base.Hide();
			if (this.m_dropdown != null && this.m_onSelected != null)
			{
				this.m_dropdown.OnSelected -= this.m_onSelected;
				this.m_onSelected = null;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005968 File Offset: 0x00003B68
		public virtual uMyGUI_PopupDropdown SetEntries(string[] p_entries)
		{
			if (this.m_dropdown != null)
			{
				this.m_dropdown.Entries = p_entries;
			}
			return this;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005985 File Offset: 0x00003B85
		public virtual uMyGUI_PopupDropdown SetOnSelected(Action<int> p_onSelected)
		{
			if (this.m_dropdown != null)
			{
				this.m_onSelected = p_onSelected;
				this.m_dropdown.OnSelected += p_onSelected;
			}
			return this;
		}

		// Token: 0x04000080 RID: 128
		[SerializeField]
		protected uMyGUI_Dropdown m_dropdown;

		// Token: 0x04000081 RID: 129
		protected Action<int> m_onSelected;
	}
}
