using System;
using System.Collections;
using Colorful;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000004 RID: 4
public class Intro : MonoBehaviour
{
	// Token: 0x0600000A RID: 10 RVA: 0x000021D3 File Offset: 0x000003D3
	private void Awake()
	{
		this.pixels = GameObject.Find("camera_ui").GetComponent<Pixelate>();
		this.pixels.Scale = 0f;
		base.StartCoroutine("Delay");
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002208 File Offset: 0x00000408
	private void Update()
	{
		if (this.pixels.Scale < 250f)
		{
			this.pixels.Scale += 600f * Time.deltaTime;
		}
		else
		{
			this.pixels.Scale = 250f;
		}
		if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.JoystickButton6) && this.load)
		{
			this.moveCamera = true;
		}
		if (this.moveCamera)
		{
			this.cmtp = Camera.main.transform.position;
			this.cmtp.y = this.cmtp.y - 6f * Time.deltaTime;
			Camera.main.transform.position = this.cmtp;
			if (this.cmtp.y < -2f)
			{
				SceneManager.LoadScene("loading");
			}
		}
		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton6))
		{
			Application.Quit();
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002303 File Offset: 0x00000503
	private IEnumerator Delay()
	{
		yield return new WaitForSeconds(0.3f);
		this.load = true;
		yield break;
	}

	// Token: 0x04000003 RID: 3
	private Pixelate pixels;

	// Token: 0x04000004 RID: 4
	private bool moveCamera;

	// Token: 0x04000005 RID: 5
	private Vector3 cmtp;

	// Token: 0x04000006 RID: 6
	private bool load;
}
