using System.Web.UI.WebControls;

namespace Code.Library
{
    public static class ControlsHelper
    {
        /// <summary>
        /// check/uncheck checkbox
        /// </summary>
        /// <param name="checkBox"></param>
        /// <param name="isChecked"></param>
        public static void ToggleCheckbox(CheckBox checkBox, bool? isChecked)
        {
            if (isChecked != null) checkBox.Checked = (bool)isChecked;
        }

        /// <summary>
        /// Select drop down item 
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
