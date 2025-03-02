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
    using System.Windows.Forms;
    using SlimDX;
	using D3D10 = SlimDX.Direct3D10;

	public class VirtualTextureDemo: DemoForm
    {
		// Virtual Texturing
		VirtualTexture		virtualtexture;
		FeedbackBuffer		feedback;

		// Variable tweaking
		bool update = true;
		bool showprepass = false;

		// Effects
		D3D10.Effect effect;
        D3D10.EffectTechnique technique;
        D3D10.EffectPass pass1;
		D3D10.EffectPass pass2;
		D3D10.EffectPass pass3;

		// Effect variables
		D3D10.EffectMatrixVariable fxworldviewproj;

		// Geometry
		IGame.Scene	geometry;

		public VirtualTextureDemo( DemoInfo demoinfo ): base( demoinfo )
		{
			LoadEffect();
			LoadVirtualTexture( demoinfo );
			LoadGeometry( demoinfo );
		}

        void LoadEffect()
		{
			SlimDX.D3DCompiler.ShaderFlags shaderflags = SlimDX.D3DCompiler.ShaderFlags.EnableStrictness;
#if DEBUG
			shaderflags |= SlimDX.D3DCompiler.ShaderFlags.Debug | SlimDX.D3DCompiler.ShaderFlags.SkipOptimization;
#endif // DEBUG

			string errors = string.Empty;
			try
			{
                effect = D3D10.Effect.FromFile( device, "D:\\Work\\DevSpace\\VirtualTextureDemo_1.02\\Effects\\Default.fx", "fx_4_0", shaderflags, SlimDX.D3DCompiler.EffectFlags.None, null, null, null, out errors );
			}

			catch
			{
				if( !string.IsNullOrEmpty( errors ) )
				{
					MessageBox.Show( errors );
					return;
				}
			}

            technique = effect.GetTechniqueByIndex(0);
            pass1 = technique.GetPassByIndex(0);
			pass2 = technique.GetPassByIndex(1);
			pass3 = technique.GetPassByIndex(2);

			// Setup shader variables
			fxworldviewproj = effect.GetVariableByName( "WorldViewProj" ).AsMatrix();
		}

		void LoadVirtualTexture( DemoInfo demoinfo )
		{
			demoinfo.VirtualTextureInfo.PrintInfo();
			Console.WriteLine();

			virtualtexture = new VirtualTexture( device, demoinfo.VirtualTextureInfo, demoinfo.AtlasSize, demoinfo.UploadsPerFrame, demoinfo.TextureFile );
			virtualtexture.BindToEffect( effect );

			Console.Write( "Creating FeedbackBuffer..." );
			feedback = new FeedbackBuffer( device, demoinfo.VirtualTextureInfo, demoinfo.FeedbackBufferSize );
			Console.WriteLine( "done." );
		}

		void LoadGeometry( DemoInfo demoinfo )
		{
			Console.Write( "Loading Geometry..." );
			geometry = new IGame.Scene( device, demoinfo.GeometryFile );
			geometry.BindToPass( pass1 );
			Console.WriteLine( "done." );
		}

		public override void Dispose()
		{
			virtualtexture.Dispose();
			feedback.Dispose();

			geometry.Dispose();
			effect.Dispose();

			base.Dispose();
        }

		protected override void UpdateInput()
		{
			if( input.IsKeyPressed( Keys.Escape ) )
				Application.Exit();

			if( input.IsKeyPressed( Keys.Space ) )
			{
				update = !update;
				Console.WriteLine("Update: {0}", update );
			}

			if( input.IsKeyPressed( Keys.F1 ) )
			{
				virtualtexture.Loader.ColorMipLevels = !virtualtexture.Loader.ColorMipLevels;
				virtualtexture.Clear();
			}

			if( input.IsKeyPressed( Keys.F2 ) )
			{
				virtualtexture.Loader.ShowBorders = !virtualtexture.Loader.ShowBorders;
				virtualtexture.Clear();
			}

			if( input.IsKeyPressed( Keys.D1 ) )
			{
				--virtualtexture.MipBias;
			}

			if( input.IsKeyPressed( Keys.D2 ) )
			{
				++virtualtexture.MipBias;
			}

			if( input.IsKeyPressed( Keys.P ) )
			{
				showprepass = !showprepass;
			}

			if( input.IsKeyPressed( Keys.Tab ) )
			{
				input.CursorHidden = !input.CursorHidden;
			}
		}

		protected override void Draw()
		{
			fxworldviewproj.SetMatrix( camera.View * camera.Projection );

			if( update )
			{
				// Process the last frame's data
				feedback.Download();
				virtualtexture.Update( feedback.Requests );
				
				// First Pass
				feedback.SetAsRenderTarget();
				feedback.Clear();
				{
					pass1.Apply();
					geometry.Draw();
				}

				// Start copying the frame results to the cpu
				feedback.Copy();
			}

			// Second Pass
			device.Rasterizer.SetViewports( viewport );
			device.OutputMerger.SetTargets( depthbufferview, rendertarget );
			{
				if( showprepass )
					pass1.Apply();
				else
					pass2.Apply();
				geometry.Draw();
			}
		}
    }
}