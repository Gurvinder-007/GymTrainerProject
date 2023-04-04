using System.Windows.Input;
using GymTrainer.Models;
using GymTrainer.Services;
using static Android.Telephony.CarrierConfigManager;

namespace GymTrainer.ViewModels
{
    public partial class MyDaysViewModel : BaseViewModel
    {
        MyDayService myDayService;

        public ICommand TimerCommand { get; }

        // ObservableCollection is used since it has built-in support to raise CollectionChanged
        // OnPropertyChanged does not need to be called
        public ObservableCollection<Day> MyDays { get; } = new();

        // Inject MyDayService through the constructor
        public MyDaysViewModel(MyDayService myDayService)
        {
            Title = "Workout Scheduler";
            this.myDayService = myDayService;

            TimerCommand = new Command(async () =>
            {
                // show a timer dialog to the user
                var duration = await Shell.Current.DisplayPromptAsync("Gym Timer", "Enter exercise duration (in seconds):");

                if (!string.IsNullOrEmpty(duration) && int.TryParse(duration, out int seconds))
                {
                    // start the timer
                    var remaining = seconds;

                    while (remaining > 0)
                    {
                        // update the timer dialog with the remaining time
                        var result = await Shell.Current.DisplayAlert("Gym Timer", $"Time remaining: {remaining} seconds", "Pause", "Cancel");

                        if (result)
                        {
                            // user clicked "Pause"
                            result = await Shell.Current.DisplayAlert("Gym Timer", "Timer paused. Continue?", "Resume", "Cancel");

                            if (!result)
                            {
                                // user clicked "Cancel"
                                break;
                            }
                        }

                        // decrement the remaining time by 1 second
                        remaining--;
                    }

                    // timer finished
                    await Shell.Current.DisplayAlert("Gym Timer", "Exercise completed!", "OK");
                }
            });


        }

        // Generates GoToDetailsCommand automatically
        [RelayCommand]
        async Task GoToDetailsAsync(Day day)
        {
            if (day is null)
                return;

            // Use the built-in Shell Navigation API
            // When item is selected, push a new DetailsPage with the day as the parameter
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", 
                true,
                new Dictionary<string, object>
                {
                    // Query identifier
                    {nameof(Day), day}
                });
        }

        // RelayCommand enables us to bind the command and data between the viewmodel and view
        // Generates GetMyDaysCommand automatically
        [RelayCommand]
        async Task GetMyDaysAsync()
        {
            if (IsBusy)
                return;

            // Toggle IsBusy to true when making calls to the server
            // False when finished with the call to the server
            try
            {
                IsBusy = true;

                var days = await myDayService.GetDays();

                if(MyDays.Count != 0)
                    MyDays.Clear();

                foreach(var day in days)
                    MyDays.Add(day);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to get days.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

