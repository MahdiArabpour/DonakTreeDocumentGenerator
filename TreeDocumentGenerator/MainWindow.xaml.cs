using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace TreeDocumentGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void PlaqueImageBrows_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                PlaqueImagePath.Text = openFileDialog.FileName;
            }
        }

        private void PlaqueImagePath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var plaqueImagePath = PlaqueImagePath.Text.Trim();
            if (File.Exists(plaqueImagePath))
            {
                AddPlaqueImage(plaqueImagePath);
            }
        }

        private void AddPlaqueImage(string plaqueImagePath)
        {
            var cropedPlaqueImage = CropPlaqueImage(plaqueImagePath);

            Bitmap templateImage = resizeImage(Image.FromFile("Images/TreeDocument.png") as Bitmap, new System.Drawing.Size(1080, 1080));


            Bitmap img = new
                Bitmap(templateImage.Width, templateImage.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(cropedPlaqueImage, new System.Drawing.Point((templateImage.Width / 2), 0));
                gr.DrawImage(templateImage, new System.Drawing.Point(0, 0));
            }
            OutputImage.Source = BitmapToImageSource(img);
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private Bitmap resizeImage(Bitmap imgToResize, System.Drawing.Size size)
        {
            return (new Bitmap(imgToResize, size));
        }

        private Bitmap CropPlaqueImage(string plaqueImagePath)
        {
            Bitmap src = Image.FromFile(plaqueImagePath) as Bitmap;

            Rectangle cropRect = new Rectangle((src.Height / 8), 0, (src.Height / 2), src.Height);

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            return resizeImage(target, new System.Drawing.Size(540, 1080));
        }

        private void UpdateImage()
        {
            OutputImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\output.png", UriKind.Absolute));
        }
    }
}
