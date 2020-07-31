using Magic.Adorners;
using Magic.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Magic.Controls
{
    public class DesignerCanvas : Canvas
    {
        public DesignerCanvas()
        {
            this.LayoutUpdated += DesignerCanvas_LayoutUpdated;
            backgroundBrush = (Brush)this.FindResource("DESIGNER_AREA_BACKGROUND");
            this.Loaded += DesignerCanvas_Loaded;
            this.AllowDrop = true;
            this.Focusable = true;
        }

        #region Work Area Background Render
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawRectangle(backgroundBrush, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
        }
        #endregion

        #region  Mouse Events

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Handled)
            {
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.SelectedItem = null;
                selectionStartPoint = e.GetPosition(this);
                //e.Handled = true;
            }
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                DropMovePoint = e.GetPosition(this);
                //e.Handled = true;
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DropMovePoint.HasValue)
            {
                var realpos = e.GetPosition(this);
                var v = DropMovePoint.Value - realpos;
                var scroll = this.Parent as ScrollViewer;
                this.CaptureMouse();
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset + v.Y);
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset + v.X);
                DropMovePoint = realpos;
                e.Handled = true;
            }
            if (selectionStartPoint.HasValue)
            {
                // create rubberband adorner
                var selectionadorner = new RubberbandAdorner(this);
                selectionadorner.OnAttach += Selectionadorner_OnAttach;
                selectionadorner.OnUpdateSelection += Selectionadorner_OnUpdateSelection;
                selectionadorner.OnDetach += Selectionadorner_OnDestroy;
                selectionadorner.Attach(selectionStartPoint.Value);
                e.Handled = true;
            }
        }



        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.LeftButton == MouseButtonState.Released)
            {
                this.ReleaseMouseCapture();
                selectionStartPoint = null;
                e.Handled = true;
            }
            if (e.MiddleButton == MouseButtonState.Released)
            {
                DropMovePoint = null;
                e.Handled = true;
            }
        }

        #endregion

        #region Canvas Events

        private void DesignerCanvas_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void DesignerCanvas_LayoutUpdated(object sender, EventArgs e)
        {
            //  ((ScrollViewer)this.Parent).UpdateLayout();
            //  this.InvalidateMeasure();
            //  this.InvalidateVisual();
        }

        #endregion

        #region Rubberband Area Selection

        private void Selectionadorner_OnAttach(RubberbandAdorner sender, SelectionEventArgs e)
        {

        }


        private void Selectionadorner_OnUpdateSelection(RubberbandAdorner sender, SelectionEventArgs e)
        {
            Console.WriteLine(e.Range);
        }

        private void Selectionadorner_OnDestroy(RubberbandAdorner sender, SelectionEventArgs e)
        {
            sender.OnUpdateSelection -= Selectionadorner_OnUpdateSelection;
            sender.OnDetach -= Selectionadorner_OnDestroy;
            selectionStartPoint = null;
        }






        #endregion


        #region SelectedProperty

        public Object SelectedProperty
        {
            get
            {
                return (Object)GetValue(SelectedPropertyProperty);
            }
            set
            {
                SetValue(SelectedPropertyProperty, value);
            }
        }

        public static readonly DependencyProperty SelectedPropertyProperty =
          DependencyProperty.Register("SelectedProperty",
                                       typeof(Object),
                                       typeof(DesignerCanvas),
                                       new FrameworkPropertyMetadata(null));
        #endregion

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            Console.WriteLine(dragObject);
            if (dragObject != null)
            {
                var curposition = e.GetPosition(this).Round(2);


            }
            e.Handled = true;

        }


        #region SelectedItem

        public DesignerItem SelectedItem
        {
            get
            {
                return (DesignerItem)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
                UpdateSelectedItem();
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem",
                                       typeof(DesignerItem),
                                       typeof(DesignerCanvas),
                                       new FrameworkPropertyMetadata(null, SelectedItemPropertyChangedCallback));

        public static void SelectedItemPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;
            var canvas = d as DesignerCanvas;
            if (canvas != null)
            {
                canvas.UpdateSelectedItem();
            }
        }

        private void UpdateSelectedItem()
        {
            var childs = this.FindChilds<DesignerItem>();
            foreach (var item in childs)
            {
                item.IsFocus = false;
            }
            if (SelectedItem == null)
            {
                return;
            }
            var par = SelectedItem.FindParents<DesignerItem, DesignerCanvas>();
            foreach (var item in par)
            {
                item.IsFocus = true;
            }
           // SelectedProperty = this.SelectedItem;
        }



        #endregion



        #region Private Peoperty

        private Point? selectionStartPoint { get; set; }

        private Point? DropMovePoint { get; set; }

        private Brush backgroundBrush { get; set; }

        #endregion


    }
}
