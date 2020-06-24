using System.ComponentModel.DataAnnotations;

namespace YS.FileStore
{
    public class BucketRuleAttribute : MixedValidationAttribute
    {
        private const string BucketNameRegex = "^[a-zA-Z0-9-_\\.]+$";

        public BucketRuleAttribute() : base(new RequiredAttribute(), new RegularExpressionAttribute(BucketNameRegex))
        {
        }
    }
}
