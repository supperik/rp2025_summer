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
        // ширина и высота должны быть положительные
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(0, 0), new Point2D(0, 0)));
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(1, 1), new Point2D(0, 0)));
        Assert.Throws<ArgumentException>(() => new Rectangle2D(new Point2D(0, 5), new Point2D(5, 4)));
    }

    [Fact]
    public void Can_calculate_area_and_perimeter()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 0), new Point2D(3, 2));
        Assert.Equal(6, rect.Area, precision: Point2D.Precision);
        Assert.Equal(10, rect.Perimeter, precision: Point2D.Precision);
    }

    [Fact]
    public void Can_calculate_diagonal()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 0), new Point2D(3, 4));

        Assert.Equal(5, rect.Diagonal, precision: Point2D.Precision);
    }

    [Fact]
    public void Can_calculate_center()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 0), new Point2D(4, 4));

        Assert.Equal(new Point2D(2, 2), rect.Center);
    }

    [Fact]
    public void Can_check_contains_point()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 0), new Point2D(4, 4));
        Assert.True(rect.Contains(new Point2D(2, 2)));
        Assert.False(rect.Contains(new Point2D(5, 5)));
    }

    [Fact]
    public void Can_check_intersection()
    {
        Rectangle2D rect1 = new Rectangle2D(new Point2D(0, 0), new Point2D(4, 4));
        Rectangle2D rect2 = new Rectangle2D(new Point2D(2, 2), new Point2D(6, 6));
        Rectangle2D rect3 = new Rectangle2D(new Point2D(5, 5), new Point2D(7, 7));

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

        Assert.Equal(new Point2D(0, 0), box.TopLeft);
        Assert.Equal(new Point2D(2, 3), box.BottomRight);
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

        // Здесь должно упасть исключение, потому что ширина/высота == 0
        Assert.Throws<ArgumentException>(() => Rectangle2D.GetBoundingBox(points));
    }
}