using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CodedThought.Core.Configuration {
    public sealed class CoreSettingElement : ConfigurationElement {
        static ConfigurationProperty _key;
        static ConfigurationProperty _value;
        static ConfigurationPropertyCollection _elementProperties;

        #region Constructor

        static CoreSettingElement() {
            _key = new ConfigurationProperty(
                "key" ,
                typeof( string ) ,
                null ,
                ConfigurationPropertyOptions.IsRequired );
            _value = new ConfigurationProperty(
                "value" ,
                typeof( string ) ,
                null ,
                ConfigurationPropertyOptions.IsRequired );

            _elementProperties = new ConfigurationPropertyCollection();
            _elementProperties.Add( _key );
            _elementProperties.Add( _value );
        }

        #endregion Constructor

        #region Properties

        public string Key => ( string ) base[ _key ];
        public string Value => ( string ) base[ _value ];
        protected override ConfigurationPropertyCollection Properties => _elementProperties;

        #endregion
    }
}
