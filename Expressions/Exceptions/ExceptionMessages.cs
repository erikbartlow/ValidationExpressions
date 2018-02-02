using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodedThought.Core.Configuration;

namespace CodedThought.Core.Validation.Exceptions {
    public class ExceptionMessages {


        public static string RequiredMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.Required);
        public static string EqualsMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.Equals);
        public static string GreaterThanMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.GreaterThan);
        public static string GreaterThanOrEqualToMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.GreaterThanEqTo);
        public static string LessThanMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.LessThan);
        public static string LessThanOrEqualToMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.LessThanEqTo);
        public static string NotEqualToMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.NotEqual);
        public static string InvalidEmailMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.InvalidEmail);
        public static string NotInListMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.NotInList);
        public static string NotBetweenMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.NotBetween);
        public static string NotUpperCaseMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.NotUpper);
        public static string NotLowerCaseMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.NotLower);
        public static string ExceedsMaxMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.ExceedsMax);
        public static string MinimumNotReachedMessage => CoreSection.GetSection().ValidationMessages.GetMessage(ValidationMessageKeys.MinimumNotReached);

    }
}
