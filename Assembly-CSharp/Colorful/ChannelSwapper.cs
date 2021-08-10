using System;
using UnityEngine;

namespace Colorful
{
	// Token: 0x02000038 RID: 56
	[HelpURL("http://www.thomashourdel.com/colorful/doc/color-correction/channel-swapper.html")]
	[ExecuteInEditMode]
	[AddComponentMenu("Colorful FX/Color Correction/Channel Swapper")]
	public class ChannelSwapper : BaseEffect
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000A808 File Offset: 0x00008A08
		protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.Material.SetVector("_Red", ChannelSwapper.m_Channels[(int)this.RedSource]);
			base.Material.SetVector("_Green", ChannelSwapper.m_Channels[(int)this.GreenSource]);
			base.Material.SetVector("_Blue", ChannelSwapper.m_Channels[(int)this.BlueSource]);
			Graphics.Blit(source, destination, base.Material);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A882 File Offset: 0x00008A82
		protected override string GetShaderName()
		{
			return "Hidden/Colorful/Channel Swapper";
		}

		// Token: 0x04000122 RID: 290
		[Tooltip("Source channel to use for the output red channel.")]
		public ChannelSwapper.Channel RedSource;

		// Token: 0x04000123 RID: 291
		[Tooltip("Source channel to use for the output green channel.")]
		public ChannelSwapper.Channel GreenSource = ChannelSwapper.Channel.Green;

		// Token: 0x04000124 RID: 292
		[Tooltip("Source channel to use for the output blue channel.")]
		public ChannelSwapper.Channel BlueSource = ChannelSwapper.Channel.Blue;

		// Token: 0x04000125 RID: 293
		private static Vector4[] m_Channels = new Vector4[]
		{
			new Vector4(1f, 0f, 0f, 0f),
			new Vector4(0f, 1f, 0f, 0f),
			new Vector4(0f, 0f, 1f, 0f)
		};

		// Token: 0x02000091 RID: 145
		public enum Channel
		{
			// Token: 0x04000290 RID: 656
			Red,
			// Token: 0x04000291 RID: 657
			Green,
			// Token: 0x04000292 RID: 658
			Blue
		}
	}
}
