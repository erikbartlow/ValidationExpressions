using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CodedThought.Core.Configuration {
    public sealed class CoreValidationCollection : ConfigurationElementCollection {

        private const string REQUIRED_MESSAGE = "This value is required.";
        private const string EQUALS_MESSAGE = "The target value does not equal comparison value.";
        private const string GREATERTHAN_MESSAGE = "The value provided is not greater than the expected value.";
        private const string GREATERTHAN_OR_EQUALTO_MESSAGE = "The value provided is neither greater than or equal to the expected value.";
        private const string LESSTHAN_MESSAGE = "The value provided is not less than the expected value.";
        private const string LESSTHAN_OR_EQUALTO_MESSAGE = "The value provided is neither less than or equal to the expected value.";
        private const string NOTEQUALTO_MESSAGE = "The value provided is not equal to the expected value.";
        private const string INVALIDEMAIL_MESSAGE = "The email message provided is not a valid email address.";
        private const string NOTINLIST_MESSAGE = "The value provided is not within the expected list of values.";
        private const string NOTBETWEEN_MESSAGE = "The value provided is not between the expected values.";
        private const string NOTUPPER_MESSAGE = "The value provided must be all uppercase.";
        private const string NOTLOWER_MESSAGE = "The value provided must be all lowercase.";
        private const string MAXEXCEEDED_MESSAGE = "The value provided has exceeded the set maximum value.";
        private const string MINIMUMNOTREACHED_MESSAGE = "The value provide is not at or above the minimum value set.";

        protected override ConfigurationElement CreateNewElement() => new ValidationMessageElement();
        protected override object GetElementKey(ConfigurationElement element) => ((ValidationMessageElement)element);

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreConnectionCollection"/> class.
        /// </summary>
        public CoreValidationCollection() { }

        public List<ValidationMessageElement> GetMessages() {
            List<ValidationMessageElement> messages = new List<ValidationMessageElement>(base.Count);
            for (int i = 0; i < base.Count; i++) {
                messages.Add((ValidationMessageElement)BaseGet(i));
            }
            return messages;
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <returns></returns>
        public string GetMessage(ValidationMessageKeys messageKey) {
            List<ValidationMessageElement> messages = new List<ValidationMessageElement>();
            ValidationMessageElement msg = null;
            messages = GetMessages();
            if ( messages.Count != 0) {
                msg = GetMessages().Where(m => m.Key == messageKey.ToString()).FirstOrDefault();
            }
            
            if ( msg == null) {
                // Return the default message if no configured message is found.
                switch (messageKey) {
                    case ValidationMessageKeys.Equals:
                        return EQUALS_MESSAGE;
                    case ValidationMessageKeys.GreaterThan:
                        return GREATERTHAN_MESSAGE;
                    case ValidationMessageKeys.GreaterThanEqTo:
                        return GREATERTHAN_OR_EQUALTO_MESSAGE;
                    case ValidationMessageKeys.InvalidEmail:
                        return INVALIDEMAIL_MESSAGE;
                    case ValidationMessageKeys.LessThan:
                        return LESSTHAN_MESSAGE;
                    case ValidationMessageKeys.LessThanEqTo:
                        return LESSTHAN_OR_EQUALTO_MESSAGE;
                    case ValidationMessageKeys.NotBetween:
                        return NOTBETWEEN_MESSAGE;
                    case ValidationMessageKeys.NotEqual:
                        return NOTEQUALTO_MESSAGE;
                    case ValidationMessageKeys.NotInList:
                        return NOTINLIST_MESSAGE;
                    case ValidationMessageKeys.ExceedsMax:
                        return MAXEXCEEDED_MESSAGE;
                    case ValidationMessageKeys.MinimumNotReached:
                        return MINIMUMNOTREACHED_MESSAGE;
                    case ValidationMessageKeys.NotUpper:
                        return NOTUPPER_MESSAGE;
                    case ValidationMessageKeys.NotLower:
                        return NOTLOWER_MESSAGE;
                    case ValidationMessageKeys.Required:
                        return REQUIRED_MESSAGE;
                    default:
                        return string.Empty;
                }
            } else {
                return msg.Message;
            }
        }

    }
}
