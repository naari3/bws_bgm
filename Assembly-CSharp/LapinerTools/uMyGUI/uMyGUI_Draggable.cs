using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000015 RID: 21
	public class uMyGUI_Draggable : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004531 File Offset: 0x00002731
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00004539 File Offset: 0x00002739
		public bool IsResetRotationWhenDragged
		{
			get
			{
				return this.m_isResetRotationWhenDragged;
			}
			set
			{
				this.m_isResetRotationWhenDragged = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004542 File Offset: 0x00002742
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000454A File Offset: 0x0000274A
		public bool IsSnapBackOnEndDrag
		{
			get
			{
				return this.m_isSnapBackOnEndDrag;
			}
			set
			{
				this.m_isSnapBackOnEndDrag = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004553 File Offset: 0x00002753
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000455B File Offset: 0x0000275B
		public bool IsTopInHierarchyWhenDragged
		{
			get
			{
				return this.m_isTopInHierarchyWhenDragged;
			}
			set
			{
				this.m_isTopInHierarchyWhenDragged = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004564 File Offset: 0x00002764
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000456C File Offset: 0x0000276C
		public CanvasGroup DisableBlocksRaycastsOnDrag
		{
			get
			{
				return this.m_disableBlocksRaycastsOnDrag;
			}
			set
			{
				this.m_disableBlocksRaycastsOnDrag = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004575 File Offset: 0x00002775
		public bool IsDragged
		{
			get
			{
				return this.m_isDragged;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004580 File Offset: 0x00002780
		public void OnBeginDrag(PointerEventData p_event)
		{
			this.m_isDragged = true;
			this.m_initialParentTransform = (this.m_transform.parent as RectTransform);
			this.m_initialPosition = this.m_transform.position;
			this.m_initialRotation = this.m_transform.rotation;
			Vector3 b;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_canvasTransform, p_event.position, p_event.pressEventCamera, out b))
			{
				this.m_dragOffset = this.m_transform.position - b;
			}
			else
			{
				this.m_dragOffset = Vector3.zero;
			}
			if (this.m_isResetRotationWhenDragged)
			{
				this.m_transform.rotation = Quaternion.identity;
			}
			if (this.m_isTopInHierarchyWhenDragged)
			{
				this.m_initialSiblingIndex = this.m_transform.GetSiblingIndex();
				this.m_transform.SetParent(this.m_canvasTransform, true);
				this.m_transform.SetAsLastSibling();
			}
			if (this.m_disableBlocksRaycastsOnDrag != null)
			{
				this.m_disableBlocksRaycastsOnDrag.blocksRaycasts = false;
			}
			if (this.m_onBeginDrag != null)
			{
				this.m_onBeginDrag(this, new uMyGUI_Draggable.DragEvent(p_event));
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004690 File Offset: 0x00002890
		public void OnDrag(PointerEventData p_event)
		{
			Vector3 a;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_canvasTransform, p_event.position, p_event.pressEventCamera, out a))
			{
				this.m_transform.position = a + this.m_dragOffset;
			}
			if (this.m_onDrag != null)
			{
				this.m_onDrag(this, new uMyGUI_Draggable.DragEvent(p_event));
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000046EC File Offset: 0x000028EC
		public void OnEndDrag(PointerEventData p_event)
		{
			if (this.m_isDragged)
			{
				this.m_isDragged = false;
				if (this.m_isSnapBackOnEndDrag)
				{
					this.m_transform.position = this.m_initialPosition;
					this.m_transform.rotation = this.m_initialRotation;
				}
				if (this.m_isTopInHierarchyWhenDragged)
				{
					this.m_transform.SetParent(this.m_initialParentTransform, true);
					this.m_transform.SetSiblingIndex(this.m_initialSiblingIndex);
				}
				if (this.m_disableBlocksRaycastsOnDrag != null)
				{
					this.m_disableBlocksRaycastsOnDrag.blocksRaycasts = true;
				}
				if (this.m_onEndDrag != null)
				{
					this.m_onEndDrag(this, new uMyGUI_Draggable.DragEvent(p_event));
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004794 File Offset: 0x00002994
		private void Start()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			if (componentInParent != null)
			{
				this.m_canvasTransform = componentInParent.GetComponent<RectTransform>();
			}
			else
			{
				Debug.LogError("uMyGUI_Draggable: no Canvas component was found in parent!");
				base.enabled = false;
			}
			this.m_transform = base.GetComponent<RectTransform>();
		}

		// Token: 0x0400004E RID: 78
		[SerializeField]
		private bool m_isResetRotationWhenDragged;

		// Token: 0x0400004F RID: 79
		[SerializeField]
		private bool m_isSnapBackOnEndDrag;

		// Token: 0x04000050 RID: 80
		[SerializeField]
		private bool m_isTopInHierarchyWhenDragged = true;

		// Token: 0x04000051 RID: 81
		[SerializeField]
		private CanvasGroup m_disableBlocksRaycastsOnDrag;

		// Token: 0x04000052 RID: 82
		private bool m_isDragged;

		// Token: 0x04000053 RID: 83
		public EventHandler<uMyGUI_Draggable.DragEvent> m_onBeginDrag;

		// Token: 0x04000054 RID: 84
		public EventHandler<uMyGUI_Draggable.DragEvent> m_onDrag;

		// Token: 0x04000055 RID: 85
		public EventHandler<uMyGUI_Draggable.DragEvent> m_onEndDrag;

		// Token: 0x04000056 RID: 86
		private RectTransform m_initialParentTransform;

		// Token: 0x04000057 RID: 87
		private RectTransform m_canvasTransform;

		// Token: 0x04000058 RID: 88
		private RectTransform m_transform;

		// Token: 0x04000059 RID: 89
		private int m_initialSiblingIndex;

		// Token: 0x0400005A RID: 90
		private Vector3 m_initialPosition;

		// Token: 0x0400005B RID: 91
		private Quaternion m_initialRotation;

		// Token: 0x0400005C RID: 92
		private Vector3 m_dragOffset = Vector3.zero;

		// Token: 0x02000075 RID: 117
		public class DragEvent : EventArgs
		{
			// Token: 0x060002BD RID: 701 RVA: 0x0000E547 File Offset: 0x0000C747
			public DragEvent(PointerEventData p_event)
			{
				this.m_event = p_event;
			}

			// Token: 0x04000230 RID: 560
			public readonly PointerEventData m_event;
		}
	}
}
