namespace WaxNBT.Tags;

public class NbtByte : NbtTag
{
    public byte Value;

    public NbtByte(byte value = 0, string? name = null)
    {
        Name = name;
        Value = value;
    }

    public static NbtByte FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        byte value = reader.ReadByte();
        
        return new NbtByte(value, name);
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Byte;
}