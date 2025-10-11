using System.Buffers;
using WaxNBT.Tags;

namespace WaxNBT;

public class NbtFile
{
    public NbtCompound Root { get; set; }

    public NbtFile(string rootName = "")
    {
        Root = new NbtCompound(rootName);
    }

    public static NbtFile Parse(Stream stream)
    {
        var reader = NbtReader.FromStream(stream);
        return new NbtFile { Root = (NbtCompound)reader.ReadTag() };
    }

    public static NbtFile Parse(byte[] data)
    {
        var reader = new NbtReader(data);
        return new NbtFile { Root = (NbtCompound)reader.ReadTag() };
    }

    public static NbtFile Parse(NbtReader reader)
    {
        return new NbtFile { Root = (NbtCompound)reader.ReadTag() };
    }

    public ReadOnlyMemory<byte> Serialize()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);

        Root.Serialize(ref writer);

        return buffer.WrittenMemory;
    }

    public Stream SerializeToStream()
    {
        var data = Serialize();
        return new MemoryStream(data.ToArray());
    }
}