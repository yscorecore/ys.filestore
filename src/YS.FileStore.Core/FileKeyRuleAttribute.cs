using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace YS.FileStore
{
    public class FileKeyRuleAttribute : MixedValidationAttribute
    {
        private const string FileKeyRegex = "^[a-zA-Z0-9-_\\.]+(/[a-zA-Z0-9-_\\.]+)*$";
        private const int MaxFileKeyLength = 256;
        public FileKeyRuleAttribute() : this(true)
        {

        }
        public FileKeyRuleAttribute(bool required)
        : base(required ?
            new ValidationAttribute[] { new RequiredAttribute(), new RegularExpressionAttribute(FileKeyRegex), new MaxLengthAttribute(MaxFileKeyLength) } :
            new ValidationAttribute[] { new RegularExpressionAttribute(FileKeyRegex), new MaxLengthAttribute(MaxFileKeyLength) })
        {
        }
    }
}
