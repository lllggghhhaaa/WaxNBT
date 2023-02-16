namespace WaxNBT.Tags;

public class NbtByteArray : NbtTag
{
    public byte[] Data;

    public NbtByteArray(byte[] data, string? name = null)
    {
        Name = name;
        Data = data;
    }

    public static NbtByteArray FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        int lenght = reader.ReadInt();
        byte[] data = reader.ReadArray(lenght);

        return new NbtByteArray(data, name);
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        writer.Write(Data);
    }

    public override NbtTagType GetType() => NbtTagType.ByteArray;
}