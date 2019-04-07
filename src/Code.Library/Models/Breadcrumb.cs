namespace Code.Library
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The breadcrumb.
    /// </summary>
    public class Breadcrumb
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Breadcrumb"/> class.
        /// </summary>
        /// <param name="activePageTitle">
        /// The active page title.
        /// </param>
        public Breadcrumb(string activePageTitle)
        {
            if (!string.IsNullOrWhiteSpace(activePageTitle))
            {
                ActivePageTitle = activePageTitle;
            }
        }

        /// <summary>
        /// Gets or sets the active page title.
        /// </summary>
        private string ActivePageTitle { get; set; }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="nodes">
        /// The nodes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Generate(IEnumerable<BreadcrumbNode> nodes)
        {
            var output = new StringBuilder();

            output.Append("<ul class='breadcrumb' itemscope itemtype='http://schema.org/BreadcrumbList'>");
            output.AppendFormat("<li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'><a itemprop='item' href='/'><span itemprop='name'>{0}</span></a><meta itemprop='position' content='1' /></li>", "Home");

            var position = 2;

            foreach (var node in nodes)
            {
                output.AppendFormat("<li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'><a itemprop='item' href='{0}'><span itemprop='name'>{1}</span></a><meta itemprop='position' content='{2}' /></li>", node.URL, node.Title, position);

                position++;
            }

            if (!string.IsNullOrWhiteSpace(ActivePageTitle))
            {
                output.AppendFormat("<li itemprop='itemListElement' itemscope itemtype='http://schema.org/ListItem'><span itemprop='name'><strong>{0}</strong></span><meta itemprop='position' content='{1}' /></li>", ActivePageTitle, position);
            }

            output.Append("</ul>");

            return output.ToString();
        }
    }
}