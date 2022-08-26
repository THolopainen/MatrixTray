﻿namespace MatrixTray.Common;

/// <summary>
/// <para>Common Coordinate struct to be published via fields or properties.</para>
/// </summary>
public readonly struct MatrixCoord
{
    public readonly float XCoord;
    public readonly float YCoord;

    public MatrixCoord(float XCoord, float YCoord)
    {
        this.XCoord = XCoord;
        this.YCoord = YCoord;
    }
}

/// <summary>
/// <para>Ref struct which guarantees that it will live only in the calling stack.</para>
/// </summary>
public ref struct MatrixCoordRef
{
    public float XCoord = 0f;
    public float YCoord = 0f;

    public MatrixCoordRef(float XCoord, float YCoord)
    {
        this.XCoord = XCoord;
        this.YCoord = YCoord;
    }
}
