using System.Buffers;

namespace WaxNBT.Tests;

public class NbtReaderTests
{
    public NbtReader CreateReader()
    {
        var buffer = new ArrayBufferWriter<byte>();
        
        NbtWriter writer = new NbtWriter(buffer);
        writer.Write(NbtTagType.Int);
        writer.Write((byte)2);
        writer.Write(new byte[] { 1, 2, 3, 4 });
        writer.Write(8.2d);
        writer.Write(4.1f);
        writer.Write(4);
        writer.Write(8L);
        writer.Write((short)2);
        writer.Write("Ceira");

        return new NbtReader(buffer.WrittenSpan);
    }
    
    [Fact]
    public void TestTagType()
    {
        var reader = CreateReader();
        NbtTagType tagType = reader.ReadTagType();
        
        Assert.Equal(NbtTagType.Int, tagType);
    }

    [Fact]
    public void TestByte()
    {
        var reader = CreateReader();
        reader.Skip(1);
        byte result = reader.ReadByte();
        
        Assert.Equal((byte)2, result);
    }
    
    [Fact]
    public void TestByteArray()
    {
        var reader = CreateReader();
        reader.Skip(2);
        byte[] result = reader.ReadArray(4);
        
        Assert.Equal(new byte[] { 1, 2, 3, 4 }, result);
    }

    [Fact]
    public void TestDouble()
    {
        var reader = CreateReader();
        reader.Skip(6);
        double result = reader.ReadDouble();
        
        Assert.Equal(8.2d, result);
    }

    [Fact]
    public void TestFloat()
    {
        var reader = CreateReader();
        reader.Skip(14);
        float result = reader.ReadFloat();
        
        Assert.Equal(4.1f, result);
    }
    
    [Fact]
    public void TestInt()
    {
        var reader = CreateReader();
        reader.Skip(18);
        int result = reader.ReadInt();
        
        Assert.Equal(4, result);
    }

    [Fact]
    public void TestLong()
    {
        var reader = CreateReader();
        reader.Skip(22);
        long result = reader.ReadLong();
        
        Assert.Equal(8, result);
    }

    [Fact]
    public void TestShort()
    {
        var reader = CreateReader();
        reader.Skip(30);
        short result = reader.ReadShort();
        
        Assert.Equal((short)2, result);
    }

    [Fact]
    public void TestString()
    {
        var reader = CreateReader();
        reader.Skip(32);
        string result = reader.ReadString();
        
        Assert.Equal("Ceira", result);
    }
}