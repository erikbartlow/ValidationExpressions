using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CodedThought.Core.Configuration {
    public sealed class CoreSettingsCollection : ConfigurationElementCollection {
        protected override ConfigurationElement CreateNewElement() => new CoreSettingElement();
        protected override object GetElementKey( ConfigurationElement element ) => this.GetSetting( element );
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreSettingsCollection"/> class.
        /// </summary>
        public CoreSettingsCollection() { }

        public List<CoreSettingElement> GetSettings() {
            List<CoreSettingElement> settings = new List<CoreSettingElement>( base.Count );
            for ( int i = 0; i < base.Count; i++ ) {
                settings.Add( ( CoreSettingElement ) BaseGet( i ) );
            }
            return settings;
        }
        public object GetSetting(ConfigurationElement element ) {
            return ( ( CoreSettingElement ) element ).Key;
        }
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns></returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public CoreSettingElement GetSetting( string settingKey ) {
            CoreSettingElement cn = GetSettings().Where<CoreSettingElement>( c => c.Key == settingKey ).First();
            if ( cn != null ) {
                return cn;
            } else {
                throw new ConfigurationErrorsException( $"The setting name, {settingKey}, was not found in the configuration file." );
            }

        }

    }
}
