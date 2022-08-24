namespace MatrixTray.Common;

/// <summary>
/// <para>Raw Data for the matrix.</para>
/// <para>As it is just raw data, there is no reason to abstract around it.</para>
/// </summary>
public sealed class MatrixData
{
    public MatrixStartCorner StartCorner
        => _corner;
    public MatrixDirection Direction
        => _direction;
    public MatrixDefinition Definition
        => _matrix;
    public MatrixId[] IDS 
        => _ids;
    public TrayMatrixUsage[] InUse
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
    public byte[] Status
        => _status;


    public MatrixData(MatrixDefinition Matrix)
    {
        _matrix = Matrix;
        _ids = new MatrixId[_matrix.MatrixSize];
        _inUse = new TrayMatrixUsage[_matrix.MatrixSize];
        _subSectionId = new byte[_matrix.MatrixSize];
        _xAbsPosInMm = new float[_matrix.MatrixSize];
        _yAbsPosInMm = new float[_matrix.MatrixSize];
        _xOffsetInMm = new float[_matrix.MatrixSize];
        _yOffsetInMm = new float[_matrix.MatrixSize];
        _socketAngle = new float[_matrix.MatrixSize];
        _socketYCorrection = new float[_matrix.MatrixSize];
        _socketXCorrection = new float[_matrix.MatrixSize];
        _tag = new object[_matrix.MatrixSize];
        _status = new byte[_matrix.MatrixSize];

        InitializeMatrixIdentities();

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _inUse[i] = TrayMatrixUsage.InUse;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _subSectionId[i] = 0;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _xOffsetInMm[i] = 10;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _yOffsetInMm[i] = 10;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _socketAngle[i] = 0;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _socketYCorrection[i] = 0;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _socketXCorrection[i] = 0;

        for (ushort i = 0; i < _matrix.MatrixSize; i++)
            _status[i] = 0;

        void InitializeMatrixIdentities()
        {
            ushort total = 0;
            byte idy, idx;
            for (byte y = 0; y < _matrix.YCount; y++)
            {
                idy = y;
                idy += 1;
                for (byte x = 0; x < _matrix.XCount; x++)
                {
                    idx = x;
                    idx += 1;

                    _ids[total++] = new(idy, idx);
                }
            }
        }

    }

    private readonly MatrixDefinition _matrix;
    private MatrixStartCorner _corner = MatrixStartCorner.UpperLeft;
    private MatrixDirection _direction = MatrixDirection.Xdirection;
    private readonly TrayMatrixUsage[] _inUse;
    private readonly byte[] _subSectionId;
    private readonly MatrixId[] _ids;
    private readonly float[] _xAbsPosInMm;
    private readonly float[] _yAbsPosInMm;
    private readonly byte[] _status;
    private readonly float[] _xOffsetInMm; //0.0001 mm
    private readonly float[] _yOffsetInMm; //0.0001 mm
    private readonly float[] _socketAngle; //0.0001 mm
    private readonly float[] _socketYCorrection; //0.0001 mm
    private readonly float[] _socketXCorrection; //0.0001 mm
    private readonly object[] _tag;
}

