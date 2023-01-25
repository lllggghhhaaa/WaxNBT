namespace WaxNBT.Tags;

public class NbtByteArray : NbtTag
{
    public byte[] Data;

    public NbtByteArray(byte[] data) => Data = data;

    public NbtByteArray(string name, byte[] data)
    {
        Name = name;
        Data = data;
    }

    public NbtByteArray(byte[] data, string? name = null)
    {
        Name = name;
        Data = data;
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        writer.Write(Data);
    }

    public override NbtTagType GetType() => NbtTagType.ByteArray;
}