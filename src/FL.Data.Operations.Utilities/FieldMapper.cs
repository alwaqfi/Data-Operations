using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Data.Operations.Utilities
{
   public sealed class FieldMapper
    {
        public static void Map<ISourceType, ITargetType>(ISourceType source, ITargetType target)
        {
            var targetType = target.GetType();
            var sourceType = source.GetType();

            var sourcePropList = sourceType.GetProperties();

            foreach (var prp in sourcePropList)
            {
                var propValue = sourceType.GetProperty(prp.Name).GetValue(source);
                var prop = targetType.GetProperty(prp.Name);
                if (prop != null)
                    prop.SetValue(target, propValue);
            }
        }
    }
}
