namespace GeometryLib;

public class Rectangle2D
{
    private readonly Point2D topLeft;
    private readonly Point2D bottomRight;

    public Rectangle2D(Point2D topLeft, Point2D bottomRight)
    {
        if (bottomRight.X <= topLeft.X || bottomRight.Y <= topLeft.Y)
        {
            throw new ArgumentException("Invalid rectangle: width and height must be positive");
        }

        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }

    public Point2D TopLeft => topLeft;

    public Point2D BottomRight => bottomRight;

    public double Width => bottomRight.X - topLeft.X;

    public double Height => bottomRight.Y - topLeft.Y;

    public double Diagonal => topLeft.DistanceTo(bottomRight);

    public Point2D Center => new Point2D(
        (topLeft.X + bottomRight.X) / 2.0,
        (topLeft.Y + bottomRight.Y) / 2.0);

    public double Area => Width * Height;

    public double Perimeter => 2 * (Width + Height);

    public bool Contains(Point2D p)
    {
        return p.X >= topLeft.X && p.X <= bottomRight.X
            && p.Y >= topLeft.Y && p.Y <= bottomRight.Y;
    }

    public bool IntersectsWith(Rectangle2D other)
    {
        return !(
            other.bottomRight.X < topLeft.X ||
            other.topLeft.X > bottomRight.X ||
            other.bottomRight.Y < topLeft.Y ||
            other.topLeft.Y > bottomRight.Y);
    }

    public static Rectangle2D GetBoundingBox(IEnumerable<Point2D> points)
    {
        if (points == null || !points.Any())
        {
            throw new ArgumentException("Points collection must not be empty");
        }

        double minX = points.Min(p => p.X);
        double minY = points.Min(p => p.Y);
        double maxX = points.Max(p => p.X);
        double maxY = points.Max(p => p.Y);

        return new Rectangle2D(new Point2D(minX, minY), new Point2D(maxX, maxY));
    }
}
