# ValidationExpressions
C# Library for expression based data validation.  The validation expression library is designed to provide the designer with a flexible system of expressions to validate a file contents.  Validation expressions are applied at the file level as well as the column level.

Each validation is comprised of a series of one or more expressions that are applied at the moment of upload.  Expressions configured at the file level are applied initially, followed by each column’s expression according to their ordinal position in the file.

The goal of an expression is translate to a TRUE or FALSE statement.  As in any boolean type expression a false found in any part of the expression results in a false for the entire expression.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See Installing for notes on how to deploy the project on a live system.

### Prerequisites

The only dependency is [NewtonSoft.Json](https://www.newtonsoft.com/json).

### Installing

Installation is simply done by referencing the CodedThought.Core.Validation library.

### Configuration
*Optionally you can set the validation exception messages in the .config file.  If these are not set then the default constants will be used.*

#### Configuration Section
Add the following section to the `<configSections>` node in your app.config or web.config.
```xml
  <configSections>
    <section name="HPCoreSettings" type="HP.Core.Configuration.CoreSection,HP.Core.Configuration" />
  </configSections>
```
#### Config File Validation Settings

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

## Modifier Reference
Every expression can be broken down into two parts.  The target and the modifer.
The example below shows an expression in its most basic form.  All expressions must be surrounded by brackets, and the value, *this*, to the left of the pipe, **|**, is the target of the expression while the **r** is the modifier that will be used to validate the target.
```
[ this | r ]
```

||Description|
|-|-|
| \|u | Upper Case:  Applied to a string expression and the evaluation will compare the column and expressions’s upper case values|

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Erik Bartlow 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone who's code was used
* Inspiration
* etc
