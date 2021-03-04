using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.ViewModel {

  /// <summary>
  /// Provides methods for validating values.
  /// </summary>
  public class Validate {

    /// <summary>
    /// Validates whether the given quantity is positive or zero.
    /// </summary>
    /// <param name="quantity">The quantity to be validated.</param>
    /// <returns></returns>
    public static bool Quantity(double quantity) {
      return quantity >= 0;
    }

    /// <summary>
    /// Validates whether the given email contains an @-sign.
    /// </summary>
    /// <param name="email">The email to be validated.</param>
    /// <returns></returns>
    public static bool Email(string email) {
      return email.ToLower().Contains('@');
    }

    /// <summary>
    /// Validates wheter the phone number contains digits????
    /// </summary>
    /// <param name="phoneNumber">The phone number to be validated.</param>
    /// <returns></returns>
    public static bool PhoneNumber(string phoneNumber) {
      throw new NotImplementedException();
      return true;
    }

    // Is it possible to validate this??? Do all countries have numbers in their zip code... ??
    public static bool ZipCode(string zipCode) {
      throw new NotImplementedException();
      return true;
    }
  }
}