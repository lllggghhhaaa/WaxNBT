namespace WaxNBT.Tests;

public class NbtReaderTests
{
    private NbtReader _reader;
    
    public NbtReaderTests()
    {
        NbtWriter writer = new NbtWriter();
        writer.Write(NbtTagType.Int);
        writer.Write((byte)2);
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
        
        Assert.Equal(NbtTagType.Int, tagType);
    }

    [Fact]
    public void TestByte()
    {
        _reader.Skip(1);
        byte result = _reader.ReadByte();
        
        Assert.Equal((byte)2, result);
    }
    
    [Fact]
    public void TestByteArray()
    {
        _reader.Skip(2);
        byte[] result = _reader.ReadArray(4);
        
        Assert.Equal(new byte[] { 1, 2, 3, 4 }, result);
    }
}