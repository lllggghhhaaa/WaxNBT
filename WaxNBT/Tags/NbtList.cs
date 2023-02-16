namespace WaxNBT.Tags;

public class NbtList : NbtTag
{
    public List<NbtTag> Data = new();

    public NbtList(string? name = null) => Name = name;

    public static NbtList FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        NbtTagType type = reader.ReadTagType();
        int count = reader.ReadInt();

        NbtList list = new NbtList(name);
        
        for (int i = 0; i < count; i++) list.Data.Add(reader.ReadTag(type, false));

        return list;
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data[0].GetType());
        writer.Write(Data.Count);
        foreach (NbtTag tag in Data) tag.SerializeValue(ref writer);
    }

    public override NbtTagType GetType() => NbtTagType.List;
}