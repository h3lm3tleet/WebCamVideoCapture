using System.Windows;
using System.Windows.Media;

namespace WebCamVideoCapture
{
    public partial class MainWindow : Window
    {
        private void WebCameraFlipperButton_Click(object sender, RoutedEventArgs e)
        {
            ImageWebcamFrame.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            if (!isFlipped)
            {
                scaleTransform.ScaleY = -1.0;
                ImageWebcamFrame.RenderTransform = scaleTransform;
                isFlipped = true;
            }
            else
            {
                scaleTransform.ScaleY = 1.0;
                ImageWebcamFrame.RenderTransform = scaleTransform;
                isFlipped = false;
            }
        }
        private void WebCameraMirrorButton_Click(object sender, RoutedEventArgs e)
        {
            ImageWebcamFrame.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            if (!isMirror)
            {
                scaleTransform.ScaleX = -1.0;
                ImageWebcamFrame.RenderTransform = scaleTransform;
                isMirror = true;
            }
            else
            {
                scaleTransform.ScaleX = 1.0;
                ImageWebcamFrame.RenderTransform = scaleTransform;
                isMirror = false;
            }
        }
        private void WebCameraZoomIn_Click(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleY += 0.1;
            scaleTransform.ScaleX += 0.1;
            ImageWebcamFrame.RenderTransform = scaleTransform;
        }
        private void WebCameraZoomOut_Click(object sender, RoutedEventArgs e)
        {
            if (scaleTransform.ScaleY > 1.0 && scaleTransform.ScaleX > 1.0)
            {
                scaleTransform.ScaleY -= 0.1;
                scaleTransform.ScaleX -= 0.1;
                ImageWebcamFrame.RenderTransform = scaleTransform;
            }
        }
        private void ImageWebcamFrame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var image = sender as System.Windows.Controls.Image;
            startPoint = e.GetPosition(image);
            image.CaptureMouse();
            isDragging = true;
        }
        private void ImageWebcamFrame_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                var image = sender as System.Windows.Controls.Image;
                System.Windows.Point newPoint = e.GetPosition(image);
                var matrix = image.RenderTransform.Value;
                matrix.Translate(newPoint.X - startPoint.X, newPoint.Y - startPoint.Y);
                image.RenderTransform = new MatrixTransform(matrix);
                startPoint = newPoint;
            }
        }
        private void ImageWebcamFrame_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var image = sender as System.Windows.Controls.Image;
            image.ReleaseMouseCapture();
            isDragging = false;
        }
        private void WebCameraResetPosition_Click(object sender, RoutedEventArgs e)
        {
            scaleTransform.ScaleX = 1.0;
            scaleTransform.ScaleY = 1.0;
            ImageWebcamFrame.RenderTransform = scaleTransform;
        }
    }
}