namespace GymTrainer.Views;
using ViewModels;
public partial class DetailsPage : ContentPage
{
	public DetailsPage(MyDayDetailsViewModel viewModel)
	{
		InitializeComponent();

        // Inject view model to the details page
        BindingContext = viewModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}