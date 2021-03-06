namespace AutomatedCar.Models
{
    using System.Collections.ObjectModel;
    using System.Drawing;
    using Avalonia.Media;
    using ReactiveUI;

    public class WorldObject : ReactiveObject
    {
        private int x;
        private int y;

        private double rotation;

        private bool highlighted;

        public WorldObject(int x, int y, string filename, int zindex = 1, bool collideable = false, WorldObjectType worldObjectType = WorldObjectType.Other)
        {
            this.highlighted = false;
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = zindex;
            this.Collideable = collideable;
            this.WorldObjectType = worldObjectType;
            this.Id = World.Instance.GetNextId();
        }

        public int ZIndex { get; set; }

        public bool Highlighted
        {
            get => this.highlighted;
            set => this.RaiseAndSetIfChanged(ref this.highlighted, value);
        }

        public double Rotation
        {
            get => this.rotation;
            set => this.RaiseAndSetIfChanged(ref this.rotation, value % 360);
        }

        public int X
        {
            get => this.x;
            set => this.RaiseAndSetIfChanged(ref this.x, value);
        }

        public int Y
        {
            get => this.y;
            set => this.RaiseAndSetIfChanged(ref this.y, value);
        }

        public int Id { get; private set; }

        public Point RotationPoint { get; set; }

        public string RenderTransformOrigin { get; set; }

        public ObservableCollection<PolylineGeometry> Geometries { get; set; } = new ();

        public ObservableCollection<PolylineGeometry> RawGeometries { get; set; } = new ();

        public string Filename { get; set; }

        public bool Collideable { get; set; }

        public WorldObjectType WorldObjectType { get; set; }
    }
}