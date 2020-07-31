using Magic.Commands;
using Magic.Core;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Magic.Models.Controls
{
    public delegate void CostomTabItemCloseHandle(CostomTabItem item );


    [DefaultEvent("IsSelectedChanged")]
    public abstract class CostomTabItem : Freezable// BaseModel
    {

        public CostomTabItem()
        {
            this.IsChanged = false;
            this.CloseCommand = new ActionCommand(event_close);
        }

        #region Title

        public String Title
        {
            get
            {
                return (String)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title",
                                       typeof(String),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(null, TitleChanged));

        public static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region ToolTip

        public String ToolTip
        {
            get
            {
                return (String)GetValue(ToolTipProperty);
            }
            set
            {
                SetValue(ToolTipProperty, value);
            }
        }

        public static readonly DependencyProperty ToolTipProperty =
          DependencyProperty.Register("ToolTip",
                                       typeof(String),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(null, ToolTipChanged));

        public static void ToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region IsChanged

        public Boolean IsChanged
        {
            get
            {
                return (Boolean)GetValue(IsChangedProperty);
            }
            set
            {
                SetValue(IsChangedProperty, value);
            }
        }

        public static readonly DependencyProperty IsChangedProperty =
          DependencyProperty.Register("IsChanged",
                                       typeof(Boolean),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(false, IsChangedChanged));

        public static void IsChangedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion


        #region IsSelected
        [Bindable(true)]
        [Category("Appearance")]
        public Boolean IsSelected
        {
            get
            {
                return (Boolean)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(Boolean),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.Journal, IsSelectedChanged));

        public static void IsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
    
        }
        #endregion

        

        #region Content

        public FrameworkElement Content
        {
            get
            {
                return (FrameworkElement)GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        public static readonly DependencyProperty ContentProperty =
          DependencyProperty.Register("Content",
                                       typeof(FrameworkElement),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(null, ContentChanged));

        public static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CostomTabItem costomTabItem)
            {
                costomTabItem.Content_Changed();
            }
        }
        #endregion

        #region ZoomValue

        public Double ZoomValue
        {
            get
            {
                return (Double)GetValue(ZoomValueProperty);
            }
            set
            {
                SetValue(ZoomValueProperty, value);
            }
        }

        public static readonly DependencyProperty ZoomValueProperty =
          DependencyProperty.Register("ZoomValue",
                                       typeof(Double),
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(1d, ZoomValuePropertyChanged));

        public static void ZoomValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CostomTabItem costomTabItem)
            {
                costomTabItem.ZoomValue_Changed();
            }
        }


        protected virtual void ZoomValue_Changed()
        {

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
                                       typeof(CostomTabItem),
                                       new FrameworkPropertyMetadata(null));


        #endregion




        protected virtual void Content_Changed()
        {

        }


        protected override Freezable CreateInstanceCore()
        {
            Type type = base.GetType();
            return (Freezable)Activator.CreateInstance(type);
        }

        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand CloseCommand { get; protected set; }

        

        protected virtual void event_close(Object param)
        {
            onClose?.Invoke(this);
        }

        /// <summary>
        /// 选项卡关闭事件
        /// </summary>
        public CostomTabItemCloseHandle onClose;
    }
}
