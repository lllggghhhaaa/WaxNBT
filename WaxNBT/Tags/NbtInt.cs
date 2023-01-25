namespace WaxNBT.Tags;

public class NbtInt : NbtTag
{
    public int Value;

    public NbtInt(int value = 0) => Value = value;

    public NbtInt(string? name = null, int value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Int;
}