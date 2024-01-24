using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using System.Windows.Media;
using System.Windows;

namespace WebCamVideoCapture
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection availableWebcams;
        private VideoCaptureDevice userWebcam;
        private static ScaleTransform scaleTransform = new ScaleTransform(1, 1);
        private static Grayscale grayFilter = new Grayscale(0.2125, 0.7154, 0.0721);
        private static Invert negativeFilter = new Invert();
        string projectFolderPath = AppDomain.CurrentDomain.BaseDirectory;

        private bool isMirror, isFlipped, isDragging = false;
        private Point startPoint;
    }
}
