using System;
using LapinerTools.uMyGUI;
using UnityEngine;

namespace LapinerTools.Steam.UI
{
	// Token: 0x02000025 RID: 37
	public class SteamLeaderboardsPopup : uMyGUI_Popup
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008E02 File Offset: 0x00007002
		public SteamLeaderboardsUI LeaderboardUI
		{
			get
			{
				return this.m_leaderboardUI;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008E0A File Offset: 0x0000700A
		public SteamLeaderboardsPopup()
		{
			this.DestroyOnHide = true;
		}

		// Token: 0x040000DA RID: 218
		[SerializeField]
		protected SteamLeaderboardsUI m_leaderboardUI;
	}
}
