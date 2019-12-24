using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{
    [Serializable]
    [Table("SysUserToken")]
    public class SysUserToken
    {
        public Guid Id { set; get; }
        public Guid SysUserId { set; get; }
        public DateTime ExpireTime { set; get; }

        [ForeignKey("SysUserId")]
        public virtual SysUser SysUser { set; get; }
    }
}
