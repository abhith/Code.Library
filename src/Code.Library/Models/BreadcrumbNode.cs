namespace Code.Library
{
    /// <summary>
    /// The breadcrumb item.
    /// </summary>
    public class BreadcrumbNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbNode"/> class.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        public BreadcrumbNode(string title, string url)
        {
            Title = title;
            URL = string.IsNullOrWhiteSpace(url) ? "javascript:void(0);" : url;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string URL { get; set; }
    }
}