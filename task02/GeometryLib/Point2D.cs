using System.Diagnostics.CodeAnalysis;

namespace GeometryLib;

public readonly struct Point2D(double x, double y)
    : IEquatable<Point2D>
{
    public const double Tolerance = 1e-10;
    public const int Precision = 10;

    public double X { get; } = x;

    public double Y { get; } = y;

    public static bool operator ==(Point2D left, Point2D right) => left.Equals(right);

    public static bool operator !=(Point2D left, Point2D right) => !(left == right);

    public double DistanceTo(Point2D other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }

    public bool Equals(Point2D other)
    {
        return Math.Abs(X - other.X) < Tolerance
            && Math.Abs(Y - other.Y) < Tolerance;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Point2D other && Equals(other);
    }

    public override int GetHashCode() => (X, Y).GetHashCode();

    public override string ToString() => $"({X}, {Y})";
}
