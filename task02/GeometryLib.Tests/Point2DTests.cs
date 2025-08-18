using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GeometryLib;

namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public void Can_calculate_distance(Point2D a, Point2D b, double expected)
    {
        Assert.Equal(expected, a.DistanceTo(b), precision: Point2D.Precision);
    }

    public static TheoryData<Point2D, Point2D, double> DistanceTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(3, 4), 5 }, // тройка Пифагора
            { new Point2D(1, 1), new Point2D(1, 1), 0 }, // совпадающие точки
            { new Point2D(-1, -1), new Point2D(1, 1), Math.Sqrt(8) }, // отрицательные координаты
            { new Point2D(double.MaxValue, 0), new Point2D(double.MaxValue, 0), 0 }, // совпадение на больших числах
            { new Point2D(double.MinValue, 0), new Point2D(double.MinValue, 0), 0 }, // то же самое
        };
    }

    [Fact]
    public void Equality_should_respect_tolerance()
    {
        Point2D a = new Point2D(1.00000000001, 2.0);
        Point2D b = new Point2D(1.00000000002, 2.0);

        Assert.True(a.Equals(b)); // разница меньше Tolerance
    }

    [Fact]
    public void Equality_should_fail_when_difference_is_too_big()
    {
        Point2D a = new Point2D(1.0, 2.0);
        Point2D b = new Point2D(1.0001, 2.0);

        Assert.False(a.Equals(b)); // разница больше Tolerance
    }

    [Fact]
    public void ToString_should_return_coordinates()
    {
        Point2D p = new Point2D(1.23, -4.56);
        string s = p.ToString();

        Assert.Contains("1,23", s);
        Assert.Contains("-4,56", s);
    }
}