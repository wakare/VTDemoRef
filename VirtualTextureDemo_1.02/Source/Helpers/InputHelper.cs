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
	using System.Windows.Forms;

	public class InputHelper
	{	
		Control			control;

		bool[]			keydown;
		bool[]			pressed;

		MouseButtons	mousedown;
		MouseButtons	mousepressed;
		int				mousedelta;

		Point			lastpos;

		bool			cursorhidden;
		bool			hidecursoronfocus;
		
		// Properties
		public Point MousePosition	{ get; private set; }

		public Point RelativeMousePosition
		{
			get 
			{
				int x = MousePosition.X - lastpos.X;
				int y = MousePosition.Y - lastpos.Y;
				return new Point( x, y );
			}
		}

		public bool CursorHidden
		{
			get { return cursorhidden; }
			set 
			{
				if( value )
					Cursor.Hide();
				else
					Cursor.Show();

				cursorhidden = value;
			}
		}

		public Point ControlCenter
		{
			get
			{
				int x = control.ClientRectangle.Width/2  + control.ClientRectangle.Left;
				int y = control.ClientRectangle.Height/2 + control.ClientRectangle.Top;
				return new Point( x, y );
			}
		}

		// Constructor
		public InputHelper( Control control )
		{
			this.control = control;

			keydown = new bool[256];
			pressed = new bool[256];

			control.GotFocus	+= GotFocus;
			control.LostFocus	+= LostFocus;

			control.KeyDown		+= KeyDown;
			control.KeyUp		+= KeyUp;

			control.MouseDown	+= MouseDown;
			control.MouseUp		+= MouseUp;
			control.MouseEnter	+= MouseEnter;
			control.MouseLeave	+= MouseLeave;
			control.MouseWheel  += MouseWheel;

			Cursor.Position = control.PointToScreen( ControlCenter );
			Cursor.Show();
		}

		// Window Events
		void GotFocus( object sender, EventArgs e )
		{
			if( hidecursoronfocus )
			{
				CursorHidden = true;
				hidecursoronfocus = false;
			}
		}

		void LostFocus( object sender, EventArgs e )
		{
			if( CursorHidden )
			{
				CursorHidden = false;
				hidecursoronfocus = true;
			}
		}

		// Keyboard Events
		void KeyDown( object sender, KeyEventArgs e )
		{
			int keycode = (int)( e.KeyCode & Keys.KeyCode );
			keydown[keycode] = true;
		}

		void KeyUp( object sender, KeyEventArgs e )
		{
			int keycode = (int)( e.KeyCode & Keys.KeyCode );
			keydown[keycode] = false;
		}

		// Mouse Events
		void MouseDown( object sender, MouseEventArgs e )
		{
			mousedown |= e.Button;
			mousepressed |= e.Button;
		}

		void MouseUp( object sender, MouseEventArgs e )
		{
			mousedown &= ~e.Button;
		}

		void MouseEnter( object sender, System.EventArgs e )
		{
			if( cursorhidden )
				Cursor.Hide();
		}

		void MouseLeave( object sender, System.EventArgs e )
		{
			if( cursorhidden )
				Cursor.Show();
		}

		void MouseWheel( object sender, MouseEventArgs e )
		{
			mousedelta = e.Delta;
		}

		// Public Functions
		public bool IsKeyDown( Keys key )
		{
			int keycode = (int)( key & Keys.KeyCode );
			return keydown[keycode];
		}

		public bool IsKeyPressed( Keys key )
		{
			int keycode = (int)( key & Keys.KeyCode );
			bool p = pressed[keycode];
			pressed[keycode] = false;
			return p;
		}

		public void SetKeyPressed( Keys key )
		{
			int keycode = (int)( key & Keys.KeyCode );
			pressed[keycode] = true;
			//System.Console.WriteLine("Key Press: {0} = {1} = {2}", key, (byte)key, keycode);
		}

		public bool IsMouseDown( MouseButtons button )
		{
			return ( mousedown & button ) == button;
		}

		public bool IsMousePressed( MouseButtons button )
		{
			bool p = ( mousepressed & button ) == button;
			mousepressed &= ~button;
			return p;
		}

		public int GetMouseWheelDelta()
		{
			int value = mousedelta;
			mousedelta = 0;
			return value;
		}

		// Cursor Functions - Used for controlling cursor when updating camera
		public void ForcePosition()
		{
			Point point = control.PointToScreen( ControlCenter );
			MousePosition = control.PointToClient( point );
			lastpos = MousePosition;
			Cursor.Position = point;
		}

		public void Update()
		{
			lastpos = MousePosition;
			MousePosition = control.PointToClient( Cursor.Position );
		}
	}
}