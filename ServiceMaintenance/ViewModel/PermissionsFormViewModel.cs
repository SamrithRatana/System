﻿using System.Collections.Generic;

namespace ServiceMaintenance.ViewModel
{
    public class PermissionsFormViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<CheckBoxViewModel> RoleCalims { get; set; }
    }
}