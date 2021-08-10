using System;
using System.Collections.Generic;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x0200001B RID: 27
	public class uMyGUI_PopupManager : MonoBehaviour
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000059B4 File Offset: 0x00003BB4
		public static uMyGUI_PopupManager Instance
		{
			get
			{
				if (uMyGUI_PopupManager.s_instance == null)
				{
					uMyGUI_PopupManager.s_instance = Object.FindObjectOfType<uMyGUI_PopupManager>();
				}
				if (uMyGUI_PopupManager.s_instance == null)
				{
					uMyGUI_PopupManager.s_instance = new GameObject(typeof(uMyGUI_PopupManager).Name).AddComponent<uMyGUI_PopupManager>();
				}
				return uMyGUI_PopupManager.s_instance;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005A08 File Offset: 0x00003C08
		public static bool IsInstanceSet
		{
			get
			{
				return uMyGUI_PopupManager.s_instance != null;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005A15 File Offset: 0x00003C15
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005A1D File Offset: 0x00003C1D
		public uMyGUI_Popup[] Popups
		{
			get
			{
				return this.m_popups;
			}
			set
			{
				this.m_popups = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005A26 File Offset: 0x00003C26
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005A2E File Offset: 0x00003C2E
		public string[] PopupNames
		{
			get
			{
				return this.m_popupNames;
			}
			set
			{
				this.m_popupNames = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005A37 File Offset: 0x00003C37
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005A3F File Offset: 0x00003C3F
		public CanvasGroup[] DeactivatedElementsWhenPopupIsShown
		{
			get
			{
				return this.m_deactivatedElementsWhenPopupIsShown;
			}
			set
			{
				this.m_deactivatedElementsWhenPopupIsShown = value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005A48 File Offset: 0x00003C48
		public uMyGUI_Popup ShowPopup(string p_name)
		{
			int num = 0;
			while (num < this.m_popupNames.Length && num < this.m_popups.Length)
			{
				if (this.m_popupNames[num] == p_name)
				{
					return this.ShowPopup(num);
				}
				num++;
			}
			if (this.LoadPopupFromResources(p_name) != null)
			{
				return this.ShowPopup(p_name);
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public uMyGUI_Popup HidePopup(string p_name)
		{
			int num = 0;
			while (num < this.m_popupNames.Length && num < this.m_popups.Length)
			{
				if (this.m_popupNames[num] == p_name)
				{
					return this.HidePopup(num);
				}
				num++;
			}
			return null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005AE8 File Offset: 0x00003CE8
		public uMyGUI_Popup ShowPopup(int p_index)
		{
			if (p_index >= 0 && p_index < this.m_popups.Length)
			{
				this.m_popups[p_index].Show();
				return this.m_popups[p_index];
			}
			Debug.LogError(string.Concat(new object[]
			{
				"uMyGUI_PopupManager: ShowPopup: popup index '",
				p_index,
				"' is out of bounds [0,",
				this.m_popups.Length,
				"]!"
			}));
			return null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005B5C File Offset: 0x00003D5C
		public uMyGUI_Popup HidePopup(int p_index)
		{
			if (p_index >= 0 && p_index < this.m_popups.Length)
			{
				this.m_popups[p_index].Hide();
				return this.m_popups[p_index];
			}
			Debug.LogError(string.Concat(new object[]
			{
				"uMyGUI_PopupManager: HidePopup: popup index '",
				p_index,
				"' is out of bounds [0,",
				this.m_popups.Length,
				"]!"
			}));
			return null;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public bool IsPopupShown
		{
			get
			{
				for (int i = 0; i < this.m_popups.Length; i++)
				{
					if (this.m_popups[i] != null && this.m_popups[i].IsShown)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005C14 File Offset: 0x00003E14
		public bool HasPopup(string p_name)
		{
			for (int i = 0; i < this.m_popupNames.Length; i++)
			{
				if (this.m_popupNames[i] == p_name)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005C48 File Offset: 0x00003E48
		public bool AddPopup(uMyGUI_Popup p_popup, string p_name)
		{
			Canvas canvas = null;
			if (this.m_popups.Length != 0 && this.m_popups[0] != null && this.m_popups[0].transform.parent != null)
			{
				canvas = this.m_popups[0].transform.parent.GetComponentInParent<Canvas>();
			}
			if (canvas == null)
			{
				canvas = base.GetComponentInParent<Canvas>();
			}
			if (canvas == null)
			{
				canvas = Object.FindObjectOfType<Canvas>();
			}
			if (canvas == null)
			{
				Debug.LogError("uMyGUI_PopupManager: AddPopup: there is no Canvas in this level!");
				return false;
			}
			uMyGUI_Popup[] popups = this.m_popups;
			string[] popupNames = this.m_popupNames;
			this.m_popups = new uMyGUI_Popup[this.m_popups.Length + 1];
			this.m_popupNames = new string[this.m_popupNames.Length + 1];
			Array.Copy(popups, this.m_popups, popups.Length);
			Array.Copy(popupNames, this.m_popupNames, popupNames.Length);
			this.m_popups[this.m_popups.Length - 1] = p_popup;
			this.m_popupNames[this.m_popups.Length - 1] = p_name;
			p_popup.transform.SetParent(canvas.transform, false);
			this.HidePopup(this.m_popups.Length - 1);
			return true;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005D74 File Offset: 0x00003F74
		public bool RemovePopup(uMyGUI_Popup p_popup)
		{
			for (int i = 0; i < this.m_popups.Length; i++)
			{
				if (this.m_popups[i] == p_popup)
				{
					List<uMyGUI_Popup> list = new List<uMyGUI_Popup>(this.m_popups);
					list.RemoveAt(i);
					this.m_popups = list.ToArray();
					List<string> list2 = new List<string>(this.m_popupNames);
					list2.RemoveAt(i);
					this.m_popupNames = list2.ToArray();
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005DE8 File Offset: 0x00003FE8
		private uMyGUI_Popup LoadPopupFromResources(string p_name)
		{
			uMyGUI_Popup uMyGUI_Popup = Object.Instantiate<uMyGUI_Popup>(Resources.Load<uMyGUI_Popup>("popup_" + p_name + "_root"));
			if (uMyGUI_Popup != null && this.AddPopup(uMyGUI_Popup, p_name))
			{
				return uMyGUI_Popup;
			}
			return null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005E28 File Offset: 0x00004028
		private void Awake()
		{
			if (this.m_popups.Length != this.m_popupNames.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"uMyGUI_PopupManager: m_popups and m_popupNames must have the same length (",
					this.m_popups.Length,
					"!=",
					this.m_popupNames.Length,
					")!"
				}));
			}
			for (int i = 0; i < this.m_popups.Length; i++)
			{
				this.HidePopup(i);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005EAC File Offset: 0x000040AC
		private void Update()
		{
			bool interactable = !this.IsPopupShown;
			for (int i = 0; i < this.m_deactivatedElementsWhenPopupIsShown.Length; i++)
			{
				this.m_deactivatedElementsWhenPopupIsShown[i].interactable = interactable;
			}
		}

		// Token: 0x04000082 RID: 130
		public const string POPUP_LOADING = "loading";

		// Token: 0x04000083 RID: 131
		public const string POPUP_TEXT = "text";

		// Token: 0x04000084 RID: 132
		public const string POPUP_DROPDOWN = "dropdown";

		// Token: 0x04000085 RID: 133
		public const string BTN_OK = "ok";

		// Token: 0x04000086 RID: 134
		public const string BTN_YES = "yes";

		// Token: 0x04000087 RID: 135
		public const string BTN_NO = "no";

		// Token: 0x04000088 RID: 136
		private static uMyGUI_PopupManager s_instance;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		private uMyGUI_Popup[] m_popups = new uMyGUI_Popup[0];

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private string[] m_popupNames = new string[0];

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private CanvasGroup[] m_deactivatedElementsWhenPopupIsShown = new CanvasGroup[0];
	}
}
