using System;
using UnityEngine;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000021 RID: 33
	public class uMyGUI_TexturePicker : MonoBehaviour
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006730 File Offset: 0x00004930
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00006738 File Offset: 0x00004938
		public GameObject TexturePrefab
		{
			get
			{
				return this.m_texturePrefab;
			}
			set
			{
				this.m_texturePrefab = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006741 File Offset: 0x00004941
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006749 File Offset: 0x00004949
		public GameObject SelectionPrefab
		{
			get
			{
				return this.m_selectionPrefab;
			}
			set
			{
				this.m_selectionPrefab = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006752 File Offset: 0x00004952
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000675A File Offset: 0x0000495A
		public float OffsetStart
		{
			get
			{
				return this.m_offsetStart;
			}
			set
			{
				this.m_offsetStart = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006763 File Offset: 0x00004963
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000676B File Offset: 0x0000496B
		public float OffsetEnd
		{
			get
			{
				return this.m_offsetEnd;
			}
			set
			{
				this.m_offsetEnd = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006774 File Offset: 0x00004974
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000677C File Offset: 0x0000497C
		public float Padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006785 File Offset: 0x00004985
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000678D File Offset: 0x0000498D
		public Action<int> ButtonCallback
		{
			get
			{
				return this.m_buttonCallback;
			}
			set
			{
				this.m_buttonCallback = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006796 File Offset: 0x00004996
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000679E File Offset: 0x0000499E
		public Texture2D[] Textures
		{
			get
			{
				return this.m_textures;
			}
			set
			{
				this.m_textures = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000067A8 File Offset: 0x000049A8
		private RectTransform RTransform
		{
			get
			{
				if (!(this.m_rectTransform != null))
				{
					return this.m_rectTransform = base.GetComponent<RectTransform>();
				}
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000067D9 File Offset: 0x000049D9
		public GameObject[] Instances
		{
			get
			{
				return this.m_instances;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000067E4 File Offset: 0x000049E4
		public void SetSelection(int p_selectionIndex)
		{
			if (!(this.m_selectionPrefab != null))
			{
				Debug.LogError("uMyGUI_TexturePicker: SetSelection: you have passed a non negative selection index '" + p_selectionIndex + "', but the SelectionPrefab was not provided in the inspector or via script!");
				return;
			}
			if (p_selectionIndex < 0 || p_selectionIndex >= this.m_instances.Length)
			{
				Object.Destroy(this.m_selectionInstance);
				this.m_selectionInstance = null;
				return;
			}
			if (this.m_selectionInstance == null)
			{
				this.m_selectionInstance = Object.Instantiate<GameObject>(this.m_selectionPrefab);
			}
			else
			{
				RectTransform component = this.m_selectionInstance.GetComponent<RectTransform>();
				Vector2 anchoredPosition = component.anchoredPosition;
				anchoredPosition.x = this.m_selectionPrefab.GetComponent<RectTransform>().anchoredPosition.x;
				component.anchoredPosition = anchoredPosition;
			}
			this.SetRectTransformPosition(this.m_selectionInstance.GetComponent<RectTransform>(), p_selectionIndex, this.m_elementSize);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000068AC File Offset: 0x00004AAC
		public void SetTextures(Texture2D[] p_textures, int p_selectedIndex)
		{
			if (this.m_texturePrefab != null)
			{
				this.m_textures = p_textures;
				Object.Destroy(this.m_selectionInstance);
				for (int i = 0; i < this.m_instances.Length; i++)
				{
					Object.Destroy(this.m_instances[i]);
				}
				this.m_instances = new GameObject[p_textures.Length];
				float num = 0f;
				for (int j = 0; j < p_textures.Length; j++)
				{
					this.m_instances[j] = Object.Instantiate<GameObject>(this.m_texturePrefab);
					RectTransform component = this.m_instances[j].GetComponent<RectTransform>();
					this.m_elementSize = component.rect.width;
					this.SetRectTransformPosition(component, j, this.m_elementSize);
					RawImage rawImage = this.TryFindComponent<RawImage>(this.m_instances[j]);
					if (rawImage != null)
					{
						rawImage.texture = p_textures[j];
					}
					else
					{
						Debug.LogError("uMyGUI_TexturePicker: SetTextures: TexturePrefab must have a RawImage component attached (can be in children).");
					}
					if (this.m_buttonCallback != null)
					{
						Button button = this.TryFindComponent<Button>(this.m_instances[j]);
						if (button != null)
						{
							int indexCopy = j;
							button.onClick.AddListener(delegate()
							{
								this.m_buttonCallback(indexCopy);
							});
						}
					}
					num = component.anchoredPosition.x + this.m_elementSize;
					if (j == p_selectedIndex)
					{
						if (this.m_selectionPrefab != null)
						{
							this.m_selectionInstance = Object.Instantiate<GameObject>(this.m_selectionPrefab);
							this.SetRectTransformPosition(this.m_selectionInstance.GetComponent<RectTransform>(), j, this.m_elementSize);
						}
						else
						{
							Debug.LogError("uMyGUI_TexturePicker: SetTextures: you have passed a non negative selection index '" + p_selectedIndex + "', but the SelectionPrefab was not provided in the inspector or via script!");
						}
					}
				}
				this.RTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num - this.RTransform.rect.xMin + this.m_offsetEnd);
				return;
			}
			Debug.LogError("uMyGUI_TexturePicker: SetTextures: you must provide the TexturePrefab in the inspector or via script!");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006A88 File Offset: 0x00004C88
		private void OnDestroy()
		{
			this.m_buttonCallback = null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006A94 File Offset: 0x00004C94
		private void SetRectTransformPosition(RectTransform p_transform, int p_positionIndex, float p_size)
		{
			p_transform.SetParent(this.RTransform, false);
			Vector2 anchoredPosition = p_transform.anchoredPosition;
			anchoredPosition.x += this.m_offsetStart + (float)p_positionIndex * (p_size + this.m_padding);
			p_transform.anchoredPosition = anchoredPosition;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006ADC File Offset: 0x00004CDC
		private T TryFindComponent<T>(GameObject p_object) where T : Component
		{
			T t = p_object.GetComponent<T>();
			if (t == null)
			{
				T[] componentsInChildren = p_object.GetComponentsInChildren<T>(true);
				if (componentsInChildren.Length != 0)
				{
					t = componentsInChildren[0];
				}
			}
			return t;
		}

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private GameObject m_texturePrefab;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private GameObject m_selectionPrefab;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private float m_offsetStart;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private float m_offsetEnd;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private float m_padding = 4f;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private Action<int> m_buttonCallback;

		// Token: 0x040000AE RID: 174
		private Texture2D[] m_textures = new Texture2D[0];

		// Token: 0x040000AF RID: 175
		private RectTransform m_rectTransform;

		// Token: 0x040000B0 RID: 176
		private float m_elementSize = 1f;

		// Token: 0x040000B1 RID: 177
		private GameObject m_selectionInstance;

		// Token: 0x040000B2 RID: 178
		private GameObject[] m_instances = new GameObject[0];
	}
}
