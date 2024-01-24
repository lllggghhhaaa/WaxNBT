using WaxNBT;
using WaxNBT.Tags;

NbtFile nbt = new NbtFile();

NbtCompound numbers = new NbtCompound("numbers");
NbtCompound texts = new NbtCompound("texts");
NbtCompound arrays = new NbtCompound("arrays");

numbers.Add(new NbtByte("byte", 8));
numbers.Add(new NbtShort("short", 16));
numbers.Add(new NbtInt("int", 32));
numbers.Add(new NbtLong("long", 64));
numbers.Add(new NbtFloat("float", 32.1f));
numbers.Add(new NbtDouble("double", 64.2d));

texts.Add(new NbtString("first", "ceira"));
texts.Add(new NbtString("second", "pura"));
texts.Add(new NbtString("third", "sinas"));

arrays.Add(new NbtByteArray("bytes", new byte[] { 9, 99 }));
arrays.Add(new NbtIntArray("ints", new[] { 9, 99, 999, 9999 }));
arrays.Add(new NbtLongArray("longs", new long[] { 9, 99, 999, 9999, 99999, 999999, 9999999, 99999999 }));

var list = new NbtList
{
    new NbtString("pessoa"),
    new NbtString("de"),
    new NbtString("ceira"),
    new NbtString("cheia"),
    new NbtString("de"),
    new NbtString("ceira")
};

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