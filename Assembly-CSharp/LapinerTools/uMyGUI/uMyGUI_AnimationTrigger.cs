using System;
using System.Collections;
using UnityEngine;

namespace LapinerTools.uMyGUI
{
	// Token: 0x02000013 RID: 19
	public class uMyGUI_AnimationTrigger : MonoBehaviour
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000408C File Offset: 0x0000228C
		private void OnEnable()
		{
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.ON_ENABLE)
			{
				this.Play();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000409C File Offset: 0x0000229C
		private void OnDisable()
		{
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.ON_DISABLE)
			{
				this.Play();
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000040B0 File Offset: 0x000022B0
		private void uMyGUI_OnActivateTab()
		{
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.ON_UMYGUI_ACTIVATETAB)
			{
				this.Play();
				return;
			}
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.REDIRECT_ONMYGUI_EVENTS)
			{
				if (this.m_redirectDestination == null)
				{
					Debug.LogError("LE_AnimationTrigger: uMyGUI_OnActivateTab: REDIRECT_ONMYGUI_EVENTS mode requires m_redirectDestination to be set!");
					return;
				}
				if (this.m_redirectDestination.activeInHierarchy)
				{
					this.m_redirectDestination.SendMessage("uMyGUI_OnActivateTab");
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000410C File Offset: 0x0000230C
		private void uMyGUI_OnDeactivateTab()
		{
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.ON_UMYGUI_DEACTIVATETAB)
			{
				this.Play();
				return;
			}
			if (this.m_condition == uMyGUI_AnimationTrigger.ETriggerMode.REDIRECT_ONMYGUI_EVENTS)
			{
				if (this.m_redirectDestination == null)
				{
					Debug.LogError("LE_AnimationTrigger: uMyGUI_OnDeactivateTab: REDIRECT_ONMYGUI_EVENTS mode requires m_redirectDestination to be set!");
					return;
				}
				if (this.m_redirectDestination.activeInHierarchy)
				{
					this.m_redirectDestination.SendMessage("uMyGUI_OnDeactivateTab");
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004168 File Offset: 0x00002368
		private void Play()
		{
			if (this.m_animation != null)
			{
				if (this.m_isActivateOnAnimStart)
				{
					this.m_animation.gameObject.SetActive(true);
				}
				if (this.m_isDeactivateOnAnimEnd && this.m_animation[this.m_clipName] != null)
				{
					((this.m_alternativeCoroutineWorker != null) ? this.m_alternativeCoroutineWorker : this).StartCoroutine(this.DeactivateAfterDelay(this.m_animation.gameObject, this.m_animation[this.m_clipName].length));
				}
				this.m_animation.Play(this.m_clipName);
				return;
			}
			Debug.LogError("LE_AnimationTrigger: OnDisable: lost reference to Animation!");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004221 File Offset: 0x00002421
		private IEnumerator DeactivateAfterDelay(GameObject p_object, float p_delay)
		{
			yield return new WaitForSeconds(p_delay);
			if (p_object != null)
			{
				p_object.SetActive(false);
			}
			yield break;
		}

		// Token: 0x04000041 RID: 65
		[SerializeField]
		private Animation m_animation;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		private string m_clipName;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		private uMyGUI_AnimationTrigger.ETriggerMode m_condition;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private bool m_isActivateOnAnimStart;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private bool m_isDeactivateOnAnimEnd;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private MonoBehaviour m_alternativeCoroutineWorker;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		private GameObject m_redirectDestination;

		// Token: 0x02000072 RID: 114
		public enum ETriggerMode
		{
			// Token: 0x04000226 RID: 550
			ON_ENABLE,
			// Token: 0x04000227 RID: 551
			ON_DISABLE,
			// Token: 0x04000228 RID: 552
			ON_UMYGUI_ACTIVATETAB,
			// Token: 0x04000229 RID: 553
			ON_UMYGUI_DEACTIVATETAB,
			// Token: 0x0400022A RID: 554
			REDIRECT_ONMYGUI_EVENTS
		}
	}
}
