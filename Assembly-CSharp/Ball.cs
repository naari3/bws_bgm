﻿using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class Ball : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void OnBecameInvisible()
	{
		Object.Destroy(base.gameObject);
	}
}
