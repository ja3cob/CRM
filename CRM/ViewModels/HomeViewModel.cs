using CRM.Models.General;

namespace CRM.ViewModels
{
    internal class HomeViewModel
    {
        public Month Month { get; set; }

        public HomeViewModel(Month month)
        {
            Month = month;
        }
    }
}
