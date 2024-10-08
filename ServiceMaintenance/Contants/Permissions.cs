using ServiceMaintenance.Contants;
using System;
using System.Collections.Generic;

namespace ServiceMaintenance.Contants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Access",  // Module-level permission
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }

        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();

            var modules = Enum.GetValues(typeof(Modules));

            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allPermissions;
        }

        public static class Products
        {
            public const string Access = "Permissions.Products.Access";
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class DashBoard
        {
            public const string Access = "Permissions.DashBoard.Access";
            public const string View = "Permissions.DashBoard.View";
            public const string Create = "Permissions.DashBoard.Create";
            public const string Edit = "Permissions.DashBoard.Edit";
            public const string Delete = "Permissions.DashBoard.Delete";
        }

        public static class Customer
        {
            public const string Access = "Permissions.CustomerList.Access";
            public const string View = "Permissions.CustomerList.View";
            public const string Create = "Permissions.CustomerList.Create";
            public const string Edit = "Permissions.CustomerList.Edit";
            public const string Delete = "Permissions.CustomerList.Delete";
        }

         

        public static class SparePart
        {
            public const string Access = "Permissions.SparePart.Access";
            public const string View = "Permissions.SparePart.View";
            public const string Create = "Permissions.SparePart.Create";
            public const string Edit = "Permissions.SparePart.Edit";
            public const string Delete = "Permissions.SparePart.Delete";
        }

        public static class PrinterModel
        {
            public const string Access = "Permissions.PrinterModelList.Access";
            public const string View = "Permissions.PrinterModelList.View";
            public const string Create = "Permissions.PrinterModelList.Create";
            public const string Edit = "Permissions.PrinterModelList.Edit";
            public const string Delete = "Permissions.PrinterModelList.Delete";
        }
        public static class ItemModelList
        {
            public const string Access = "Permissions.ItemModelList.Access";
            public const string View = "Permissions.ItemModelList.View";
            public const string Create = "Permissions.ItemModelList.Create";
            public const string Edit = "Permissions.ItemModelList.Edit";
            public const string Delete = "Permissions.ItemModelList.Delete";
        }
        public static class RepairServicesList
        {
            public const string Access = "Permissions.RepairServicesList.Access";
            public const string View = "Permissions.RepairServicesList.View";
            public const string Create = "Permissions.RepairServicesList.Create";
            public const string Edit = "Permissions.RepairServicesList.Edit";
            public const string Delete = "Permissions.RepairServicesList.Delete";
        }

    }
}
