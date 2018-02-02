using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

namespace CodedThought.Core.Configuration {
    public sealed class CoreSection : ConfigurationSection {
        public const string SECTION_NAME = "CodedThoughtValidationSettings";
        static ConfigurationProperty _connections;
        static ConfigurationProperty _settings;
        static ConfigurationProperty _validationMessages;
        static ConfigurationPropertyCollection _sectionProperties;

        static CoreSection() {
            _validationMessages = new ConfigurationProperty(
                "validationMessages",
                typeof(CoreValidationCollection),
                null,
                ConfigurationPropertyOptions.None);
            _sectionProperties = new ConfigurationPropertyCollection();
            _sectionProperties.Add(_validationMessages);
        }

        #region Properties

        /// <summary>
        /// Gets the validation messages.
        /// </summary>
        /// <value>
        /// The validation messages.
        /// </value>
        [ConfigurationProperty("validationMessages", IsRequired = false)]
        public CoreValidationCollection ValidationMessages => (CoreValidationCollection)base[_validationMessages];
        /// <summary>
        /// Gets the collection of properties.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties => _sectionProperties;

        #endregion

        #region Static Methods


        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <returns>An CodedThought.Core Configuration section, if defined in the configSections.</returns>
        /// <exception cref="ConfigurationErrorsException">The section is not defined in the .config file under configSections.</exception>
        public static CoreSection GetSection() {
            return GetSection(SECTION_NAME);
        }
        /// <summary>
        /// Gets the section by the passed section name.
        /// </summary>
        /// <param name="userDefinedName">Name of the user defined.</param>
        /// <returns>An CodedThought.Core Configuration section, if defined in the configSections.</returns>
        /// <exception cref="ConfigurationErrorsException">The section is not defined in the .config file under configSections.</exception>
        public static CoreSection GetSection(string userDefinedName) {
            string errMsgFileName = ".config";

            CoreSection coreSettingsSection = null;
            if (string.IsNullOrEmpty(userDefinedName)) {
                userDefinedName = SECTION_NAME;
            }
            if (HttpContext.Current != null) {
                coreSettingsSection = WebConfigurationManager.GetSection(userDefinedName) as CoreSection;
                errMsgFileName = "web.config";
            } else {
                coreSettingsSection = ConfigurationManager.GetSection(userDefinedName) as CoreSection;
                errMsgFileName = "(app).config";
            }
            if (coreSettingsSection == null) {
                throw new ConfigurationErrorsException($"The <{userDefinedName}> section is not defined as a <section> under <configSections> in the {errMsgFileName} file.");
            }
            return coreSettingsSection;
        }
        #endregion
    }
}
