using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

public class Surface
{
    public int Width { get; }
    public int Height { get; }

    public Surface(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Test()
    {
        using (var image = new Image<Rgba32>(400, 400))
        {
            image[200, 200] = Rgba32.White;
            image.Save("x.png");
        }
    }
}