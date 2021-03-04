using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElogicSystem.ViewModel;
using System.Collections.ObjectModel;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel.Tests {

  [TestClass]
  public class MainWindowViewModelTests {
    public MainWindowViewModel ViewModel;
    public MockQuotationIWindowFactory MockQuotation;
    public MockCustomerIWindowFactory MockCustomer;

    [TestInitialize]
    public void Initialize() {
      ViewModel = new MainWindowViewModel();
      MockQuotation = new MockQuotationIWindowFactory();
      MockCustomer = new MockCustomerIWindowFactory();
    }

    [TestMethod]
    public void AddNewQuotationCommand_CreatesANewQuotationAndAddToCollection_IsInstanceOfType() {
      ViewModel.AddNewQuotationCommand.Execute(MockQuotation);

      int count = ViewModel.QuotationData.Count;

      Assert.IsInstanceOfType(ViewModel.QuotationData[count - 1], typeof(QuotationDataViewModel));
    }

    [TestMethod]
    public void AddNewCustomerCommand_CreatesANewCustomerAndAddToCollection_IsInstanceOfType() {
      ViewModel.AddNewCustomerCommand.Execute(MockCustomer);

      int count = ViewModel.CustomerData.Count;

      Assert.IsInstanceOfType(ViewModel.CustomerData[count - 1], typeof(CustomerDataViewModel));
    }

    [TestMethod]
    public void DeleteCommand_DeletesSelectedQuotationFromCollection_IsTrue() {
      QuotationDataViewModel item1 = new QuotationDataViewModel();
      QuotationDataViewModel expectedRemovedItem = new QuotationDataViewModel(645, "test", "test", "lars", 0, DateTime.Now, new ObservableCollection<Panel>());
      QuotationDataViewModel item3 = new QuotationDataViewModel();
      ViewModel.AddQuotaionToCollection(item1);
      ViewModel.AddQuotaionToCollection(expectedRemovedItem);
      ViewModel.AddQuotaionToCollection(item3);

      ViewModel.SelectedDataItem = expectedRemovedItem;
      ViewModel.DeleteCommand.Execute(new object());

      Assert.IsTrue(!ViewModel.QuotationData.Contains(expectedRemovedItem));
    }

    [TestMethod]
    public void DeleteCommand_DeletesSelectedCustomerFromCollection_IsTrue() {
      CustomerDataViewModel item1 = new CustomerDataViewModel();
      CustomerDataViewModel expectedRemovedItem = new CustomerDataViewModel("test", "test", "test", "test", "test");
      CustomerDataViewModel item3 = new CustomerDataViewModel();
      ViewModel.AddCustomerToCollection(item1);
      ViewModel.AddCustomerToCollection(expectedRemovedItem);
      ViewModel.AddCustomerToCollection(item3);

      ViewModel.SelectedDataItem = expectedRemovedItem;
      ViewModel.DeleteCommand.Execute(new object());

      Assert.IsTrue(!ViewModel.CustomerData.Contains(expectedRemovedItem));
    }

    [TestMethod]
    public void EditSelectedItemCommand_EditsTheSelectedQuotation_IsTrue() {
      QuotationDataViewModel item1 = new QuotationDataViewModel();
      QuotationDataViewModel expectedChangedItem = new QuotationDataViewModel(0, "test", "test", "lars", 0, DateTime.Now, new ObservableCollection<Panel>());
      QuotationDataViewModel item3 = new QuotationDataViewModel();
      QuotationDataViewModel expectedItemChangeTo = new QuotationDataViewModel(0, "Has been changed", "Has been changed", "Has been changed", 0, DateTime.Now, new ObservableCollection<Panel>());
      ;

      ViewModel.AddQuotaionToCollection(item1);
      ViewModel.AddQuotaionToCollection(expectedChangedItem);
      ViewModel.AddQuotaionToCollection(item3);

      ViewModel.SelectedDataItem = expectedChangedItem;
      ViewModel.SelectedTabIndex = 0;

      ViewModel.EditSelectedItemCommand.Execute(MockQuotation);

      Assert.IsTrue(IsQuotationsEquivalent(ViewModel.QuotationData[1], expectedItemChangeTo));
    }

    private bool IsQuotationsEquivalent(QuotationDataViewModel quotation1, QuotationDataViewModel quotation2) {
      return string.Equals(quotation1.Customer, quotation2.Customer) && string.Equals(quotation1.Description, quotation2.Description) && string.Equals(quotation1.Employee, quotation2.Employee);
    }

    [TestMethod]
    public void EditSelectedItemCommand_EditsTheSelectedCustomer_IsTrue() {
      CustomerDataViewModel item1 = new CustomerDataViewModel();
      CustomerDataViewModel expectedChangedItem = new CustomerDataViewModel("test", "test", "test", "test", "test");
      CustomerDataViewModel item3 = new CustomerDataViewModel();
      CustomerDataViewModel expectedItemChangeTo = new CustomerDataViewModel("Has been changed", "Has been changed", "Has been changed", "Has been changed", "Has been changed");

      ViewModel.AddCustomerToCollection(item1);
      ViewModel.AddCustomerToCollection(expectedChangedItem);
      ViewModel.AddCustomerToCollection(item3);

      ViewModel.SelectedDataItem = expectedChangedItem;
      ViewModel.SelectedTabIndex = 1;

      ViewModel.EditSelectedItemCommand.Execute(MockCustomer);

      Assert.IsTrue(IsCustomersEquivalent(expectedItemChangeTo, ViewModel.CustomerData[1]));
    }

    private bool IsCustomersEquivalent(CustomerDataViewModel customer1, CustomerDataViewModel customer2) {
      return string.Equals(customer1.Address, customer2.Address) && string.Equals(customer1.Name, customer2.Name) && string.Equals(customer1.Email, customer2.Email) && string.Equals(customer1.PhoneNumber, customer2.PhoneNumber) && string.Equals(customer1.ZipCode, customer2.ZipCode);
    }

    [TestMethod]
    public void AddQuotationToCollection_AddAQuotationToTheCollection_IsTrue() {
      QuotationDataViewModel expectedContainedItem = new QuotationDataViewModel(0, "test2", "test2", "lars", 0, DateTime.Now, new ObservableCollection<Panel>());

      ViewModel.AddQuotaionToCollection(expectedContainedItem);

      Assert.IsTrue(ViewModel.QuotationData.Contains(expectedContainedItem));
    }

    [TestMethod]
    public void AddCustomerToCollection_AddACustomerToTheCollection_IsTrue() {
      CustomerDataViewModel expectedContainedItem = new CustomerDataViewModel("TestTest", "TestTest", "TestTest", "TestTest", "TestTest");

      ViewModel.AddCustomerToCollection(expectedContainedItem);

      Assert.IsTrue(ViewModel.CustomerData.Contains(expectedContainedItem));
    }

    [TestMethod]
    public void RemoveQuotationFromCollection_RemovesAQuotationFromCollection_IsTrue() {
      QuotationDataViewModel item1 = new QuotationDataViewModel();
      QuotationDataViewModel expectedRemovedItem = new QuotationDataViewModel(645, "test", "test", "lars", 0, DateTime.Now, new ObservableCollection<Panel>());
      QuotationDataViewModel item3 = new QuotationDataViewModel();
      ViewModel.AddQuotaionToCollection(item1);
      ViewModel.AddQuotaionToCollection(expectedRemovedItem);
      ViewModel.AddQuotaionToCollection(item3);

      ViewModel.SelectedDataItem = expectedRemovedItem;
      ViewModel.DeleteCommand.Execute(new object());

      Assert.IsTrue(!ViewModel.QuotationData.Contains(expectedRemovedItem));
    }

    [TestMethod]
    public void RemoveCustomerFromCollection_RemovesACustomerFromCollection_IsTrue() {
      CustomerDataViewModel item1 = new CustomerDataViewModel();
      CustomerDataViewModel expectedRemovedItem = new CustomerDataViewModel("test", "test", "test", "test", "test");
      CustomerDataViewModel item3 = new CustomerDataViewModel();
      ViewModel.AddCustomerToCollection(item1);
      ViewModel.AddCustomerToCollection(expectedRemovedItem);
      ViewModel.AddCustomerToCollection(item3);

      ViewModel.SelectedDataItem = expectedRemovedItem;
      ViewModel.DeleteCommand.Execute(new object());

      Assert.IsTrue(!ViewModel.CustomerData.Contains(expectedRemovedItem));
    }
  }
}