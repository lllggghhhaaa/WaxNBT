namespace WaxNBT.Tags;

public class NbtDouble : NbtTag
{
    public double Value;

    public NbtDouble(double value = 0) => Value = value;

    public NbtDouble(string? name = null, double value = 0)
    {
        Name = name;
        Value = value;
    }

    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.Double;
}