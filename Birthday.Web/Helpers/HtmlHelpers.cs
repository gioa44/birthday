using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Birthday.Tools;

namespace Birthday.Web.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString SelectListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, Type enumType)
        {
            var list = new List<SelectListItem>();

            if (!enumType.IsEnum)
            {
                throw new InvalidCastException();
            }

            foreach (Enum item in enumType.GetEnumValues())
            {
                var listItem = new SelectListItem();
                listItem.Text = item.GetDescription();
                listItem.Value = item.ToString();

                list.Add(listItem);
            }

            return helper.DropDownListFor(expression, list);
        }
    }
}