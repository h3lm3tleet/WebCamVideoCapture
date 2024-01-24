using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WebCamVideoCapture
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillWebcams();
            SetFirstWebcam();
            this.ResizeMode = ResizeMode.NoResize;
        }

        private void FillWebcams()
        {
            availableWebcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            for (int i = 0; i < availableWebcams.Count; i++)
                ComboBoxWebcams.Items.Add(availableWebcams[i].Name);
        }

        private void SetFirstWebcam()
        {
            if (ComboBoxWebcams.Items.Count > 0)
            {
                ComboBoxWebcams.SelectedIndex = 0;
            }
        }

        private void ComboBoxWebcams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StopWebcam();
            StartWebcam();
        }

        private void StartWebcam()
        {
            userWebcam = new VideoCaptureDevice(availableWebcams[ComboBoxWebcams.SelectedIndex].MonikerString);
            userWebcam.Start();
            userWebcam.NewFrame += new NewFrameEventHandler(Webcam_NewFrame);
        }


        private void StopWebcam()
        {
            if (userWebcam != null && userWebcam.IsRunning)
            {
                userWebcam.SignalToStop();
                userWebcam.NewFrame -= new NewFrameEventHandler(Webcam_NewFrame);
                userWebcam = null;
            }
        }

        private void Webcam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Dispatcher.Invoke(() =>
            {
                switch (WebCamFilters.FilterSelector())
                {
                    case "isGrayFiltered":
                        Bitmap grayFrame = grayFilter.Apply(eventArgs.Frame);
                        ImageWebcamFrame.Source = BitmapToBitmapImage(grayFrame);
                        break;
                    case "isNegativeFiltered":
                        Bitmap negativeFrame = negativeFilter.Apply(eventArgs.Frame);
                        ImageWebcamFrame.Source = BitmapToBitmapImage(negativeFrame);
                        break;
                    default:
                        ImageWebcamFrame.Source = BitmapToBitmapImage(eventArgs.Frame);
                        break;
                }
            });
        }

        private void WebCameraGrayMode_Click(object sender, RoutedEventArgs e)
        {
            WebCamFilters.isGrayFiltered = !WebCamFilters.isGrayFiltered;
        }

        private void WebCameraNegativeMode_Click(object sender, RoutedEventArgs e)
        {
            WebCamFilters.isNegativeFiltered = !WebCamFilters.isNegativeFiltered;
        }   

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            StopWebcam();
        }
    }
}
