using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppSolution.Mvc.Website.Models
{
    /// <summary>
    /// Using model list: @model ListModel<yourClass>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListModel<T> where T : class
    {
        public List<T> Items { get; set; }
        public ListModel(List<T> list)
        {
            Items = list;
        }

        public ListModel()
        {
            Items = new List<T>();
        }
    }
    public class ListModel<T, G> where T : class where G : class, new()
    {
        public G Header { get; set; }
        public List<T> Items { get; set; }
        public ListModel(List<T> list)
        {
            Items = list;
        }

        public ListModel()
        {
            Header = new G();
            Items = new List<T>();
        }
    }
}