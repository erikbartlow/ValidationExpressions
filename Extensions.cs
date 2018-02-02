using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodedThought.Core.Validation {
    public static partial class Extensions {
	   public static TargetTypeEnum GetValueType( this object value ) {
		  if ( value is Int32 || value is int || value is long ) { return TargetTypeEnum.Numeric; }
		  if ( value is String || value is Char ) { return TargetTypeEnum.Text; }
		  if ( value is Decimal || value is float || value is double ) { return TargetTypeEnum.Numeric; }
		  if ( value is DateTime ) { return TargetTypeEnum.Date; }
		  return TargetTypeEnum.Text;
	   }
    }
}
