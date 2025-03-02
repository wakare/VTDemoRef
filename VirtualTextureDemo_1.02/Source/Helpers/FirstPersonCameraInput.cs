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
	using SlimDX;

	using System.Drawing;
	using System.Windows.Forms;

	public class FirstPersonCameraInput
	{
		const float DefaultSpeed	= 0.125f;
		const float MinSpeed		= 0.05f;
		const float MaxSpeed		= 6.0f;

		const float Sensitivity		= 0.00004f;
		const float MinAngle		= -89.0f * (float)System.Math.PI / 180.0f;
		const float MaxAngle		=  89.0f * (float)System.Math.PI / 180.0f;
	
		const float ZoomFov			= 10.0f;
		const float DefaultFov		= 45.0f;
		const float ZoomRate		= 1.05f;

		readonly Camera			camera;
		readonly InputHelper	input;

		float		speed;
		Vector2		rotation;
		float		fov;

		public FirstPersonCameraInput( Camera camera, InputHelper input )
		{
			this.camera = camera;
			this.input  = input;

			// Update so we are pointing the right way
			rotation.X = -(float)System.Math.Atan2( camera.Direction.Z, camera.Direction.X ) + (float)System.Math.PI*0.5f;
			rotation.Y = (float)System.Math.Acos( camera.Direction.Y ) - (float)System.Math.PI*0.5f;

			speed = DefaultSpeed;
			fov = camera.Fov * 180.0f / (float)System.Math.PI;
		}

		public void Update( float dt )
		{
			if( input.CursorHidden )
			{
				// Update Fov
				if( input.IsMouseDown( MouseButtons.Left ) )
				{
					if( fov > ZoomFov )
						fov /= ZoomRate;
				}

				else if( fov < DefaultFov )
				{
					fov *= ZoomRate;
				}
			
				// Update Rotation
				Point pt = input.RelativeMousePosition;

				if( pt != Point.Empty )
				{
					Vector2 rotate = new Vector2( pt.X, pt.Y ) * Sensitivity * fov;
					rotation += rotate;
					rotation.Y = Clamp( rotation.Y, MinAngle, MaxAngle );
				}
			}

			// Update Speed
			//speed = Clamp( speed + input.GetMouseWheelDelta() * 0.005f, MinSpeed, MaxSpeed );
			speed = Clamp( speed + input.GetMouseWheelDelta() * speed * 0.003f, MinSpeed, MaxSpeed );	// This is works better for low speeds

			// Update Position
			Vector3 move = Vector3.Zero;

			if( input.IsKeyDown( Keys.Up )	     || input.IsKeyDown( Keys.W ) )	move.Z += 1.0f;
			if( input.IsKeyDown( Keys.Down )     || input.IsKeyDown( Keys.S ) )	move.Z -= 1.0f;
			if( input.IsKeyDown( Keys.Right )    || input.IsKeyDown( Keys.D ) )	move.X += 1.0f;
			if( input.IsKeyDown( Keys.Left )     || input.IsKeyDown( Keys.A ) )	move.X -= 1.0f;
			if( input.IsKeyDown( Keys.PageUp )   || input.IsKeyDown( Keys.E ) )	move.Y += 1.0f;
			if( input.IsKeyDown( Keys.PageDown ) || input.IsKeyDown( Keys.C ) )	move.Y -= 1.0f;

			move = Vector3.Normalize( move );

			float tempspeed = speed;
			if( input.IsKeyDown( Keys.ShiftKey ) )
				tempspeed *= 4.0f;

			move *= tempspeed * dt;

			// Update Rotation
			Matrix rotmat     = Matrix.RotationYawPitchRoll( rotation.X, rotation.Y, 0.0f );
			Vector3 targetdir = new Vector3( rotmat.M31, rotmat.M32, rotmat.M33 );

			// Update Position
			Matrix view = camera.View;
			Vector3 right = new Vector3( view.M11, view.M21, view.M31 );
			Vector3 pos   = camera.Position + right * move.X + Vector3.UnitY * move.Y + camera.Direction * move.Z;

			camera.Persepective( fov * (float)System.Math.PI / 180.0f, camera.Aspect, camera.Near, camera.Far );
			camera.LookAt( pos, pos + targetdir );

			/*
			Vector3 v = pos + targetdir * 10.0f;
			System.Console.WriteLine("{0}f, {1}f, {2}f, {3}f, {4}f, {5}f", pos.X, pos.Y, pos.Z, v.X, v.Y, v.Z);
			//*/

			if( input.CursorHidden )
				input.ForcePosition();
		}

		static float Clamp( float value, float min, float max )
		{
			if( value < min ) value = min;
			else if( value > max ) value = max;
			return value;
		}
	}
}