﻿using System.Collections.Generic;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// Restaurant menu interface
    /// @author: Andrew Kao
    /// @updated: 3/11/18
    /// </summary>
    public interface IRestaurantMenu
    {
        string MenuName { get; set; }
        IEnumerable<IMenuItem> Items { get; set; }
    }
}