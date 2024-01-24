using System.Collections;

namespace WaxNBT.Tags;

public class NbtList : NbtTag, IEnumerable<NbtTag>
{
    private readonly List<NbtTag> _data = new();

    public NbtList(string? name = null) => Name = name;

    public NbtTag this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }
    
    public static NbtList FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        NbtTagType type = reader.ReadTagType();
        int count = reader.ReadInt();

        NbtList list = new NbtList(name);
        
        for (int i = 0; i < count; i++) list._data.Add(reader.ReadTag(type, false));

        return list;
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(_data[0].GetType());
        writer.Write(_data.Count);
        foreach (NbtTag tag in _data) tag.SerializeValue(ref writer);
    }
    
    public override NbtTagType GetType() => NbtTagType.List;
    
    
    public IEnumerator<NbtTag> GetEnumerator() => _data.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(NbtTag tag) => _data.Add(tag);

    public void AddRange(NbtTag[] tags) => _data.AddRange(tags);
}