using Birthday.Properties.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public class Navigation
    {
        private List<Item> _Items;

        public List<Item> Items
        {
            get
            {
                return _Items;
            }
        }

        private string _Action;
        private string _Controller;

        public Navigation(string action, string controller)
        {
            _Action = action;
            _Controller = controller;
            _Items = new List<Item>();

            AddItem(GeneralResource.DayAnniversary, "Index", "Home");
            AddItem(GeneralResource.Rules, "Rules", "Home");
            AddItem(GeneralResource.Reserve, "Reserve", "Home");
            _Items.Add(new Item(GeneralResource.Visualization, "Index", "Visualization", controller == "Visualization"));
            AddItem(GeneralResource.AnniversaryHistory, "AnniversaryHistory", "Home");
            AddItem(GeneralResource.Contact, "Contact", "Home");
        }

        private void AddItem(string name, string action, string controller)
        {
            _Items.Add(new Item(name, action, controller, IsActive(action, controller)));
        }

        private bool IsActive(string action, string controller)
        {
            return _Action == action && _Controller == controller;
        }

        public class Item
        {
            public string Name { get; private set; }
            public string Action { get; private set; }
            public string Controller { get; private set; }
            public bool Active { get; private set; }

            public Item(string name, string action, string controller, bool active)
            {
                Name = name;
                Action = action;
                Controller = controller;
                Active = active;
            }
        }
    }
}