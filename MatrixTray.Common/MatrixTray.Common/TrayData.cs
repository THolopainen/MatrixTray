namespace MatrixTray.Common;

/// <summary>
/// <para>Raw Data for the matrix.</para>
/// <para>As it is just raw data, there is no reason to abstract around it.</para>
/// </summary>
public sealed class TrayData
{
    public TrayStartCorner StartCorner
        => _corner;
    public TrayDirection Direction
        => _direction;
    public TrayDefinition Definition
        => _tray;
    public TrayID[] IDS 
        => _ids;
    public TrayUsage[] InUse
        => _inUse;
    public byte[] SubSectionID
        => _subSectionId;
    public float[] XPositionInMm
        => _xAbsPosInMm;
    public float[] YPositionInMm
        => _yAbsPosInMm;
    public float[] XOffSet
        => _xOffsetInMm;
    public float[] YOffSet
        => _yOffsetInMm;
    public float[] SocketAngle
        => _socketAngle;
    public float[] SocketYCorrection
        => _socketYCorrection;
    public float[] SocketXCorrection
        => _socketXCorrection;
    public object[] Tag
        => _tag;
    public TrayStatus[] Status
        => _status;


    public TrayData(TrayDefinition Matrix)
    {
        _tray = Matrix;
        _ids = new TrayID[_tray.MatrixSize];
        _inUse = new TrayUsage[_tray.MatrixSize];
        _subSectionId = new byte[_tray.MatrixSize];
        _xAbsPosInMm = new float[_tray.MatrixSize];
        _yAbsPosInMm = new float[_tray.MatrixSize];
        _xOffsetInMm = new float[_tray.MatrixSize];
        _yOffsetInMm = new float[_tray.MatrixSize];
        _socketAngle = new float[_tray.MatrixSize];
        _socketYCorrection = new float[_tray.MatrixSize];
        _socketXCorrection = new float[_tray.MatrixSize];
        _tag = new object[_tray.MatrixSize];
        _status = new TrayStatus[_tray.MatrixSize];

        InitializeMatrixIdentities();

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _inUse[i] = TrayUsage.InUse;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _subSectionId[i] = 0;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _xOffsetInMm[i] = 10;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _yOffsetInMm[i] = 10;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _socketAngle[i] = 0;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _socketYCorrection[i] = 0;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _socketXCorrection[i] = 0;

        for (ushort i = 0; i < _tray.MatrixSize; i++)
            _status[i] = TrayStatus.Nothing;

        void InitializeMatrixIdentities()
        {
            ushort total = 0;
            byte idy, idx;
            for (byte y = 0; y < _tray.YCount; y++)
            {
                idy = y;
                idy += 1;
                for (byte x = 0; x < _tray.XCount; x++)
                {
                    idx = x;
                    idx += 1;

                    _ids[total++] = new(idy, idx);
                }
            }
        }

    }

    private readonly TrayDefinition _tray;
    private TrayStartCorner _corner = TrayStartCorner.UpperLeft;
    private TrayDirection _direction = TrayDirection.X;
    private readonly TrayUsage[] _inUse;
    private readonly byte[] _subSectionId;
    private readonly TrayID[] _ids;
    private readonly float[] _xAbsPosInMm;
    private readonly float[] _yAbsPosInMm;
    private readonly TrayStatus[] _status;
    private readonly float[] _xOffsetInMm; //0.0001 mm
    private readonly float[] _yOffsetInMm; //0.0001 mm
    private readonly float[] _socketAngle; //0.0001 mm
    private readonly float[] _socketYCorrection; //0.0001 mm
    private readonly float[] _socketXCorrection; //0.0001 mm
    private readonly object[] _tag;
}

