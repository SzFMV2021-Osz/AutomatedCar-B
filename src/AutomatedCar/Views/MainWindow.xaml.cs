namespace AutomatedCar.Views
{
    using AutomatedCar.Models;
    using Avalonia.Controls;
    using Avalonia.Input;
    using Avalonia.Markup.Xaml;
    using Avalonia.Threading;
    using System;

    public class MainWindow : Window
    {
        private readonly DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };

        public MainWindow()
        {
            this.InitializeComponent();
            this.timer.Tick += delegate { this.FocusCar(); };
            this.timer.IsEnabled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Keyboard.Keys.Add(e.Key);
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Keyboard.Keys.Remove(e.Key);
            base.OnKeyUp(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void FocusCar()
        {
            var scrollViewer = this.Get<CourseDisplayView>("courseDisplay").Get<ScrollViewer>("scrollViewer");
            var offsetX = World.Instance.ControlledCar.X - (scrollViewer.Viewport.Width / 2);
            var offsetY = World.Instance.ControlledCar.Y - (scrollViewer.Viewport.Height / 2);
            scrollViewer.Offset = new Avalonia.Vector(offsetX, offsetY);
        }
    }
}