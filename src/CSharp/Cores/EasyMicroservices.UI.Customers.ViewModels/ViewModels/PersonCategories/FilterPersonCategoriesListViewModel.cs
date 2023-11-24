using Customer.GeneratedServices;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;

namespace EasyMicroservices.UI.Customers.ViewModels.PersonCategories
{
    public class FilterPersonCategoriesListViewModel : BaseViewModel
    {
        public FilterPersonCategoriesListViewModel(PersonCategoryClient personCategoryClient)
        {
            _personCategoryClient = personCategoryClient;
            SearchCommand = new TaskRelayCommand(this, Search);
            DeleteCommand = new TaskRelayCommand<PersonCategoryContract>(this, Delete);
            SearchCommand.Execute(null);
        }

        public ICommandAsync SearchCommand { get; set; }
        public ICommandAsync DeleteCommand { get; set; }

        public Action<PersonCategoryContract> OnDelete { get; set; }
        readonly PersonCategoryClient _personCategoryClient;
        PersonCategoryContract _SelectedPersonCategoryContract;
        public PersonCategoryContract SelectedPersonCategoryContract
        {
            get => _SelectedPersonCategoryContract;
            set
            {
                _SelectedPersonCategoryContract = value;
                OnPropertyChanged(nameof(_SelectedPersonCategoryContract));
            }
        }

        public int Index { get; set; } = 0;
        public int Length { get; set; } = 10;
        public int TotalCount { get; set; }
        public string SortColumnNames { get; set; }
        public ObservableCollection<PersonCategoryContract> PersonCategories { get; set; } = new ObservableCollection<PersonCategoryContract>();

        private async Task Search()
        {
            var filteredResult = await _personCategoryClient.FilterAsync(new FilterRequestContract()
            {
                IsDeleted = false,
                Index = Index,
                Length = Length,
                SortColumnNames = SortColumnNames
            }).AsCheckedResult(x => (x.Result, x.TotalCount));

            PersonCategories.Clear();
            TotalCount = (int)filteredResult.TotalCount;
            foreach (var personCategory in filteredResult.Result)
            {
                PersonCategories.Add(personCategory);
            }
        }

        public async Task Delete(PersonCategoryContract contract)
        {
            await _personCategoryClient.SoftDeleteByIdAsync(new Int64SoftDeleteRequestContract()
            {
                Id = contract.Id,
                IsDelete = true
            }).AsCheckedResult(x => x);
            PersonCategories.Remove(contract);
            OnDelete?.Invoke(contract);
        }

        public override Task OnError(Exception exception)
        {
            return base.OnError(exception);
        }

        public override Task DisplayFetchError(ServiceContracts.ErrorContract errorContract)
        {
            return base.DisplayFetchError(errorContract);
        }
    }
}

