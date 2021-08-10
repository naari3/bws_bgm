using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000030 RID: 48
	public static class CLib
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00009F71 File Offset: 0x00008171
		public static float Frac(float f)
		{
			return f - Mathf.Floor(f);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009F7B File Offset: 0x0000817B
		public static bool IsLinearColorSpace()
		{
			return QualitySettings.activeColorSpace == ColorSpace.Linear;
		}

		// Token: 0x04000103 RID: 259
		public const float PI_2 = 1.5707964f;

		// Token: 0x04000104 RID: 260
		public const float PI2 = 6.2831855f;
	}
}
