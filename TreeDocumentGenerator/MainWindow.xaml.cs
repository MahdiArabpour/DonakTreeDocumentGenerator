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

        private Bitmap _templateImage;
        private Bitmap _plaqueImage;
        private Bitmap _documentImage;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _templateImage = GetTemplateImage();
        }
        private Bitmap GetTemplateImage()
        {
            return resizeImage(Image.FromFile("Images/TreeDocument.png") as Bitmap, new System.Drawing.Size(1080, 1080));
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
        /// <summary>
        /// takes all of the current strings and plaque image and puts them into template image
        /// </summary>
        private void MakeImage()
        {
            Bitmap documentImage;

            if (_plaqueImage != null)
            {
                documentImage = AddPlaqueImage();
            } else
            {
                documentImage = GetTemplateImage();
            }

            var nameOnThePlaque = NameOnThePlaque.Text.Trim();

            if (!nameOnThePlaque.Equals(""))
            {
                DrawNameonThePlaque(nameOnThePlaque, documentImage);
            }

            var customerName = CustomerName.Text.Trim();

            if (!customerName.Equals(""))
            {
                DrawCustomerName(customerName, documentImage);
            }

            var treeType = TreeType.Text.Trim();

            if (!treeType.Equals(""))
            {
                DrawTreeType(treeType, documentImage);
            }

            var treeId = TreeId.Text.Trim();

            if (!treeId.Equals(""))
            {
                DrawTreeId(treeId, documentImage);
            }

            var location = Location.Text.Trim();

            if (!location.Equals(""))
            {
                DrawLocationString(location, documentImage);
            }

            var date = Date.Text.Trim();

            if (!date.Equals(""))
            {
                DrawDateString(date, documentImage);
            }

            OutputImage.Source = BitmapToImageSource(documentImage);

            _documentImage = documentImage;
        }
        private void PlaqueImagePath_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var plaqueImagePath = PlaqueImagePath.Text.Trim();
            if (File.Exists(plaqueImagePath))
            {
                _plaqueImage = CropPlaqueImage(plaqueImagePath);
            } else
            {
                _plaqueImage = null;
            }
            MakeImage();
        }
        private Bitmap AddPlaqueImage()
        {
            Bitmap img = new
                Bitmap(_templateImage.Width, _templateImage.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(_plaqueImage, new System.Drawing.Point((_templateImage.Width / 2), 0));
                gr.DrawImage(_templateImage, new System.Drawing.Point(0, 0));
            }
            return img;
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
        private Bitmap DrawTextOnImage(string text, Bitmap templateImage, float textDistanceFromTop, float fontSize, Brush brush, float textDistanceFromLeft = 15, System.Drawing.FontStyle fontStyle = System.Drawing.FontStyle.Bold)
        {
            using (Graphics g = Graphics.FromImage(templateImage))
            {
                g.DrawString(
                    text,
                    new Font(
                        "Fonts/Lalezar.ttf",
                        fontSize,
                        fontStyle),
                    brush, 
                    new RectangleF(
                        new PointF(textDistanceFromLeft, textDistanceFromTop),
                        new SizeF(445, 85)
                        ),
                    new StringFormat(StringFormatFlags.DirectionRightToLeft)
                    );
            }
            return templateImage;
        }
        private Bitmap DrawNameonThePlaque(string nameOnThePlaque, Bitmap templateImage)
        {
            return DrawTextOnImage(nameOnThePlaque, templateImage, 200, 50, Brushes.Purple);
        }

        private Bitmap DrawCustomerName(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 560, 30, Brushes.Black);
        }

        private Bitmap DrawTreeType(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 670, 30, Brushes.Black);
        }
        private Bitmap DrawLocationString(string locationString, Bitmap templateImage)
        {
            return DrawTextOnImage(locationString, templateImage, 785, 30, Brushes.Black);
        }
        private Bitmap DrawDateString(string dateString, Bitmap templateImage)
        {
            return DrawTextOnImage(dateString, templateImage, 905, 30, Brushes.Black);
        }
        private Bitmap DrawTreeId(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 987, 24, Brushes.Black, 410);
        }


        private void AnyTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MakeImage();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
            saveFileDialog.FileName = "donak_tree_document.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                _documentImage.Save(saveFileDialog.FileName);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            PlaqueImagePath.Text = "";
            TreeType.Text = "";
            TreeId.Text = "";
            Location.Text = "";
            NameOnThePlaque.Text = "";
            Date.Text = "";
        }
    }
}
