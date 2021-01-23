﻿using System;
using GHelper.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Image = Microsoft.UI.Xaml.Controls.Image;

namespace GHelper.View 
{
	public partial class ApplicationSelectorView : StackPanel, SelectableItem
	{
		public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register(
			nameof (ApplicationSelectorView.Application),
			typeof (ApplicationViewModel),
			typeof (ApplicationSelectorView),
			new PropertyMetadata(null)
		);
		
		public ApplicationViewModel Application
		{
			get { return (ApplicationViewModel) GetValue(ApplicationProperty); }
			set { SetValue(ApplicationProperty, value); }
		}

		public Image Poster
		{
			get { return Application.Poster; }
		}


		public event EventHandler? Selected;

		public ApplicationSelectorView()
		{
			this.InitializeComponent();
		}


		private Visibility posterImageVisibility
		{
			get 
			{
				if (this.Poster == ApplicationViewModel.DefaultPosterImage)
				{
					return Visibility.Collapsed;
				}
				else
				{
					return Visibility.Visible;
				}
			}
		}
		
		public void NotifySelected(object? sender, EventArgs eventInfo)
		{
			this.Selected?.Invoke(sender, eventInfo);
		}
	}
}