using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cutting.Lib.Geometry2D
{
    public class Point2D
    {
        public Point2D() { }
        public Point2D(Double x,Double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// X coordinate figure
        /// </summary>
        private Double _X;
        /// <summary>
        /// Y coordinate figure
        /// </summary>
        private Double _Y;

        public Double X
        {
            get
            {
                return this._X;
            }

            set
            {
                this._X = value;
            }
        }

        public Double Y
        {
            get
            {
                return this._Y;
            }

            set
            {
                this._Y = value;
            }
        }

        public Double GetDistanceTo(Point2D p)
        {
            Double dx = this.X - p.X;
            Double dy = this.Y - p.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public Vector2D GetVector2DTo(Point2D p)
        {
            return new Vector2D(p.X - this.X, p.Y - this.Y);
        }

        public static Point2D operator + (Point2D a, Point2D b)
        {
            return new Point2D((a.X + b.X), (a.Y + b.Y));
        }

        public static Point2D operator - (Point2D a, Point2D b)
        {
            return new Point2D((a.X - b.X), (a.Y - b.Y));
        }
    }

    public class LineSegment2D
    {
        public LineSegment2D() { }
        public LineSegment2D(Point2D p1, Point2D p2)
        {
            this.StartPt = p1; this.EndPt = p2;
        }

        private Point2D _StartPt;
        private Point2D _EndPt;

        public Point2D StartPt
        {
            get
            {
                return this._StartPt;
            }

            set
            {
                this._StartPt = value;
            }
        }

        public Point2D EndPt
        {
            get
            {
                return this._EndPt;
            }

            set
            {
                this._EndPt = value;
            }
        }

        public Double Length
        {
            get
            {
                return StartPt.GetDistanceTo(EndPt);
            }
        }
    }

    public class Vector2D
    {
        public Vector2D() { }
        public Vector2D(Double x, Double y)
        {
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// X coordinate figure
        /// </summary>
        private Double _X;
        /// <summary>
        /// Y coordinate figure
        /// </summary>
        private Double _Y;
        private const Double _precision = 0.00001;
        public Double X
        {
            get
            {
                return this._X;
            }

            set
            {
                this._X = value;
            }
        }

        public Double Y
        {
            get
            {
                return this._Y;
            }

            set
            {
                this._Y = value;
            }
        }

        public Double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }

        public Double StartAngle
        {
            get
            {
                Double angle = Math.Acos(this.X / this.Length);
                if (this.Y < 0) return 2*Math.PI - angle;
                return angle;
            }
        }

        /// <summary>
        /// Counter clock wise Cross-Production to Vector2D(v)
        /// as the two vectors are both on X-Y plane, their Cross-Production is on Z-Axis
        /// </summary>
        public Double CrossProductTo(Vector2D v)
        {
            return (this.X * v.Y - this.Y * v.X);
        }

        public bool IsSameDirection(Vector2D v)
        {
            Double DeltaAngle = Math.Abs(v.StartAngle-this.StartAngle) ;
            return DeltaAngle > 0 && DeltaAngle < _precision;
        }

        public bool IsCounterDirection(Vector2D v)
        {
            Double DeltaAngle = Math.Abs(Math.Abs(v.StartAngle - this.StartAngle)-Math.PI);
            return DeltaAngle > 0 && DeltaAngle < _precision;
        }

        public bool IsClockWiseTo(Vector2D v)
        {
            Double DeltaAngle = v.StartAngle - this.StartAngle;
            return DeltaAngle < 0;
        }

        public bool IsCounterClockWiseTo(Vector2D v)
        {
            Double DeltaAngle = v.StartAngle - this.StartAngle;
            return DeltaAngle > 0;
        }

        public Double GetNormalAngleTo(Vector2D v)
        {
            Double DotProduction = this * v;
            Double projection = (DotProduction) / (this.Length * v.Length);
            return Math.Acos(projection);
        }

        public Double GetBigAngleTo(Vector2D v)
        {
            Double angle = this.GetNormalAngleTo(v);
            return 2 * Math.PI - angle;
        }

        public Double GetAngleTo(Vector2D v)
        {
            Double DeltaAngle = v.StartAngle - this.StartAngle;
            return DeltaAngle;
        }

        public override String ToString()
        {
            return "(" + this.X + "," + this.Y + ")";
        }

        public static Vector2D operator + (Vector2D a, Vector2D b)
        {
            return new Vector2D((a.X + b.X), (a.Y + b.Y));
        }

        public static Vector2D operator - (Vector2D a, Vector2D b)
        {
            return new Vector2D((a.X - b.X), (a.Y - b.Y));
        }

        public static Double operator * (Vector2D a, Vector2D b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }
    }

    public class Arc2D
    {
        public Arc2D() { }
        public Arc2D(Point2D center, Point2D start, Point2D end)
        {
            if (Math.Abs(center.GetDistanceTo(start) - center.GetDistanceTo(end))>0.0001)
            {
                throw new Exception("Radius length is not equal!");
            }

            this.CenterPt = center;
            this.StartPt = start;
            this.EndPt = end;
        }

        private Point2D _CenterPt;
        private Point2D _StartPt;
        private Point2D _EndPt;

        public Point2D CenterPt
        {
            get
            {
                return this._CenterPt;
            }

            set
            {
                this._CenterPt = value;
            }
        }

        public Point2D StartPt
        {
            get
            {
                return this._StartPt;
            }

            set
            {
                this._StartPt = value;
            }
        }

        public Point2D EndPt
        {
            get
            {
                return this._EndPt;
            }

            set
            {
                this._EndPt = value;
            }
        }

        public Vector2D StartRadius
        {
            get
            {
                return this.CenterPt.GetVector2DTo(this.StartPt);
            }
        }

        public Vector2D EndRadius
        {
            get
            {
                return this.CenterPt.GetVector2DTo(this.EndPt);
            }
        }

        public Double StartAngle
        {
            get
            {
                return this.StartRadius.StartAngle;
            }
        }

        public Double EndAngle
        {
            get
            {
                return this.EndRadius.StartAngle;
            }
        }

        /// <summary>
        /// Angle is from StartAngle to EndAngle in Counter-Clock-Wise
        /// </summary>
        public Double Angle
        {
            get
            {
                Double angle = this.EndAngle - this.StartAngle;
                return angle;
            }
        }

        public Double Bulge
        {
            get
            {
                return Math.Tan((this.EndAngle - this.StartAngle) / 4.0);
            }
        }

        public Double Length
        {
            get
            {
                return this.StartRadius.Length * this.Angle;
            }
        }


    }


}
