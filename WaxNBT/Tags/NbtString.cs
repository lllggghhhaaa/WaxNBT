namespace WaxNBT.Tags;

public class NbtString : NbtTag
{
    public string Value;

    public NbtString(string value) => Value = value;
    
    public NbtString(string? name, string value)
    {
        Name = name;
        Value = value;
    }
    
    public static implicit operator NbtString(string value) => new(value);
    public static implicit operator string(NbtString tag) => tag.Value;

    public static NbtString FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        string value = reader.ReadString();

        return new NbtString(name, value);
    }
    
    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.String;
}