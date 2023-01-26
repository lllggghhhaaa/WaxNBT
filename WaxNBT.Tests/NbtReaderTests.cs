namespace WaxNBT.Tests;

public class NbtReaderTests
{
    private NbtReader _reader;
    
    public NbtReaderTests()
    {
        NbtWriter writer = new NbtWriter();
        writer.Write(NbtTagType.Byte);
        writer.Write((byte)1);
        writer.Write(new byte[] { 1, 2, 3, 4 });
        writer.Write(8.2d);
        writer.Write(4.1f);
        writer.Write(4);
        writer.Write(8L);
        writer.Write((short)2);
        writer.Write("Ceira");

        Stream stream = writer.GetStream();
        stream.Position = 0;

        _reader = new NbtReader(stream);
    }
    
    [Fact]
    public void TestTagType()
    {
        NbtTagType tagType = _reader.ReadTagType();
        
        Assert.Equal(NbtTagType.Byte, tagType);
    }

    [Fact]
    public void TestByte()
    {
        byte result = _reader.ReadByte();
        
        Assert.Equal((byte)1, result);
    }
}