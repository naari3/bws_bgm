using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000022 RID: 34
	public class uMyGUI_TreeBrowser : MonoBehaviour
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006B49 File Offset: 0x00004D49
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006B51 File Offset: 0x00004D51
		public GameObject InnerNodePrefab
		{
			get
			{
				return this.m_innerNodePrefab;
			}
			set
			{
				this.m_innerNodePrefab = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006B5A File Offset: 0x00004D5A
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006B62 File Offset: 0x00004D62
		public GameObject LeafNodePrefab
		{
			get
			{
				return this.m_leafNodePrefab;
			}
			set
			{
				this.m_leafNodePrefab = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006B6B File Offset: 0x00004D6B
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006B73 File Offset: 0x00004D73
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006B7C File Offset: 0x00004D7C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006B84 File Offset: 0x00004D84
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006B8D File Offset: 0x00004D8D
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006B95 File Offset: 0x00004D95
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006B9E File Offset: 0x00004D9E
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006BA6 File Offset: 0x00004DA6
		public float IndentSize
		{
			get
			{
				return this.m_indentSize;
			}
			set
			{
				this.m_indentSize = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006BAF File Offset: 0x00004DAF
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006BB7 File Offset: 0x00004DB7
		public float ForcedEntryHeight
		{
			get
			{
				return this.m_forcedEntryHeight;
			}
			set
			{
				this.m_forcedEntryHeight = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006BC0 File Offset: 0x00004DC0
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006BC8 File Offset: 0x00004DC8
		public bool UseExplicitNavigation
		{
			get
			{
				return this.m_useExplicitNavigation;
			}
			set
			{
				this.m_useExplicitNavigation = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006BD1 File Offset: 0x00004DD1
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006BD9 File Offset: 0x00004DD9
		public float NavScrollSpeed
		{
			get
			{
				return this.m_navScrollSpeed;
			}
			set
			{
				this.m_navScrollSpeed = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006BE2 File Offset: 0x00004DE2
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006BEA File Offset: 0x00004DEA
		public float NavScrollSmooth
		{
			get
			{
				return this.m_navScrollSmooth;
			}
			set
			{
				this.m_navScrollSmooth = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006BF3 File Offset: 0x00004DF3
		public ScrollRect ParentScroller
		{
			get
			{
				return this.m_parentScroller;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006BFC File Offset: 0x00004DFC
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

		// Token: 0x0600010B RID: 267 RVA: 0x00006C2D File Offset: 0x00004E2D
		public void BuildTree(uMyGUI_TreeBrowser.Node[] p_rootNodes)
		{
			this.BuildTree(p_rootNodes, 0, 0);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006C38 File Offset: 0x00004E38
		public void BuildTree(uMyGUI_TreeBrowser.Node[] p_rootNodes, int p_insertAt, int p_indentLevel)
		{
			if (this.m_innerNodePrefab != null && this.m_leafNodePrefab != null)
			{
				List<uMyGUI_TreeBrowser.InternalNode> list = new List<uMyGUI_TreeBrowser.InternalNode>();
				float num = 0f;
				float p_currY = (this.m_nodes.Count >= p_insertAt && p_insertAt > 0) ? this.m_nodes[p_insertAt - 1].m_minY : (-this.m_offsetStart);
				for (int i = 0; i < p_rootNodes.Length; i++)
				{
					if (p_rootNodes[i] != null)
					{
						bool flag = p_rootNodes[i].Children != null && p_rootNodes[i].Children.Length != 0;
						GameObject gameObject;
						if (flag)
						{
							gameObject = Object.Instantiate<GameObject>(this.m_innerNodePrefab);
						}
						else
						{
							gameObject = Object.Instantiate<GameObject>(this.m_leafNodePrefab);
						}
						RectTransform component = gameObject.GetComponent<RectTransform>();
						if (this.m_forcedEntryHeight != 0f)
						{
							component.sizeDelta = new Vector2(component.sizeDelta.x, this.m_forcedEntryHeight);
						}
						float height = component.rect.height;
						if (p_rootNodes[i].SendMessageData != null)
						{
							if (!gameObject.activeInHierarchy)
							{
								Debug.LogError("uMyGUI_TreeBrowser: BuildTree: node has SendMessageData set, but instance is inactive! SendMessage call will fail! Make your prefab active!");
							}
							gameObject.SendMessage("uMyGUI_TreeBrowser_InitNode", p_rootNodes[i].SendMessageData);
						}
						uMyGUI_TreeBrowser.InternalNode internalNode = new uMyGUI_TreeBrowser.InternalNode(p_rootNodes[i], gameObject, p_indentLevel);
						list.Add(internalNode);
						if (flag)
						{
							this.SetupInnerNode(internalNode);
						}
						else
						{
							this.SetupLeafNode(internalNode);
						}
						p_currY = this.SetRectTransformPosition(component, p_currY, height, p_indentLevel);
						internalNode.m_minY = component.anchoredPosition.y - height;
						num = internalNode.m_minY;
						if (this.OnNodeInstantiate != null)
						{
							this.OnNodeInstantiate(this, new uMyGUI_TreeBrowser.NodeInstantiateEventArgs(p_rootNodes[i], gameObject));
						}
					}
				}
				if (p_insertAt < this.m_nodes.Count)
				{
					float p_moveDist;
					if (p_insertAt == 0)
					{
						p_moveDist = num;
					}
					else
					{
						p_moveDist = num - this.m_nodes[p_insertAt - 1].m_minY;
					}
					this.UpdateNodePosition(p_insertAt, p_moveDist);
				}
				if (p_insertAt < this.m_nodes.Count)
				{
					this.m_nodes.InsertRange(p_insertAt, list);
				}
				else
				{
					this.m_nodes.AddRange(list);
				}
				if (this.m_nodes.Count > 0)
				{
					this.RTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(this.m_nodes[this.m_nodes.Count - 1].m_minY - this.RTransform.rect.yMax - this.m_offsetEnd));
				}
				if (this.m_useExplicitNavigation)
				{
					this.SetExplicitNavigationTargets();
					return;
				}
			}
			else
			{
				Debug.LogError("uMyGUI_TreeBrowser: BuildTree: you must provide the InnerNodePrefab and LeafNodePrefab in the inspector or via script!");
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006EB0 File Offset: 0x000050B0
		public void Clear()
		{
			for (int i = 0; i < this.m_nodes.Count; i++)
			{
				Object.Destroy(this.m_nodes[i].m_instance);
			}
			this.m_nodes.Clear();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006EF4 File Offset: 0x000050F4
		private void Start()
		{
			this.m_parentScroller = base.GetComponentInParent<ScrollRect>();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006F04 File Offset: 0x00005104
		private void LateUpdate()
		{
			if (this.m_parentScroller == null)
			{
				return;
			}
			EventSystem current = EventSystem.current;
			GameObject currentSelectedGameObject;
			if (current != null && (currentSelectedGameObject = current.currentSelectedGameObject) != null && currentSelectedGameObject.transform.IsChildOf(base.transform))
			{
				if (currentSelectedGameObject != this.m_lastSelectedGO)
				{
					this.m_lastSelectedGO = currentSelectedGameObject;
					Transform transform = currentSelectedGameObject.transform;
					while (transform.parent != base.transform && transform.parent != null)
					{
						transform = transform.parent;
					}
					RectTransform component = transform.GetComponent<RectTransform>();
					if (component == null)
					{
						return;
					}
					Vector3[] array = new Vector3[4];
					component.GetWorldCorners(array);
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.m_parentScroller.transform.InverseTransformPoint(array[i]);
					}
					Vector3 vector = Vector3.Min(Vector3.Min(array[0], array[1]), Vector3.Min(array[2], array[3]));
					Vector3 vector2 = Vector3.Max(Vector3.Max(array[0], array[1]), Vector3.Max(array[2], array[3]));
					this.m_parentScroller.GetComponent<RectTransform>().GetLocalCorners(array);
					Vector3 vector3 = Vector3.Min(Vector3.Min(array[0], array[1]), Vector3.Min(array[2], array[3]));
					Vector3 vector4 = Vector3.Max(Vector3.Max(array[0], array[1]), Vector3.Max(array[2], array[3]));
					if (vector.y < vector3.y)
					{
						if (this.m_parentScroller.verticalNormalizedPosition >= 1f)
						{
							this.m_parentScroller.verticalNormalizedPosition = 0.999f;
						}
						this.m_parentScroller.velocity = Vector3.up * Mathf.Max(5f, this.m_navScrollSpeed * ((this.m_navScrollSmooth != 0f) ? ((vector3.y - vector.y) / this.m_navScrollSmooth) : 1f));
						this.m_lastSelectedGO = null;
						return;
					}
					if (vector2.y > vector4.y)
					{
						if (this.m_parentScroller.verticalNormalizedPosition <= 0f)
						{
							this.m_parentScroller.verticalNormalizedPosition = 0.001f;
						}
						this.m_parentScroller.velocity = Vector3.down * Mathf.Max(5f, this.m_navScrollSpeed * ((this.m_navScrollSmooth != 0f) ? ((vector2.y - vector4.y) / this.m_navScrollSmooth) : 1f));
						this.m_lastSelectedGO = null;
						return;
					}
				}
			}
			else
			{
				this.m_lastSelectedGO = null;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000071F8 File Offset: 0x000053F8
		private void OnDestroy()
		{
			this.OnInnerNodeClick = null;
			this.OnLeafNodeClick = null;
			this.OnNodeInstantiate = null;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007210 File Offset: 0x00005410
		private void SetExplicitNavigationTargets()
		{
			if (this.m_nodes.Count > 2)
			{
				RectTransform rectTransform = this.m_nodes[0].m_transform;
				RectTransform transform = this.m_nodes[1].m_transform;
				Selectable[] array = rectTransform.GetComponentsInChildren<Selectable>();
				Selectable[] componentsInChildren = transform.GetComponentsInChildren<Selectable>();
				this.SetAutomaticNavigation(this.m_nodes[0].m_transform);
				this.SetAutomaticNavigation(this.m_nodes[this.m_nodes.Count - 1].m_transform);
				for (int i = 1; i < this.m_nodes.Count - 1; i++)
				{
					Object x = rectTransform;
					rectTransform = transform;
					transform = this.m_nodes[i + 1].m_transform;
					Selectable[] array2 = array;
					array = componentsInChildren;
					componentsInChildren = transform.GetComponentsInChildren<Selectable>();
					if (x != null && rectTransform != null && transform != null && array2.Length == array.Length && componentsInChildren.Length == array.Length)
					{
						for (int j = 0; j < array.Length; j++)
						{
							Navigation navigation = array[j].navigation;
							navigation.mode = Navigation.Mode.Explicit;
							navigation.selectOnUp = array2[j];
							navigation.selectOnDown = componentsInChildren[j];
							array[j].navigation = navigation;
						}
					}
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007358 File Offset: 0x00005558
		private void SetAutomaticNavigation(RectTransform p_nodeTransform)
		{
			if (p_nodeTransform != null)
			{
				Selectable[] componentsInChildren = p_nodeTransform.GetComponentsInChildren<Selectable>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Navigation navigation = componentsInChildren[i].navigation;
					navigation.mode = Navigation.Mode.Automatic;
					componentsInChildren[i].navigation = navigation;
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000073A0 File Offset: 0x000055A0
		private float SetRectTransformPosition(RectTransform p_transform, float p_currY, float p_size, int p_indentLevel)
		{
			p_transform.SetParent(this.RTransform, false);
			Vector2 anchoredPosition = p_transform.anchoredPosition;
			anchoredPosition.x += (float)p_indentLevel * this.m_indentSize;
			anchoredPosition.y += p_currY;
			p_currY -= this.m_padding + p_size;
			p_transform.anchoredPosition = anchoredPosition;
			return p_currY;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000073F8 File Offset: 0x000055F8
		private void UpdateNodePosition(int p_startIndex, float p_moveDist)
		{
			for (int i = p_startIndex; i < this.m_nodes.Count; i++)
			{
				Vector2 anchoredPosition = this.m_nodes[i].m_transform.anchoredPosition;
				anchoredPosition.y += p_moveDist;
				this.m_nodes[i].m_transform.anchoredPosition = anchoredPosition;
				this.m_nodes[i].m_minY = anchoredPosition.y - this.m_nodes[i].m_transform.rect.height;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007490 File Offset: 0x00005690
		private void SetupInnerNode(uMyGUI_TreeBrowser.InternalNode p_node)
		{
			if (p_node.m_instance.GetComponent<Button>() != null)
			{
				p_node.m_instance.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.ToggleInnerNodeFoldout(p_node);
				});
				return;
			}
			if (p_node.m_instance.GetComponent<Toggle>() != null)
			{
				p_node.m_instance.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool p_isOn)
				{
					this.ToggleInnerNodeFoldout(p_node);
				});
				return;
			}
			Debug.LogError("uMyGUI_TreeBrowser: BuildTree: the inner node prefabs must have either a Button or a Toggle script attached to the root. Otherwise they cannot fold out!");
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000753C File Offset: 0x0000573C
		private void SetupLeafNode(uMyGUI_TreeBrowser.InternalNode p_node)
		{
			if (p_node.m_instance.GetComponent<Button>() != null)
			{
				p_node.m_instance.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.SafeCallOnLeafNodeClick(p_node);
				});
			}
			else if (p_node.m_instance.GetComponent<Toggle>() != null)
			{
				p_node.m_instance.GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool p_isOn)
				{
					this.SafeCallOnLeafNodeClick(p_node);
				});
			}
			EventTrigger eventTrigger = p_node.m_instance.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = p_node.m_instance.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
			triggerEvent.AddListener(delegate(BaseEventData p_downEvent)
			{
				this.SafeCallOnLeafNodePointerDown(p_node);
			});
			entry.callback = triggerEvent;
			if (eventTrigger.triggers == null)
			{
				eventTrigger.triggers = new List<EventTrigger.Entry>();
			}
			eventTrigger.triggers.Add(entry);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007654 File Offset: 0x00005854
		private void ToggleInnerNodeFoldout(uMyGUI_TreeBrowser.InternalNode p_node)
		{
			int num = this.m_nodes.IndexOf(p_node);
			p_node.m_isFoldout = !p_node.m_isFoldout;
			if (p_node.m_isFoldout)
			{
				this.BuildTree(p_node.m_node.Children, num + 1, p_node.m_indentLevel + 1);
			}
			else
			{
				float num2 = 0f;
				for (int i = 0; i < p_node.m_node.Children.Length; i++)
				{
					int index = num + p_node.m_node.Children.Length - i;
					uMyGUI_TreeBrowser.InternalNode internalNode = this.m_nodes[index];
					num2 += internalNode.m_transform.rect.height;
					if (i + 1 < p_node.m_node.Children.Length)
					{
						num2 += this.m_padding;
					}
					if (internalNode.m_isFoldout)
					{
						this.ToggleInnerNodeFoldout(internalNode);
					}
					this.m_nodes.RemoveAt(index);
					Object.Destroy(internalNode.m_instance);
				}
				this.UpdateNodePosition(num + 1, num2);
				this.RTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.RTransform.sizeDelta.y - num2);
			}
			if (this.OnInnerNodeClick != null)
			{
				this.OnInnerNodeClick(this, new uMyGUI_TreeBrowser.NodeClickEventArgs(p_node.m_node));
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000778D File Offset: 0x0000598D
		private void SafeCallOnLeafNodePointerDown(uMyGUI_TreeBrowser.InternalNode p_node)
		{
			if (this.OnLeafNodePointerDown != null)
			{
				this.OnLeafNodePointerDown(this, new uMyGUI_TreeBrowser.NodeClickEventArgs(p_node.m_node));
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000077AE File Offset: 0x000059AE
		private void SafeCallOnLeafNodeClick(uMyGUI_TreeBrowser.InternalNode p_node)
		{
			if (this.OnLeafNodeClick != null)
			{
				this.OnLeafNodeClick(this, new uMyGUI_TreeBrowser.NodeClickEventArgs(p_node.m_node));
			}
		}

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private GameObject m_innerNodePrefab;

		// Token: 0x040000B4 RID: 180
		[SerializeField]
		private GameObject m_leafNodePrefab;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private float m_offsetStart;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private float m_offsetEnd;

		// Token: 0x040000B7 RID: 183
		[SerializeField]
		private float m_padding = 4f;

		// Token: 0x040000B8 RID: 184
		[SerializeField]
		private float m_indentSize = 20f;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		private float m_forcedEntryHeight;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private bool m_useExplicitNavigation;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private float m_navScrollSpeed = 200f;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		private float m_navScrollSmooth = 20f;

		// Token: 0x040000BD RID: 189
		private ScrollRect m_parentScroller;

		// Token: 0x040000BE RID: 190
		public EventHandler<uMyGUI_TreeBrowser.NodeClickEventArgs> OnInnerNodeClick;

		// Token: 0x040000BF RID: 191
		public EventHandler<uMyGUI_TreeBrowser.NodeClickEventArgs> OnLeafNodeClick;

		// Token: 0x040000C0 RID: 192
		public EventHandler<uMyGUI_TreeBrowser.NodeClickEventArgs> OnLeafNodePointerDown;

		// Token: 0x040000C1 RID: 193
		public EventHandler<uMyGUI_TreeBrowser.NodeInstantiateEventArgs> OnNodeInstantiate;

		// Token: 0x040000C2 RID: 194
		private RectTransform m_rectTransform;

		// Token: 0x040000C3 RID: 195
		private List<uMyGUI_TreeBrowser.InternalNode> m_nodes = new List<uMyGUI_TreeBrowser.InternalNode>();

		// Token: 0x040000C4 RID: 196
		private GameObject m_lastSelectedGO;

		// Token: 0x0200007E RID: 126
		public class Node
		{
			// Token: 0x060002D8 RID: 728 RVA: 0x0000E740 File Offset: 0x0000C940
			public Node(object p_sendMessageData, uMyGUI_TreeBrowser.Node[] p_children)
			{
				this.SendMessageData = p_sendMessageData;
				this.Children = p_children;
			}

			// Token: 0x04000248 RID: 584
			public readonly object SendMessageData;

			// Token: 0x04000249 RID: 585
			public readonly uMyGUI_TreeBrowser.Node[] Children;
		}

		// Token: 0x0200007F RID: 127
		public class NodeClickEventArgs : EventArgs
		{
			// Token: 0x060002D9 RID: 729 RVA: 0x0000E756 File Offset: 0x0000C956
			public NodeClickEventArgs(uMyGUI_TreeBrowser.Node p_clickedNode)
			{
				this.ClickedNode = p_clickedNode;
			}

			// Token: 0x0400024A RID: 586
			public readonly uMyGUI_TreeBrowser.Node ClickedNode;
		}

		// Token: 0x02000080 RID: 128
		public class NodeInstantiateEventArgs : EventArgs
		{
			// Token: 0x060002DA RID: 730 RVA: 0x0000E765 File Offset: 0x0000C965
			public NodeInstantiateEventArgs(uMyGUI_TreeBrowser.Node p_node, GameObject p_instance)
			{
				this.Node = p_node;
				this.Instance = p_instance;
			}

			// Token: 0x0400024B RID: 587
			public readonly uMyGUI_TreeBrowser.Node Node;

			// Token: 0x0400024C RID: 588
			public readonly GameObject Instance;
		}

		// Token: 0x02000081 RID: 129
		private class InternalNode
		{
			// Token: 0x060002DB RID: 731 RVA: 0x0000E77B File Offset: 0x0000C97B
			public InternalNode(uMyGUI_TreeBrowser.Node p_node, GameObject p_instance, int p_indentLevel)
			{
				this.m_node = p_node;
				this.m_instance = p_instance;
				this.m_indentLevel = p_indentLevel;
				this.m_transform = this.m_instance.GetComponent<RectTransform>();
			}

			// Token: 0x0400024D RID: 589
			public readonly uMyGUI_TreeBrowser.Node m_node;

			// Token: 0x0400024E RID: 590
			public GameObject m_instance;

			// Token: 0x0400024F RID: 591
			public int m_indentLevel;

			// Token: 0x04000250 RID: 592
			public RectTransform m_transform;

			// Token: 0x04000251 RID: 593
			public bool m_isFoldout;

			// Token: 0x04000252 RID: 594
			public float m_minY;
		}
	}
}
