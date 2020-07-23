using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace Magic.Actions
{
    public class PlaySoundAction : TriggerAction<DependencyObject>
	{
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Uri), typeof(PlaySoundAction), null);

		public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register("Volume", typeof(double), typeof(PlaySoundAction), new PropertyMetadata(0.5));

		public Uri Source
		{
			get
			{
				return (Uri)base.GetValue(PlaySoundAction.SourceProperty);
			}
			set
			{
				base.SetValue(PlaySoundAction.SourceProperty, value);
			}
		}

		public double Volume
		{
			get
			{
				return (double)base.GetValue(PlaySoundAction.VolumeProperty);
			}
			set
			{
				base.SetValue(PlaySoundAction.VolumeProperty, value);
			}
		}

		protected virtual void SetMediaElementProperties(MediaElement mediaElement)
		{
			if (mediaElement != null)
			{
				mediaElement.Source = this.Source;
				mediaElement.Volume = this.Volume;
			}
		}

		protected override void Invoke(object parameter)
		{
			if (this.Source == null || base.AssociatedObject == null)
			{
				return;
			}
			Popup popup = new Popup();
			MediaElement mediaElement = new MediaElement();
			popup.Child = mediaElement;
			mediaElement.Visibility = Visibility.Collapsed;
			this.SetMediaElementProperties(mediaElement);
			mediaElement.MediaEnded += delegate (object param0, RoutedEventArgs param1)
			{
				popup.Child = null;
				popup.IsOpen = false;
			};
			mediaElement.MediaFailed += delegate (object param0, ExceptionRoutedEventArgs param1)
			{
				popup.Child = null;
				popup.IsOpen = false;
			};
			popup.IsOpen = true;
		}
	}

}
