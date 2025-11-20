using AlchemyGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlchemyGame.ViewModel;

namespace AlchemyGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel _vm;
        private string _firstElement = null;
        private bool _isDragging = false;
        private Point _clickPosition;         
        private Image _draggedImage = null;    




        public MainWindow()
        {
            InitializeComponent();
            _vm = new GameViewModel();
            DataContext = _vm;

        }



        private void ElementsList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ElementsList.SelectedItem is Element element)
                DragDrop.DoDragDrop(ElementsList, element, DragDropEffects.Copy);
        }
        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Element))) return;

            var droppedElement = e.Data.GetData(typeof(Element)) as Element;
            var canvas = sender as Canvas;
            Point dropPosition = e.GetPosition(canvas);

         
            Image elementImage = CreateElementImage(droppedElement);
            Canvas.SetLeft(elementImage, dropPosition.X - 32);
            Canvas.SetTop(elementImage, dropPosition.Y - 32);
            canvas.Children.Add(elementImage);

   
        }

        private bool IsOverlapping(Image img1, Image img2)
        {
            double x1 = Canvas.GetLeft(img1);
            double y1 = Canvas.GetTop(img1);
            double w1 = img1.ActualWidth;
            double h1 = img1.ActualHeight;

            double x2 = Canvas.GetLeft(img2);
            double y2 = Canvas.GetTop(img2);
            double w2 = img2.ActualWidth;
            double h2 = img2.ActualHeight;

            Rect r1 = new Rect(x1, y1, w1, h1);
            Rect r2 = new Rect(x2, y2, w2, h2);

            return r1.IntersectsWith(r2);
        }

        private void ElementImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggedImage = sender as Image;
            _isDragging = true;

            
            _clickPosition = e.GetPosition(_draggedImage);

           
            _draggedImage.CaptureMouse();
        }

        private void ElementImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedImage != null)
            {
                var canvas = _draggedImage.Parent as Canvas;
                if (canvas == null) return;

               
                Point mousePos = e.GetPosition(canvas);

                Canvas.SetLeft(_draggedImage, mousePos.X - _clickPosition.X);
                Canvas.SetTop(_draggedImage, mousePos.Y - _clickPosition.Y);
            }
        }

     
        private void ElementImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_draggedImage == null) return;

            _draggedImage.ReleaseMouseCapture();
            _isDragging = false;

            var draggedElement = _draggedImage.Tag as Element;
            if (draggedElement == null)
            {
                _draggedImage = null;
                return;
            }

            var canvas = _draggedImage.Parent as Canvas;
            if (canvas == null)
            {
                _draggedImage = null;
                return;
            }

            bool combined = false;

           
            foreach (Image otherImage in canvas.Children.OfType<Image>().ToList()) 
            {
                if (otherImage == _draggedImage) continue;

                var otherElement = otherImage.Tag as Element;
                if (otherElement == null) continue;

               
                if (IsOverlapping(_draggedImage, otherImage))
                {
                    string result = _vm.TryCombine(draggedElement.Name, otherElement.Name);

                    if (result != null) 
                    {
                        
                        canvas.Children.Remove(_draggedImage);
                        canvas.Children.Remove(otherImage);

                    
                        Element newElement = _vm.Elements.First(el => el.Name == result);

                        Image newImage = CreateElementImage(newElement);

                      
                        double centerX = (Canvas.GetLeft(_draggedImage) + Canvas.GetLeft(otherImage) + 64) / 2 - 32;
                        double centerY = (Canvas.GetTop(_draggedImage) + Canvas.GetTop(otherImage) + 64) / 2 - 32;

                        Canvas.SetLeft(newImage, centerX);
                        Canvas.SetTop(newImage, centerY);
                        canvas.Children.Add(newImage);

                       
                        var dialog = new NewElementWindow(newElement.Name, newElement.IconPath);
                        dialog.Owner = this;
                        dialog.Show();

                        combined = true;
                        break; 
                    }
                }
            }

           

            _draggedImage = null;
        }

        private Image CreateElementImage(Element element)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(element.IconPath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            var image = new Image
            {
                Width = 64,
                Height = 64,
                Source = bitmap,
                Tag = element
            };

            image.MouseLeftButtonDown += ElementImage_MouseLeftButtonDown;
            image.MouseMove += ElementImage_MouseMove;
            image.MouseLeftButtonUp += ElementImage_MouseLeftButtonUp;

            return image;
        }
        private void ClearCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            
            var hint = GameCanvas.Children.OfType<TextBlock>().FirstOrDefault();
            GameCanvas.Children.Clear();
            if (hint != null)
                GameCanvas.Children.Add(hint);

            _firstElement = null; 
        }



    }
}
