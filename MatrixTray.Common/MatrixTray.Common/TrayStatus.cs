namespace MatrixTray.Common;

public enum TrayStatus : byte
{
    Nothing         = 0,
    Initialized     = 1,
    Processing      = 2,
    Completed       = 3,
    Fail            = 255,
}
