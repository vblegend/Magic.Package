using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Magic.Controls
{
    public class Button
    {
        public Button()
        {

        }


        #region Button Path Data Geometry

        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.RegisterAttached("Geometry", typeof(Geometry), typeof(Button),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


        public static Geometry GetGeometry(DependencyObject dpo)
        {
            return (Geometry)dpo.GetValue(GeometryProperty);
        }



        public static void SetGeometry(DependencyObject dpo, Geometry value)
        {
            dpo.SetValue(GeometryProperty, value);
        }
        #endregion

        #region  GeometryWidth

        public static readonly DependencyProperty GeometryWidthProperty =
            DependencyProperty.RegisterAttached("GeometryWidth", typeof(Double), typeof(Button),
            new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsRender));


        public static Double GetGeometryWidth(DependencyObject dpo)
        {
            return (Double)dpo.GetValue(GeometryWidthProperty);
        }



        public static void SetGeometryWidth(DependencyObject dpo, Double value)
        {
            dpo.SetValue(GeometryWidthProperty, value);
        }

        #endregion

        #region  GeometryHeight

        public static readonly DependencyProperty GeometryHeightProperty =
            DependencyProperty.RegisterAttached("GeometryHeight", typeof(Double), typeof(Button),
            new FrameworkPropertyMetadata(16d, FrameworkPropertyMetadataOptions.AffectsRender));


        public static Double GetGeometryHeight(DependencyObject dpo)
        {
            return (Double)dpo.GetValue(GeometryHeightProperty);
        }



        public static void SetGeometryHeight(DependencyObject dpo, Double value)
        {
            dpo.SetValue(GeometryHeightProperty, value);
        }









        #endregion

        #region  StrokeThickness

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.RegisterAttached("StrokeThickness", typeof(Double), typeof(Button),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));


        public static Double GetStrokeThickness(DependencyObject dpo)
        {
            return (Double)dpo.GetValue(StrokeThicknessProperty);
        }



        public static void SetStrokeThickness(DependencyObject dpo, Double value)
        {
            dpo.SetValue(StrokeThicknessProperty, value);
        }









        #endregion

    }
}
