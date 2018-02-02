# ValidationExpressions
C# Library for expression based data validation.  The validation expression library is designed to provide the designer with a flexible system of expressions to validate a file contents.  Validation expressions are applied at the file level as well as the column level.

Each validation is comprised of a series of one or more expressions that are applied at the moment of upload.  Expressions configured at the file level are applied initially, followed by each column’s expression according to their ordinal position in the file.

The goal of an expression is translate to a TRUE or FALSE statement.  As in any boolean type expression a false found in any part of the expression results in a false for the entire expression.

## Installation

Installation is simply done by referencing the CodedThought.Core.Validation library.

## Configuration
Optionally you can set the validation exception messages in the .config file.  If these are not set then the default constants will be used.
### Config File Section
Add the following section to the `<configSections>` node in your app.config or web.config.
```xml
  <configSections>
    <section name="HPCoreSettings" type="HP.Core.Configuration.CoreSection,HP.Core.Configuration" />
  </configSections>
```
### Config File Validation Settings

Add the `<CodedThoughtValidationSettings>` section after the `<appSettings>` node.
  
```xml
  <CodedThoughtValidationSettings>
    <add key="Required" message="" />
    <add key="Equals" message="" />
    <add key="GreaterThan" message="" />
    <add key="GreaterThanEqTo" message="" />
    <add key="LessThan" message="" />
    <add key="LessThanEqTo" message="" />
    <add key="NotEqual" message="" />
    <add key="InvalidEmail" message="" />
    <add key="NotInList" message="" />
    <add key="NotBetween" message="" />
    <add key="NotUpper" message="" />
    <add key="NotLower" message="" />
    <add key="ExceedsMax" message="" />
    <add key="MinimumNotReached" message="" />
  </CodedThoughtValidationSettings>
```
  
### Expression Reference
An expression is made up of a sequence of characters refered to as the target, that when translated return a BOOLEAN.  If the contents of the target column value will not be tested against an expression like those seen in 1.2.2 then the this keyword should be used.  This will instruct the expression validator to apply any modifiers if any are present to the current value being validated.

  #### Syntax
|||
|------|------|
| | [target \| u ] |

### Custom Attributes
There are two custom attributes used by `HP.Core.Data`.
* DataTable
* DataColumn

#### DataTable Attribute Properties
| Name | Description &amp; Options |
| ------ | ------|
| _TableName_ | Physical name of the table to get, save, or delete from. |
| _ViewName_ | Physical name of a view to get from.<br />_Note: This is useful for developers when you have a particular set of data elements that need to be joined to display or report on._ | 
##### Sample Usage
```cs
[DataTable( "tblRegions" )]
[DataTable( "tblRegions", "vRegions" )]
```
#### DataColumn Attribute Properties
| Name | Description &amp; Options | Required 
| ------ | ------ | ------
| _ColumnName_ | Physical name of the column in the table to get, save, or delete from. | Yes |
| _IsPrimaryKey_ | Is this property/column the primary key in the table? `true | false` <br />_Default: false_  |  Yes |
| _IsUpdateable_ | Is this property/column considered updateable?  This is usually true unless it is also the primary key. `true | false` <br />_Default: true_  |  No
| _Size_ | Denotes the maximum size of data stored in the column.<br />_Note: This is only used for string based columns._ | No |
| _ColumnType_ | Specifies the `System.Data.DbType` associated with the column.| Yes |
| _PropertyType_ | Used by bulk copy procedures to dynamically ascertain the class to table column type mappings.|No|
| _PropertyName_ | Used by bulk copy procedures to dynamically ascertain the class to table column name mappings.|No|
| _ExtendedPropertyType_ | When setting this to another classes type the developer can set a property that is of an object type rather than a simple type.  This is very useful when a class property references an `ENUM`.<br />_Usage: typeof(MyNamespace.MyEnum)_|No|
| _ExtendedPropertyName_ | The property containing the data for the column.<br />_Note: Only used when the `ExtendedPropertyType` is set and is of type `Object`| No|
##### Sample Usage
```cs
// Constructor Samples
[DataColumn( name: "Id", type: System.Data.Int32, isKey: true )]
[DataColumn( name: "Name", type: System.Data.String, size: 20 )]
[DataColumn( name: "Name", type: System.Data.String, extendedPropertyType: typeof(Country), "CountryName" )]

```

> IMPORTANT: Setting the __`ViewName`__ property will override any `TableName` value previously set for all GET routines.  However, Save and Delete routines will always use the `TableName` value since updateable views are not always supported across database platforms.

##### Create Data Aware classes
A key component to the HP.Core.Data framework are the custom tags designed to enable the framework to "learn" how to communicate with your database.  You can design them like any C# class, but it is key to understand that the framework is dynamically creating parameterized queries based on the custom attributes you set.


##### Create a Controller Class that inherits from the `GenericDataStoreController` with at least one constructor.
> Important: The GenericDataStoreController class provides your controller with all the necessary methods for CRUD through the `DataStore` object.  The `DataStore` is the derived instance of the `DatabaseObject`.
```cs
public DbController() {
  // Connection to database.
  DatabaseConnection cn = new DatabaseConnection( *Connection Name* );
  try {
    DataStore = new GenericDataStore( cn );
  } catch ( Exception ex ) {
    throw;
  }
}
```

##### Create a class with data aware properties.
```cs
using System.Data;
using HP.Core.Data;

[DataTable( "tblRegion" )]
public class Region{

	[DataColumn( name: "regionId", type: System.Data.Int32, isKey: true )]
    public int Id{ get; set; }
    [DataColumn( name: "regionName", type: System.Data.String, size: 20 )]
    public string Name { get; set; }
    
}
```
##### Add CRUD Methods to the Controller class
```cs
public Region GetRegion( int id ){
	try{
    	return DataStore.Get<Region>( id );
    } catch {
    	throw;
	}
}

public List<Region> GetRegions(){
	try{
    	// The GetMultiple method requires a ParameterCollection.  So to simply
        // return all records we just have to pass a null ParameterCollection.
    	return DataStore.GetMultiple<Region>(null);
    } catch {
    
    }
}
```
##### ParameterCollection
The ParameterCollection inherits from CollectionBase and implements the IList, ICollection, IEnumerable interface.

| Methods | Description &amp; Options
| ------ | ------
| _Add_ | Adds a parameter to the collection.
| | __Overloads__: <ul><li>( IDataParameterCollection parameters )</li><li>( ParameterCollection parameters )</li><li>( IDataParameter parameter )</li></ul>
| _AddBooleanParameter()_ | `string` Column Name, `boolean` Value
| _AddCharParameter()_ | `string` Column Name, `string` Value, `int` Size
| _AddDataTimeParameter()_ | `string` Column Name, `DateTime` Value
| _AddDoubleParameter()_ |`string` Column Name, `double` Value
| _AddInt32Parameter()_ | `string` Column Name, `int` Value
| _AddOutputParameter()_ | `string` Parameter Name, `DBTypeSupported` Return Value
| _AddStringParameter()_ | `string` Column Name, `string` Value
| _AddSubGroupParameterList()_ | `ParameterCollection` list
| _AddXmlParameter()_ | `string` Column Name, `string` Value
| _Remove()_ | `int` index
| _SubParameterGroupWhereType_ | Using the `DatabaseObject.WhereType` enumerator this instructs the framework on how to handle any sub parameter groups if found.<br /> _Note: Only used when the `AddSubGroupParameterList()` method is used._
