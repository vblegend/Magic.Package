using Magic.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Magic.Models.Controls
{

    public delegate void CustomTreeViewItemExpandedChanged(CustomTreeViewItem sender, Boolean isExpanded);


    public class CustomTreeViewItem : BaseModel
    {
        public CustomTreeViewItem()
        {
            Children = new ObservableCollection<CustomTreeViewItem>();
        }

        public String Header
        {
            get
            {
                return base.GetValue<String>();
            }
            set
            {
                if (value != Header)
                {
                    base.SetValue(value);
                }
            }
        }

        public ObservableCollection<CustomTreeViewItem> Children
        {
            get
            {
                return base.GetValue<ObservableCollection<CustomTreeViewItem>>();
            }
            set
            {
                if (value != Children)
                {
                    base.SetValue(value);
                }
            }
        }

        public bool HasItems
        {
            get
            {
                return Children != null && Children.Count > 0;
            }
        }

        public Boolean IsSelected
        {
            get
            {
                return base.GetValue<Boolean>();
            }
            set
            {
                if (value != IsSelected)
                {
                    base.SetValue(value);
                }
            }
        }


        public Boolean IsExpanded
        {
            get
            {
                return base.GetValue<Boolean>();
            }
            set
            {
                if (value != IsExpanded)
                {
                    base.SetValue(value);
                    ExpandedChanged?.Invoke(this,value);
                }
            }
        }


        public Object Icon
        {
            get
            {
                return base.GetValue<Object>();
            }
            set
            {
                base.SetValue(value);
            }
        }


        public void loadPath()
        {
            Path p = new Path();
            p.Stroke = new SolidColorBrush(Colors.White);
            p.Fill = null;
            p.SnapsToDevicePixels = true;
            p.StrokeThickness = 1;
            p.Data = Geometry.Parse("M2,2 L9,2 9,5 12,5 12,14 2,14 2,2 M9,2 L12,5 M5,4 L7,4 M5,6 L7,6 M5,8 L9,8 M5,10 L9,10 M5,12 L9,12");
            this.Icon = p;
        }




        public void loadIcon(String imagePath, UriKind uriKind = UriKind.Relative)
        {
            // Create source   
            BitmapImage myBitmapImage = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block   
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(imagePath, uriKind);
            // To save significant application memory, set the DecodePixelWidth or     
            // DecodePixelHeight of the BitmapImage value of the image source to the desired    
            // height or width of the rendered image. If you don't do this, the application will    
            // cache the image as though it were rendered as its normal size rather then just    
            // the size that is displayed.   
            // Note: In order to preserve aspect ratio, set DecodePixelWidth   
            // or DecodePixelHeight but not both.   
            //myBitmapImage.DecodePixelWidth = 2048;
            myBitmapImage.EndInit();
            //set image source   
            Image img = new Image();
            img.Source = myBitmapImage;
            this.Icon = img;
        }




        public event CustomTreeViewItemExpandedChanged ExpandedChanged;
    }
}
