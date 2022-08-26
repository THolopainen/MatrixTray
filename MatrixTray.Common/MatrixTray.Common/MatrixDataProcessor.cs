namespace MatrixTray.Common;

public sealed class MatrixDataProcessor
{
    private MatrixData _data;

    public MatrixDataProcessor(MatrixData Data)
    {
        _ = Data ?? throw new ArgumentNullException(nameof(Data));
        _data = Data;
    }

    public void InitializeMatrixCoordinates(MatrixCoord FirstSocket, MatrixCoord OffsetToNext)
    {
        //make all points to define to same origin coordinate;

        for (int i = 0; i < _data.XPositionInMm.Length; i++)
            _data.XPositionInMm[i] = FirstSocket.XCoord;

        for (int i = 0; i < _data.YPositionInMm.Length; i++)
            _data.YPositionInMm[i] = FirstSocket.YCoord;

        //move all positions ( sans origin ) by the accumulated X/Y offset

        //suggestion :
        //we could create the arrays for the offsets once and copy that array
        //with range offsets.

        float totalXOffset;
        float totalYOffset;

        int idx = 0;
        for (int y = 0; y < _data.Definition.YCount; y++)
        {
            totalYOffset = y * OffsetToNext.YCoord;

            for (int x = 0; x < _data.Definition.XCount; x++)
            {
                totalXOffset = x * OffsetToNext.XCoord;

                _data.YPositionInMm[idx] += totalYOffset;
                _data.XPositionInMm[idx] += totalXOffset;
                idx++;
            }
        }
    }


    public void DisableSocketOnTray(MatrixId Id)
    {
        _data.InUse[GetIndex(Id)] = TrayMatrixUsage.NoInUse;
    }

    public void EnableSocketOnTray(MatrixId Id)
    {
        _data.InUse[GetIndex(Id)] = TrayMatrixUsage.InUse;
    }

    public void SkipSocketOnTray(MatrixId Id)
    {
        _data.InUse[GetIndex(Id)] = TrayMatrixUsage.SkipOver;
    }

    public void SetSocketAngle(MatrixId Id, float angle)
    {
        _data.SocketAngle[GetIndex(Id)] = angle;
    }

    public void SetSubsectionIDForSocket(MatrixId Id, byte SubSectionID)
    {
        _data.SubSectionID[GetIndex(Id)] = SubSectionID;
    }

    public void SetSocketAngle(MatrixId Id, byte StatusCode)
    {
        _data.Status[GetIndex(Id)] = StatusCode;
    }

    public MatrixCoord GetSocketCoordinates(MatrixId Id)
    {
        ushort idx = GetIndex(Id);
        return new MatrixCoord(_data.XPositionInMm[idx], _data.YPositionInMm[idx]);
    }

    public void GetSocketCoordinates(MatrixId Id, ref MatrixCoordRef coord)
    {
        ushort idx = GetIndex(Id);
        coord.XCoord = _data.XPositionInMm[idx];
        coord.YCoord = _data.YPositionInMm[idx];
    }


    public async Task AdjustXOffsetForColumnAsync(byte FromXColumnNumber, float OffsetToNextColumn)
    {
        await Task.Factory.StartNew(() => 
        {
            AdjustXOffsetForColumn(FromXColumnNumber, OffsetToNextColumn);
        });
    }

    public void AdjustXOffsetForColumn(byte FromXColumnNumber, float OffsetToNextColumn)
    {
        for (int i = 0; i < _data.IDS.Length; i++)
        {
            if (_data.IDS[i].Xpos <= FromXColumnNumber)
                continue;

            _data.XPositionInMm[i] += OffsetToNextColumn;
            _data.XOffSet[i] += OffsetToNextColumn;
        }
    }

    public async Task AdjustYOffsetForRowsAsync(byte FromYRowNumber, float OffsetToNextRow)
    {
        await Task.Factory.StartNew(() =>
        {
            AdjustXOffsetForColumn(FromYRowNumber, OffsetToNextRow);
        });
    }

    public void AdjustYOffsetForRows(byte FromYRowNumber, float OffsetToNextRow)
    {
        for (int i = 0; i < _data.IDS.Length; i++)
        {
            if (_data.IDS[i].Ypos <= FromYRowNumber)
                continue;

            _data.XPositionInMm[i] += OffsetToNextRow;
            _data.XOffSet[i] += OffsetToNextRow;
        }
    }





    private MatrixId _cachedId = MatrixId.Empty;
    private ushort _cachedIdx = 0;
    private ushort GetIndex(MatrixId id)
    {
        //perf.
        //help JIT compiler to decide if to inline by making implementation <= 16 bytes
        if (_cachedId == id)
            return _cachedIdx;

        return CalculateIndex(id);
    }

    private ushort CalculateIndex(MatrixId id)
    {
        if (IsOutOfBounds(id))
            throw new ArgumentOutOfRangeException(nameof(id));

        for (ushort i = 0; i < _data.IDS.Length; i++)
        {
            if (_data.IDS[i] == id)
            {
                _cachedId = id;
                _cachedIdx = i;
                return i;
            }
        }

        return 0;
    }

    private bool IsOutOfBounds(MatrixId Id)
    {
        MatrixDefinition def = _data.Definition;

        if (Id.Ypos > def.YCount)
            return true;

        if (Id.Xpos > def.XCount)
            return true;

        return false;
    }
}




