using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class Balls : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	private void Start()
	{
		base.InvokeRepeating("CreateBallA", 0f, 12f);
		base.InvokeRepeating("CreateBallB", 0f, 9f);
		base.InvokeRepeating("CreateBallC", 0f, 6f);
		base.InvokeRepeating("CreateBallD", 0f, 3f);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020C9 File Offset: 0x000002C9
	private void Update()
	{
		this.tp = base.transform.position;
		this.tp.x = Random.Range(-5f, 5f);
		base.transform.position = this.tp;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002107 File Offset: 0x00000307
	private void CreateBallA()
	{
		if (Level.score >= 1f && Level.score < 5f)
		{
			Object.Instantiate<Transform>(this.ball, base.transform.position, Random.rotation);
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000213D File Offset: 0x0000033D
	private void CreateBallB()
	{
		if (Level.score >= 5f && Level.score < 10f)
		{
			Object.Instantiate<Transform>(this.ball, base.transform.position, Random.rotation);
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002173 File Offset: 0x00000373
	private void CreateBallC()
	{
		if (Level.score >= 10f && Level.score < 15f)
		{
			Object.Instantiate<Transform>(this.ball, base.transform.position, Random.rotation);
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000021A9 File Offset: 0x000003A9
	private void CreateBallD()
	{
		if (Level.score >= 15f)
		{
			Object.Instantiate<Transform>(this.ball, base.transform.position, Random.rotation);
		}
	}

	// Token: 0x04000001 RID: 1
	public Transform ball;

	// Token: 0x04000002 RID: 2
	private Vector3 tp;
}
