using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Tests;

public class Rectangle2DTests
{
    [Fact]
    public void Cannot_create_invalid_rectangle()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(0, 0), new Point2D(0, 0))); // ширина и высота = 0
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(1, 1), new Point2D(0, 2))); // topLeft.X > bottomRight.X
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(0, 1), new Point2D(5, 2))); // topLeft.Y < bottomRight.Y
    }

    [Fact]
    public void Can_calculate_area_and_perimeter()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 2), new Point2D(3, 0));
        Assert.Equal(6, rect.Area, precision: Point2D.Precision);
        Assert.Equal(10, rect.Perimeter, precision: Point2D.Precision);
    }

    [Fact]
    public void Can_calculate_diagonal()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 4), new Point2D(3, 0));

        Assert.Equal(5, rect.Diagonal, precision: Point2D.Precision);
    }

    [Fact]
    public void Can_calculate_center()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 4), new Point2D(4, 0));

        Assert.Equal(new Point2D(2, 2), rect.Center);
    }

    [Fact]
    public void Can_check_contains_point()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 4), new Point2D(4, 0));
        Assert.True(rect.Contains(new Point2D(2, 2)));
        Assert.False(rect.Contains(new Point2D(5, 5)));
    }

    [Fact]
    public void Can_check_intersection()
    {
        Rectangle2D rect1 = new Rectangle2D(new Point2D(0, 4), new Point2D(4, 0));
        Rectangle2D rect2 = new Rectangle2D(new Point2D(2, 6), new Point2D(6, 2));
        Rectangle2D rect3 = new Rectangle2D(new Point2D(5, 7), new Point2D(7, 5));

        Assert.True(rect1.IntersectsWith(rect2));
        Assert.False(rect1.IntersectsWith(rect3));
    }

    [Fact]
    public void Can_build_bounding_box()
    {
        Point2D[] points = new[]
        {
            new Point2D(0, 0),
            new Point2D(2, 1),
            new Point2D(1, 3),
        };

        Rectangle2D box = Rectangle2D.GetBoundingBox(points);

        Assert.Equal(new Point2D(0, 3), box.TopLeft);
        Assert.Equal(new Point2D(2, 0), box.BottomRight);
    }

    [Fact]
    public void Bounding_box_throws_on_empty_input()
    {
        Assert.Throws<ArgumentException>(() => Rectangle2D.GetBoundingBox(Array.Empty<Point2D>()));
    }

    [Fact]
    public void Bounding_box_for_single_point_is_degenerate_rectangle()
    {
        Point2D[] points = new[] { new Point2D(1, 1) };

        Assert.Throws<ArgumentException>(() => Rectangle2D.GetBoundingBox(points));
    }
}