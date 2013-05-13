using System.IO;
using System.Text;
using HtmlAgilityPack;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace Harvest.OrchardDevToolbelt.Filters {
    [OrchardFeature("Harvest.OrchardDevToolbelt.EmailLinkConverter")]
    public class EmailHtmlFilter : IHtmlFilter {
        public string ProcessContent(string text, string flavor) {
            if (flavor != "html")
                return text;

            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            var emailNodes = doc.DocumentNode.SelectNodes("//a[contains(text(), '@')]");
            if (emailNodes != null) {
                foreach (var node in emailNodes) {
                    node.Name = "span";
                    node.Attributes.Remove("href");
                }
            }
            
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb)) {
                doc.Save(writer);
                return sb.ToString();
            }
        }
    }
}