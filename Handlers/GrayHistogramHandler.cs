using AForge.Imaging.Filters;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;

namespace WebCamVideoCapture
{
    public partial class MainWindow : Window
    {
        private void WebCameraGrayHistogram_Click(object sender, RoutedEventArgs e)
        {
            string pathImageFolder = projectFolderPath + @"\WebCamsScreens";

            if (!Directory.Exists(pathImageFolder))
                Directory.CreateDirectory(pathImageFolder);

            string nameImage = string.Format("{0}.png", pathImageFolder);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageWebcamFrame.Source));
            using (FileStream fileStream = new FileStream(nameImage, FileMode.Create, FileAccess.ReadWrite))
            {
                encoder.Save(fileStream);
            }

            Thread.Sleep(100);

            Bitmap bitmap = new Bitmap(pathImageFolder + @".png");

            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = filter.Apply(bitmap);

            int[] histogram = new int[256];
            for (int i = 0; i < grayImage.Width; i++)
            {
                for (int j = 0; j < grayImage.Height; j++)
                {
                    System.Drawing.Color pixel = grayImage.GetPixel(i, j);
                    histogram[pixel.R]++;
                }
            }

            var plotModel = new PlotModel { Title = "Гистограмма по уровню серого" };
            var columnSeries = new OxyPlot.Series.ColumnSeries();
            for (int i = 0; i < histogram.Length; i++)
            {
                columnSeries.Items.Add(new ColumnItem { Value = histogram[i] });
            }
            plotModel.Series.Add(columnSeries);

            var window = new System.Windows.Window
            {
                Title = "Histogram Window",
                Content = new PlotView { Model = plotModel },
                Width = 800,
                Height = 600
            };
            window.Show();
        }
    }
}
