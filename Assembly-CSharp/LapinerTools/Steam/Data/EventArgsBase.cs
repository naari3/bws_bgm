using System;

namespace LapinerTools.Steam.Data
{
	// Token: 0x0200002C RID: 44
	public class EventArgsBase : EventArgs
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00009AED File Offset: 0x00007CED
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009AF5 File Offset: 0x00007CF5
		public bool IsError { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00009AFE File Offset: 0x00007CFE
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00009B06 File Offset: 0x00007D06
		public string ErrorMessage { get; set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x00009B0F File Offset: 0x00007D0F
		public EventArgsBase()
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00009B17 File Offset: 0x00007D17
		public EventArgsBase(EventArgsBase p_copyFromArgs)
		{
			if (p_copyFromArgs != null)
			{
				this.IsError = p_copyFromArgs.IsError;
				this.ErrorMessage = p_copyFromArgs.ErrorMessage;
			}
		}
	}
}
