using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using SixLabors.Shapes;
using SixLabors.Primitives;
using System.IO;

public class Surface
{
    private readonly Image<Rgba32> image;

    public int Width { get; }
    public int Height { get; }

    public Surface(int width, int height)
    {
        Width = width;
        Height = height;
        this.image = new Image<Rgba32>(width, height);
        this.image.Mutate(ctx => ctx
            .Fill(Rgba32.Purple, new Rectangle(0, 0, width, height)));
    }

    public void Test()
    {
        using (var image = new Image<Rgba32>(400, 400))
        {
            image.Mutate(dc => dc.Fill(Brushes.Solid(Rgba32.White)));
            image[200, 200] = Rgba32.White;
            image.Save("x.png");
        }
    }

    public byte[] EncodePng()
    {
        using (var stream = new MemoryStream())
        {
            this.image.SaveAsPng(stream);
            return stream.ToArray();
        }
    }
}
