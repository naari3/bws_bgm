using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class RotateMoveCamera : MonoBehaviour
{
	// Token: 0x06000041 RID: 65 RVA: 0x00003CAC File Offset: 0x00001EAC
	private void Update()
	{
		float axis = Input.GetAxis("Mouse X");
		float axis2 = Input.GetAxis("Mouse Y");
		if (axis != this.MouseX || axis2 != this.MouseY)
		{
			this.rotationX += axis * this.sensX * Time.deltaTime;
			this.rotationY += axis2 * this.sensY * Time.deltaTime;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minY, this.maxY);
			this.MouseX = axis;
			this.MouseY = axis2;
			this.Camera.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0f);
		}
		if (Input.GetKey(KeyCode.W))
		{
			base.transform.Translate(new Vector3(0f, 0f, 0.1f));
		}
		else if (Input.GetKey(KeyCode.S))
		{
			base.transform.Translate(new Vector3(0f, 0f, -0.1f));
		}
		if (Input.GetKey(KeyCode.D))
		{
			base.transform.Translate(new Vector3(0.1f, 0f, 0f));
			return;
		}
		if (Input.GetKey(KeyCode.A))
		{
			base.transform.Translate(new Vector3(-0.1f, 0f, 0f));
		}
	}

	// Token: 0x0400002E RID: 46
	public GameObject Camera;

	// Token: 0x0400002F RID: 47
	public float minX = -360f;

	// Token: 0x04000030 RID: 48
	public float maxX = 360f;

	// Token: 0x04000031 RID: 49
	public float minY = -45f;

	// Token: 0x04000032 RID: 50
	public float maxY = 45f;

	// Token: 0x04000033 RID: 51
	public float sensX = 100f;

	// Token: 0x04000034 RID: 52
	public float sensY = 100f;

	// Token: 0x04000035 RID: 53
	private float rotationY;

	// Token: 0x04000036 RID: 54
	private float rotationX;

	// Token: 0x04000037 RID: 55
	private float MouseX;

	// Token: 0x04000038 RID: 56
	private float MouseY;
}
