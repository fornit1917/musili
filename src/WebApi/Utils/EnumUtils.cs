using System;
using System.Collections.Generic;

namespace Musili.WebApi.Utils {
    public static class EnumUtils {
        public static List<TEnum> ParseEnumValuesList<TEnum>(string valuesList, char separator = ',') where TEnum : struct {
            var result = new List<TEnum>();
            if (valuesList != null) {
                string[] parts = valuesList.Split(separator);
                foreach (string part in parts) {
                    TEnum enumValue;
                    if (Enum.TryParse(part, true, out enumValue)) {
                        result.Add(enumValue);
                    }
                }
            }
            return result;
        }
    }
}
