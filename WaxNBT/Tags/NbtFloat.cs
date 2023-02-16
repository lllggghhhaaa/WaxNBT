namespace WaxNBT.Tags;

public class NbtFloat : NbtTag
{
    public float Value;

    public NbtFloat(float value = 0, string? name = null)
    {
        Name = name;
        Value = value;
    }

    public static NbtFloat FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        float value = reader.ReadFloat();

        return new NbtFloat(value, name);
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Float;
}