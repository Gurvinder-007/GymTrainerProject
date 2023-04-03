using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymTrainer.Models;

namespace GymTrainer.ViewModels
{

    // Assign the Day object to the viewmodel
    [QueryProperty(nameof(Day), "Day")]
    public partial class MyDayDetailsViewModel : BaseViewModel
    {
        public MyDayDetailsViewModel()
        {

        }

        [ObservableProperty]
        Day day;
    }
}
