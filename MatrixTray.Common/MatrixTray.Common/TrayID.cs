using System.Runtime.InteropServices;
namespace MatrixTray.Common;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public readonly struct TrayID
{
    [FieldOffset(0)]
    public readonly ushort Id;

    [FieldOffset(2)]
    public readonly byte Ypos;

    [FieldOffset(3)]
    public readonly byte Xpos;

    public TrayID(byte y, byte x)
    {
        Id = (ushort)(y << 8 | x);
        Xpos = x;
        Ypos = y;
    }

    public TrayID(ushort id)
    {
        Id = id;
        Xpos = (byte)(0xF & id);
        Ypos = (byte)(0xF & (id >> 8));
    }

    public static TrayID Empty
        => new(0, 0);

    public static bool operator ==(TrayID left, TrayID right)
        => left.Id == right.Id;

    public static bool operator !=(TrayID left, TrayID right)
        => left.Id != right.Id;

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not TrayID comparand)
            return false;

        return Equals(comparand);
    }

    public bool Equals(TrayID comparand)
        => this.Id == comparand.Id;

    public override int GetHashCode()
        => 121057 + Id;  //121057 = 17 * 7121
}

