using System.Text;

namespace AccOsuMemory.Core.OsuDataReader;

internal static class StreamExt
{
    private static readonly byte[] IntBuffer = new byte[sizeof(int)];
    private static readonly byte[] BooleanBuffer = new byte[sizeof(bool)];
    private static readonly byte[] ShortBuffer = new byte[sizeof(short)];
    private static readonly byte[] LongBuffer = new byte[sizeof(long)];
    private static readonly byte[] DoubleBuffer = new byte[sizeof(double)];
    private static readonly byte[] FloatBuffer = new byte[sizeof(float)];
    
    public static int ReadInt(this Stream stream)
    {
        _ = stream.Read(IntBuffer, 0, IntBuffer.Length);
        return BitConverter.ToInt32(IntBuffer);
    }

    public static bool ReadBoolean(this Stream stream)
    {
       
        _ = stream.Read(BooleanBuffer, 0, BooleanBuffer.Length);
        return BooleanBuffer[0] != 0;
    }

    public static short ReadShort(this Stream stream)
    {
        _ = stream.Read(ShortBuffer, 0, ShortBuffer.Length);
        return BitConverter.ToInt16(ShortBuffer);
    }

    public static long ReadLong(this Stream stream)
    {
        _ = stream.Read(LongBuffer, 0, LongBuffer.Length);
        return BitConverter.ToInt64(LongBuffer);
    }
    
    private const int J = 1 << 7;

    public static string? ReadString(this Stream stream)
    {
        switch (stream.ReadByte())
        {
            case 0x0b:
                var strlen = 0;
                var offset = 0;
                while (true)
                {
                    var t = stream.ReadByte();
                    strlen |= (t & 127) << offset;
                    if ((t & J) == 0) break;
                    offset += 7;
                }

                var bytes = new byte[strlen];
                _ = stream.Read(bytes,0,bytes.Length);
                return Encoding.UTF8.GetString(bytes);
 
            default:
                return null;
        }
    }
    
    
    public static double ReadDouble(this Stream stream)
    {
        _ = stream.Read(DoubleBuffer, 0, DoubleBuffer.Length);
        return BitConverter.ToDouble(DoubleBuffer);
    }
    
    public static float ReadFloat(this Stream stream)
    {
        _ = stream.Read(FloatBuffer, 0, FloatBuffer.Length);
        return BitConverter.ToSingle(FloatBuffer);
    }

    public static List<double> ReadStarRatings(this Stream stream)
    {
        var count = stream.ReadInt();
        var list = new List<double>();
        for (int i = 0; i < count; i++)
        {
            _ = stream.ReadByte();
            _ = stream.ReadInt();
            _ = stream.ReadByte();
            list.Add(stream.ReadDouble());
        }

        return list;
    }

    public static List<TimingPoint> ReadTimingPoints(this Stream stream)
    {
        var count = stream.ReadInt();
        var list = new List<TimingPoint>();
        for (int i = 0; i < count; i++)
        {
            var bpm = stream.ReadDouble();
            var offset = stream.ReadDouble();
            var inherit = stream.ReadBoolean();
            list.Add(new TimingPoint(bpm, offset, inherit));
        }

        return list;
    }
    
}