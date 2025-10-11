using System.Buffers;
using BenchmarkDotNet.Attributes;
using WaxNBT;

namespace WaxNBT.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class NbtWriterBenchmarks
{
    [Benchmark(Baseline = true)]
    public void WritePrimitives_Old()
    {
        var writer = new NbtWriterOld();
        
        for (int i = 0; i < 100; i++)
        {
            writer.Write((byte)42);
            writer.Write((short)12345);
            writer.Write(987654321);
            writer.Write(123456789012345L);
            writer.Write(3.14159f);
            writer.Write(2.718281828459);
            writer.Write("Hello NBT World!");
        }
    }

    [Benchmark]
    public void WritePrimitives_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        
        for (int i = 0; i < 100; i++)
        {
            writer.Write((byte)42);
            writer.Write((short)12345);
            writer.Write(987654321);
            writer.Write(123456789012345L);
            writer.Write(3.14159f);
            writer.Write(2.718281828459);
            writer.Write("Hello NBT World!");
        }
    }

    [Benchmark]
    public void WriteByte_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write((byte)42);
        }
    }

    [Benchmark]
    public void WriteByte_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write((byte)42);
        }
    }

    [Benchmark]
    public void WriteShort_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write((short)12345);
        }
    }

    [Benchmark]
    public void WriteShort_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write((short)12345);
        }
    }

    [Benchmark]
    public void WriteInt_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(987654321);
        }
    }

    [Benchmark]
    public void WriteInt_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(987654321);
        }
    }

    [Benchmark]
    public void WriteLong_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(123456789012345L);
        }
    }

    [Benchmark]
    public void WriteLong_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(123456789012345L);
        }
    }

    [Benchmark]
    public void WriteFloat_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(3.14159f);
        }
    }

    [Benchmark]
    public void WriteFloat_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(3.14159f);
        }
    }

    [Benchmark]
    public void WriteDouble_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(2.718281828459);
        }
    }

    [Benchmark]
    public void WriteDouble_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write(2.718281828459);
        }
    }

    [Benchmark]
    public void WriteString_Old()
    {
        var writer = new NbtWriterOld();
        for (int i = 0; i < 1000; i++)
        {
            writer.Write("Hello NBT World!");
        }
    }

    [Benchmark]
    public void WriteString_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        for (int i = 0; i < 1000; i++)
        {
            writer.Write("Hello NBT World!");
        }
    }

    [Benchmark]
    public void WriteStringLarge_Old()
    {
        var writer = new NbtWriterOld();
        var largeString = new string('X', 512);
        for (int i = 0; i < 100; i++)
        {
            writer.Write(largeString);
        }
    }

    [Benchmark]
    public void WriteStringLarge_New()
    {
        var buffer = new ArrayBufferWriter<byte>();
        var writer = new NbtWriter(buffer);
        var largeString = new string('X', 512);
        for (int i = 0; i < 100; i++)
        {
            writer.Write(largeString);
        }
    }
}
