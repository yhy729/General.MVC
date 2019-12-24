using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace General.Entities
{
    [Table("SysUserLoginLog")]
    public class SysUserLoginLog
    {
        public Guid Id { set; get; }

        public Guid UserId { set; get; }
        public string IpAddress { set; get; }
        public DateTime LoginTime { set; get; }

        public string Message { set; get; }
        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}
