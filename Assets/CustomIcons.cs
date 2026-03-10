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
    }
}
