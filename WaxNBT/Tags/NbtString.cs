namespace WaxNBT.Tags;

public class NbtString : NbtTag
{
    public string Value;
    
    public NbtString(string value = "", string? name = null)
    {
        Name = name;
        Value = value;
    }

    public static NbtString FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        string value = reader.ReadString();

        return new NbtString(value, name);
    }
    
    internal override void SerializeValue(ref NbtWriter writer) => writer.Write(Value);

    public override NbtTagType GetType() => NbtTagType.String;
}