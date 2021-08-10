using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000020 RID: 32
	public class uMyGUI_TabBox : MonoBehaviour
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000062E4 File Offset: 0x000044E4
		public void SelectTab(int p_tabIndex)
		{
			if (p_tabIndex == this.m_selectedIndex)
			{
				return;
			}
			if (p_tabIndex < 0 || p_tabIndex >= this.m_tabs.Length)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"uMyGUI_TabBox: SelectTab tab index '",
					p_tabIndex,
					"' is out of bounds [0,",
					this.m_tabs.Length,
					"]!"
				}));
				return;
			}
			if (this.m_tabs[p_tabIndex] == null)
			{
				Debug.LogError("uMyGUI_TabBox: SelectTab tab index '" + p_tabIndex + "' is null! Check the tabs array in the inspector!");
				return;
			}
			if (this.m_isMoveDownInHierarchyOnSelect)
			{
				this.m_tabs[p_tabIndex].SetAsLastSibling();
			}
			switch (this.m_animMode)
			{
			case uMyGUI_TabBox.EAnimMode.TAB_ONLY:
				base.StopAllCoroutines();
				this.AnimateRectRectTransformSelection(p_tabIndex, this.m_tabs, this.m_fadeInAnimTab, this.m_fadeOutAnimTab, true);
				goto IL_10F;
			case uMyGUI_TabBox.EAnimMode.BTN_ONLY:
				base.StopAllCoroutines();
				this.AnimateRectRectTransformSelection(p_tabIndex, this.m_btns, this.m_fadeInAnimBtn, this.m_fadeOutAnimBtn, false);
				this.UpdateTabActiveStates(p_tabIndex);
				goto IL_10F;
			case uMyGUI_TabBox.EAnimMode.TAB_AND_BTN:
				base.StopAllCoroutines();
				this.AnimateRectRectTransformSelection(p_tabIndex, this.m_tabs, this.m_fadeInAnimTab, this.m_fadeOutAnimTab, true);
				this.AnimateRectRectTransformSelection(p_tabIndex, this.m_btns, this.m_fadeInAnimBtn, this.m_fadeOutAnimBtn, false);
				goto IL_10F;
			}
			this.UpdateTabActiveStates(p_tabIndex);
			IL_10F:
			this.m_selectedIndex = p_tabIndex;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000644C File Offset: 0x0000464C
		private void Start()
		{
			if (this.m_isSelectTabOnStart)
			{
				this.UpdateTabActiveStates(this.m_selectedIndex);
				if (this.m_isPlayTabAnimOnStart && (this.m_animMode == uMyGUI_TabBox.EAnimMode.TAB_ONLY || this.m_animMode == uMyGUI_TabBox.EAnimMode.TAB_AND_BTN))
				{
					this.AnimateRectRectTransformSelection(this.m_selectedIndex, this.m_tabs, this.m_fadeInAnimTab, this.m_fadeOutAnimTab, false);
				}
				if (this.m_isPlayBtnAnimOnStart)
				{
					this.AnimateRectRectTransformSelection(this.m_selectedIndex, this.m_btns, this.m_fadeInAnimBtn, this.m_fadeOutAnimBtn, false);
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000064D0 File Offset: 0x000046D0
		private void UpdateTabActiveStates(int p_tabIndex)
		{
			for (int i = 0; i < this.m_tabs.Length; i++)
			{
				bool flag = i == p_tabIndex;
				if (this.m_tabs[i] != null)
				{
					if (this.m_isSendMessage)
					{
						this.m_tabs[i].gameObject.SendMessage(flag ? "uMyGUI_OnActivateTab" : "uMyGUI_OnDeactivateTab", SendMessageOptions.DontRequireReceiver);
					}
					this.m_tabs[i].gameObject.SetActive(flag);
				}
				if (this.m_btns.Length > i && this.m_btns[i] != null)
				{
					Selectable component = this.m_btns[i].GetComponent<Selectable>();
					if (component != null)
					{
						component.interactable = !flag;
					}
				}
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006584 File Offset: 0x00004784
		private void AnimateRectRectTransformSelection(int p_tabIndex, RectTransform[] p_transforms, string p_fadeInAnim, string p_fadeOutAnim, bool p_isActivateChanged)
		{
			for (int i = 0; i < p_transforms.Length; i++)
			{
				if (p_transforms[i] != null)
				{
					Animation component = p_transforms[i].GetComponent<Animation>();
					if (component != null)
					{
						if (i == this.m_selectedIndex && p_tabIndex != this.m_selectedIndex)
						{
							if (component[p_fadeOutAnim] != null)
							{
								if (p_isActivateChanged)
								{
									base.StartCoroutine(this.DeactivateAfterDelay(p_transforms[i].gameObject, component[p_fadeOutAnim].length));
								}
								if (this.m_isSendMessage)
								{
									p_transforms[i].gameObject.SendMessage("uMyGUI_OnDeactivateTab", SendMessageOptions.DontRequireReceiver);
								}
								component.Play(p_fadeOutAnim);
							}
						}
						else if (i == p_tabIndex)
						{
							if (p_isActivateChanged)
							{
								p_transforms[i].gameObject.SetActive(true);
							}
							if (this.m_isSendMessage)
							{
								p_transforms[i].gameObject.SendMessage("uMyGUI_OnActivateTab", SendMessageOptions.DontRequireReceiver);
							}
							component.Play(p_fadeInAnim);
						}
					}
					else
					{
						Debug.LogError("uMyGUI_TabBox: AnimateRectRectTransformSelection: object at index '" + i + "' has no Animation component and cannot fade in or out!");
					}
					Selectable component2 = p_transforms[i].GetComponent<Selectable>();
					if (component2 != null)
					{
						component2.interactable = (i != p_tabIndex);
					}
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000066AD File Offset: 0x000048AD
		private IEnumerator DeactivateAfterDelay(GameObject p_object, float p_delay)
		{
			yield return new WaitForSeconds(p_delay);
			if (p_object != null)
			{
				p_object.SetActive(false);
			}
			yield break;
		}

		// Token: 0x04000099 RID: 153
		public const string SEND_MESSAGE_ACTIVATE_NAME = "uMyGUI_OnActivateTab";

		// Token: 0x0400009A RID: 154
		public const string SEND_MESSAGE_DEACTIVATE_NAME = "uMyGUI_OnDeactivateTab";

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private RectTransform[] m_btns = new RectTransform[0];

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private RectTransform[] m_tabs = new RectTransform[0];

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private int m_selectedIndex;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		private bool m_isSelectTabOnStart = true;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private bool m_isPlayTabAnimOnStart = true;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private bool m_isPlayBtnAnimOnStart = true;

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private uMyGUI_TabBox.EAnimMode m_animMode;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private string m_fadeInAnimTab = "tab_fade_in";

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private string m_fadeOutAnimTab = "tab_fade_out";

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private string m_fadeInAnimBtn = "btn_fade_in";

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private string m_fadeOutAnimBtn = "btn_fade_out";

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private bool m_isSendMessage;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private bool m_isMoveDownInHierarchyOnSelect;

		// Token: 0x0200007B RID: 123
		public enum EAnimMode
		{
			// Token: 0x0400023E RID: 574
			NONE,
			// Token: 0x0400023F RID: 575
			TAB_ONLY,
			// Token: 0x04000240 RID: 576
			BTN_ONLY,
			// Token: 0x04000241 RID: 577
			TAB_AND_BTN
		}
	}
}
