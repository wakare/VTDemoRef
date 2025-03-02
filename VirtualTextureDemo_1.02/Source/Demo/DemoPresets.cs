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
	public static class DemoPresets
	{
		public static DemoInfo[] Infos =
		{
			//*
			new DemoInfo()
			{
				Name = "32k Demo",
				TextureFile = "Data\\DemoScene_32k.raw",
				GeometryFile = "Data\\DemoScene.xml",
			},
			//*/

			new DemoInfo()
			{
				Name = "16k Demo",
				TextureFile = "Data\\DemoScene_16k.tga",
				GeometryFile = "Data\\DemoScene.xml",
			},

			new DemoInfo()
			{
				Name = "8k Demo",
				TextureFile = "Data\\DemoScene_8k.tga",
				GeometryFile = "Data\\DemoScene.xml",
			},

			/*
			new DemoInfo()
			{
				Name = "Teapot",
				TextureFile = "Data\\Lightmap_16k.tga",
				GeometryFile = "Data\\Teapot.xml",
			},

			new DemoInfo()
			{
				Name = "Planets",
				TextureFile = "Data\\Planets_64k.raw",
				GeometryFile = "Data\\Planets.xml",
			}

			new DemoInfo()
			{
				Name = "Tree",
				TextureFile = "Data\\Branches.tga",
				GeometryFile = "Data\\Tree.xml",
			},
			//*/
		};
	}
}