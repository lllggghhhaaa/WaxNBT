namespace WaxNBT.Tags;

public class NbtLongArray : NbtTag
{
    public long[] Data;

    public NbtLongArray(long[] data, string? name = null)
    {
        Name = name;
        Data = data;
    }
    
    public static NbtLongArray FromReader(NbtReader reader, bool readName = true)
    {
        string? name = readName ? reader.ReadString() : null;
        int lenght = reader.ReadInt();

        long[] data = new long[lenght];

        for (int i = 0; i < lenght; i++)
            data[i] = reader.ReadLong();

        return new NbtLongArray(data, name);
    }

    internal override void SerializeValue(ref NbtWriter writer)
    {
        writer.Write(Data.Length);
        foreach (long i in Data) writer.Write(i);
    }

    public override NbtTagType GetType() => NbtTagType.LongArray;
}