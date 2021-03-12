using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Util
{
    public class JQueryUtils
    {
        public static void RegisterTextBoxForDatePicker(Page page, params TextBox[] textBoxes)
        {
            RegisterTextBoxForDatePicker(page, "dd/mm/yy", textBoxes);
        }

        public static void RegisterTextBoxForDatePicker(Page page, string format, params TextBox[] textBoxes)
        {
            bool allTextBoxNull = true;
            foreach (TextBox textBox in textBoxes)
            {
                if (textBox != null) allTextBoxNull = false;
            }

            if (allTextBoxNull) return;

            page.ClientScript.RegisterClientScriptInclude(page.GetType(), "jquery", "./JQuery/jquery.js");
            page.ClientScript.RegisterClientScriptInclude(page.GetType(), "jquery.ui.all", "./JQuery/ui/jquery.ui.all.js");
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "datepickerCss", "<link  rel=\"stylesheet\" href=\"./JQuery/themes/flora/flora.datepicker.css\" />");

            StringBuilder sb = new StringBuilder();

            sb.Append("$(document).ready(function() {");

            foreach (TextBox textBox in textBoxes)
            {
                if (textBox != null)
                {
                    sb.Append("$('#" + textBox.ClientID + "').datepicker({dateFormat: \"" + format + "\"});");
                }
            }

            sb.Append("});");

            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "jQueryScript", sb.ToString(), true);
        }
    }

}
