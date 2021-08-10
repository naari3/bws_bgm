using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000016 RID: 22
	public class uMyGUI_Dropdown : MonoBehaviour
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000047F6 File Offset: 0x000029F6
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000047FE File Offset: 0x000029FE
		public string[] Entries
		{
			get
			{
				return this.m_entries;
			}
			set
			{
				this.m_entries = value;
				this.HideEntries();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000480D File Offset: 0x00002A0D
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004815 File Offset: 0x00002A15
		public int SelectedIndex
		{
			get
			{
				return this.m_selectedIndex;
			}
			set
			{
				this.m_selectedIndex = Mathf.Clamp(value, -1, this.m_entries.Length - 1);
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000073 RID: 115 RVA: 0x00004830 File Offset: 0x00002A30
		// (remove) Token: 0x06000074 RID: 116 RVA: 0x00004868 File Offset: 0x00002A68
		public event Action<int> OnSelected;

		// Token: 0x06000075 RID: 117 RVA: 0x000048A0 File Offset: 0x00002AA0
		public void Select(int p_selectedIndex)
		{
			int num = Mathf.Clamp(p_selectedIndex, -1, this.m_entries.Length - 1);
			bool flag = num != this.m_selectedIndex;
			this.m_selectedIndex = num;
			this.UpdateText();
			if (flag && this.OnSelected != null)
			{
				this.OnSelected(this.m_selectedIndex);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000048F4 File Offset: 0x00002AF4
		private void Start()
		{
			if (this.m_text != null)
			{
				this.UpdateText();
			}
			else
			{
				Debug.LogError("uMyGUI_Dropdown: m_text must be set in the inspector!");
			}
			if (this.m_button != null)
			{
				this.m_button.onClick.AddListener(new UnityAction(this.OnClick));
			}
			else
			{
				Debug.LogError("uMyGUI_Dropdown: m_button must be set in the inspector!");
			}
			if (this.m_entriesRoot != null && this.m_entriesBG != null)
			{
				this.HideEntries();
				return;
			}
			Debug.LogError("uMyGUI_Dropdown: m_entriesRoot and m_entriesBG must be set in the inspector!");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004984 File Offset: 0x00002B84
		private void LateUpdate()
		{
			if (this.m_improveNavigationFocus)
			{
				EventSystem current = EventSystem.current;
				if (current != null && (current.currentSelectedGameObject == null || !current.currentSelectedGameObject.activeInHierarchy))
				{
					if (current.lastSelectedGameObject != null && current.lastSelectedGameObject.activeInHierarchy)
					{
						current.SetSelectedGameObject(current.lastSelectedGameObject);
						return;
					}
					if (this.m_entriesRoot != null)
					{
						if (this.m_entriesRoot.gameObject.activeSelf && this.m_entriesRoot.childCount > 0 && this.m_entriesRoot.GetChild(0).GetComponentInChildren<Button>() != null)
						{
							this.m_entriesRoot.GetChild(0).GetComponentInChildren<Button>().Select();
							return;
						}
						if (this.m_button != null)
						{
							this.m_button.Select();
						}
					}
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004A6B File Offset: 0x00002C6B
		private void OnClick()
		{
			if (this.m_entriesRoot != null)
			{
				if (this.m_entriesRoot.gameObject.activeSelf)
				{
					this.HideEntries();
					return;
				}
				this.ShowEntries();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004A9C File Offset: 0x00002C9C
		private void ShowEntries()
		{
			if (this.m_entriesRoot != null && this.m_entriesBG != null && this.m_entryButton != null)
			{
				this.m_entriesRoot.gameObject.SetActive(true);
				this.ClearEntries();
				float size = (this.GetHeight(this.m_entryButton) + (float)this.m_entrySpacing) * (float)this.m_entries.Length + (float)this.m_entrySpacing;
				this.m_entriesBG.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
				RectTransform component = this.m_entryButton.GetComponent<RectTransform>();
				this.SetText(this.m_entryButton, this.m_entries[0]);
				this.SetOnClick(this.m_entryButton, 0);
				this.m_entryButton.interactable = (this.m_selectedIndex != 0);
				for (int i = 1; i < this.m_entries.Length; i++)
				{
					Button button = Object.Instantiate<Button>(this.m_entryButton);
					button.interactable = (i != this.m_selectedIndex);
					RectTransform component2 = button.GetComponent<RectTransform>();
					component2.SetParent(component.parent, true);
					component2.localScale = component.localScale;
					component2.offsetMin = component.offsetMin;
					component2.offsetMax = component.offsetMax;
					component2.localPosition = component.localPosition + Vector3.down * (float)i * (this.GetHeight(this.m_entryButton) + (float)this.m_entrySpacing);
					this.SetText(button, this.m_entries[i]);
					this.SetOnClick(button, i);
				}
				if (this.m_entriesScrollbar != null)
				{
					base.StartCoroutine(this.UpdateScrollBarVisibility());
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004C42 File Offset: 0x00002E42
		private void HideEntries()
		{
			if (this.m_entriesRoot != null)
			{
				this.ClearEntries();
				this.m_entriesRoot.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004C6C File Offset: 0x00002E6C
		private void ClearEntries()
		{
			if (this.m_entriesBG != null && this.m_entryButton != null)
			{
				for (int i = 0; i < this.m_entriesBG.childCount; i++)
				{
					if (this.m_entriesBG.GetChild(i) != this.m_entryButton.transform)
					{
						Object.Destroy(this.m_entriesBG.GetChild(i).gameObject);
					}
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004CE0 File Offset: 0x00002EE0
		private void UpdateText()
		{
			if (this.m_text != null)
			{
				bool flag = this.m_selectedIndex >= 0 && this.m_selectedIndex < this.m_entries.Length;
				this.m_text.text = this.m_staticText + (flag ? this.m_entries[this.m_selectedIndex] : this.m_nothingSelectedText);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004D48 File Offset: 0x00002F48
		private void SetOnClick(Button p_button, int p_selectedIndex)
		{
			p_button.onClick.RemoveAllListeners();
			p_button.onClick.AddListener(delegate()
			{
				this.Select(p_selectedIndex);
				this.HideEntries();
			});
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004D8C File Offset: 0x00002F8C
		private void SetText(Button p_button, string p_text)
		{
			Text componentInChildren = p_button.GetComponentInChildren<Text>();
			if (componentInChildren != null)
			{
				componentInChildren.text = p_text;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004DB0 File Offset: 0x00002FB0
		private float GetHeight(Button p_button)
		{
			if (!(p_button != null))
			{
				return 0f;
			}
			return this.GetHeight(p_button.GetComponent<RectTransform>());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004DD0 File Offset: 0x00002FD0
		private float GetHeight(RectTransform p_rTransform)
		{
			if (!(p_rTransform != null))
			{
				return 0f;
			}
			return p_rTransform.rect.yMax - p_rTransform.rect.yMin;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004E09 File Offset: 0x00003009
		private IEnumerator UpdateScrollBarVisibility()
		{
			yield return new WaitForEndOfFrame();
			if (this.m_entriesScrollbar != null)
			{
				this.m_entriesScrollbar.gameObject.SetActive(this.m_entriesScrollbar.size < 0.985f);
			}
			yield break;
		}

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private Button m_button;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private Text m_text;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private RectTransform m_entriesRoot;

		// Token: 0x04000060 RID: 96
		[SerializeField]
		private RectTransform m_entriesBG;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		private Scrollbar m_entriesScrollbar;

		// Token: 0x04000062 RID: 98
		[SerializeField]
		private Button m_entryButton;

		// Token: 0x04000063 RID: 99
		[SerializeField]
		private int m_entrySpacing = 5;

		// Token: 0x04000064 RID: 100
		[SerializeField]
		private string m_staticText = "";

		// Token: 0x04000065 RID: 101
		[SerializeField]
		private string m_nothingSelectedText = "";

		// Token: 0x04000066 RID: 102
		[SerializeField]
		protected bool m_improveNavigationFocus = true;

		// Token: 0x04000067 RID: 103
		[SerializeField]
		private string[] m_entries = new string[0];

		// Token: 0x04000068 RID: 104
		[SerializeField]
		private int m_selectedIndex = -1;
	}
}
