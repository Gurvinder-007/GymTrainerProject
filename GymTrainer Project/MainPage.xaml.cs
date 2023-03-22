using GymTrainer.ViewModels;

namespace GymTrainer;

public partial class MainPage : ContentPage
{

    public MainPage(MyDaysViewModel viewModel)
    {
        InitializeComponent();

        // Inject view model to the main page
        BindingContext = viewModel;

    }

}


