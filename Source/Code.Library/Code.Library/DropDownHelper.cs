using System.Web.UI.WebControls;

namespace Code.Library
{
    public static class DropDownHelper
    {
        /// <summary>
        /// Select drop down item
        /// Author : Abhith
        /// Date : 20 July 2015
        /// Reference : Sysberries
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="value"></param>
        public static void SelectDropDownValue(DropDownList dropDownList, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var item = dropDownList.Items.FindByValue(value);

            if (item != null)
            {
                dropDownList.SelectedValue = value;
            }
        }
    }
}
