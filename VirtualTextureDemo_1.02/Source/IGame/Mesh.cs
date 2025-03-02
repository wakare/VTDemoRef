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
	using System.Runtime.InteropServices;
	using System.Collections.Generic;
	
	using SlimDX;
	using D3D10 = SlimDX.Direct3D10;
	using DXGI  = SlimDX.DXGI;

	public struct IGameVertex
	{
		public Vector3 Position;
		//public Vector3 Normal;
		public Vector2 TexCoord;

		public static D3D10.InputLayout GetInputLayout( D3D10.Device device, D3D10.EffectPass pass )
		{
			D3D10.InputElement[] inputElements = 
			{
				new D3D10.InputElement( "POSITION", 0, DXGI.Format.R32G32B32_Float,  0, 0 ),
				//new D3D10.InputElement( "NORMAL",   0, DXGI.Format.R32G32B32_Float, 12, 0 ),
				new D3D10.InputElement( "TEXCOORD", 0, DXGI.Format.R32G32_Float,    12, 0 )
			};

			return new D3D10.InputLayout( device, inputElements, pass.Description.Signature );
		}

		public static int Size
		{
			get { return Marshal.SizeOf(typeof(IGameVertex)); } 
		}
	}

	public class Mesh: IDisposable
	{
		readonly D3D10.Device	Device;

		D3D10.Buffer	vertexbuffer;
		//D3D10.Buffer	indexbuffer;

		D3D10.InputLayout layout;

		public Vector3[]	Vertices;

		public int		FaceCount;

		public uint[]	FaceVertices;
		public uint[]	FaceNormals;
		public uint[]	MaterialIDs;
		public uint[]	SmoothingGroups;
		public uint[]	EdgeVisibility;

		public int		MapChannelCount;

		public MapChannel[]	MapChannels;

		// Constructor
		public Mesh( D3D10.Device device, XmlReader reader )
		{
			this.Device = device;
			
			reader.ReadToFollowing( "Vertices" );
			Vertices = ParseVec3( reader );

			reader.ReadToFollowing( "Faces" );
			//Normals = ParseVec3( reader );

			FaceCount = int.Parse( reader.GetAttribute( "Count" ) );

			reader.ReadToFollowing( "FaceVertices" );
			FaceVertices = ParseArray<uint>( reader );
			FaceNormals = ParseArray<uint>( reader );
			MaterialIDs = ParseArray<uint>( reader );
			SmoothingGroups = ParseArray<uint>( reader );
			EdgeVisibility = ParseArray<uint>( reader );

			if( reader.ReadToFollowing( "MapChannels" ) )
			{
				MapChannelCount = int.Parse( reader.GetAttribute( "Count" ) );

				MapChannels = new MapChannel[MapChannelCount];
				for( int i = 0; i < MapChannelCount; ++i )
				{
					MapChannels[i] = new MapChannel();

					reader.ReadToFollowing( "MapVertices" );
					MapChannels[i].MapVertices = ParseVec3( reader );
					MapChannels[i].MapFaces = ParseArray<uint>( reader );
				}
			}

			UploadVertices();
		}

		public void Dispose()
		{
			vertexbuffer.Dispose();
			//indexbuffer.Dispose();
			layout.Dispose();
		}

		Vector3[] ParseVec3( XmlReader reader )
		{
			int count = int.Parse( reader.GetAttribute( "Count" ) );
			string vstr = reader.ReadElementContentAsString();

			vstr = vstr.Trim();
			string[] tokens = vstr.Split( new char[] { '\r', '\t', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries );

			Vector3[] data = new Vector3[count];
			for( int i = 0; i < count; ++i )
			{
				data[i].X = float.Parse( tokens[3*i+0] );
				data[i].Y = float.Parse( tokens[3*i+1] );
				data[i].Z = float.Parse( tokens[3*i+2] );
			}

			return data;
		}

		T[] ParseArray<T>( XmlReader reader )
		{
			string vstr = reader.ReadElementContentAsString();

			vstr.Trim();
			string[] tokens = vstr.Split( new char[] { '\r', '\t', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries );
			
			return Array.ConvertAll( tokens, new Converter<string,T>( Convert<T> ) );
		}

		static Type Convert<Type>( string value )
		{
			System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(Type));
            Type result = (Type)tc.ConvertFrom(value);
			return result;
		}

		// Public Functions
		public void BindToPass( D3D10.EffectPass pass )
		{
			layout = IGameVertex.GetInputLayout( Device, pass );
		}

		public void Draw()
		{
			Device.InputAssembler.SetInputLayout( layout );
            Device.InputAssembler.SetPrimitiveTopology( D3D10.PrimitiveTopology.TriangleList );
            Device.InputAssembler.SetVertexBuffers( 0, new D3D10.VertexBufferBinding( vertexbuffer, IGameVertex.Size, 0 ) );
			
			Device.Draw( FaceCount * 3, 0 );
		}

		public void UploadVertices()
		{
			int vertexcount = FaceCount * 3;

			SlimDX.DataStream stream = new SlimDX.DataStream( vertexcount * IGameVertex.Size, false, true );

			if( /*MapChannelCount > 1 ||*/ MapChannelCount == 0 )
				throw new Exception( string.Format( "MapChannelsCount OutOfRange: {0}", MapChannelCount ) );

			for( int i = 0; i < FaceCount; i++ )
			{
				for( int j = 0; j < 3; ++j )
				{
					IGameVertex vertex = new IGameVertex();

					int face = 3*i+j;
					//vertex.Normal = Normals[FaceNormals[face]];

					if( MapChannels != null )
					{
						uint uvindex = MapChannels[0].MapFaces[face];
						Vector3 mtc = MapChannels[0].MapVertices[uvindex];
						vertex.TexCoord = new Vector2( mtc.X, 1.0f - mtc.Y );
					}
			
					vertex.Position = Vertices[FaceVertices[face]];

					stream.Write<IGameVertex>( vertex );
				}
			}

			stream.Position = 0;

			D3D10.BufferDescription bufferDescription = new D3D10.BufferDescription();
			bufferDescription.BindFlags = D3D10.BindFlags.VertexBuffer;
			bufferDescription.CpuAccessFlags = D3D10.CpuAccessFlags.None;
			bufferDescription.OptionFlags = D3D10.ResourceOptionFlags.None;
			bufferDescription.SizeInBytes = vertexcount * IGameVertex.Size;
			bufferDescription.Usage = D3D10.ResourceUsage.Default;

			vertexbuffer = new D3D10.Buffer( Device, stream, bufferDescription );
			stream.Close();
		}
	}
}