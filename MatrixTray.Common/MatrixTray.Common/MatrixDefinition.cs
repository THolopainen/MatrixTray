using System.Runtime.InteropServices;
namespace MatrixTray.Common;

[StructLayout(LayoutKind.Explicit, Size = 2)]
public readonly struct MatrixDefinition
{
    [FieldOffset(0)]
    public readonly ushort MatrixSize;

    [FieldOffset(2)]
    public readonly byte XCount;
    
    [FieldOffset(3)]
    public readonly byte YCount;
    
    public MatrixDefinition(byte Xcount, byte Ycount)
    {
        XCount = Xcount;
        YCount = Ycount;
        MatrixSize = (ushort)(Xcount * Ycount);
    }

    public static MatrixDefinition Empty()
        => new(0, 0);
}





