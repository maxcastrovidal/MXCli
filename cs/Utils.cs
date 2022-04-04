using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApp
{
    public class Utils
    {
        public static object Nz(object InputValue, object OutputIfIsNull)
        {
            return InputValue == null || InputValue == DBNull.Value ? OutputIfIsNull : InputValue;
        }

        public static SelectList AddFirstItem(SelectList origList, SelectListItem firstItem)
        {
            //Agrega primer elemento a SelectList
            //https://stackoverflow.com/questions/668589/how-can-i-add-an-item-to-a-selectlist-in-asp-net-mvc
            List<SelectListItem> newList = origList.ToList();
            newList.Insert(0, firstItem);

            var selectedItem = newList.FirstOrDefault(item => item.Selected);
            var selectedItemValue = String.Empty;
            if (selectedItem != null)
            {
                selectedItemValue = selectedItem.Value;
            }

            return new SelectList(newList, "Value", "Text", selectedItemValue);
        }

    }
}
