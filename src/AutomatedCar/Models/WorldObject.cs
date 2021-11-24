namespace AutomatedCar.Models
{
    using System.Collections.ObjectModel;
    using System.Drawing;
    using Avalonia.Media;
    using ReactiveUI;
    using Point = Avalonia.Point;
    using Polygon = Avalonia.Controls.Shapes.Polygon;

    public class WorldObject : ReactiveObject
    {
        private int x;
        private int y;

        private double rotation;

        //Ádám
        public List<List<Point>> BasePoints { get; private set; }

        public WorldObject(int x, int y, string filename, int zindex = 1, bool collideable = false, WorldObjectType worldObjectType = WorldObjectType.Other)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = zindex;
            this.Collideable = collideable;
            this.WorldObjectType = worldObjectType;
            
            //Ádám
            this.NetPolygons = this.GenerateNetPolygons(polyPoints);

        }

        public int ZIndex { get; set; }

        public double Rotation
        {
            get => this.rotation;
            set => this.RaiseAndSetIfChanged(ref this.rotation, value % 360);
        }

        //Ádám
        public List<Polygon> Polygons { get; set; }
        public List<LineString> NetPolygons { get; set; }

        public int X
        {
            get => this.x;
            set
            {
                //Ádám
                this.UpdatePolygons();

                this.RaiseAndSetIfChanged(ref this.x, value);
            }
        }

        public int Y
        {
            get => this.y;
            set
            {
                //Ádám
                this.UpdatePolygons();

                this.RaiseAndSetIfChanged(ref this.y, value);
            }
        }

        //Ádám
        private void UpdatePolygons()
        {
            this.NetPolygons = this.GenerateNetPolygons(this.BasePoints);
        }

        public Point RotationPoint { get; set; }

        public string RenderTransformOrigin { get; set; }

        public ObservableCollection<PolylineGeometry> Geometries { get; set; } = new ();

        public ObservableCollection<PolylineGeometry> RawGeometries { get; set; } = new ();

        public string Filename { get; set; }

        public bool Collideable { get; set; }

        //Ádám
        public bool IsColliding { get; set; }
        private List<Polygon> GeneratePolygons(List<List<Point>> polyPoints)
        {
            List<Polygon> objectPolygons = new List<Polygon>();
            foreach (List<Point> points in polyPoints)
            {
                objectPolygons.Add(new Polygon() { Points = points });
            }

            return objectPolygons;
        }
        public List<LineString> GenerateNetPolygons(List<List<Point>> polyPoints)
        {
            polyPoints = RotatePoints(polyPoints);
            List<LineString> objectLineStrings = new List<LineString>();
            foreach (List<Point> points in polyPoints)
            {
                var coordinates = points.Select(point => new Coordinate(point.X + this.referenceOffsetX + this.X, point.Y + this.referenceOffsetY + this.Y)).ToArray();
                objectLineStrings.Add(new LineString(coordinates));
            }

            return objectLineStrings;
        }

        public WorldObjectType WorldObjectType { get; set; }
    }
}