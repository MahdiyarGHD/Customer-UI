using Customer.GeneratedServices;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;

namespace EasyMicroservices.UI.Customers.ViewModels.PersonCategories
{
    public class AddOrUpdatePersonCategoryViewModel : BaseViewModel
    {
        public AddOrUpdatePersonCategoryViewModel(PersonCategoryClient  personCategoryClient)
        {
            _personCategoryClient = personCategoryClient;
            SaveCommand = new TaskRelayCommand(this, Save);
            Clear();
        }

        public TaskRelayCommand SaveCommand { get; set; }

        readonly PersonCategoryClient _personCategoryClient;

        public Action OnSuccess { get; set; }
        PersonCategoryContract _UpdatePersonCategoryContract;
        /// <summary>
        /// for update
        /// </summary>
        public PersonCategoryContract UpdatePersonCategoryContract
        {
            get
            {
                return _UpdatePersonCategoryContract;
            }
            set
            {
                if (value is not null)
                {
                    Name = value.Name;
                }
                _UpdatePersonCategoryContract = value;
            }
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public async Task Save()
        {
            if (UpdatePersonCategoryContract is not null)
                await UpdatePerson();
            else
                await AddPerson();
            OnSuccess?.Invoke();
        }

        public async Task AddPerson()
        {
            await _personCategoryClient.AddAsync(new CreatePersonCategoryRequestContract()
            {
                Names = GetNames()
            }).AsCheckedResult(x => x.Result);
            Clear();
        }

        public override Task OnError(Exception exception)
        {
            return base.OnError(exception);
        }

        public async Task UpdatePerson()
        {
            await _personCategoryClient.UpdateChangedValuesOnlyAsync(new  UpdatePersonCategoryRequestContract()
            {
                Id = UpdatePersonCategoryContract.Id,
                Names = GetNames()
            }).AsCheckedResult(x => x.Result);
            Clear();
        }

        List<LanguageDataContract> GetNames()
        {
            return new List<LanguageDataContract>()
            {
                new LanguageDataContract()
                {
                    Data = Name,
                    Language = "fa-IR"
                }
            };
        }

        public void Clear()
        {
            Name = "";
            UpdatePersonCategoryContract = default;
        }
    }
}
