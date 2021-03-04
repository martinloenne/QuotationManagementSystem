using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElogicSystem.Model.Tests {
  [TestClass]
  public class CustomerTests {
    private Customer _customer;
    private Quotation _quotation;
    private QuotationStatus _quotationStatus;
    private CustomerInfo _customerInfo;
    private CustomerInfo _customerInfo2;

    [TestInitialize]
    public void TestInitialize() {
      // Arrange
      _quotationStatus = new QuotationStatus(new DateTime(), QuotationStatusType.Created, "Employee1");
      _customerInfo = new CustomerInfo("Random person", new PhoneNumber("1338"), new Email("test@test.dk"), "Random Adress", new ZipCode("1337"));
      _customerInfo2 = new CustomerInfo("Random person2", new PhoneNumber("1337"), new Email("test@test.dk"), "Random Adress2", new ZipCode("1337"));
      _customer = new Customer(_customerInfo);
      _quotation = new Quotation(_quotationStatus);
    }

    [TestMethod]
    public void AddQuotation_AddsQuotationToCustomer_AddedQuotationToCustomer() {
      _customer.AddQuotation(_quotation);
      bool actualResult = _customer.Quotations.Contains(_quotation);
      Assert.IsTrue(actualResult);
    }

    [TestMethod]
    public void RemoveQuotation_RemoveQuotationToCustomer_RemovedQuotationFromCustomer() {
      _customer.AddQuotation(_quotation);
      _customer.RemoveQuotation(_quotation);
      bool actualResult = _customer.Quotations.Contains(_quotation);
      Assert.IsFalse(actualResult);
    }

    [TestMethod]
    public void ChangeCustomerInfo_ChangeCustomerInfo_ChangedToCustomerInfo2() {
      _customer.ChangeCustomerInfo(_customerInfo2);
      Assert.ReferenceEquals(_customer.CustomerInfo, _customerInfo2);
    }
  }
}