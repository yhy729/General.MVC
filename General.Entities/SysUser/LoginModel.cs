using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace General.Entities
{
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage ="请输入账号")]
        public string Account { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="请输入密码")]
        public string Password { set; get; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string R { set; get; }
    }
}
