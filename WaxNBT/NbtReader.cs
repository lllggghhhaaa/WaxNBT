using System.Buffers.Binary;
using System.Text;

namespace WaxNBT;

public class NbtReader
{
    public Encoding StringEncoder = Encoding.UTF8;

    private readonly MemoryStream _stream = new();
    
    public void Write(NbtTagType type) => Write((byte)type);
    
    public void Write(byte value) => _stream.WriteByte(value);

    public void Write(byte[] data) => _stream.Write(data);
    
    public short ReadShort()
    {
        byte[] data = new byte[2];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt16BigEndian(data);
    }

    public int ReadInt()
    {
        byte[] data = new byte[4];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt32BigEndian(data);
    }

    public long ReadLong()
    {
        byte[] data = new byte[8];
        int read = _stream.Read(data);
        return BinaryPrimitives.ReadInt64BigEndian(data);
    }
    
    public float ReadFloat()
    {
        byte[] data = new byte[4];
        int read = _stream.Read(data);
        
        Array.Reverse(data);
        return BitConverter.ToSingle(data);
    }
    
    public double ReadDouble()
    {
        byte[] data = new byte[8];
        int read = _stream.Read(data);
        
        Array.Reverse(data);
        return BitConverter.ToDouble(data);
    }

    public string ReadString()
    {
        short lenght = ReadShort();

        byte[] data = new byte[lenght];
        int read = _stream.Read(data);

        return StringEncoder.GetString(data);
    }
    
    public Stream GetStream() => _stream;
}