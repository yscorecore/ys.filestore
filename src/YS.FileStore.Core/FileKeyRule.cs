using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace YS.FileStore
{
    public class FileKeyRuleAttribute : Attribute
    {
        public FileKeyRuleAttribute() : this(true)
        {

        }
        public FileKeyRuleAttribute(bool required)
        {

        }
    }
}
