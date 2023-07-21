using CRM.Models.General;

namespace CRM.ViewModels
{
    internal class IndexViewModel
    {
        public Month Month { get; set; }

        public IndexViewModel(Month month)
        {
            Month = month;
        }
    }
}
