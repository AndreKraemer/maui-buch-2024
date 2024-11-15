﻿using CollectionViewSamples.ViewModels;
using Microsoft.Maui.Controls;


namespace CollectionViewSamples.Views
{

    public partial class GroupSamplePage : ContentPage
    {
        private GroupSampleViewModel _viewModel;

        public GroupSamplePage(GroupSampleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            await  _viewModel.Initialize();
            base.OnNavigatedTo(args);
        }
    }
}