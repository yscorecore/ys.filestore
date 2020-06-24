using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YS.FileStore
{
    [TestClass]
    public class FileKeyRuleAttributeTest
    {
        [DataRow("abc/bcd/cde", true)]
        [DataRow("123/path1/name", true)]
        [DataRow("abc/path1/name", true)]
        [DataRow("abc-txt/path1/name", true)]
        [DataRow("abc.txt/path1/name", true)]
        [DataRow("123/name", true)]
        [DataRow("abc/name", true)]
        [DataRow("abc-txt/name", true)]
        [DataRow("abc.txt/name", true)]
        [DataRow("123", true)]
        [DataRow("abc", true)]
        [DataRow("abc-txt", true)]
        [DataRow("abc_txt", true)]
        [DataRow("abc.txt", true)]
        [DataRow("", false)]
        [DataRow(null, false)]
        [DataTestMethod]
        public void ShouldBeAValidFileKey(string fileKey, bool required)
        {
            var attr = new FileKeyRuleAttribute(required);
            Assert.IsTrue(attr.IsValid(fileKey));
        }
        [DataRow("abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde/abc/bcd/cde", true)]
        [DataRow("abc*", true)]
        [DataRow("abc\\\\bcd", true)]
        [DataRow("abc//bcd", true)]
        [DataRow("abc\\", true)]
        [DataRow("abc/", true)]
        [DataRow("\\abc", true)]
        [DataRow("/abc", true)]
        [DataRow("", true)]
        [DataRow(null, true)]
        [DataTestMethod]
        public void ShouldBeAnInValidFileKey(string fileKey, bool required)
        {
            var attr = new FileKeyRuleAttribute(required);
            Assert.IsFalse(attr.IsValid(fileKey));
        }

        [DataRow("")]
        [DataRow(null)]
        [DataTestMethod]
        public void ShouldBeAnInvalidFile(string fileKey)
        {
            var attr = new FileKeyRuleAttribute();
            Assert.IsFalse(attr.IsValid(fileKey));
        }
    }
}
