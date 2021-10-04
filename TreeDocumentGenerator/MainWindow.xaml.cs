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
                if (openFileDialog.FileName != PlaqueImagePath.Text)
                {
                    _plaqueImageDx = 0;
                    _plaqueImageDy = 0;
                    _plaqueImageX = 0;
                    _plaqueImageY = 0;
                    _plaqueZoomRatio = 1;
                }
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
                _plaqueImage = ResizePlaqueImage(plaqueImagePath);
            } else
            {
                _plaqueImage = null;
            }
            _plaqueImageDx = 0;
            _plaqueImageDy = 0;
            _plaqueImageX = 0;
            _plaqueImageY = 0;
            _plaqueZoomRatio = 1;
            MakeImage();
        }
        private Bitmap AddPlaqueImage()
        {
            Bitmap img = new
                Bitmap(_templateImage.Width, _templateImage.Height);
            using (Graphics gr = Graphics.FromImage(img))
            {
                gr.DrawImage(_plaqueImage, new System.Drawing.Point(((_templateImage.Width / 2) - (_plaqueImage.Width - (_plaqueImage.Height / 2)) / 2) + _plaqueImageDx, _plaqueImageDy));
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
        private Bitmap ResizePlaqueImage(string plaqueImagePath)
        {
            Bitmap src = Image.FromFile(plaqueImagePath) as Bitmap;

            //var CropPosition = (src.Width - (src.Height / 2)) / 2;

            //Rectangle cropRect = new Rectangle(CropPosition, 0, (src.Height / 2), src.Height);

            //Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            //using (Graphics g = Graphics.FromImage(target))
            //{
            //    g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
            //                     cropRect,
            //                     GraphicsUnit.Pixel);
            //}
            //return resizeImage(target, new System.Drawing.Size(540 - (_templateImage.Width / 27), 1080 - (_templateImage.Height / 11)));

            var srcRatio = (double) src.Width / src.Height;

            int width = Convert.ToInt32(_templateImage.Height * srcRatio * _plaqueZoomRatio);
            int height = Convert.ToInt32(_templateImage.Height * _plaqueZoomRatio);

            return resizeImage(src, new System.Drawing.Size(width, height));
        }

        private Bitmap DrawTextOnImage(string text, Bitmap templateImage, float textDistanceFromTop, float fontSize, Brush brush, float textDistanceFromLeft = 12, AnjomanFontStyle fontStyle = AnjomanFontStyle.Bold, float boxHeight = 85, float boxWidth = 448)
        {
            using (Graphics g = Graphics.FromImage(templateImage))
            {
                var myFonts = new System.Drawing.Text.PrivateFontCollection();
                myFonts.AddFontFile("Fonts\\Anjoman-Black.ttf");
                myFonts.AddFontFile("Fonts\\Anjoman-Bold.ttf");
                myFonts.AddFontFile("Fonts\\Anjoman-SemiBold.ttf");

                int fontIndex = 1;

                switch (fontStyle)
                {
                    case AnjomanFontStyle.Black:
                        fontIndex = 0;
                        break;
                    case AnjomanFontStyle.Bold:
                        fontIndex = 1;
                        break;
                    case AnjomanFontStyle.SemiBold:
                        fontIndex = 2;
                        break;
                    default:
                        break;
                }

                var oFont = new System.Drawing.Font(
                    myFonts.Families[fontIndex], 
                    fontSize,
                    System.Drawing.FontStyle.Bold);


                g.DrawString(
                    text,
                    oFont,
                    brush, 
                    new RectangleF(
                        new PointF(textDistanceFromLeft, textDistanceFromTop),
                        new SizeF(boxWidth, boxHeight)
                        ),
                    new StringFormat(StringFormatFlags.DirectionRightToLeft)
                    );
            }
            return templateImage;
        }
        private Bitmap DrawNameonThePlaque(string nameOnThePlaque, Bitmap templateImage)
        {
            return DrawTextOnImage(nameOnThePlaque, templateImage, 185, (float)37.5, new SolidBrush(Color.FromArgb(69,0,168)), 12, AnjomanFontStyle.Black, 160, 453);
        }

        private Bitmap DrawCustomerName(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 560, 22, Brushes.Black);
        }

        private Bitmap DrawTreeType(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 670, 22, Brushes.Black);
        }
        private Bitmap DrawLocationString(string locationString, Bitmap templateImage)
        {
            return DrawTextOnImage(locationString, templateImage, 785, 22, Brushes.Black);
        }
        private Bitmap DrawDateString(string dateString, Bitmap templateImage)
        {
            return DrawTextOnImage(dateString, templateImage, 905, 22, Brushes.Black);
        }
        private Bitmap DrawTreeId(string treeType, Bitmap templateImage)
        {
            return DrawTextOnImage(treeType, templateImage, 987, 18, Brushes.Black, 410, AnjomanFontStyle.SemiBold);
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
            CustomerName.Text = "";
            Location.Text = "";
            NameOnThePlaque.Text = "";
            Date.Text = "";
            _plaqueImageDx = 0;
            _plaqueImageDy = 0;
            _plaqueImageX = 0;
            _plaqueImageY = 0;
            _plaqueZoomRatio = 1;
        }

        private System.Windows.Point _mouseLoc;

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseLoc = e.GetPosition(sender as UIElement);
        }

        int _plaqueImageDx = 0;
        int _plaqueImageDy = 0;
        int _plaqueImageX = 0;
        int _plaqueImageY = 0;

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var position = e.GetPosition(sender as UIElement);
                _plaqueImageDx = Convert.ToInt32(position.X - _mouseLoc.X + _plaqueImageX);
                _plaqueImageDy = Convert.ToInt32(position.Y - _mouseLoc.Y + _plaqueImageY);
                MakeImage();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _plaqueImageX = _plaqueImageDx;
            _plaqueImageY = _plaqueImageDy;
        }


        double _plaqueZoomRatio = 1;
        
        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0) 
            {
                _plaqueZoomRatio *= 1.05;
            }

            else if (e.Delta < 0)
            {
                _plaqueZoomRatio *= 0.95;
            }

            var plaqueImagePath = PlaqueImagePath.Text.Trim();
            if (File.Exists(plaqueImagePath))
            {
                _plaqueImage = ResizePlaqueImage(plaqueImagePath);
            }
            MakeImage();
        }
    }

    enum AnjomanFontStyle
    {
        Black,
        Bold,
        SemiBold,
    }
}
