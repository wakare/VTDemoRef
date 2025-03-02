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

namespace VirtualTextureDemo.IGame
{
	using System;
	using System.Xml;
	using System.Collections.Generic;

	using D3D10 = SlimDX.Direct3D10;
	using DXGI  = SlimDX.DXGI;
	
	public class Scene: IDisposable
	{
		public List<Mesh>	Meshes;

		// Constructor
		public Scene( SlimDX.Direct3D10.Device device, string filename )
		{
			Meshes = new List<Mesh>();

			XmlReaderSettings settings = new XmlReaderSettings();
			settings.ConformanceLevel = ConformanceLevel.Fragment;
			settings.IgnoreComments = true;
			settings.IgnoreWhitespace = true;
			
			XmlReader reader = XmlReader.Create( filename, settings );
			reader.MoveToContent();

			while( reader.ReadToFollowing( "Node" ) )
			{
				string nodetype = reader.GetAttribute( "NodeType" );

				switch( nodetype )
				{
					case "Mesh":
					{
						reader.ReadToFollowing( "Mesh" );
						Mesh mesh  = new Mesh( device, reader );
						Meshes.Add( mesh );
					} break;

					default:
						Console.WriteLine("Unsupported node type: {0}", nodetype );
						break;
				}
			}
		}

		public void BindToPass( D3D10.EffectPass pass )
		{
			foreach( Mesh mesh in Meshes )
				mesh.BindToPass( pass );
		}

		public void Dispose()
		{
			foreach( Mesh mesh in Meshes )
				mesh.Dispose();
			Meshes.Clear();
		}

		// Public Functions
		public void Draw()
		{
			foreach( Mesh mesh in Meshes )
				mesh.Draw();
		}
	}
}