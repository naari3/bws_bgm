using System;
using System.Collections.Generic;
using LapinerTools.Steam.Data;
using LapinerTools.Steam.Data.Internal;
using Steamworks;
using UnityEngine;

namespace LapinerTools.Steam
{
	// Token: 0x02000024 RID: 36
	public class SteamMainBase<SteamMainT> : MonoBehaviour where SteamMainT : SteamMainBase<SteamMainT>
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008660 File Offset: 0x00006860
		public static SteamMainT Instance
		{
			get
			{
				if (SteamMainBase<SteamMainT>.s_instance == null)
				{
					SteamMainBase<SteamMainT>.s_instance = Object.FindObjectOfType<SteamMainT>();
				}
				if (SteamMainBase<SteamMainT>.s_instance == null)
				{
					SteamMainBase<SteamMainT>.s_instance = new GameObject(typeof(SteamMainT).Name).AddComponent<SteamMainT>();
				}
				return SteamMainBase<SteamMainT>.s_instance;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000086BE File Offset: 0x000068BE
		public static bool IsInstanceSet
		{
			get
			{
				return SteamMainBase<SteamMainT>.s_instance != null;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600014D RID: 333 RVA: 0x000086D0 File Offset: 0x000068D0
		// (remove) Token: 0x0600014E RID: 334 RVA: 0x00008708 File Offset: 0x00006908
		public event Action<ErrorEventArgs> OnError;

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000873D File Offset: 0x0000693D
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00008745 File Offset: 0x00006945
		public bool IsDebugLogEnabled
		{
			get
			{
				return this.m_isDebugLogEnabled;
			}
			set
			{
				this.m_isDebugLogEnabled = value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008750 File Offset: 0x00006950
		public void Execute<T>(SteamAPICall_t p_steamCall, CallResult<T>.APIDispatchDelegate p_onCompleted)
		{
			CallResult<T> callResult = CallResult<T>.Create(p_onCompleted);
			callResult.Set(p_steamCall, null);
			this.m_pendingRequests.Add<T>(callResult);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008778 File Offset: 0x00006978
		protected virtual void OnDisable()
		{
			if (this.m_pendingRequests != null)
			{
				this.m_pendingRequests.Cancel();
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008790 File Offset: 0x00006990
		protected virtual void LateUpdate()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_pendingRequests.RemoveInactive();
				if (this.IsDebugLogEnabled && Time.frameCount % 300 == 0)
				{
					if (this.m_pendingRequests.Count() > 0)
					{
						Debug.Log(typeof(SteamMainT).Name + ": pending requests left: " + this.m_pendingRequests.Count());
					}
					foreach (KeyValuePair<string, List<object>> keyValuePair in this.m_singleShotEventHandlers)
					{
						if (keyValuePair.Value.Count > 0)
						{
							Debug.Log(string.Concat(new object[]
							{
								typeof(SteamMainT).Name,
								": pending signle shot event handlers for '",
								keyValuePair.Key,
								"' left: ",
								keyValuePair.Value.Count
							}));
						}
					}
				}
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000088C8 File Offset: 0x00006AC8
		protected virtual bool CheckAndLogResultNoEvent<Trequest>(string p_logText, EResult p_result, bool p_bIOFailure)
		{
			Action<object> action = null;
			return this.CheckAndLogResult<Trequest, object>(p_logText, p_result, p_bIOFailure, null, ref action);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000088E4 File Offset: 0x00006AE4
		protected virtual bool CheckAndLogResult<Trequest, Tevent>(string p_logText, EResult p_result, bool p_bIOFailure, string p_eventName, ref Action<Tevent> p_event)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_pendingRequests.RemoveInactive<Trequest>();
				if (this.IsDebugLogEnabled)
				{
					Debug.Log(string.Concat(new object[]
					{
						p_logText,
						": (fail:",
						p_bIOFailure.ToString(),
						") ",
						p_result,
						" requests left: ",
						this.m_pendingRequests.Count<Trequest>()
					}));
				}
			}
			if (p_result == EResult.k_EResultOK && !p_bIOFailure)
			{
				return true;
			}
			ErrorEventArgs errorEventArgs = ErrorEventArgs.Create(p_result);
			this.HandleError(p_logText + ": failed! ", errorEventArgs);
			if (p_eventName != null && p_event != null)
			{
				this.CallSingleShotEventHandlers<Tevent>(p_eventName, (Tevent)((object)Activator.CreateInstance(typeof(Tevent), new object[]
				{
					errorEventArgs
				})), ref p_event);
			}
			return false;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000089D8 File Offset: 0x00006BD8
		protected virtual void HandleError(string p_logPrefix, ErrorEventArgs p_error)
		{
			Debug.LogError(p_logPrefix + p_error.ErrorMessage);
			this.InvokeEventHandlerSafely<ErrorEventArgs>(this.OnError, p_error);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000089F8 File Offset: 0x00006BF8
		protected virtual void InvokeEventHandlerSafely<T>(Action<T> p_handler, T p_data)
		{
			try
			{
				if (p_handler != null)
				{
					p_handler(p_data);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new object[]
				{
					typeof(SteamMainT).Name,
					": your event handler ('",
					p_handler.Target,
					"' - System.Action<",
					typeof(T),
					">) has thrown an excepotion!\n",
					ex
				}));
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008A78 File Offset: 0x00006C78
		protected virtual void SetSingleShotEventHandler<T>(string p_eventName, ref Action<T> p_event, Action<T> p_handler)
		{
			if (p_handler != null)
			{
				if (!this.m_singleShotEventHandlers.ContainsKey(p_eventName))
				{
					this.m_singleShotEventHandlers.Add(p_eventName, new List<object>());
				}
				this.m_singleShotEventHandlers[p_eventName].Add(p_handler.Target);
				p_event = (Action<T>)Delegate.Combine(p_event, p_handler);
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008AD0 File Offset: 0x00006CD0
		protected virtual void CallSingleShotEventHandlers<T>(string p_eventName, T p_args, ref Action<T> p_event)
		{
			if (p_event != null && this.m_singleShotEventHandlers.ContainsKey(p_eventName))
			{
				int count = this.m_singleShotEventHandlers[p_eventName].Count;
				Delegate[] invocationList = p_event.GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					if (this.m_singleShotEventHandlers[p_eventName].Contains(@delegate.Target))
					{
						p_event = (Action<T>)Delegate.Remove(p_event, (Action<T>)@delegate);
						this.m_singleShotEventHandlers[p_eventName].Remove(@delegate.Target);
						try
						{
							@delegate.DynamicInvoke(new object[]
							{
								p_args
							});
						}
						catch (Exception ex)
						{
							Debug.LogError(string.Concat(new object[]
							{
								typeof(SteamMainT).Name,
								": your event handler ('",
								@delegate.Target,
								"' - System.Action<",
								typeof(T),
								">) has thrown an excepotion!\n",
								ex
							}));
						}
					}
				}
				if (this.IsDebugLogEnabled)
				{
					Debug.Log(string.Concat(new object[]
					{
						typeof(SteamMainT).Name,
						": CallSingleShotEventHandlers '",
						p_eventName,
						"' left handlers: ",
						(p_event != null) ? p_event.GetInvocationList().Length : 0,
						"/",
						invocationList.Length,
						" left single shots: ",
						this.m_singleShotEventHandlers[p_eventName].Count,
						"/",
						count
					}));
				}
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008C9C File Offset: 0x00006E9C
		protected virtual void ClearSingleShotEventHandlers<T>(string p_eventName, ref Action<T> p_event)
		{
			if (p_event != null && this.m_singleShotEventHandlers.ContainsKey(p_eventName))
			{
				int count = this.m_singleShotEventHandlers[p_eventName].Count;
				Delegate[] invocationList = p_event.GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					if (this.m_singleShotEventHandlers[p_eventName].Contains(@delegate.Target))
					{
						p_event = (Action<T>)Delegate.Remove(p_event, (Action<T>)@delegate);
						this.m_singleShotEventHandlers[p_eventName].Remove(@delegate.Target);
					}
				}
				if (this.IsDebugLogEnabled)
				{
					Debug.Log(string.Concat(new object[]
					{
						typeof(SteamMainT).Name,
						": ClearSingleShotEventHandler '",
						p_eventName,
						"' left handlers: ",
						(p_event != null) ? p_event.GetInvocationList().Length : 0,
						"/",
						invocationList.Length,
						" left single shots: ",
						this.m_singleShotEventHandlers[p_eventName].Count,
						"/",
						count
					}));
				}
			}
		}

		// Token: 0x040000D4 RID: 212
		protected static SteamMainT s_instance;

		// Token: 0x040000D5 RID: 213
		protected SteamRequestList m_pendingRequests = new SteamRequestList();

		// Token: 0x040000D6 RID: 214
		private Dictionary<string, List<object>> m_singleShotEventHandlers = new Dictionary<string, List<object>>();

		// Token: 0x040000D7 RID: 215
		protected object m_lock = new object();

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		[Tooltip("Set this property to true if you want to see a detailed log in the console. Disabled by default.")]
		protected bool m_isDebugLogEnabled;
	}
}
