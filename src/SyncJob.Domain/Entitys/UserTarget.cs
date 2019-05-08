﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Entitys
{
    [Table("User")]
    public class UserTarget : EntityBase
    {
        public int UserID { get; set; }

        public string UserCode { get; set; }

        public int UserType { get; set; }

        public int? CompanyID { get; set; }

        public int? CompanyBranchID { get; set; }

        public int? EmpID { get; set; }

        public int? GroupPersonID { get; set; }

        public int? PromoterID { get; set; }

        public bool? IsDisabled { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { UserID };
        }
    }
}
