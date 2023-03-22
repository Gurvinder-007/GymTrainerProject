namespace GymTrainer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register the detail page with the route of "DetailsPage" to the shell routing system
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}
