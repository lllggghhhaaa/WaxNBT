namespace WaxNBT.Tags;

public class NbtLongArray : NbtTag
{
    public long[] Data;

    public NbtLongArray(long[] data) => Data = data;
    
    public NbtLongArray(string? name, long[] data)
    {
        Name = name;
        Data = data;
    }
    
    public static implicit operator NbtLongArray(long[] data) => new(data);
    public static implicit operator long[](NbtLongArray tag) => tag.Data;
    
    public static NbtLongArray FromReader(NbtReader reader, bool readName = true)
    {
        var name = readName ? reader.ReadString() : null;
        var lenght = reader.ReadInt();

        var data = new long[lenght];

        for (var i = 0; i < lenght; i++)
            data[i] = reader.ReadLong();

        return new NbtLongArray(name, data);
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        foreach (var i in Data) writer.Write(i);
    }

    public override NbtTagType GetType() => NbtTagType.LongArray;
}