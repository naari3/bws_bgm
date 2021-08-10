using System;
using System.Collections.Generic;

namespace LapinerTools.Steam.Data
{
	// Token: 0x02000028 RID: 40
	public class LeaderboardsDownloadedScoresEventArgs : EventArgsBase
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000094BC File Offset: 0x000076BC
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000094C4 File Offset: 0x000076C4
		public string LeaderboardName { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000094CD File Offset: 0x000076CD
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000094D5 File Offset: 0x000076D5
		public List<LeaderboardsScoreEntry> Scores { get; set; }

		// Token: 0x0600017B RID: 379 RVA: 0x000094DE File Offset: 0x000076DE
		public LeaderboardsDownloadedScoresEventArgs()
		{
			this.LeaderboardName = "";
			this.Scores = new List<LeaderboardsScoreEntry>();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000094FC File Offset: 0x000076FC
		public LeaderboardsDownloadedScoresEventArgs(string p_leaderboardName, List<LeaderboardsScoreEntry> p_scores)
		{
			this.LeaderboardName = p_leaderboardName;
			this.Scores = p_scores;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009512 File Offset: 0x00007712
		public LeaderboardsDownloadedScoresEventArgs(EventArgsBase p_errorEventArgs) : base(p_errorEventArgs)
		{
			this.LeaderboardName = "";
			this.Scores = new List<LeaderboardsScoreEntry>();
		}
	}
}
