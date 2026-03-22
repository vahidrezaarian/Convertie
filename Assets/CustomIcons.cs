using System.Windows.Media;

namespace Convertie.Assets
{
    internal static class CustomIcons
    {
        public static DrawingImage ScreenHorizontal(Color color)
        {
            var drawingGroup = new DrawingGroup();
            var brush = new SolidColorBrush(color);

            var frameGeometry = Geometry.Parse(
                "M19,4H5C3.9,4 3,4.9 3,6V18C3,19.1 3.9,20 5,20H19C20.1,20 21,19.1 21,18V6C21,4.9 20.1,4 19,4ZM19.5,18.5H4.5V5.5H19.5V18.5Z");
            drawingGroup.Children.Add(new GeometryDrawing(brush, null, frameGeometry));

            var dividerGeometry = Geometry.Parse(
                "M4.5,11.25H19.5V12.75H4.5V11.25Z");
            drawingGroup.Children.Add(new GeometryDrawing(brush, null, dividerGeometry));

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            return drawingImage;
        }

        public static DrawingImage ScreenVertical(Color color)
        {
            var drawingGroup = new DrawingGroup();
            var brush = new SolidColorBrush(color);

            var frameGeometry = Geometry.Parse(
                "M19,4H5C3.9,4 3,4.9 3,6V18C3,19.1 3.9,20 5,20H19C20.1,20 21,19.1 21,18V6C21,4.9 20.1,4 19,4ZM19.5,18.5H4.5V5.5H19.5V18.5Z");
            drawingGroup.Children.Add(new GeometryDrawing(brush, null, frameGeometry));

            var dividerGeometry = Geometry.Parse(
                "M11.25,5.5H12.75V18.5H11.25V5.5Z");
            drawingGroup.Children.Add(new GeometryDrawing(brush, null, dividerGeometry));

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            return drawingImage;
        }

        public static DrawingImage ArrowLeft(Color color)
        {
            var drawingGroup = new DrawingGroup();
            var brush = new SolidColorBrush(color);

            var geometry = Geometry.Parse(
                "M10.28,7.22 a0.75,0.75 0 0 1 0,1.06 L7.09,11.5 H18 a0.75,0.75 0 0 1 0,1.5 H7.09 l3.19,3.22 a0.75,0.75 0 1 1 -1.06,1.06 l-4.5,-4.5 a0.75,0.75 0 0 1 0,-1.06 l4.5,-4.5 a0.75,0.75 0 0 1 1.06,0 Z");

            drawingGroup.Children.Add(new GeometryDrawing(brush, null, geometry));

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();
            return drawingImage;
        }

        public static DrawingImage ArrowUp(Color color)
        {
            var drawingGroup = new DrawingGroup();
            var brush = new SolidColorBrush(color);

            var geometry = Geometry.Parse(
                "M7.22,10.28 a0.75,0.75 0 0 0 1.06,0 L11.5,7.09 V18 a0.75,0.75 0 0 0 1.5,0 V7.09 l3.22,3.19 a0.75,0.75 0 1 0 1.06,-1.06 l-4.5,-4.5 a0.75,0.75 0 0 0 -1.06,0 l-4.5,4.5 a0.75,0.75 0 0 0 0,1.06 Z");

            drawingGroup.Children.Add(new GeometryDrawing(brush, null, geometry));

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();
            return drawingImage;
        }

        public static DrawingImage Copy(Color color)
        {
            var drawingGroup = new DrawingGroup();

            var geometry1 = Geometry.Parse(
                "M16 12.9V17.1C16 20.6 14.6 22 11.1 22H6.9C3.4 22 2 20.6 2 17.1V12.9C2 9.4 3.4 8 6.9 8H11.1C14.6 8 16 9.4 16 12.9Z");
            var drawing1 = new GeometryDrawing(new SolidColorBrush(color), null, geometry1);
            drawingGroup.Children.Add(drawing1);

            var geometry2 = Geometry.Parse(
                "M17.0998 2H12.8998C9.81668 2 8.37074 3.09409 8.06951 5.73901C8.00649 6.29235 8.46476 6.75 9.02167 6.75H11.0998C15.2998 6.75 17.2498 8.7 17.2498 12.9V14.9781C17.2498 15.535 17.7074 15.9933 18.2608 15.9303C20.9057 15.629 21.9998 14.1831 21.9998 11.1V6.9C21.9998 3.4 20.5998 2 17.0998 2Z");
            var drawing2 = new GeometryDrawing(new SolidColorBrush(color), null, geometry2);
            drawingGroup.Children.Add(drawing2);

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            return drawingImage;
        }

        public static DrawingImage Copied(Color color)
        {
            var drawingGroup = new DrawingGroup();

            var geometry1 = Geometry.Parse(
                "M17.0998 2H12.8998C9.81668 2 8.37074 3.09409 8.06951 5.73901C8.00649 6.29235 8.46476 6.75 9.02167 6.75H11.0998C15.2998 6.75 17.2498 8.7 17.2498 12.9V14.9781C17.2498 15.535 17.7074 15.9933 18.2608 15.9303C20.9057 15.629 21.9998 14.1831 21.9998 11.1V6.9C21.9998 3.4 20.5998 2 17.0998 2Z");
            var drawing1 = new GeometryDrawing(new SolidColorBrush(color), null, geometry1);
            drawingGroup.Children.Add(drawing1);

            var geometry2 = Geometry.Parse(
                "M11.1 8H6.9C3.4 8 2 9.4 2 12.9V17.1C2 20.6 3.4 22 6.9 22H11.1C14.6 22 16 20.6 16 17.1V12.9C16 9.4 14.6 8 11.1 8ZM12.29 13.65L8.58 17.36C8.44 17.5 8.26 17.57 8.07 17.57C7.88 17.57 7.7 17.5 7.56 17.36L5.7 15.5C5.42 15.22 5.42 14.77 5.7 14.49C5.98 14.21 6.43 14.21 6.71 14.49L8.06 15.84L11.27 12.63C11.55 12.35 12 12.35 12.28 12.63C12.56 12.91 12.57 13.37 12.29 13.65Z");
            var drawing2 = new GeometryDrawing(new SolidColorBrush(color), null, geometry2);
            drawingGroup.Children.Add(drawing2);

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            return drawingImage;
        }

        public static DrawingImage Close(Color color)
        {
            var drawingGroup = new DrawingGroup();

            var geometry = Geometry.Parse("M18,18 L12,12 M12,12 L6,6 M12,12 L18,6 M12,12 L6,18");

            var pen = new Pen(new SolidColorBrush(color), 2)
            {
                StartLineCap = PenLineCap.Round,
                EndLineCap = PenLineCap.Round,
                LineJoin = PenLineJoin.Round
            };

            var drawing = new GeometryDrawing(null, pen, geometry);
            drawingGroup.Children.Add(drawing);

            var drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            return drawingImage;
        }
    }
}
