/* 
MIT License

Copyright (c) 2009 Brad Blanchard (www.linedef.com)

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/

namespace VirtualTextureDemo
{
	using System;
	using System.Drawing;
	using System.Diagnostics;
	using System.Windows.Forms;

	using SlimDX;
	using D3D10 = SlimDX.Direct3D10;
	using DXGI = SlimDX.DXGI;

	public abstract class DemoForm: Form, IDisposable
	{
		// Direct3D
		protected D3D10.Device		device;
		protected D3D10.Viewport	viewport;
        protected DXGI.SwapChain	swapchain;

		// Buffers
		protected D3D10.RenderTargetView	rendertarget;
		protected D3D10.Texture2D			depthbuffer;
		protected D3D10.DepthStencilView	depthbufferview;

		// Input / Control
		protected InputHelper				input;
		protected Camera					camera;
		protected FirstPersonCameraInput	camerainput;

		// Timing
		protected Stopwatch	stopwatch;
		long frametime;
		long lasttime;

		public DemoForm( DemoInfo info )
		{
			Text = "Virtual Texture Demo";
			ClientSize = info.WindowSize;
			StartPosition = FormStartPosition.CenterScreen;
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;

			Rectangle screen = Screen.PrimaryScreen.Bounds;
			if( info.WindowSize.Width >= screen.Width || info.WindowSize.Height >= screen.Height )
				WindowState = FormWindowState.Maximized;
#if DEBUG
			SlimDX.Configuration.EnableObjectTracking = true;
			SlimDX.Configuration.DetectDoubleDispose = true;
#endif // DEBUG

			input = new InputHelper( this );
			input.CursorHidden = true;
			input.ForcePosition();

			camera = new Camera();
			camera.LookAt( new Vector3( -68.41964f, 1.566079f, 95.00811f ), new Vector3( -58.47616f, 1.828684f, 93.97941f ) );
			camera.Persepective( 45.0f * (float)Math.PI / 180.0f, ClientSize.Width/(float)ClientSize.Height, 0.025f, 1200.0f );

			camerainput = new FirstPersonCameraInput( camera, input );

			SetupDirect3D( info );

			stopwatch = Stopwatch.StartNew();

			Application.Idle += Application_Idle;
		}

		void SetupDirect3D( DemoInfo info )
		{
			DXGI.SampleDescription sampledesc = new SlimDX.DXGI.SampleDescription( info.Multisamples, 0 );

			DXGI.ModeDescription modedesc = new SlimDX.DXGI.ModeDescription()
			{
				
				Format = DXGI.Format.R8G8B8A8_UNorm,
				RefreshRate = new Rational( 60, 1 ),
				Scaling = DXGI.DisplayModeScaling.Unspecified,
				ScanlineOrdering = DXGI.DisplayModeScanlineOrdering.Unspecified,
				Width = ClientSize.Width,
				Height = ClientSize.Height
			};

			DXGI.SwapChainDescription swapchaindesc = new SlimDX.DXGI.SwapChainDescription()
			{
				ModeDescription = modedesc,
				SampleDescription = sampledesc,
				BufferCount = 1,
				Flags = DXGI.SwapChainFlags.None,
				IsWindowed = true,
				OutputHandle = this.Handle,
				SwapEffect = DXGI.SwapEffect.Discard,
				Usage = DXGI.Usage.RenderTargetOutput
			};

			D3D10.DeviceCreationFlags deviceflags = D3D10.DeviceCreationFlags.None;
#if DEBUG
			deviceflags |= D3D10.DeviceCreationFlags.Debug;
#endif // DEBUG

			D3D10.Device.CreateWithSwapChain( null, D3D10.DriverType.Hardware, deviceflags, swapchaindesc, out device, out swapchain );

			DXGI.Factory factory = swapchain.GetParent<DXGI.Factory>();
			factory.SetWindowAssociation( this.Handle, DXGI.WindowAssociationFlags.IgnoreAll );

			int factorycount = factory.GetAdapterCount();
			for( int i = 0; i < factorycount; ++i )
			{
				DXGI.Adapter adapter = factory.GetAdapter( i );
				if( adapter != null )
				{
					DXGI.AdapterDescription desc = adapter.Description;
					if( desc != null )
					Console.WriteLine(adapter.Description.Description);
				}
			}		
			
			using( D3D10.Texture2D backbuffer = D3D10.Texture2D.FromSwapChain<D3D10.Texture2D>( swapchain, 0 ) )
			{
				rendertarget = new D3D10.RenderTargetView( device, backbuffer );
			}

			D3D10.Texture2DDescription depthbufferdesc = new D3D10.Texture2DDescription()
			{
				Width = ClientSize.Width,
				Height = ClientSize.Height,
				MipLevels = 1,
				ArraySize = 1,
				Format = SlimDX.DXGI.Format.D24_UNorm_S8_UInt,
				SampleDescription = sampledesc,
				Usage = D3D10.ResourceUsage.Default,
				BindFlags = D3D10.BindFlags.DepthStencil,
				CpuAccessFlags = D3D10.CpuAccessFlags.None,
				OptionFlags = D3D10.ResourceOptionFlags.None
			};

			depthbuffer = new D3D10.Texture2D( device, depthbufferdesc );
			depthbufferview = new D3D10.DepthStencilView( device, depthbuffer );

			viewport = new D3D10.Viewport()
			{
				X = 0,
				Y = 0,
				Width = ClientSize.Width,
				Height = ClientSize.Height,
				MinZ = 0.0f,
				MaxZ = 1.0f
			};

			SetDefaultRenderTarget();
		}

		public virtual new void Dispose()
		{
			Application.Idle -= Application_Idle;

			device.InputAssembler.SetInputLayout( null );

            device.ClearState();

            rendertarget.Dispose();
			depthbuffer.Dispose();
			depthbufferview.Dispose();

            device.Dispose();
            swapchain.Dispose();

			base.Dispose();
		}

		void Application_Idle( object sender, System.EventArgs e )
		{		
			while( SlimDX.Windows.MessagePump.IsApplicationIdle )
			{
				long time = stopwatch.ElapsedMilliseconds;
				frametime = time - lasttime;
				lasttime = time;

				input.Update();
				UpdateInput();
				camerainput.Update( frametime * 0.01f );

                device.ClearRenderTargetView( rendertarget, Color.LightGray );
				device.ClearDepthStencilView( depthbufferview, D3D10.DepthStencilClearFlags.Depth, 1.0f, 0 );

				Draw();

                swapchain.Present(0, DXGI.PresentFlags.None);
			}
		}

		public void SetDefaultRenderTarget()
		{
			device.Rasterizer.SetViewports( viewport );
			device.OutputMerger.SetTargets( depthbufferview, rendertarget );
		}

		#region Abstract Functions
		protected abstract void UpdateInput();
		protected abstract void Draw();
		#endregion Abstract Functions

		#region Overrides
		protected override bool ProcessDialogKey( Keys keyData )
		{
			input.SetKeyPressed( keyData );
			return base.ProcessDialogKey( keyData );
		}
		#endregion Overrides
	}
}