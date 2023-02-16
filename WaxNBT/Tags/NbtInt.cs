namespace WaxNBT.Tags;

public class NbtInt : NbtTag
{
    public int Value;

    public NbtInt(int value = 0, string? name = null)
    {
        Name = name;
        Value = value;
    }

    public static NbtInt FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        int value = reader.ReadInt();

        return new NbtInt(value, name);
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Int;
}