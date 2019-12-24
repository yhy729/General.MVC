using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{
    [Serializable]
    [Table("SysUser")]
    public class SysUser
    {
        public SysUser()
        {
            SysUserTokens = new HashSet<SysUserToken>();
            SysUserLoginLogs = new HashSet<SysUserLoginLog>();
        }
        public Guid Id { set; get; }
        public string Account { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string MobilePhone { set; get; }
        public string Password { set; get; }
        public string Salt { set; get; }
        public string Sex { set; get; }
        public bool Enabled { set; get; }
        public bool IsAdmin { set; get; }
        public DateTime CreationTime { set; get; }
        public int LoginFailedNum { set; get; }
        public DateTime? AllowLoginTime { set; get; }
        public bool LoginLock { set; get; }
        public DateTime? LastLoginTime { set; get; }
        public string LastIpAddress { set; get; }
        public DateTime? LastActivityTime { set; get; }
        public bool IsDeleted { set; get; }
        public DateTime? DeleteTime { set; get; }
        public DateTime? ModifiedTime { set; get; }
        public Guid? Modifier { set; get; }
        public Guid? Creator { set; get; }

        [Column(TypeName = "image")]
        public byte[] Avatar { set; get; }

        public virtual ICollection<SysUserToken> SysUserTokens { set; get; }
        public virtual ICollection<SysUserLoginLog> SysUserLoginLogs { set; get; }
    }
}
