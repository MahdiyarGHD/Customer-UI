using Customer.GeneratedServices;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Places.ViewModels.Cities;
using EasyMicroservices.UI.Places.ViewModels.Countries;
using EasyMicroservices.UI.Places.ViewModels.Provinces;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace EasyMicroservices.UI.Customers.ViewModels.Persons
{
    public class AddOrUpdatePersonViewModel : BaseViewModel
    {
        public AddOrUpdatePersonViewModel(PersonClient personClient, PersonCategoryClient personCategoryClient, FilterCountriesListViewModel countriesListViewModel, FilterProvincesListViewModel provincesListViewModel, FilterCitiesListViewModel citiesListViewModel)
        {
            _personClient = personClient;
            _personCategoryClient = personCategoryClient;
            CountriesViewModel = countriesListViewModel;
            ProvincesListViewModel = provincesListViewModel;
            CitiesListViewModel = citiesListViewModel;

            SaveCommand = new TaskRelayCommand(this, Save);
            Clear();
        }

        public TaskRelayCommand SaveCommand { get; set; }

        readonly PersonClient _personClient;
        readonly PersonCategoryClient _personCategoryClient;
        public FilterCountriesListViewModel CountriesViewModel { get; set; }
        public FilterProvincesListViewModel ProvincesListViewModel { get; set; }
        public FilterCitiesListViewModel CitiesListViewModel { get; set; }

        public Action OnSuccess { get; set; }
        PersonContract _UpdatePersonContract;
        /// <summary>
        /// for update
        /// </summary>
        public PersonContract UpdatePersonContract
        {
            get
            {
                return _UpdatePersonContract;
            }
            set
            {
                if (value is not null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    Description = value.Description;
                    PersonType = value.Type;
                    ExternalServiceIdentifier = value.ExternalServiceIdentifier;
                    Address = value.Addresses?.Select(x => x.Address).FirstOrDefault();
                    EmailAddress = value.Emails?.Select(x => x.Address).FirstOrDefault();
                    WebsiteAddress = value.Links?.Select(x => x.Address).FirstOrDefault();
                    VisaNumber = value.Visas?.Select(x => x.Number).FirstOrDefault();
                    PassportNumber = value.Passports?.Select(x => x.Number).FirstOrDefault();
                    PostalCode = value.Addresses?.Select(x => x.PostalCode).FirstOrDefault();
                    NationalCode = value.NationalCode;
                    PhoneNumber = value.Phones?.Where(x => x.NumberType == PhoneNumberType.Home).Select(x => x.Number).FirstOrDefault();
                    MobileNumber = value.Phones?.Where(x => x.NumberType == PhoneNumberType.Mobile).Select(x => x.Number).FirstOrDefault();
                    SelectedCityId = value.CityId.GetValueOrDefault();
                    SelectedProvinceId = value.ProvinceId.GetValueOrDefault();
                    SelectedCountryId = value.CountryId.GetValueOrDefault();
                }
                _UpdatePersonContract = value;
            }
        }

        string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        string _LastName;
        public string LastName
        {
            get => _LastName;
            set
            {
                _LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        PersonType _PersonType = PersonType.RealPerson;
        public PersonType PersonType
        {
            get => _PersonType;
            set
            {
                _PersonType = value;
                OnPropertyChanged(nameof(PersonType));
            }
        }

        string _PassportNumber;
        public string PassportNumber
        {
            get => _PassportNumber;
            set
            {
                _PassportNumber = value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }

        string _PostalCode;
        public string PostalCode
        {
            get => _PostalCode;
            set
            {
                _PostalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        string _ExternalServiceIdentifier;
        public string ExternalServiceIdentifier
        {
            get => _ExternalServiceIdentifier;
            set
            {
                _ExternalServiceIdentifier = value;
                OnPropertyChanged(nameof(ExternalServiceIdentifier));
            }
        }

        string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        string _EmailAddress;
        public string EmailAddress
        {
            get => _EmailAddress;
            set
            {
                _EmailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        string _WebsiteAddress;
        public string WebsiteAddress
        {
            get => _WebsiteAddress;
            set
            {
                _WebsiteAddress = value;
                OnPropertyChanged(nameof(WebsiteAddress));
            }
        }

        string _VisaNumber;
        public string VisaNumber
        {
            get => _VisaNumber;
            set
            {
                _VisaNumber = value;
                OnPropertyChanged(nameof(VisaNumber));
            }
        }

        string _PhoneNumber;
        public string PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        string _MobileNumber;
        public string MobileNumber
        {
            get => _MobileNumber;
            set
            {
                _MobileNumber = value;
                OnPropertyChanged(nameof(MobileNumber));
            }
        }

        string _NationalCode;
        public string NationalCode
        {
            get => _NationalCode;
            set
            {
                _NationalCode = value;
                OnPropertyChanged(nameof(NationalCode));
            }
        }

        public long SelectedPersonCategoryId { get; set; }

        public ObservableCollection<PersonCategoryContract> PersonCategories { get; set; } = new ObservableCollection<PersonCategoryContract>();
        private long _selectedProvinceId;
        public long SelectedProvinceId
        {
            get => _selectedProvinceId;
            set
            {
                _selectedProvinceId = value;
                OnPropertyChanged(nameof(SelectedProvinceId));
                _ = SearchCities();
            }
        }

        public long SelectedCityId { get; set; }
        long _SelectedCountryId;
        public long SelectedCountryId
        {
            get => _SelectedCountryId;
            set
            {
                _SelectedCountryId = value;
                OnPropertyChanged(nameof(SelectedCountryId));
                _ = SearchProvince();
            }
        }


        public async Task Save()
        {
            if (UpdatePersonContract is not null)
                await UpdatePerson();
            else
                await AddPerson();
            OnSuccess?.Invoke();
        }

        public async Task AddPerson()
        {

            await _personClient.AddAsync(new CreatePersonRequestContract()
            {
                FirstNames = GetFirstNames(),
                LastNames = GetLastNames(),
                Description = Description,
                Type = PersonType,
                Addresses = GetAddresses(),
                Emails = GetEmails(),
                Links = GetSites(),
                Visas = GetVisas(),
                Phones = GetPhones(),
                Passports = GetPassports(),
                CityId = SelectedCityId,
                NationalCode = NationalCode,
                ProvinceId = SelectedProvinceId,
                CountryId = SelectedCountryId,
                ExternalServiceIdentifier = ExternalServiceIdentifier
            }).AsCheckedResult(x => x.Result);
            Clear();

        }

        public override Task OnError(Exception exception)
        {
            return base.OnError(exception);
        }

        public async Task UpdatePerson()
        {
            try
            {
                await _personClient.UpdateChangedValuesOnlyAsync(new UpdatePersonRequestContract()
                {
                    Id = UpdatePersonContract.Id,
                    FirstNames = GetFirstNames(),
                    LastNames = GetLastNames(),
                    Description = Description,
                    Type = PersonType,
                    Addresses = GetAddresses(),
                    Emails = GetEmails(),
                    Links = GetSites(),
                    Passports = GetPassports(),
                    Visas = GetVisas(),
                    Phones = GetPhones(),
                    CityId = SelectedCityId,
                    NationalCode = NationalCode,
                    ProvinceId = SelectedProvinceId,
                    CountryId = SelectedCountryId,
                    ExternalServiceIdentifier = ExternalServiceIdentifier
                });
                Clear();
            } catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }

        List<Customer.GeneratedServices.LanguageDataContract> GetFirstNames()
        {
            return new List<Customer.GeneratedServices.LanguageDataContract>()
            {
                new Customer.GeneratedServices.LanguageDataContract()
                {
                    Data = FirstName,
                    Language = "fa-IR"
                }
            };
        }

        List<Customer.GeneratedServices.LanguageDataContract> GetLastNames()
        {
            return new List<Customer.GeneratedServices.LanguageDataContract>()
            {
                new Customer.GeneratedServices.LanguageDataContract()
                {
                    Data = LastName,
                    Language = "fa-IR"
                }
            };
        }

        List<UpdateAddressRequestContract> GetAddresses()
        {
            return new List<UpdateAddressRequestContract>
            {
                new UpdateAddressRequestContract()
                {
                    Id = UpdatePersonContract?.Addresses?.FirstOrDefault()?.Id ?? 0L,
                    PostalCode = PostalCode,
                    Address = Address
                }
            };
        }

        List<UpdateEmailRequestContract> GetEmails()
        {
            return new List<UpdateEmailRequestContract>
            {
                new  UpdateEmailRequestContract()
                {
                    Id = UpdatePersonContract?.Emails?.FirstOrDefault()?.Id ?? 0L,
                    Address = EmailAddress
                }
            };
        }

        List<UpdateLinkRequestContract> GetSites()
        {
            return new List<UpdateLinkRequestContract>
            {
                new  UpdateLinkRequestContract()
                {
                    Id = UpdatePersonContract?.Links?.FirstOrDefault()?.Id ?? 0L,
                    Address = WebsiteAddress
                }
            };
        }

        List<UpdateVisaRequestContract> GetVisas()
        {
            return new List<UpdateVisaRequestContract>
            {
                new  UpdateVisaRequestContract()
                {
                    Id = UpdatePersonContract?.Visas?.FirstOrDefault()?.Id ?? 0L,
                    Number = VisaNumber
                }
            };
        }

        List<UpdatePassportRequestContract> GetPassports()
        {
            return new List<UpdatePassportRequestContract>
            {
                new UpdatePassportRequestContract()
                {
                    Id = UpdatePersonContract?.Passports?.FirstOrDefault()?.Id ?? 0L,
                    Number = PassportNumber
                }
            };
        }

        List<UpdatePhoneRequestContract> GetPhones()
        {
            return new List<UpdatePhoneRequestContract>
            {
                new UpdatePhoneRequestContract()
                {
                    Id = UpdatePersonContract?.Phones?.First().Id ?? 0L,
                    Number = PhoneNumber,
                    NumberType = PhoneNumberType.Home
                },
                new  UpdatePhoneRequestContract()
                {
                    Id = UpdatePersonContract?.Phones?.Last().Id ?? 0L,
                    Number = MobileNumber,
                    NumberType = PhoneNumberType.Mobile
                }
            };
        }

        public async Task LoadConfig()
        {
            var personCategories = await _personCategoryClient.FilterAsync(new Customer.GeneratedServices.FilterRequestContract());
            if (personCategories.IsSuccess)
            {
                PersonCategories.Clear();
                foreach (var personCategory in personCategories.Result)
                {
                    PersonCategories.Add(personCategory);
                }
            }

            if (UpdatePersonContract != null)
            {
                var personContract = await _personClient.GetByIdAsync(new Int64GetByIdLanguageRequestContract()
                {
                    Id = UpdatePersonContract.Id,
                    LanguageShortName = "fa-IR"
                });
                if (personContract.IsSuccess)
                {
                    UpdatePersonContract = personContract.Result;
                }
            }

            await CountriesViewModel.Search();
        }

        public async Task SearchProvince()
        {
            await ProvincesListViewModel.Search();
        }

        public async Task SearchCities()
        {
            await CitiesListViewModel.Search();
        }

        public void Clear()
        {
            ExternalServiceIdentifier = default;
            FirstName = "";
            UpdatePersonContract = default;
            FirstName = default;
            LastName = default;
            Description = default;
            PersonType = PersonType.RealPerson;
            Address = default;
            PostalCode = default;
            PassportNumber = default;
            EmailAddress = default;
            WebsiteAddress = default;
            VisaNumber = default;
            PhoneNumber = default;
            MobileNumber = default;
            NationalCode = default;
            SelectedCityId = default;
            SelectedProvinceId = default;
            SelectedCountryId = default;
        }
    }
}
