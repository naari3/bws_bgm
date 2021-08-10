using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Steamworks;

namespace LapinerTools.Steam.Data.Internal
{
	// Token: 0x0200002D RID: 45
	public class SteamRequestList
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00009B3C File Offset: 0x00007D3C
		public void Add<T>(CallResult<T> p_request)
		{
			Type typeFromHandle = typeof(T);
			List<object> list;
			if (!this.m_requests.TryGetValue(typeFromHandle, out list))
			{
				list = new List<object>();
				this.m_requests.Add(typeFromHandle, list);
			}
			list.Add(p_request);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009B7E File Offset: 0x00007D7E
		public int Count()
		{
			return this.m_requests.Values.Sum((List<object> requestList) => requestList.Count);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009BB0 File Offset: 0x00007DB0
		public int Count<T>()
		{
			Type typeFromHandle = typeof(T);
			List<object> list;
			if (this.m_requests.TryGetValue(typeFromHandle, out list))
			{
				return list.Count;
			}
			return 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public void Clear<T>()
		{
			Type typeFromHandle = typeof(T);
			List<object> list;
			if (this.m_requests.TryGetValue(typeFromHandle, out list))
			{
				list.Clear();
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00009C10 File Offset: 0x00007E10
		public void RemoveInactive()
		{
			foreach (KeyValuePair<Type, List<object>> keyValuePair in this.m_requests)
			{
				base.GetType().GetMethod("RemoveInactiveInternal", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					keyValuePair.Key
				}).Invoke(this, new object[]
				{
					keyValuePair.Value
				});
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009C9C File Offset: 0x00007E9C
		public void RemoveInactive<T>()
		{
			Type typeFromHandle = typeof(T);
			List<object> list;
			if (this.m_requests.TryGetValue(typeFromHandle, out list))
			{
				base.GetType().GetMethod("RemoveInactiveInternal", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					typeFromHandle
				}).Invoke(this, new object[]
				{
					list
				});
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009CF8 File Offset: 0x00007EF8
		public void Cancel()
		{
			foreach (KeyValuePair<Type, List<object>> keyValuePair in this.m_requests)
			{
				base.GetType().GetMethod("CancelInternal", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					keyValuePair.Key
				}).Invoke(this, new object[]
				{
					keyValuePair.Value
				});
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009D84 File Offset: 0x00007F84
		public void Cancel<T>()
		{
			Type typeFromHandle = typeof(T);
			List<object> list;
			if (this.m_requests.TryGetValue(typeFromHandle, out list))
			{
				base.GetType().GetMethod("CancelInternal", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[]
				{
					typeFromHandle
				}).Invoke(this, new object[]
				{
					list
				});
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009DE0 File Offset: 0x00007FE0
		private static void CancelInternal<T>(List<object> p_requests)
		{
			for (int i = p_requests.Count - 1; i >= 0; i--)
			{
				(p_requests[i] as CallResult<T>).Cancel();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009E14 File Offset: 0x00008014
		private static void RemoveInactiveInternal<T>(List<object> p_requests)
		{
			for (int i = p_requests.Count - 1; i >= 0; i--)
			{
				if (!(p_requests[i] as CallResult<T>).IsActive())
				{
					p_requests.RemoveAt(i);
				}
			}
		}

		// Token: 0x040000FF RID: 255
		private Dictionary<Type, List<object>> m_requests = new Dictionary<Type, List<object>>();
	}
}
