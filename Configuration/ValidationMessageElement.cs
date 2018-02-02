using System.Configuration;
using System.Collections;
using System.Collections.Generic;

namespace CodedThought.Core.Configuration {
    public enum ValidationMessageKeys {
       Required, Equals, GreaterThan, GreaterThanEqTo, LessThan, LessThanEqTo, NotEqual, InvalidEmail, NotInList, NotBetween, NotUpper, NotLower, ExceedsMax, MinimumNotReached
    }
    public sealed class ValidationMessageElement : ConfigurationElement {

        static ConfigurationProperty _key;
        static ConfigurationProperty _message;

        static ConfigurationPropertyCollection _elementProperties;

        public ValidationMessageElement() {
            _key = new ConfigurationProperty(
                "key",
                typeof(string),
                null,
                ConfigurationPropertyOptions.None);
            _message = new ConfigurationProperty(
                "message",
                typeof(bool),
                null,
                ConfigurationPropertyOptions.None);


            _elementProperties = new ConfigurationPropertyCollection();
            _elementProperties.Add(_key);
            _elementProperties.Add(_message);
        }

        /// <summary>
        /// Gets the equals message.
        /// </summary>
        /// <value>
        /// The equals message.
        /// </value>
        [ConfigurationProperty("key")]
        [CallbackValidator(Type = typeof(ValidationMessageElement), CallbackMethodName = "CheckMessageKey")]
        public string Key => (string)base[_key];
        /// <summary>
        /// Gets a value indicating whether [greater than message].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [greater than message]; otherwise, <c>false</c>.
        /// </value>
        [ConfigurationProperty("message")]
        public string Message => (string)base[_message];


        /// <summary>
        /// Checks the message name for supported message keys.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static void CheckMessageKey(object value) {
            string providerValue = (string)value;
            List<string> validMessageKeys = new List<string>();
            validMessageKeys.AddRange("Equals, GreaterThan, GreaterThanEqTo, LessThan, LessThanEqTo, NotEqual, InvalidEmail, NotInList, NotBetween".ToLower().Split(",".ToCharArray()));

            if (!validMessageKeys.Contains(providerValue.ToLower())) {
                throw new ConfigurationErrorsException($"The validation message key, {value}, is not valie.  Please use one of the following message keys, { string.Join(", ", validMessageKeys.ToArray())}");
            }

        }

        protected override ConfigurationPropertyCollection Properties => _elementProperties;
    }
}