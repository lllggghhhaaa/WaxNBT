using WaxNBT;
using WaxNBT.Tags;

NbtFile nbt = new NbtFile();

NbtCompound numbers = new NbtCompound("numbers");
NbtCompound texts = new NbtCompound("texts");
NbtCompound arrays = new NbtCompound("arrays");

numbers.Add(new NbtByte(8, "byte"));
numbers.Add(new NbtShort(16, "short"));
numbers.Add(new NbtInt(32, "int"));
numbers.Add(new NbtLong(64, "long"));
numbers.Add(new NbtFloat(32.1f, "float"));
numbers.Add(new NbtDouble(64.2d, "double"));

texts.Add(new NbtString("ceira", "first"));
texts.Add(new NbtString("pura", "second"));
texts.Add(new NbtString("sinas", "third"));

arrays.Add(new NbtByteArray(new byte[] { 9, 99 }, "bytes"));
arrays.Add(new NbtIntArray(new[] { 9, 99, 999, 9999 }, "ints"));
arrays.Add(new NbtLongArray(new long[] { 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999 }, "longs"));

NbtList list = new NbtList("list");
list.Data.AddRange(new []
{
    new NbtString("pessoa"),
    new NbtString("de"),
    new NbtString("ceira"),
    new NbtString("cheia"),
    new NbtString("de"),
    new NbtString("ceira")
});

nbt.Root.Add(numbers);
nbt.Root.Add(texts);
nbt.Root.Add(arrays);
nbt.Root.Add(list);
Stream stream = nbt.Serialize();

FileStream fs = File.Create("ceira.nbt");
stream.CopyTo(fs);

stream.Close();
fs.Close();

NbtFile readFile = NbtFile.Parse(File.ReadAllBytes("ceira.nbt"));

Console.WriteLine(readFile.Root.Name);
foreach (NbtTag child in readFile.Root.Children)
{
    Console.WriteLine(child.Name);
    Console.WriteLine(child.GetType());
}