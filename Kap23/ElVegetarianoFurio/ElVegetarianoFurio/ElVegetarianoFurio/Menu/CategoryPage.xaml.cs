using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ElVegetarianoFurio.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public partial class CategoryPage : ContentPage
    {
        private readonly CategoryViewModel _viewModel;

        public CategoryPage()
        {
            InitializeComponent();
            _viewModel = Startup.ServiceProvider.GetService<CategoryViewModel>();
            BindingContext = _viewModel;
        }

        public CategoryPage(string categoryId): this()
        {
            CategoryId = categoryId;
        }

        public string CategoryId
        {
            get;
            set;
        }

        protected override async void OnAppearing()
        {
            if (int.TryParse(CategoryId, out var category))
            {
                _viewModel.CategoryId = category;
            }

            await _viewModel.Initialize();
            base.OnAppearing();
        }
    }
}