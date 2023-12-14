using Customer.GeneratedServices;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;

namespace EasyMicroservices.UI.Customers.ViewModels.Persons
{
    public class FilterPersonsListViewModel : BaseViewModel
    {
        public FilterPersonsListViewModel(PersonClient personClient)
        {
            _personClient = personClient;
            SearchCommand = new TaskRelayCommand(this, Search);
            DeleteCommand = new TaskRelayCommand<PersonContract>(this, Delete);
            SearchCommand.Execute(null);
        }

        public IAsyncCommand SearchCommand { get; set; }
        public IAsyncCommand DeleteCommand { get; set; }

        public Action<PersonContract> OnDelete { get; set; }
        readonly PersonClient _personClient;
        PersonContract _SelectedPersonContract;
        public PersonContract SelectedPersonContract
        {
            get => _SelectedPersonContract;
            set
            {
                _SelectedPersonContract = value;
                OnPropertyChanged(nameof(SelectedPersonContract));
            }
        }

        public int Index { get; set; } = 0;
        public int Length { get; set; } = 10;
        public int TotalCount { get; set; }
        public string SortColumnNames { get; set; }
        public ObservableCollection<PersonContract> Persons { get; set; } = new ObservableCollection<PersonContract>();

        public async Task Search()
        {
            var filteredResult = await _personClient.FilterAsync(new Customer.GeneratedServices.FilterRequestContract()
            {
                IsDeleted = false,
                Index = Index,
                Length = Length,
                SortColumnNames = SortColumnNames
            }).AsCheckedResult(x => (x.Result, x.TotalCount));

            Persons.Clear();
            TotalCount = (int)filteredResult.TotalCount;
            foreach (var person in filteredResult.Result)
            {
                Persons.Add(person);
            }
        }

        public async Task Delete(PersonContract contract)
        {
            await _personClient.SoftDeleteByIdAsync(new Customer.GeneratedServices.Int64SoftDeleteRequestContract()
            {
                Id = contract.Id,
                IsDelete = true
            }).AsCheckedResult(x => x);
            Persons.Remove(contract);
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

