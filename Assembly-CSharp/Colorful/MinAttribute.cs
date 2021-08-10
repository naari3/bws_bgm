using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x0200002E RID: 46
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00009E61 File Offset: 0x00008061
		public MinAttribute(float min)
		{
			this.Min = min;
		}

		// Token: 0x04000100 RID: 256
		public readonly float Min;
	}
}
